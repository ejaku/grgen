/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace de.unika.ipd.grGen.libGr
{
    public static class DictionaryHelper
    {
        /// <summary>
        /// If dict is dictionary, the dictionary is returned together with it's key and value type
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
        /// Returns type object for type name string, to be used for dictionary
        /// </summary>
        /// <param name="typeName">Name of the type we want some type object for</param>
        /// <param name="graph">Graph to be search for enum,node,edge types / enum,node/edge type names</param>
        /// <returns>The type object corresponding to the given string, null if type was not found</returns>
        public static Type GetTypeFromNameForDictionary(String typeName, IGraph graph)
        {
            return GetTypeFromNameForDictionary(typeName, graph.Model);
        }

        /// <summary>
        /// Returns type object for type name string, to be used for dictionary
        /// </summary>
        /// <param name="typeName">Name of the type we want some type object for</param>
        /// <param name="model">Graph model to be search for enum,node,edge types / enum,node/edge type names</param>
        /// <returns>The type object corresponding to the given string, null if type was not found</returns>
        public static Type GetTypeFromNameForDictionary(String typeName, IGraphModel model)
        {
            switch (typeName)
            {
                case "boolean": return typeof(bool);
                case "int": return typeof(int);
                case "float": return typeof(float);
                case "double": return typeof(double);
                case "string": return typeof(string);
                case "object": return typeof(object);
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
            Type type = GetTypeFromNameForDictionary(typeName, model);
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
        /// <paramref name="b"/> whose keys are also contained in <paramref name="a"/>.
        /// If both dictionaries contain one key, the value from <paramref name="b"/> takes precedence
        /// for consistency with Union.
        /// </summary>
        /// <param name="a">A dictionary.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <returns>A new dictionary containing all elements from <paramref name="b"/>,
        /// which are also in <paramref name="a"/>.</returns>
        public static Dictionary<K, V> Intersect<K, V>(Dictionary<K, V> a, Dictionary<K, V> b)
        {
            // Fill new dictionary with all elements from b.
            Dictionary<K, V> newDict = new Dictionary<K, V>(b);

            // Remove all elements of a not contained in a.
            foreach(KeyValuePair<K, V> entry in b)
            {
                if(!a.ContainsKey(entry.Key))
                    newDict.Remove(entry.Key);
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

            // Remove all elements of a contained in b.
            foreach(KeyValuePair<K, V> entry in a)
            {
                if(b.ContainsKey(entry.Key))
                    newDict.Remove(entry.Key);
            }

            return newDict;
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
        /// Checks if set/map <paramref name="a"/> equals set/map <paramref name="b"/>.
        /// For a map, key and value must be same to be equal.
        /// </summary>
        /// <param name="a">A dictionary.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of set/map comparison.</returns>
        public static bool Equal<K, V>(Dictionary<K, V> a, Dictionary<K, V> b) where V : IEquatable<V>
        {
            if(a.Count!=b.Count) return false;
            if(LessOrEqual(a, b) && LessOrEqual(b, a)) return true;
            else return false;
        }

        /// <summary>
        /// Checks if set/map <paramref name="a"/> is not equal to set/map <paramref name="b"/>.
        /// For a map, key and value must be same to be equal.
        /// </summary>
        /// <param name="a">A dictionary.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of set/map comparison.</returns>
        public static bool NotEqual<K, V>(Dictionary<K, V> a, Dictionary<K, V> b) where V : IEquatable<V>
        {
            if(a.Count!=b.Count) return true;
            if(LessOrEqual(a, b) && LessOrEqual(b, a)) return false;
            else return true;
        }

        /// <summary>
        /// Checks if set/map <paramref name="a"/> is a proper superset/map of <paramref name="b"/>.
        /// For a map, key and value must be same to be equal.
        /// </summary>
        /// <param name="a">A dictionary.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of set/map comparison.</returns>
        public static bool GreaterThan<K, V>(Dictionary<K, V> a, Dictionary<K, V> b) where V : IEquatable<V>
        {
            if(GreaterOrEqual(a, b)) return b.Count!=a.Count;
            else return false;
        }

        /// <summary>
        /// Checks if set/map <paramref name="a"/> is a superset/map of <paramref name="b"/>.
        /// For a map, key and value must be same to be equal.
        /// </summary>
        /// <param name="a">A dictionary.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of set/map comparison.</returns>
        public static bool GreaterOrEqual<K, V>(Dictionary<K, V> a, Dictionary<K, V> b) where V : IEquatable<V>
        {
            return LessOrEqual(b, a);
        }

        /// <summary>
        /// Checks if set/map <paramref name="a"/> is a proper subset/map of <paramref name="b"/>.
        /// For a map, key and value must be same to be equal.
        /// </summary>
        /// <param name="a">A dictionary.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of set/map comparison.</returns>
        public static bool LessThan<K, V>(Dictionary<K, V> a, Dictionary<K, V> b) where V : IEquatable<V>
        {
            if(LessOrEqual(a, b)) return a.Count!=b.Count;
            else return false;
        }

        /// <summary>
        /// Checks if set/map <paramref name="a"/> is a subset/map of <paramref name="b"/>.
        /// For a map, key and value must be same to be equal.
        /// </summary>
        /// <param name="a">A dictionary.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of set/map comparison.</returns>
        public static bool LessOrEqual<K, V>(Dictionary<K, V> a, Dictionary<K, V> b) where V : IEquatable<V>
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
                    if(!entry.Value.Equals(b[entry.Key])) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a string representation of the given dictionary
        /// </summary>
        /// <param name="setmap">The dictionary of which to get the string representation</param>
        /// <returns>String representation of set/map.</returns>
        public static string ToString(IDictionary setmap)
        {
            string type;
            string content;
            ToString(setmap, out type, out content);
            return content;
        }

        /// <summary>
        /// Returns a string representation of the given dictionary
        /// </summary>
        /// <param name="setmap">The dictionary of which to get the string representation</param>
        /// <param name="type">The type as string, e.g set<int> or map<string,boolean> </param>
        /// <param name="content">The content as string, e.g. { 42, 43 } or { "foo"->true, "bar"->false } </param>
        public static void ToString(IDictionary setmap, out string type, out string content)
        {
            Type keyType;
            Type valueType;
            GetDictionaryTypes(setmap, out keyType, out valueType);

            StringBuilder sb = new StringBuilder(256);
            sb.Append("{");

            if(valueType==typeof(SetValueType))
            {
                type = "set<"+keyType.Name+">";
                bool first = true;
                foreach(DictionaryEntry entry in setmap)
                {
                    if(first) { sb.Append(entry.Key.ToString()); first = false; }
                    else { sb.Append(","); sb.Append(entry.Key.ToString()); }
                }
            }
            else
            {
                type = "map<"+keyType.Name+","+valueType.Name+">";
                bool first = true;
                foreach(DictionaryEntry entry in setmap)
                {
                    if(first) { sb.Append(entry.Key.ToString()); sb.Append("->"); sb.Append(entry.Value.ToString()); first = false; }
                    else { sb.Append(","); sb.Append(entry.Key.ToString()); sb.Append("->"); sb.Append(entry.Value.ToString()); }
                }
            }

            sb.Append("}");
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
        public static void ToString(IDictionary setmap, 
            AttributeChangeType changeType, Object newValue, Object keyValue,
            out string type, out string content)
        {
            if(changeType==AttributeChangeType.PutElement)
            {
                Type keyType;
                Type valueType;
                GetDictionaryTypes(setmap, out keyType, out valueType);

                if(valueType==typeof(SetValueType))
                {
                    ToString(setmap, out type, out content);
                    content += "|" + newValue.ToString();
                }
                else
                {
                    ToString(setmap, out type, out content);
                    content += "|" + keyValue.ToString() + "->" + newValue.ToString();
                }
            }
            else if(changeType==AttributeChangeType.RemoveElement)
            {
                Type keyType;
                Type valueType;
                GetDictionaryTypes(setmap, out keyType, out valueType);

                if(valueType==typeof(SetValueType))
                {
                    ToString(setmap, out type, out content);
                    content += "\\" + newValue.ToString();
                }
                else
                {
                    ToString(setmap, out type, out content);
                    content += "\\" + keyValue.ToString() + "->.";
                }
            }
            else
            {
                ToString((IDictionary)newValue, out type, out content);
            }
        }

        /// <summary>
        /// If the attribute of the given name of the given element is a set or map attribute
        /// then return a clone of the given dictionary value, otherwise just return the original value;
        /// additionally returns the AttributeType of the attribute of the element.
        /// </summary>
        public static object IfAttributeOfElementIsDictionaryThenCloneDictionaryValue(
                IGraphElement element, String AttributeName, object value, out AttributeType attrType)
        {
            attrType = element.Type.GetAttributeType(AttributeName);
            if(attrType.Kind == AttributeKind.SetAttr || attrType.Kind == AttributeKind.MapAttr)
            {
                Type keyType, valueType;
                DictionaryHelper.GetDictionaryTypes(element.GetAttribute(AttributeName), out keyType, out valueType);
                return DictionaryHelper.NewDictionary(keyType, valueType, value); // by-value-semantics -> clone dictionary
            }
            return value;
        }
    }
}
