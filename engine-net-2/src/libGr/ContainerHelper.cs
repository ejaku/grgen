/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections;

namespace de.unika.ipd.grGen.libGr
{
    public static partial class ContainerHelper
    {
        /// <summary>
        /// Creates a shallow clone of the given container.
        /// </summary>
        /// <param name="oldContainer">The container to clone.</param>
        /// <returns>A shallow clone of the container</returns>
        public static object Clone(object oldContainer)
        {
            if(oldContainer is IDictionary)
            {
                Type keyType, valueType;
                IDictionary dict = ContainerHelper.GetDictionaryTypes(
                    oldContainer, out keyType, out valueType);
                return NewDictionary(keyType, valueType, oldContainer);
            }
            else if(oldContainer is IList)
            {
                Type valueType;
                IList array = ContainerHelper.GetListType(
                    oldContainer, out valueType);
                return NewList(valueType, oldContainer);
            }
            else if(oldContainer is IDeque)
            {
                Type valueType;
                IDeque deque = ContainerHelper.GetDequeType(
                    oldContainer, out valueType);
                return NewDeque(valueType, oldContainer);
            }
            return null; // no known container type
        }

        /// <summary>
        /// Creates a new list containing all values from the given dictionary representing a set.
        /// </summary>
        public static IList AsArray(object container, IGraphModel model)
        {
            if(container is IList)
                return (IList)container;
            else if(container is IDictionary)
            {
                Type keyType;
                Type valueType;
                ContainerHelper.GetDictionaryTypes(container, out keyType, out valueType);
                if(valueType.Name == "SetValueType")
                    return SetAsArray((IDictionary)container);
                else
                    return MapAsArray((IDictionary)container, model);
            }
            else if(container is IDeque)
                return DequeAsArray((IDeque)container);
            return null;
        }

        /// <summary>
        /// Returns the value from the dictionary or list or deque at the nth position as defined by the iterator of the dictionary or the index of the list or the iterator of the deque.
        /// </summary>
        /// <param name="obj">A dictionary or a list or a deque.</param>
        /// <param name="num">The number of the element to get in the iteration sequence.</param>
        /// <returns>The element at the position to get.</returns>
        public static object Peek(object obj, int num)
        {
            if(obj is IDictionary)
            {
                IDictionary dict = (IDictionary)obj;
                IDictionaryEnumerator it = dict.GetEnumerator();
                if(num >= 0)
                    it.MoveNext();
                for(int i = 0; i < num; ++i)
                {
                    it.MoveNext();
                }
                return it.Key;
            }
            else if(obj is IList)
            {
                IList list = (IList)obj;
                return list[num];
            }
            else
            {
                IDeque deque = (IDeque)obj;
                if(num == 0)
                    return deque.Front;
                IEnumerator it = deque.GetEnumerator();
                if(num >= 0)
                    it.MoveNext();
                for(int i = 0; i < num; ++i)
                {
                    it.MoveNext();
                }
                return it.Current;
            }
        }

        public static object GetGraphElementAttributeOrElementOfMatch(object source, string attributeOrElementName)
        {
            if(source is IMatch)
            {
                IMatch match = (IMatch)source;
                object value = match.getNode(attributeOrElementName);
                if(value != null)
                    return value;
                value = match.getEdge(attributeOrElementName);
                if(value != null)
                    return value;
                value = match.getVariable(attributeOrElementName);
                return value;
            }
            else
            {
                IGraphElement elem = (IGraphElement)source;
                object value = elem.GetAttribute(attributeOrElementName);
                value = ContainerHelper.IfAttributeOfElementIsContainerThenCloneContainer(
                    elem, attributeOrElementName, value);
                return value;
            }
        }

        public static object InContainer(IGraphProcessingEnvironment procEnv, object container, object value)
        {
            if(container is IList)
            {
                IList array = (IList)container;
                return array.Contains(value);
            }
            else if(container is IDeque)
            {
                IDeque deque = (IDeque)container;
                return deque.Contains(value);
            }
            else
            {
                IDictionary setmap = (IDictionary)container;
                return setmap.Contains(value);
            }
        }

        public static int ContainerSize(IGraphProcessingEnvironment procEnv, object container)
        {
            if(container is IList)
            {
                IList array = (IList)container;
                return array.Count;
            }
            else if(container is IDeque)
            {
                IDeque deque = (IDeque)container;
                return deque.Count;
            }
            else
            {
                IDictionary setmap = (IDictionary)container;
                return setmap.Count;
            }
        }

        public static bool ContainerEmpty(IGraphProcessingEnvironment procEnv, object container)
        {
            if(container is IList)
            {
                IList array = (IList)container;
                return array.Count == 0;
            }
            else if(container is IDeque)
            {
                IDeque deque = (IDeque)container;
                return deque.Count == 0;
            }
            else
            {
                IDictionary setmap = (IDictionary)container;
                return setmap.Count == 0;
            }
        }

        public static object ContainerAccess(IGraphProcessingEnvironment procEnv, object container, object key)
        {
            if(container is IList)
            {
                IList array = (IList)container;
                return array[(int)key];
            }
            else if(container is IDeque)
            {
                IDeque deque = (IDeque)container;
                return deque[(int)key];
            }
            else
            {
                IDictionary setmap = (IDictionary)container;
                return setmap[key];
            }
        }

        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// If the attribute of the given name of the given element is a container attribute
        /// then return a clone of the given container value, otherwise just return the original value;
        /// additionally returns the AttributeType of the attribute of the element.
        /// </summary>
        public static object IfAttributeOfElementIsContainerThenCloneContainer(
                IGraphElement element, String AttributeName, object value, out AttributeType attrType)
        {
            attrType = element.Type.GetAttributeType(AttributeName);
            if(attrType.Kind == AttributeKind.SetAttr || attrType.Kind == AttributeKind.MapAttr)
            {
                Type keyType, valueType;
                ContainerHelper.GetDictionaryTypes(element.GetAttribute(AttributeName), out keyType, out valueType);
                return ContainerHelper.NewDictionary(keyType, valueType, value); // by-value-semantics -> clone dictionary
            }
            else if(attrType.Kind == AttributeKind.ArrayAttr)
            {
                Type valueType;
                ContainerHelper.GetListType(element.GetAttribute(AttributeName), out valueType);
                return ContainerHelper.NewList(valueType, value); // by-value-semantics -> clone array
            }
            else if(attrType.Kind == AttributeKind.DequeAttr)
            {
                Type valueType;
                ContainerHelper.GetDequeType(element.GetAttribute(AttributeName), out valueType);
                return ContainerHelper.NewDeque(valueType, value); // by-value-semantics -> clone deque
            }
            return value;
        }

        /// <summary>
        /// If the attribute of the given name of the given element is a conatiner attribute
        /// then return a clone of the given container value, otherwise just return the original value
        /// </summary>
        public static object IfAttributeOfElementIsContainerThenCloneContainer(
                IGraphElement element, String AttributeName, object value)
        {
            AttributeType attrType;
            return IfAttributeOfElementIsContainerThenCloneContainer(
                element, AttributeName, value, out attrType);
        }
    }
}
