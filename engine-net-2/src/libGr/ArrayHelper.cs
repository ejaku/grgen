/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
    public enum OrderMethod
    {
        OrderAscending, OrderDescending, KeepOneForEach
    }

    public static partial class ContainerHelper
    {
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
        /// The value type of the List is returned
        /// </summary>
        /// <param name="arrayType">The List type</param>
        /// <param name="valueType">The value type of the List</param>
        public static void GetListType(Type arrayType, out Type valueType)
        {
            Type[] arrayTypeArgs = arrayType.GetGenericArguments();
            valueType = arrayTypeArgs[0];
        }

        public static IList FillArray(IList arrayToCopyTo, string valueTypeName, object hopefullyArrayToCopy, IGraphModel model)
        {
            if(hopefullyArrayToCopy is IList)
                return FillArray(arrayToCopyTo, valueTypeName, (IList)hopefullyArrayToCopy, model);
            throw new Exception("Array copy constructor expects array as source.");
        }

        public static IList FillArray(IList arrayToCopyTo, string valueTypeName, IList arrayToCopy, IGraphModel model)
        {
            NodeType nodeType = TypesHelper.GetNodeType(valueTypeName, model);
            if(nodeType != null)
                FillArrayWithNode(arrayToCopyTo, nodeType, arrayToCopy);
            else
            {
                EdgeType edgeType = TypesHelper.GetEdgeType(valueTypeName, model);
                if(edgeType != null)
                    FillArrayWithEdge(arrayToCopyTo, edgeType, arrayToCopy);
                else
                {
                    Type varType = TypesHelper.GetType(valueTypeName, model);
                    FillArrayWithVar(arrayToCopyTo, varType, arrayToCopy);
                }
            }
            return arrayToCopyTo;
        }

        public static void FillArrayWithNode(IList targetArray, NodeType nodeType, IList sourceArray)
        {
            foreach(object entry in sourceArray)
            {
                INode node = entry as INode;
                if(node == null)
                    continue;
                if(node.InstanceOf(nodeType))
                    targetArray.Add(entry);
            }
        }

        public static void FillArrayWithEdge(IList targetArray, EdgeType edgeType, IList sourceArray)
        {
            foreach(object entry in sourceArray)
            {
                IEdge edge = entry as IEdge;
                if(edge == null)
                    continue;
                if(edge.InstanceOf(edgeType))
                    targetArray.Add(entry);
            }
        }

        public static void FillArrayWithVar(IList targetArray, Type varType, IList sourceArray)
        {
            foreach(object entry in sourceArray)
            {
                if(entry.GetType() == varType)
                    targetArray.Add(entry);
            }
        }

        public static List<K> FillArray<K>(List<K> arrayToCopyTo, string valueTypeName, object hopefullyArrayToCopy, IGraphModel model)
        {
            if(hopefullyArrayToCopy is IList)
                return FillArray(arrayToCopyTo, valueTypeName, (IList)hopefullyArrayToCopy, model);
            throw new Exception("Array copy constructor expects array as source.");
        }

        public static List<K> FillArray<K>(List<K> arrayToCopyTo, string valueTypeName, IList arrayToCopy, IGraphModel model)
        {
            NodeType nodeType = TypesHelper.GetNodeType(valueTypeName, model);
            if(nodeType != null)
                FillArrayWithNode(arrayToCopyTo, nodeType, arrayToCopy);
            else
            {
                EdgeType edgeType = TypesHelper.GetEdgeType(valueTypeName, model);
                if(edgeType != null)
                    FillArrayWithEdge(arrayToCopyTo, edgeType, arrayToCopy);
                else
                {
                    Type varType = TypesHelper.GetType(valueTypeName, model);
                    FillArrayWithVar(arrayToCopyTo, varType, arrayToCopy);
                }
            }
            return arrayToCopyTo;
        }

        public static void FillArrayWithNode<K>(List<K> targetArray, NodeType nodeType, IList sourceArray)
        {
            foreach(object entry in sourceArray)
            {
                INode node = entry as INode;
                if(node == null)
                    continue;
                if(node.InstanceOf(nodeType))
                    targetArray.Add((K)entry);
            }
        }

        public static void FillArrayWithEdge<K>(List<K> targetArray, EdgeType edgeType, IList sourceArray)
        {
            foreach(object entry in sourceArray)
            {
                IEdge edge = entry as IEdge;
                if(edge == null)
                    continue;
                if(edge.InstanceOf(edgeType))
                    targetArray.Add((K)entry);
            }
        }

        public static void FillArrayWithVar<K>(List<K> targetArray, Type varType, IList sourceArray)
        {
            foreach(object entry in sourceArray)
            {
                if(entry.GetType() == varType)
                    targetArray.Add((K)entry);
            }
        }

        /// <summary>
        /// Creates a new List of the given value type
        /// </summary>
        /// <param name="valueType">The value type of the List to be created</param>
        /// <returns>The newly created List, null if unsuccessfull</returns>
        public static IList NewList(Type valueType)
        {
            if(valueType == null)
                return null;

            Type genListType = typeof(List<>);
            Type listType = genListType.MakeGenericType(valueType);
            return (IList)Activator.CreateInstance(listType);
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
            if(valueType == null || oldList == null)
                return null;

            Type genListType = typeof(List<>);
            Type listType = genListType.MakeGenericType(valueType);
            return (IList)Activator.CreateInstance(listType, oldList);
        }

        /// <summary>
        /// Creates a new List of the given value type,
        /// initialized with the given capacity
        /// </summary>
        /// <param name="valueType">The value type of the List to be created</param>
        /// <param name="capacity">The capacity of the new list</param>
        /// <returns>The newly created List, containing the content of the old List,
        /// null if unsuccessfull</returns>
        public static IList NewList(Type valueType, int capacity)
        {
            if(valueType == null)
                return null;

            Type genListType = typeof(List<>);
            Type listType = genListType.MakeGenericType(valueType);
            return (IList)Activator.CreateInstance(listType, capacity);
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

        public static int IndexOf<V>(List<V> a, V entry, int startIndex)
        {
            return a.IndexOf(entry, startIndex);
        }

        /// <summary>
        /// Returns the first position of entry in the array a with a binary search - thus the array must be ordered.
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array.</param>
        /// <param name="entry">The value to search for.</param>
        /// <returns>The first position of entry in the array a, -1 if entry not in a.</returns>
        public static int IndexOfOrdered(IList a, object entry)
        {
            if(a is List<string>)
                return IndexOfOrdered((List<string>)a, (string)entry);
            return ArrayList.Adapter(a).BinarySearch(entry);
        }

        /// <summary>
        /// Returns the first position of entry in the array a with a binary search - thus the array must be ordered.
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array.</param>
        /// <param name="entry">The value to search for.</param>
        /// <returns>The first position of entry in the array a, -1 if entry not in a.</returns>
        public static int IndexOfOrdered<V>(List<V> a, V entry)
        {
            return a.BinarySearch(entry);
        }

        public static int IndexOfOrdered(List<string> a, string entry)
        {
            return a.BinarySearch(entry, StringComparer.InvariantCulture);
        }

        /// <summary>
        /// Returns the first position from the end inwards of entry in the array a
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array.</param>
        /// <param name="entry">The value to search for.</param>
        /// <returns>The first position from the end inwards of entry in the array a, -1 if entry not in a.</returns>
        public static int LastIndexOf<V>(List<V> a, V entry)
        {
            for(int i = a.Count - 1; i >= 0; --i)
            {
                if(EqualityComparer<V>.Default.Equals(a[i], entry))
                    return i;
            }

            return -1;
        }

        public static int LastIndexOf(IList a, object entry, int startIndex)
        {
            for(int i = startIndex; i >= 0; --i)
            {
                if(a[i].Equals(entry))
                    return i;
            }

            return -1;
        }

        public static int LastIndexOf<V>(List<V> a, V entry, int startIndex)
        {
            for(int i = startIndex; i >= 0; --i)
            {
                if(EqualityComparer<V>.Default.Equals(a[i], entry))
                    return i;
            }

            return -1;
        }

        public static IList Subarray(IList a, int start, int length)
        {
            IList newList = (IList)Activator.CreateInstance(a.GetType());

            for(int i = start; i < start + length; ++i)
            {
                newList.Add(a[i]);
            }

            return newList;
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

            for(int i = start; i < start + length; ++i)
            {
                newList.Add(a[i]);
            }

            return newList;
        }

        public static IList Extract(object container, string memberOrAttribute, IGraphProcessingEnvironment procEnv)
        {
            IList array = (IList)container;
            string arrayType = TypesHelper.DotNetTypeToXgrsType(array.GetType());
            string arrayValueType = TypesHelper.ExtractSrc(arrayType);
            if(arrayValueType.StartsWith("match<"))
            {
                if(arrayValueType == "match<>")
                {
                    if(array.Count > 0)
                    {
                        IMatch match = (IMatch)array[0];
                        object matchElement = match.GetMember(memberOrAttribute);
                        Type matchElementType;
                        if(matchElement is IGraphElement)
                            matchElementType = TypesHelper.GetType(((IGraphElement)matchElement).Type, procEnv.Graph.Model);
                        else
                            matchElementType = matchElement.GetType();
                        Type listType = typeof(List<>).MakeGenericType(matchElementType);
                        IList extractedArray = (IList)Activator.CreateInstance(listType);
                        ExtractMatchMember(array, memberOrAttribute, extractedArray);
                        return extractedArray;
                    }
                    else
                        return new List<object>();
                }
                else
                {
                    if(arrayValueType.StartsWith("match<class "))
                    {
                        MatchClassFilterer matchClass = procEnv.Actions.GetMatchClass(TypesHelper.GetMatchClassName(arrayValueType));
                        IPatternElement element = matchClass.info.GetPatternElement(memberOrAttribute);
                        GrGenType elementType = element.Type;
                        Type listType = typeof(List<>).MakeGenericType(TypesHelper.GetType(elementType, procEnv.Graph.Model));
                        IList extractedArray = (IList)Activator.CreateInstance(listType);
                        ExtractMatchMember(array, memberOrAttribute, extractedArray);
                        return extractedArray;
                    }
                    else
                    {
                        IAction action = procEnv.Actions.GetAction(TypesHelper.GetRuleName(arrayValueType));
                        IPatternElement element = action.RulePattern.PatternGraph.GetPatternElement(memberOrAttribute);
                        GrGenType elementType = element.Type;
                        Type listType = typeof(List<>).MakeGenericType(TypesHelper.GetType(elementType, procEnv.Graph.Model));
                        IList extractedArray = (IList)Activator.CreateInstance(listType);
                        ExtractMatchMember(array, memberOrAttribute, extractedArray);
                        return extractedArray;
                    }
                }
            }
            else
            {
                GrGenType graphElementType = TypesHelper.GetNodeOrEdgeType(arrayValueType, procEnv.Graph.Model);
                if(graphElementType != null)
                {
                    AttributeType attributeType = graphElementType.GetAttributeType(memberOrAttribute);
                    Type listType = typeof(List<>).MakeGenericType(attributeType.Type);
                    IList extractedArray = (IList)Activator.CreateInstance(listType);
                    ExtractAttribute(array, memberOrAttribute, extractedArray);
                    return extractedArray;
                }
                else
                {
                    if(array.Count > 0)
                    {
                        IGraphElement graphElement = (IGraphElement)array[0];
                        object element = graphElement.GetAttribute(memberOrAttribute);
                        Type elementType;
                        if(element is IGraphElement)
                            elementType = TypesHelper.GetType(((IGraphElement)element).Type, procEnv.Graph.Model);
                        else
                            elementType = element.GetType();
                        Type listType = typeof(List<>).MakeGenericType(elementType);
                        IList extractedArray = (IList)Activator.CreateInstance(listType);
                        ExtractAttribute(array, memberOrAttribute, extractedArray);
                        return extractedArray;
                    }
                    else
                        return new List<object>();
                }
            }
        }

        private static void ExtractMatchMember(IList array, string member, IList extractedArray)
        {
            foreach(object element in array)
            {
                IMatch match = (IMatch)element;
                extractedArray.Add(match.GetMember(member));
            }
        }

        private static void ExtractAttribute(IList array, string attribute, IList extractedArray)
        {
            foreach(object element in array)
            {
                IGraphElement graphElement = (IGraphElement)element;
                extractedArray.Add(graphElement.GetAttribute(attribute));
            }
        }

        public static List<T> ConvertIfEmpty<T>(object array)
        {
            if(!(array is List<T>) && array is IList && ((IList)array).Count == 0)
                return new List<T>();
            else
                return (List<T>)array;
        }

        /// <summary>
        /// Creates a new array containing the content of the old array but sorted.
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array.</param>
        /// <returns>A new List with the content of the old list sorted.</returns>
        public static IList ArrayOrderAscending(IList a)
        {
            if(a is List<String>)
                return ArrayOrderAscending((List<String>)a);

            IList newList = (IList)Activator.CreateInstance(a.GetType(), a);

            ArrayList.Adapter(newList).Sort();

            return newList;
        }

        /// <summary>
        /// Creates a new array containing the content of the old array but sorted.
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array.</param>
        /// <returns>A new List with the content of the old list sorted.</returns>
        public static List<V> ArrayOrderAscending<V>(List<V> a)
        {
            List<V> newList = new List<V>(a);

            newList.Sort();

            return newList;
        }

        /// <summary>
        /// Creates a new array containing the content of the old array but sorted.
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array.</param>
        /// <returns>A new List with the content of the old list sorted.</returns>
        public static List<string> ArrayOrderAscending(List<string> a)
        {
            List<string> newList = new List<string>(a);

            newList.Sort(StringComparer.InvariantCulture);

            return newList;
        }

        /// <summary>
        /// Creates a new array containing the content of the old array but sorted.
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array.</param>
        /// <returns>A new List with the content of the old list sorted.</returns>
        public static IList ArrayOrderDescending(IList a)
        {
            if(a is List<String>)
                return ArrayOrderDescending((List<String>)a);

            IList newList = (IList)Activator.CreateInstance(a.GetType(), a);

            ArrayList adapter = ArrayList.Adapter(newList);
            adapter.Sort();
            adapter.Reverse();

            return newList;
        }

        /// <summary>
        /// Creates a new array containing the content of the old array but sorted.
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array.</param>
        /// <returns>A new List with the content of the old list sorted.</returns>
        public static List<V> ArrayOrderDescending<V>(List<V> a)
        {
            List<V> newList = new List<V>(a);

            newList.Sort();
            newList.Reverse();

            return newList;
        }

        /* 
        // TODO: measure against comparison with default comparer and following reverse
        public class ReverseComparer<T> : Comparer<T>
        {
            static Comparer<T> defaultComparer = Comparer<T>.Default;

            public override int Compare(T left, T right)
            {
                return defaultComparer.Compare(right, left);
            }
        }
        */

        /// <summary>
        /// Creates a new array containing the content of the old array but sorted.
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array.</param>
        /// <returns>A new List with the content of the old list sorted.</returns>
        public static List<string> ArrayOrderDescending(List<string> a)
        {
            List<string> newList = new List<string>(a);

            newList.Sort(StringComparer.InvariantCulture);
            newList.Reverse();

            return newList;
        }

        /// <summary>
        /// Creates a new array containing the content of the old array but freed of duplicates.
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array.</param>
        /// <returns>A new List with the content of the old list freed of duplicates.</returns>
        public static IList ArrayKeepOneForEach(IList a)
        {
            IList newList = (IList)Activator.CreateInstance(a.GetType());

            Dictionary<object, SetValueType> alreadySeenElements = new Dictionary<object, SetValueType>();
            foreach(object element in a)
            {
                if(!alreadySeenElements.ContainsKey(element))
                {
                    newList.Add(element);
                    alreadySeenElements.Add(element, null);
                }
            }

            return newList;
        }

        /// <summary>
        /// Creates a new array containing the content of the old array but freed of duplicates.
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array.</param>
        /// <returns>A new List with the content of the old list freed of duplicates.</returns>
        public static List<V> ArrayKeepOneForEach<V>(List<V> a)
        {
            List<V> newList = new List<V>();

            Dictionary<V, SetValueType> alreadySeenElements = new Dictionary<V, SetValueType>();
            foreach(V element in a)
            {
                if(!alreadySeenElements.ContainsKey(element))
                {
                    newList.Add(element);
                    alreadySeenElements.Add(element, null);
                }
            }

            return newList;
        }

        /// <summary>
        /// Creates a new array containing the content of the old array but reversed.
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array.</param>
        /// <returns>A new List with the content of the old list reversed.</returns>
        public static IList ArrayReverse(IList a)
        {
            IList newList = (IList)Activator.CreateInstance(a.GetType(), a);

            ArrayList.Adapter(newList).Reverse();

            return newList;
        }

        /// <summary>
        /// Creates a new array containing the content of the old array but reversed.
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array.</param>
        /// <returns>A new List with the content of the old list reversed.</returns>
        public static List<V> ArrayReverse<V>(List<V> a)
        {
            List<V> newList = new List<V>(a);

            newList.Reverse();

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

        /// <summary>
        /// Creates a new dictionary representing a map containing all values from the given list, mapped to by their index.
        /// </summary>
        public static IDictionary ArrayAsMap(IList a)
        {
            Type valueType;
            ContainerHelper.GetListType(a, out valueType);
            IDictionary newDict = NewDictionary(typeof(int), valueType);

            for(int i = 0; i < a.Count; ++i)
            {
                newDict[i] = a[i];
            }

            return newDict;
        }

        /// <summary>
        /// Creates a new dictionary representing a map containing all values from the given list, mapped to by their index.
        /// </summary>
        public static Dictionary<int, V> ArrayAsMap<V>(List<V> a)
        {
            Dictionary<int, V> newDict =
                new Dictionary<int, V>();

            for(int i = 0; i < a.Count; ++i)
            {
                newDict[i] = a[i];
            }

            return newDict;
        }

        /// <summary>
        /// Creates a new deque containing all values from the given list.
        /// </summary>
        public static IDeque ArrayAsDeque(IList a)
        {
            Type valueType;
            ContainerHelper.GetListType(a, out valueType);
            IDeque newDeque = NewDeque(valueType);

            for(int i = 0; i < a.Count; ++i)
            {
                newDeque.Add(a[i]);
            }

            return newDeque;
        }

        /// <summary>
        /// Creates a new deque containing all values from the given list.
        /// </summary>
        public static Deque<V> ArrayAsDeque<V>(List<V> a)
        {
            Deque<V> newDeque = new Deque<V>();

            for(int i = 0; i < a.Count; ++i)
            {
                newDeque.Add(a[i]);
            }

            return newDeque;
        }

        public static string ArrayAsString(IList a, string filler)
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

        public static string ArrayAsString(List<string> a, string filler)
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

        public static List<string> StringAsArray(string input, string separator)
        {
            if(separator.Length == 0)
            {
                List<string> result = new List<string>();
                for(int i = 0; i < input.Length; ++i)
                {
                    result.Add(new string(input[i], 1));
                }
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

            return b.Count > 0;
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

            return b.Count > 0;
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

            return b.Count > 0;
        }

        public static T Peek<T>(List<T> array)
        {
            return array[array.Count - 1];
        }

        public static T Peek<T>(List<T> array, int index)
        {
            return array[index];
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
            if(a.Count != b.Count)
                return false;
            if(LessOrEqual(a, b))
                return true;
            else
                return false;
        }

        public static bool EqualIList(IList a, IList b)
        {
            if(a.Count != b.Count)
                return false;
            if(LessOrEqualIList(a, b))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks if List <paramref name="a"/> is not equal List <paramref name="b"/>.
        /// </summary>
        /// <param name="a">A List.</param>
        /// <param name="b">Another List of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of List comparison.</returns>
        public static bool NotEqual<V>(List<V> a, List<V> b)
        {
            if(a.Count != b.Count)
                return true;
            if(LessOrEqual(a, b))
                return false;
            else
                return true;
        }

        public static bool NotEqualIList(IList a, IList b)
        {
            if(a.Count != b.Count)
                return true;
            if(LessOrEqualIList(a, b))
                return false;
            else
                return true;
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
            if(a.Count == b.Count)
                return false;
            return
                GreaterOrEqual(a, b);
        }

        public static bool GreaterThanIList(IList a, IList b)
        {
            if(a.Count == b.Count)
                return false;
            return
                GreaterOrEqualIList(a, b);
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
            if(a.Count == b.Count)
                return false;
            return LessOrEqual(a, b);
        }

        public static bool LessThanIList(IList a, IList b)
        {
            if(a.Count == b.Count)
                return false;
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
            if(a.Count > b.Count)
                return false;

            for(int i = 0; i < a.Count; ++i)
            {
                if(!EqualityComparer<V>.Default.Equals(a[i], b[i]))
                    return false;
            }

            return true;
        }

        public static bool LessOrEqualIList(IList a, IList b)
        {
            if(a.Count > b.Count)
                return false;

            for(int i = 0; i < a.Count; ++i)
            {
                if(!Equals(a[i], b[i]))
                    return false;
            }

            return true;
        }
    }
}
