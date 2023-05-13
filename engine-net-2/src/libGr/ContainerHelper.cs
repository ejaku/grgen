/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections;
using System.Collections.Generic;

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
        /// Creates a deep copy of the given container.
        /// </summary>
        /// <param name="oldContainer">The container to copy.</param>
        /// <param name="graph">The graph to fetch the names of the new objects from.</param>
        /// <param name="oldToNewObjects">A dictionary mapping objects to their copies, to be supplied as empty dictionary.</param>
        /// <returns>A deep copy of the container</returns>
        public static object Copy(object oldContainer, IGraph graph, IDictionary<object, object> oldToNewObjects)
        {
            if(oldContainer is IDictionary)
            {
                return Copy((IDictionary)oldContainer, graph, oldToNewObjects);
            }
            else if(oldContainer is IList)
            {
                return Copy((IList)oldContainer, graph, oldToNewObjects);
            }
            else if(oldContainer is IDeque)
            {
                return Copy((IDeque)oldContainer, graph, oldToNewObjects);
            }
            return null; // no known container type
        }

        /// <summary>
        /// Creates a shallow clone of the given container, mapping the contained elements according to the input new to old elements map
        /// (clones a container, transplanting contained graph elements from an old to a new graph).
        /// </summary>
        /// <param name="oldContainer">The container to clone.</param>
        /// <param name="oldToNewElements">A map from old elements of an old graph to their clones, i.e. new elements in a new graph.</param>
        /// <returns>A shallow clone of the container</returns>
        public static object MappingClone(object oldContainer, IDictionary<IGraphElement, IGraphElement> oldToNewElements)
        {
            if(oldContainer is IDictionary)
            {
                return MappingClone((IDictionary)oldContainer, oldToNewElements);
            }
            else if(oldContainer is IList)
            {
                return MappingClone((IList)oldContainer, oldToNewElements);
            }
            else if(oldContainer is IDeque)
            {
                return MappingClone((IDeque)oldContainer, oldToNewElements);
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

        public static object GetAttributeOrElementOfMatch(object source, string attributeOrElementName)
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
                IAttributeBearer attributeBearer = (IAttributeBearer)source;
                object value = attributeBearer.GetAttribute(attributeOrElementName);
                value = attributeBearer is ITransientObject ? value : ContainerHelper.IfAttributeOfElementIsContainerThenCloneContainer(
                    attributeBearer, attributeOrElementName, value);
                return value;
            }
        }

        public static object InContainerOrString(IGraphProcessingEnvironment procEnv, object containerOrString, object value)
        {
            if(containerOrString is String)
            {
                String str = (String)containerOrString;
                return str.Contains((string)value);
            }
            else if(containerOrString is IList)
            {
                IList array = (IList)containerOrString;
                return array.Contains(value);
            }
            else if(containerOrString is IDeque)
            {
                IDeque deque = (IDeque)containerOrString;
                return deque.Contains(value);
            }
            else
            {
                IDictionary setmap = (IDictionary)containerOrString;
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

        public static object ContainerAddAll(IGraphProcessingEnvironment procEnv, object container, object containerToBeAdded)
        {
            if(container is IList)
            {
                IList array = (IList)container;
                IList arrayToBeAdded = (IList)containerToBeAdded;

                foreach(object arrayValue in arrayToBeAdded)
                {
                    array.Add(arrayValue);
                }

                return array;
            }
            else
            {
                IDictionary set = (IDictionary)container;
                IDictionary setToBeAdded = (IDictionary)containerToBeAdded;

                foreach(object setValue in setToBeAdded.Keys)
                {
                    set.Add(setValue, null);
                }

                return set;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////

        public static void AssignAttribute(object target, object value, string attributeName, IGraph graph)
        {
            if(target is IGraphElement)
            {
                IGraphElement elem = (IGraphElement)target;

                AttributeType attrType;
                value = ContainerHelper.IfAttributeOfElementIsContainerThenCloneContainer(
                    elem, attributeName, value, out attrType);

                BaseGraph.ChangingAttributeAssign(graph, elem, attrType, value);

                elem.SetAttribute(attributeName, value);

                BaseGraph.ChangedAttribute(graph, elem, attrType);
            }
            else if(target is IObject)
            {
                IObject elem = (IObject)target;

                AttributeType attrType = elem.Type.GetAttributeType(attributeName);

                BaseGraph.ChangingAttributeAssign(graph, elem, attrType, value);

                elem.SetAttribute(attributeName, value);
            }
            else //if(target is ITransientObject)
            {
                ITransientObject elem = (ITransientObject)target;

                elem.SetAttribute(attributeName, value);
            }
        }

        public static void AssignAttributeIndexed(object target, object key, object value, string attributeName, IGraph graph)
        {
            if(target is IGraphElement)
            {
                IGraphElement elem = (IGraphElement)target;
                object container = elem.GetAttribute(attributeName);
                AttributeType attrType = elem.Type.GetAttributeType(attributeName);

                BaseGraph.ChangingAttributeAssignElement(graph, elem, attrType, value, key);

                if(container is IList)
                {
                    IList array = (IList)container;
                    array[(int)key] = value;
                }
                else if(container is IDeque)
                {
                    IDeque deque = (IDeque)container;
                    deque[(int)key] = value;
                }
                else
                {
                    IDictionary map = (IDictionary)container;
                    map[key] = value;
                }

                BaseGraph.ChangedAttribute(graph, elem, attrType);
            }
            else if(target is IObject)
            {
                IObject elem = (IObject)target;
                object container = elem.GetAttribute(attributeName);
                AttributeType attrType = elem.Type.GetAttributeType(attributeName);

                if(container is IList)
                {
                    IList array = (IList)container;
                    array[(int)key] = value;
                }
                else if(container is IDeque)
                {
                    IDeque deque = (IDeque)container;
                    deque[(int)key] = value;
                }
                else
                {
                    IDictionary map = (IDictionary)container;
                    map[key] = value;
                }
            }
            else
            {
                ITransientObject elem = (ITransientObject)target;
                object container = elem.GetAttribute(attributeName);
                AttributeType attrType = elem.Type.GetAttributeType(attributeName);

                if(container is IList)
                {
                    IList array = (IList)container;
                    array[(int)key] = value;
                }
                else if(container is IDeque)
                {
                    IDeque deque = (IDeque)container;
                    deque[(int)key] = value;
                }
                else
                {
                    IDictionary map = (IDictionary)container;
                    map[key] = value;
                }
            }
        }

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
        /// If the attribute of the given name of the given element is a container attribute
        /// then return a clone of the given container value, otherwise just return the original value;
        /// additionally returns the AttributeType of the attribute of the element.
        /// </summary>
        public static object IfAttributeOfElementIsContainerThenCloneContainer(
                IObject element, String AttributeName, object value, out AttributeType attrType)
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
                IAttributeBearer attributeBearer, String AttributeName, object value)
        {
            AttributeType attrType;
            if(attributeBearer is IGraphElement)
                return IfAttributeOfElementIsContainerThenCloneContainer(
                    (IGraphElement)attributeBearer, AttributeName, value, out attrType);
            else if(attributeBearer is IObject)
                return IfAttributeOfElementIsContainerThenCloneContainer(
                    (IObject)attributeBearer, AttributeName, value, out attrType);
            else //if(attributeBearer is ITransientObject)
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

        /// <summary>
        /// If the attribute of the given name of the given element is a conatiner attribute
        /// then return a clone of the given container value, otherwise just return the original value
        /// </summary>
        public static object IfAttributeOfElementIsContainerThenCloneContainer(
                IObject element, String AttributeName, object value)
        {
            AttributeType attrType;
            return IfAttributeOfElementIsContainerThenCloneContainer(
                element, AttributeName, value, out attrType);
        }

        public static bool IsEqual(IObject this_, IObject that)
        {
            return this_ == that;
        }

        public static bool IsEqual(ITransientObject this_, ITransientObject that)
        {
            return this_ == that;
        }

        /// <summary>
        /// Returns whether the input values are structurally equal according to the ~~ operator, beside attribute bearers and graphs, this esp. includes containers,
        /// then it returns whether this container and that container are memberwise deeply equal,
        /// which means the scalar members are equal, and object attributes are deeply equal.
        /// (If types are unequal the result is false. For graphs, it returns whether they are structurally equal.)
        /// </summary>
        public static bool StructurallyEqual(object this_, object that, IDictionary<object, object> visitedObjects)
        {
            if(this_ is IAttributeBearer)
            {
                return DeeplyEqual((IAttributeBearer)this_, (IAttributeBearer)that, visitedObjects);
            }
            else if(this_ is IList)
            {
                return DeeplyEqual((IList)this_, (IList)that, visitedObjects);
            }
            else if(this_ is IDeque)
            {
                return DeeplyEqual((IDeque)this_, (IDeque)that, visitedObjects);
            }
            else if(this_ is IDictionary)
            {
                return DeeplyEqual((IDictionary)this_, (IDictionary)that, visitedObjects);
            }
            else
            {
                // TODO: ok if called as top level operator implementation, not ok if called on a graph attribute, there it should compare for isomorphy
                return GraphHelper.HasSameStructure((IGraph)this_, (IGraph)that);
            }
        }

        public static bool DeeplyEqual(IAttributeBearer this_, IAttributeBearer that, IDictionary<object, object> visitedObjects)
        {
            if(this_ == null && that == null)
                return true;

            if(this_ == null || that == null)
                return false;

            return this_.IsDeeplyEqual(that, visitedObjects);
        }
    }
}
