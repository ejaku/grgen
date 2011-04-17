/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Deprecated...
    /// </summary>
    public class SingleLinkedList<T> : IEnumerable<T>
    {
        class Node
        {
            public T Data;
            public Node Next;

            public Node(T data, Node next)
            {
                Data = data;
                Next = next;
            }
        }

        Node root;
        public int length;

        public void Clear()
        {
            root = null;
            length = 0;
        }

        public void AddFirst(T data)
        {
            root = new Node(data, root);
            length++;
        }

        public T RemoveFirst()
        {
            if(root == null) throw new InvalidOperationException("The list is empty!");
            T data = root.Data;
            root = root.Next;
            length--;
            return data;
        }

        public T this[int index]
        {
            get
            {
                if(index < 0 || index >= length)
                    throw new ArgumentOutOfRangeException("Index out of range: " + index);
                Node cur = root;
                for(int i=0; i < index; i++)
                    cur = cur.Next;

                return cur.Data;
            }
        }

        public int Count { get { return length; } }

        public IEnumerator<T> GetEnumerator()
        {
            Node cur = root;
            while(cur != null)
            {
                yield return cur.Data;
                cur = cur.Next;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
