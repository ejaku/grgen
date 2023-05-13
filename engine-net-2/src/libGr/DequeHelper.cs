/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
        /// The value type of the Deque is returned
        /// </summary>
        /// <param name="dequeType">The Deque type</param>
        /// <param name="valueType">The value type of the Deque</param>
        public static void GetDequeType(Type dequeType, out Type valueType)
        {
            Type[] dequeTypeArgs = dequeType.GetGenericArguments();
            valueType = dequeTypeArgs[0];
        }

        public static IDeque FillDeque(IDeque dequeToCopyTo, string valueTypeName, object hopefullyDequeToCopy, IGraphModel model)
        {
            if(hopefullyDequeToCopy is IDeque)
                return FillDeque(dequeToCopyTo, valueTypeName, (IDeque)hopefullyDequeToCopy, model);
            throw new Exception("Deque copy constructor expects deque as source.");
        }

        public static IDeque FillDeque(IDeque dequeToCopyTo, string valueTypeName, IDeque dequeToCopy, IGraphModel model)
        {
            NodeType nodeType = TypesHelper.GetNodeType(valueTypeName, model);
            if(nodeType != null)
                FillDequeWithNode(dequeToCopyTo, nodeType, dequeToCopy);
            else
            {
                EdgeType edgeType = TypesHelper.GetEdgeType(valueTypeName, model);
                if(edgeType != null)
                    FillDequeWithEdge(dequeToCopyTo, edgeType, dequeToCopy);
                else
                {
                    Type varType = TypesHelper.GetType(valueTypeName, model);
                    FillDequeWithVar(dequeToCopyTo, varType, dequeToCopy);
                }
            }
            return dequeToCopyTo;
        }

        public static void FillDequeWithNode(IDeque targetDeque, NodeType nodeType, IDeque sourceDeque)
        {
            foreach(object entry in sourceDeque)
            {
                INode node = entry as INode;
                if(node == null)
                    continue;
                if(node.InstanceOf(nodeType))
                    targetDeque.Add(entry);
            }
        }

        public static void FillDequeWithEdge(IDeque targetDeque, EdgeType edgeType, IDeque sourceDeque)
        {
            foreach(object entry in sourceDeque)
            {
                IEdge edge = entry as IEdge;
                if(edge == null)
                    continue;
                if(edge.InstanceOf(edgeType))
                    targetDeque.Add(entry);
            }
        }

        public static void FillDequeWithVar(IDeque targetDeque, Type varType, IDeque sourceDeque)
        {
            foreach(object entry in sourceDeque)
            {
                if(entry.GetType() == varType)
                    targetDeque.Add(entry);
            }
        }

        public static Deque<K> FillDeque<K>(Deque<K> dequeToCopyTo, string valueTypeName, object hopefullyDequeToCopy, IGraphModel model)
        {
            if(hopefullyDequeToCopy is IDeque)
                return FillDeque(dequeToCopyTo, valueTypeName, (IDeque)hopefullyDequeToCopy, model);
            throw new Exception("Deque copy constructor expects deque as source.");
        }

        public static Deque<K> FillDeque<K>(Deque<K> dequeToCopyTo, string valueTypeName, IDeque dequeToCopy, IGraphModel model)
        {
            NodeType nodeType = TypesHelper.GetNodeType(valueTypeName, model);
            if(nodeType != null)
                FillDequeWithNode(dequeToCopyTo, nodeType, dequeToCopy);
            else
            {
                EdgeType edgeType = TypesHelper.GetEdgeType(valueTypeName, model);
                if(edgeType != null)
                    FillDequeWithEdge(dequeToCopyTo, edgeType, dequeToCopy);
                else
                {
                    Type varType = TypesHelper.GetType(valueTypeName, model);
                    FillDequeWithVar(dequeToCopyTo, varType, dequeToCopy);
                }
            }
            return dequeToCopyTo;
        }

        public static void FillDequeWithNode<K>(Deque<K> targetDeque, NodeType nodeType, IDeque sourceDeque)
        {
            foreach(object entry in sourceDeque)
            {
                INode node = entry as INode;
                if(node == null)
                    continue;
                if(node.InstanceOf(nodeType))
                    targetDeque.Add((K)entry);
            }
        }

        public static void FillDequeWithEdge<K>(Deque<K> targetDeque, EdgeType edgeType, IDeque sourceDeque)
        {
            foreach(object entry in sourceDeque)
            {
                IEdge edge = entry as IEdge;
                if(edge == null)
                    continue;
                if(edge.InstanceOf(edgeType))
                    targetDeque.Add((K)entry);
            }
        }

        public static void FillDequeWithVar<K>(Deque<K> targetDeque, Type varType, IDeque sourceDeque)
        {
            foreach(object entry in sourceDeque)
            {
                if(entry.GetType() == varType)
                    targetDeque.Add((K)entry);
            }
        }

        /// <summary>
        /// Creates a new Deque of the given value type
        /// </summary>
        /// <param name="valueType">The value type of the Deque to be created</param>
        /// <returns>The newly created Deque, null if unsuccessfull</returns>
        public static IDeque NewDeque(Type valueType)
        {
            if(valueType == null)
                throw new NullReferenceException();

            Type genDequeType = typeof(Deque<>);
            Type dequeType = genDequeType.MakeGenericType(valueType);
            return (IDeque)Activator.CreateInstance(dequeType);
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
            if(valueType == null || oldDeque == null)
                throw new NullReferenceException();

            Type genDequeType = typeof(Deque<>);
            Type dequeType = genDequeType.MakeGenericType(valueType);
            return (IDeque)Activator.CreateInstance(dequeType, oldDeque);
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

        public static int IndexOf<V>(Deque<V> a, V entry, int index)
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

        public static IDeque Subdeque(IDeque a, int start, int length)
        {
            IDeque newDeque = (IDeque)Activator.CreateInstance(a.GetType());

            for(int i = start; i < start + length; ++i)
            {
                newDeque.Add(a[i]);
            }

            return newDeque;
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
        public static Dictionary<V, SetValueType> DequeAsSet<V>(Deque<V> a)
        {
            Dictionary<V, SetValueType> newDict =
                new Dictionary<V, SetValueType>();

            for(int i = 0; i < a.Count; ++i)
            {
                newDict[a[i]] = null;
            }

            return newDict;
        }

        /// <summary>
        /// Creates a new list representing an array containing all values from the given deque.
        /// </summary>
        public static IList DequeAsArray(IDeque a)
        {
            Type valueType;
            ContainerHelper.GetDequeType(a, out valueType);
            IList newArray = NewList(valueType);

            for(int i = 0; i < a.Count; ++i)
            {
                newArray.Add(a[i]);
            }

            return newArray;
        }

        /// <summary>
        /// Creates a new list representing an array containing all values from the given deque.
        /// </summary>
        public static List<V> DequeAsArray<V>(Deque<V> a)
        {
            List<V> newArray = new List<V>();

            for(int i = 0; i < a.Count; ++i)
            {
                newArray.Add(a[i]);
            }

            return newArray;
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

        public static T Peek<T>(Deque<T> deque)
        {
            return deque[0];
        }

        public static T Peek<T>(Deque<T> deque, int index)
        {
            return deque[index];
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
            if(a == null || b == null)
            {
                if(a == null && b == null)
                    return true;
                else
                    return false;
            }
            if(a.Count != b.Count)
                return false;
            if(LessOrEqual(a, b))
                return true;
            else
                return false;
        }

        public static bool EqualIDeque(IDeque a, IDeque b)
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
            if(LessOrEqualIDeque(a, b))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks if Deque <paramref name="a"/> is not equal Deque <paramref name="b"/>.
        /// </summary>
        /// <param name="a">A Deque.</param>
        /// <param name="b">Another Deque of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of Deque comparison.</returns>
        public static bool NotEqual<V>(Deque<V> a, Deque<V> b)
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
            if(LessOrEqual(a, b))
                return false;
            else
                return true;
        }

        public static bool NotEqualIDeque(IDeque a, IDeque b)
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
            if(LessOrEqualIDeque(a, b))
                return false;
            else
                return true;
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
            if(a.Count == b.Count)
                return false;
            return GreaterOrEqual(a, b);
        }

        public static bool GreaterThanIDeque(IDeque a, IDeque b)
        {
            if(a.Count == b.Count)
                return false;
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
            if(a.Count == b.Count)
                return false;
            return LessOrEqual(a, b);
        }

        public static bool LessThanIDeque(IDeque a, IDeque b)
        {
            if(a.Count == b.Count)
                return false;
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
            if(a.Count > b.Count)
                return false;

            for(int i = 0; i < a.Count; ++i)
            {
                if(!EqualityComparer<V>.Default.Equals(a[i], b[i]))
                    return false;
            }

            return true;
        }

        public static bool LessOrEqualIDeque(IDeque a, IDeque b)
        {
            if(a.Count > b.Count)
                return false;

            for(int i = 0; i < a.Count; ++i)
            {
                if(!Object.Equals(a[i], b[i]))
                    return false;
            }

            return true;
        }

        public static bool DeeplyEqualDequeObject<V>(Deque<V> this_, Deque<V> that, IDictionary<object, object> visitedObjects)
        {
            if(this_.Count != that.Count)
                return false;

            for(int i = 0; i < this_.Count; ++i)
            {
                V thisElem = this_[i];
                V thatElem = that[i];
                if(!EqualityComparer<V>.Default.Equals(thisElem, thatElem))
                    return false;
            }

            return true;
        }

        public static bool DeeplyEqualDequeAttributeBearer<V>(Deque<V> this_, Deque<V> that, IDictionary<object, object> visitedObjects) where V : IAttributeBearer
        {
            if(this_.Count != that.Count)
                return false;

            for(int i = 0; i < this_.Count; ++i)
            {
                IAttributeBearer thisElem = this_[i];
                IAttributeBearer thatElem = that[i];
                if(!DeeplyEqual(thisElem, thatElem, visitedObjects))
                    return false;
            }

            return true;
        }

        public static bool DeeplyEqual(IDeque this_, IDeque that, IDictionary<object, object> visitedObjects)
        {
            if(this_.Count != that.Count)
                return false;
            if(this_.Count == 0)
                return true;
            if(this_[0] is IAttributeBearer)
                return DeeplyEqualDequeAttributeBearer(this_, that, visitedObjects);
            else
                return DeeplyEqualDequeObject(this_, that, visitedObjects);
        }

        private static bool DeeplyEqualDequeObject(IDeque this_, IDeque that, IDictionary<object, object> visitedObjects)
        {
            for(int i = 0; i < this_.Count; ++i)
            {
                if(!Object.Equals(this_[i], that[i]))
                    return false;
            }

            return true;
        }

        private static bool DeeplyEqualDequeAttributeBearer(IDeque this_, IDeque that, IDictionary<object, object> visitedObjects)
        {
            for(int i = 0; i < this_.Count; ++i)
            {
                IAttributeBearer thisElem = (IAttributeBearer)this_[i];
                IAttributeBearer thatElem = (IAttributeBearer)that[i];
                if(!DeeplyEqual(thisElem, thatElem, visitedObjects))
                    return false;
            }

            return true;
        }

        public static IDeque Copy(IDeque deque, IGraph graph, IDictionary<object, object> oldToNewObjects)
        {
            IDeque copy = (IDeque)Activator.CreateInstance(deque.GetType());

            foreach(object element in deque)
            {
                if(element is IObject)
                {
                    IObject elem = (IObject)element;
                    copy.Add(elem.Copy(graph, oldToNewObjects));
                }
                else if(element is ITransientObject)
                {
                    ITransientObject elem = (ITransientObject)element;
                    copy.Add(elem.Copy(graph, oldToNewObjects));
                }
                else
                {
                    copy.Add(element);
                }
            }

            return copy;
        }

        public static Deque<T> Copy<T>(Deque<T> deque, IGraph graph, IDictionary<object, object> oldToNewObjects)
        {
            Deque<T> copy = new Deque<T>();

            foreach(T element in deque)
            {
                if(element is IObject)
                {
                    IObject elem = (IObject)element;
                    copy.Add((T)elem.Copy(graph, oldToNewObjects));
                }
                else if(element is ITransientObject)
                {
                    ITransientObject elem = (ITransientObject)element;
                    copy.Add((T)elem.Copy(graph, oldToNewObjects));
                }
                else
                {
                    copy.Add(element);
                }
            }

            return copy;
        }

        public static IDeque MappingClone(IDeque deque, IDictionary<IGraphElement, IGraphElement> oldToNewElements)
        {
            IDeque copy = (IDeque)Activator.CreateInstance(deque.GetType());

            foreach(object element in deque)
            {
                if(element is IGraphElement)
                {
                    IGraphElement elem = (IGraphElement)element;
                    copy.Add(oldToNewElements[elem]);
                }
                else
                {
                    copy.Add(element);
                }
            }

            return copy;
        }

        public static Deque<T> MappingClone<T>(Deque<T> deque, IDictionary<IGraphElement, IGraphElement> oldToNewElements)
        {
            Deque<T> copy = new Deque<T>();

            foreach(T element in deque)
            {
                if(element is IGraphElement)
                {
                    IGraphElement elem = (IGraphElement)element;
                    copy.Add((T)oldToNewElements[elem]);
                }
                else
                {
                    copy.Add(element);
                }
            }

            return copy;
        }
    }
}
