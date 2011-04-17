/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Set class only used by the dumping code in BaseGraph.
    /// The value of this class is questionable...
    /// </summary>
    /// <typeparam name="T">The type of the contained elements.</typeparam>
    public class Set<T> : IEnumerable<T>
    {
        Dictionary<T, bool> setData;

        public Set()
        {
            setData = new Dictionary<T, bool>();
        }

        public Set(int initialCapacity)
        {
            setData = new Dictionary<T, bool>(initialCapacity);
        }

        // Initializes a new Set instance that contains elements copied from the given Set
        public Set(Set<T> other)
        {
            setData = new Dictionary<T, bool>(other.setData);
        }

        public int Count { get { return setData.Count; } }

        public void Add(T elem)
        {
            setData[elem] = true;
        }

        public void Add(IEnumerable<T> i)
        {
            foreach(T elem in i)
                setData[elem] = true;
        }

        public bool Remove(T elem)
        {
            return setData.Remove(elem);
        }

        public void Remove(Set<T> other)
        {
            foreach(T elem in other)
                setData.Remove(elem);
        }

        public void Clear()
        {
            setData.Clear();
        }

        public bool Contains(T elem)
        {
            return setData.ContainsKey(elem);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach(KeyValuePair<T, bool> elem in setData)
                yield return elem.Key;
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T[] ToArray()
        {
            T[] array = new T[setData.Count];
            int i = 0;
            foreach(KeyValuePair<T, bool> elem in setData)
                array[i++] = elem.Key;
            return array;
        }
    }
}
