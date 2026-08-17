/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Rudimentary implementation of a LinkedHashSet as known from Java in C# (implementing the same interfaces as HashSet, but not inheriting from the HashSet class)
/// </summary>
/// <typeparam name="T"></typeparam>
public class LinkedHashSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ISet<T>, IReadOnlyCollection<T>
{
    Dictionary<T, LinkedListNode<T>> unorderedDictionary; // replacement for the HashSet, pointing to the LinkedListNode so that removal is an O(1)-operation
    LinkedList<T> orderedLinkedList; // for ensuring iteration order is the same as addition order, LinkedList so that removal is an O(1)-operation

    public LinkedHashSet()
    {
        unorderedDictionary = new Dictionary<T, LinkedListNode<T>>();
        orderedLinkedList = new LinkedList<T>();
    }

    public LinkedHashSet(IEnumerable<T> collection)
        : this()
    {
        foreach(T item in collection)
        {
            Add(item);
        }
    }

    public int Count
    {
        get { return unorderedDictionary.Count; }
    }

    public bool IsReadOnly
    {
        get { return false; }
    }

    public void Add(T item)
    {
        ((ISet<T>)this).Add(item);
    }

    public void Clear()
    {
        unorderedDictionary.Clear();
        orderedLinkedList.Clear();
    }

    public bool Contains(T item)
    {
        return unorderedDictionary.ContainsKey(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        orderedLinkedList.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        LinkedListNode<T> node;
        bool found = unorderedDictionary.TryGetValue(item, out node);
        if(found)
        {
            unorderedDictionary.Remove(item);
            orderedLinkedList.Remove(node);
            return true;
        }
        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return orderedLinkedList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return orderedLinkedList.GetEnumerator();
    }


    bool ISet<T>.Add(T item)
    {
        if(unorderedDictionary.ContainsKey(item))
            return false;
        LinkedListNode<T> node = orderedLinkedList.AddLast(item);
        unorderedDictionary.Add(item, node);
        return true;
    }

    public void ExceptWith(IEnumerable<T> other)
    {
        foreach(T item in other)
        {
            Remove(item);
        }
    }

    public void IntersectWith(IEnumerable<T> other)
    {
        HashSet<T> intersection = new HashSet<T>();
        foreach(T item in other)
        {
            if(unorderedDictionary.ContainsKey(item))
                intersection.Add(item);
        }
        HashSet<T> ourContentWithoutIntersection = new HashSet<T>();
        foreach(T item in unorderedDictionary.Keys)
        {
            if(!intersection.Contains(item))
                ourContentWithoutIntersection.Add(item);
        }
        ExceptWith(ourContentWithoutIntersection); // TODO: maybe better solution existing...
    }

    public bool IsProperSubsetOf(IEnumerable<T> other)
    {
        throw new System.NotImplementedException();
    }

    public bool IsProperSupersetOf(IEnumerable<T> other)
    {
        throw new System.NotImplementedException();
    }

    public bool IsSubsetOf(IEnumerable<T> other)
    {
        throw new System.NotImplementedException();
    }

    public bool IsSupersetOf(IEnumerable<T> other)
    {
        throw new System.NotImplementedException();
    }

    public bool Overlaps(IEnumerable<T> other)
    {
        throw new System.NotImplementedException();
    }

    public bool SetEquals(IEnumerable<T> other)
    {
        throw new System.NotImplementedException();
    }

    public void SymmetricExceptWith(IEnumerable<T> other)
    {
        throw new System.NotImplementedException();
    }

    public void UnionWith(IEnumerable<T> other)
    {
        foreach(T item in other)
        {
            Add(item);
        }
    }
}
