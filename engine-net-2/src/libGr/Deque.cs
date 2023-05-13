/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Collections;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Interface of a Double Ended Queue. 
    /// (Why is there none in the .NET collection library?)
    /// </summary>
    public interface IDeque : ICollection, ICloneable
    {
        //int Count { get; } from ICollection
        bool IsEmpty { get; }

        void Clear();

        void Enqueue(object item);
        void EnqueueFront(object item);
        void EnqueueAt(int index, object item);

        object Dequeue();
        object DequeueBack();
        object DequeueAt(int index);

        object Front { get; }
        object Back { get; }
        object this[int index] { get; set; }

        // IEnumerator GetEnumerator(); from ICollection : IEnumerable

        //bool IsSynchronized { get; } from ICollection
        //object SyncRoot { get; } from ICollection
        //void CopyTo(Array array, int index); from ICollection

        void Add(object item);

        int IndexOf(object item);
        int IndexOf(object item, int startIndex);
        int LastIndexOf(object item);
        int LastIndexOf(object item, int startIndex);
        bool Contains(object item);

        // object Clone(); from IClonable
    }

    /// <summary>
    /// Class implementing a Double Ended Queue. 
    /// Items can be added and removed in O(1) at the beginning and at the end, and accessed at an arbitrary index in O(1).
    /// Implemented by a ring buffer that is doubled in size (and the old content copied) when necessary (so insert is only amortized O(1)).
    /// </summary>
    public class Deque<T> : IDeque, ICollection<T>, IEnumerable<T>
    {
        // A ring buffer with the items stored in the deque.
        // The buffer is full if there is only one empty item left.
        private T[] buffer;

        // Index of the first item.
        private int begin;

        // Index of the item after the last item.
        // The deque is empty if begin==end.
        private int end;

        // Set by writes, reset at begin of enumeration, and checked during enumeration
        private bool modified;


        ////////////////////////////////////////////////////////////////////


        public Deque()
        {
            buffer = new T[4]; // we begin with 4 elements, i.e. 3 elements available to the user until first resize
            begin = 0;
            end = 0;
        }

        public Deque(Deque<T> that)
        {
            buffer = new T[that.buffer.Length];
            Array.Copy(that.buffer, 0, buffer, 0, buffer.Length);
            begin = that.begin;
            end = that.end;
        }

        public int Count
        {
            get
            {
                if(begin <= end)
                    return end - begin;
                else
                    return end + buffer.Length - begin;
            }
        }

        public bool IsEmpty
        {
            get { return begin == end; }
        }

        public void Clear()
        {
            modified = true;

            if(begin <= end)
            {
                for(int i = begin; i < end; ++i)
                {
                    buffer[i] = default(T);
                }
            }
            else
            {
                for(int i = begin; i < buffer.Length; ++i)
                {
                    buffer[i] = default(T);
                }
                for(int i = 0; i < end; ++i)
                {
                    buffer[i] = default(T);
                }
            }

            begin = 0;
            end = 0;
        }

        public void Enqueue(T item)
        {
            modified = true;

            buffer[end] = item;
            ++end;
            if(end == buffer.Length)
                end = 0;

            if(begin == end)
                Enlarge();
        }

        public void Enqueue(object item)
        {
            Enqueue((T)item);
        }

        public void EnqueueFront(T item)
        {
            modified = true;

            --begin;
            if(begin == -1)
                begin = buffer.Length - 1;
            buffer[begin] = item;

            if(begin == end)
                Enlarge();
        }

        public void EnqueueFront(object item)
        {
            EnqueueFront((T)item);
        }

        public void EnqueueAt(int index, T item)
        {
            modified = true;

            if(index < 0 || index > Count)
                throw new ArgumentOutOfRangeException("index out of bounds");

            // insert into the half that causes less moves
            int pos;
            if(index < Count / 2)
                pos = EnqueueAtMoveFirstHalf(index);
            else
                pos = EnqueueAtMoveSecondHalf(index);

            buffer[pos] = item;

            if(begin == end)
                Enlarge();
        }

        public void EnqueueAt(int index, object item)
        {
            EnqueueAt(index, (T)item);
        }

        private int EnqueueAtMoveFirstHalf(int index)
        {
            // make space for inserting item at pos by moving all items up to including it one leftwards 
            int length = buffer.Length;
            --begin;
            if(begin == -1)
                begin = length - 1;
            int pos = begin + index;

            if(pos < length)
                Array.Copy(buffer, begin + 1, buffer, begin, pos - begin);
            else
            {
                pos -= length;
                Array.Copy(buffer, begin + 1, buffer, begin, length - 1 - begin);
                buffer[length - 1] = buffer[0];
                Array.Copy(buffer, 1, buffer, 0, pos);
            }

            return pos;
        }

        private int EnqueueAtMoveSecondHalf(int index)
        {
            // make space for inserting item at pos by moving all items up to including it one rightwards
            int length = buffer.Length;
            int pos = begin + index;
            if(pos >= length)
                pos -= length;

            if(pos <= end)
            {
                Array.Copy(buffer, pos, buffer, pos + 1, end - pos);
                ++end;
                if(end == length)
                    end = 0;
            }
            else
            {
                Array.Copy(buffer, 0, buffer, 1, end);
                buffer[0] = buffer[length - 1];
                Array.Copy(buffer, pos, buffer, pos + 1, length - 1 - pos);
                ++end;
            }

            return pos;
        }

        private void Enlarge()
        {
            int length = buffer.Length;
            T[] newBuffer = new T[length * 2];
            Array.Copy(buffer, begin, newBuffer, 0, length - begin);
            Array.Copy(buffer, 0, newBuffer, length - begin, end);
            end = end + length - begin;
            begin = 0;
            buffer = newBuffer;
        }

        public T Dequeue()
        {
            modified = true;

            if(begin == end)
                throw new InvalidOperationException("collection empty!");

            T item = buffer[begin];
            buffer[begin] = default(T);
            ++begin;
            if(begin == buffer.Length)
                begin = 0;

            return item;
        }

        object IDeque.Dequeue()
        {
            return Dequeue();
        }

        public T DequeueBack()
        {
            modified = true;

            if(begin == end)
                throw new InvalidOperationException("collection empty!");

            --end;
            if(end == -1)
                end = buffer.Length - 1;
            T item = buffer[end];
            buffer[end] = default(T);

            return item;
        }

        object IDeque.DequeueBack()
        {
            return DequeueBack();
        }

        public T DequeueAt(int index)
        {
            modified = true;

            if(index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException("index out of bounds");

            // remove from the half that causes less moves
            if(index < Count / 2)
                return DequeueAtMoveFirstHalf(index);
            else
                return DequeueAtMoveSecondHalf(index);
        }

        object IDeque.DequeueAt(int index)
        {
            return DequeueAt(index);
        }

        private T DequeueAtMoveFirstHalf(int index)
        {
            // remove item at pos by moving all preceeding items one rightwards 
            int length = buffer.Length;
            int pos = index + begin;
            T result;

            if(pos < length)
            {
                result = buffer[pos];
                Array.Copy(buffer, begin, buffer, begin + 1, pos - begin);
            }
            else
            {
                pos -= length;
                result = buffer[pos];
                Array.Copy(buffer, 0, buffer, 1, pos);
                buffer[0] = buffer[length - 1];
                Array.Copy(buffer, begin, buffer, begin + 1, length - 1 - begin);
            }

            buffer[begin] = default(T);
            ++begin;
            if(begin == length)
                begin = 0;

            return result;
        }

        private T DequeueAtMoveSecondHalf(int index)
        {
            // remove item at pos by moving all following items one leftwards 
            int length = buffer.Length;
            int pos = index + begin;
            if(pos >= length)
                pos -= length;
            T result = buffer[pos];
            --end;
            if(end == -1)
                end = length - 1;

            if(pos <= end)
            {
                Array.Copy(buffer, pos + 1, buffer, pos, end - pos);
            }
            else
            {
                Array.Copy(buffer, pos + 1, buffer, pos, length - 1 - pos);
                buffer[length - 1] = buffer[0];
                Array.Copy(buffer, 1, buffer, 0, end);
            }

            buffer[end] = default(T);
            return result;
        }

        public T Front
        {
            get
            {
                if(begin == end)
                    throw new InvalidOperationException("collection empty!");

                return buffer[begin];
            }
        }

        object IDeque.Front
        {
            get
            {
                return Front;
            }
        }

        public T Back
        {
            get
            {
                if(begin == end)
                    throw new InvalidOperationException("collection empty!");

                if(end != 0)
                    return buffer[end - 1];
                else
                    return buffer[buffer.Length - 1];
            }
        }

        object IDeque.Back
        {
            get
            {
                return Back;
            }
        }

        public T this[int index]
        {
            get
            {
                if(index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException("index out of bounds");

                int length = buffer.Length;
                int pos = begin + index;
                if(pos >= length)
                    pos -= length;
                return buffer[pos];
            }

            set
            {
                modified = true;

                if(index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException("index out of bounds");

                int length = buffer.Length;
                int pos = begin + index;
                if(pos >= length)
                    pos -= length;
                buffer[pos] = value;
            }
        }

        object IDeque.this[int index]
        {
            get { return this[index]; }
            set { this[index] = (T)value; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            modified = false;

            int count = Count;
            for(int i = 0; i < count; ++i)
            {
                yield return this[i];
                if(modified)
                    throw new InvalidOperationException("changed during enumeration!");
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return this; }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            CopyTo((T[])array, index);
        }

        public void CopyTo(T[] array, int index)
        {
            if(Count == 0)
                return;

            if(array == null)
                throw new ArgumentNullException("array is null");
            if(index < 0)
                throw new ArgumentOutOfRangeException("index out of bounds");
            if(index + Count > array.Length)
                throw new ArgumentException("array too small");

            if(begin <= end)
                Array.Copy(buffer, begin, array, index, end - begin);
            else
            {
                Array.Copy(buffer, begin, array, index, buffer.Length - 1 - begin);
                Array.Copy(buffer, 0, array, index + buffer.Length - 1 - begin, end);
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }
        
        public void Add(T item)
        {
            Enqueue(item);
        }

        public void Add(object item)
        {
            Enqueue((T)item);
        }

        public int IndexOf(T item)
        {
            if(begin <= end)
            {
                for(int pos = begin; pos < end; ++pos)
                {
                    if(EqualityComparer<T>.Default.Equals(buffer[pos], item))
                        return pos - begin;
                }
            }
            else
            {
                for(int pos = begin; pos < buffer.Length; ++pos)
                {
                    if(EqualityComparer<T>.Default.Equals(buffer[pos], item))
                        return pos - begin;
                }
                for(int pos = 0; pos < end; ++pos)
                {
                    if(EqualityComparer<T>.Default.Equals(buffer[pos], item))
                        return pos + buffer.Length - begin;
                }
            }

            return -1;
        }

        int IDeque.IndexOf(object item)
        {
            return IndexOf((T)item);
        }

        public int IndexOf(T item, int startIndex)
        {
            if(startIndex < 0 || startIndex >= Count)
                throw new ArgumentOutOfRangeException("index out of bounds");

            if(begin <= end)
            {
                for(int pos = begin + startIndex; pos < end; ++pos)
                {
                    if(EqualityComparer<T>.Default.Equals(buffer[pos], item))
                        return pos - begin;
                }
            }
            else
            {
                for(int pos = begin + startIndex; pos < buffer.Length; ++pos)
                {
                    if(EqualityComparer<T>.Default.Equals(buffer[pos], item))
                        return pos - begin;
                }
                for(int pos = startIndex > buffer.Length - begin ? startIndex - (buffer.Length - begin) : 0; pos < end; ++pos)
                {
                    if(EqualityComparer<T>.Default.Equals(buffer[pos], item))
                        return pos + buffer.Length - begin;
                }
            }

            return -1;
        }

        int IDeque.IndexOf(object item, int startIndex)
        {
            return IndexOf((T)item, startIndex);
        }

        public int LastIndexOf(T item)
        {
            if(begin <= end)
            {
                for(int pos = end - 1; pos >= begin; --pos)
                {
                    if(EqualityComparer<T>.Default.Equals(buffer[pos], item))
                        return pos - begin;
                }
            }
            else
            {
                for(int pos = end - 1; pos >= 0; --pos)
                {
                    if(EqualityComparer<T>.Default.Equals(buffer[pos], item))
                        return pos + buffer.Length - begin;
                }
                for(int pos = buffer.Length - 1; pos >= begin; --pos)
                {
                    if(EqualityComparer<T>.Default.Equals(buffer[pos], item))
                        return pos - begin;
                }
            }

            return -1;
        }

        int IDeque.LastIndexOf(object item)
        {
            return LastIndexOf((T)item);
        }

        public int LastIndexOf(T item, int startIndex)
        {
            if(begin <= end)
            {
                // 0 -- unfilled space ; begin..startIndex..end ; unfilled space -- buffer.size()
                for(int pos = startIndex; pos >= begin; --pos)
                {
                    if(EqualityComparer<T>.Default.Equals(buffer[pos], item))
                        return pos - begin;
                }
            }
            else
            {
                if(startIndex <= end - 1)
                {
                    // 0..startIndex..end ; unfilled space ; begin...buffer.size
                    for(int pos = end - 1; pos >= 0; --pos)
                    {
                        if(EqualityComparer<T>.Default.Equals(buffer[pos], item))
                            return pos + buffer.Length - begin;
                    }
                    for(int pos = buffer.Length - 1; pos >= begin; --pos)
                    {
                        if(EqualityComparer<T>.Default.Equals(buffer[pos], item))
                            return pos - begin;
                    }
                }
                else
                {
                    // 0..end ; unfilled space ; begin..startIndex..buffer.size
                    for(int pos = startIndex; pos >= begin; --pos)
                    {
                        if(EqualityComparer<T>.Default.Equals(buffer[pos], item))
                            return pos - begin;
                    }
                }
            }

            return -1;
        }

        int IDeque.LastIndexOf(object item, int startIndex)
        {
            return LastIndexOf((T)item, startIndex);
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        bool IDeque.Contains(object item)
        {
            return Contains((T)item);
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if(index != -1)
            {
                DequeueAt(index);
                return true;
            }

            return false;
        }

        public Deque<T> Clone()
        {
            return new Deque<T>(this);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
