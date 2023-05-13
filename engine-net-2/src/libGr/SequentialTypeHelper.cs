/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections;

namespace de.unika.ipd.grGen.libGr
{
    public static partial class ContainerHelper
    {
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

        /// <summary>
        /// Creates a new dictionary representing a set containing all values from the given list or deque.
        /// </summary>
        public static IDictionary ArrayOrDequeAsSet(object obj)
        {
            if(obj is IList)
            {
                IList a = (IList)obj;

                Type valueType;
                ContainerHelper.GetListType(a, out valueType);
                IDictionary newDict = NewDictionary(valueType, typeof(SetValueType));

                for(int i = 0; i < a.Count; ++i)
                {
                    newDict[a[i]] = null;
                }

                return newDict;
            }
            else if(obj is IDeque)
            {
                IDeque a = (IDeque)obj;

                Type valueType;
                ContainerHelper.GetDequeType(a, out valueType);
                IDictionary newDict = NewDictionary(valueType, typeof(SetValueType));

                for(int i = 0; i < a.Count; ++i)
                {
                    newDict[a[i]] = null;
                }

                return newDict;
            }
            else
            {
                throw new Exception("asSet() can only be used on array or deque");
            }
        }

        public static int IndexOf(object obj, object entry)
        {
            if(obj is IList)
            {
                IList a = (IList)obj;
                return a.IndexOf(entry);
            }
            else if(obj is IDeque)
            {
                IDeque a = (IDeque)obj;
                return a.IndexOf(entry);
            }
            else
            {
                String a = (String)obj;
                return a.IndexOf((string)entry, StringComparison.InvariantCulture);
            }
        }

        public static int IndexOf(object obj, object entry, int startIndex)
        {
            if(obj is IList)
            {
                IList a = (IList)obj;
                for(int i = startIndex; i < a.Count; ++i)
                {
                    if(a[i].Equals(entry))
                        return i;
                }
                return -1;
            }
            else if(obj is IDeque)
            {
                IDeque a = (IDeque)obj;
                return a.IndexOf(entry, startIndex);
            }
            else
            {
                String a = (String)obj;
                return a.IndexOf((string)entry, startIndex, StringComparison.InvariantCulture);
            }
        }

        public static int LastIndexOf(object obj, object entry)
        {
            if(obj is IList)
            {
                IList a = (IList)obj;
                for(int i = a.Count - 1; i >= 0; --i)
                {
                    if(a[i].Equals(entry))
                        return i;
                }
                return -1;
            }
            else if(obj is IDeque)
            {
                IDeque a = (IDeque)obj;
                return a.LastIndexOf(entry);
            }
            else
            {
                String a = (String)obj;
                return a.LastIndexOf((string)entry, StringComparison.InvariantCulture);
            }
        }

        public static int LastIndexOf(object obj, object entry, int startIndex)
        {
            if(obj is IList)
            {
                IList a = (IList)obj;
                for(int i = startIndex; i >= 0; --i)
                {
                    if(a[i].Equals(entry))
                        return i;
                }
                return -1;
            }
            else if(obj is IDeque)
            {
                IDeque a = (IDeque)obj;
                return a.LastIndexOf(entry, startIndex);
            }
            else
            {
                String a = (String)obj;
                return a.LastIndexOf((string)entry, startIndex, StringComparison.InvariantCulture);
            }
        }
    }
}
