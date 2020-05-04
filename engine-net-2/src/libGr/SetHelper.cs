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

namespace de.unika.ipd.grGen.libGr
{
    public static partial class ContainerHelper
    {
        public static Dictionary<K, SetValueType> FillSet<K>(Dictionary<K, SetValueType> setToCopyTo, string valueTypeName, object hopefullySetToCopy, IGraphModel model)
        {
            if(hopefullySetToCopy is IDictionary)
                return FillSet(setToCopyTo, valueTypeName, (IDictionary)hopefullySetToCopy, model);
            throw new Exception("Set copy constructor expects set as source.");
        }

        public static Dictionary<K, SetValueType> FillSet<K>(Dictionary<K, SetValueType> setToCopyTo, string valueTypeName, IDictionary setToCopy, IGraphModel model)
        {
            NodeType nodeType = TypesHelper.GetNodeType(valueTypeName, model);
            if(nodeType != null)
                FillSetWithNode(setToCopyTo, nodeType, setToCopy);
            else
            {
                EdgeType edgeType = TypesHelper.GetEdgeType(valueTypeName, model);
                if(edgeType != null)
                    FillSetWithEdge(setToCopyTo, edgeType, setToCopy);
                else
                {
                    Type varType = TypesHelper.GetType(valueTypeName, model);
                    FillSetWithVar(setToCopyTo, varType, setToCopy);
                }
            }
            return setToCopyTo;
        }

        public static void FillSetWithNode<K>(Dictionary<K, SetValueType> targetSet, NodeType nodeType, IDictionary sourceSet)
        {
            foreach(DictionaryEntry entry in sourceSet)
            {
                INode node = entry.Key as INode;
                if(node == null)
                    continue;
                if(node.InstanceOf(nodeType))
                    targetSet.Add((K)entry.Key, (SetValueType)entry.Value);
            }
        }

        public static void FillSetWithEdge<K>(Dictionary<K, SetValueType> targetSet, EdgeType edgeType, IDictionary sourceSet)
        {
            foreach(DictionaryEntry entry in sourceSet)
            {
                IEdge edge = entry.Key as IEdge;
                if(edge == null)
                    continue;
                if(edge.InstanceOf(edgeType))
                    targetSet.Add((K)entry.Key, (SetValueType)entry.Value);
            }
        }

        public static void FillSetWithVar<K>(Dictionary<K, SetValueType> targetSet, Type varType, IDictionary sourceSet)
        {
            foreach(DictionaryEntry entry in sourceSet)
            {
                if(entry.Key.GetType() == varType)
                    targetSet.Add((K)entry.Key, (SetValueType)entry.Value);
            }
        }

        public static IDictionary FillSet(IDictionary setToCopyTo, string valueTypeName, object hopefullySetToCopy, IGraphModel model)
        {
            if(hopefullySetToCopy is IDictionary)
                return FillSet(setToCopyTo, valueTypeName, (IDictionary)hopefullySetToCopy, model);
            throw new Exception("Set copy constructor expects set as source.");
        }

        public static IDictionary FillSet(IDictionary setToCopyTo, string valueTypeName, IDictionary setToCopy, IGraphModel model)
        {
            NodeType nodeType = TypesHelper.GetNodeType(valueTypeName, model);
            if(nodeType != null)
                FillSetWithNode(setToCopyTo, nodeType, setToCopy);
            else
            {
                EdgeType edgeType = TypesHelper.GetEdgeType(valueTypeName, model);
                if(edgeType != null)
                    FillSetWithEdge(setToCopyTo, edgeType, setToCopy);
                else
                {
                    Type varType = TypesHelper.GetType(valueTypeName, model);
                    FillSetWithVar(setToCopyTo, varType, setToCopy);
                }
            }
            return setToCopyTo;
        }

        public static void FillSetWithNode(IDictionary targetSet, NodeType nodeType, IDictionary sourceSet)
        {
            foreach(DictionaryEntry entry in sourceSet)
            {
                INode node = entry.Key as INode;
                if(node == null)
                    continue;
                if(node.InstanceOf(nodeType))
                    targetSet.Add(entry.Key, entry.Value);
            }
        }

        public static void FillSetWithEdge(IDictionary targetSet, EdgeType edgeType, IDictionary sourceSet)
        {
            foreach(DictionaryEntry entry in sourceSet)
            {
                IEdge edge = entry.Key as IEdge;
                if(edge == null)
                    continue;
                if(edge.InstanceOf(edgeType))
                    targetSet.Add(entry.Key, entry.Value);
            }
        }

        public static void FillSetWithVar(IDictionary targetSet, Type varType, IDictionary sourceSet)
        {
            foreach(DictionaryEntry entry in sourceSet)
            {
                if(entry.Key.GetType() == varType)
                    targetSet.Add(entry.Key, entry.Value);
            }
        }

        /// <summary>
        /// Creates a new list containing all values from the given dictionary representing a set.
        /// </summary>
        public static IList SetAsArray(IDictionary a)
        {
            Type keyType;
            Type valueType;
            ContainerHelper.GetDictionaryTypes(a, out keyType, out valueType);
            IList newList = NewList(keyType);

            foreach(DictionaryEntry entry in a)
            {
                newList.Add(entry.Key);
            }

            return newList;
        }

        /// <summary>
        /// Creates a new list containing all values from the given dictionary representing a set.
        /// </summary>
        public static List<V> SetAsArray<V>(Dictionary<V, SetValueType> a)
        {
            List<V> newList = new List<V>();

            foreach(KeyValuePair<V, SetValueType> kvp in a)
            {
                newList.Add(kvp.Key);
            }

            return newList;
        }
    }
}