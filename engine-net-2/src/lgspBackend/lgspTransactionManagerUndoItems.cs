/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    public interface IUndoItem
    {
        void DoUndo(LGSPGraphProcessingEnvironment procEnv);
    }


    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoTransactionStarted : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public LGSPUndoTransactionStarted()
        {
        }

        public void DoUndo(LGSPGraphProcessingEnvironment procEnv)
        {
            // nothing to do, this is only needed to distinguish the outermost transaction
            // from the nested transactions to know when to remove the rollback information
            // if nothing happened from opening the first transaction to opening the transaction of interest,
            // they would be indistinguishable as the number of undo items did not change
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoElemAdded : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public IGraphElement _elem;

        public LGSPUndoElemAdded(IGraphElement elem)
        {
            _elem = elem;
        }

        public void DoUndo(LGSPGraphProcessingEnvironment procEnv)
        {
            if(_elem is INode) procEnv.graph.Remove((INode) _elem);
            else procEnv.graph.Remove((IEdge) _elem);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoElemRemoved : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public IGraphElement _elem;
        public String _name;
        public LinkedList<Variable> _vars;

        public LGSPUndoElemRemoved(IGraphElement elem, LGSPGraphProcessingEnvironment procEnv)
        {
            _elem = elem;
            _vars = procEnv.GetElementVariables(_elem);
            if(procEnv.graph is LGSPNamedGraph) _name = ((LGSPNamedGraph)procEnv.graph).GetElementName(_elem);
            else _name = null;
        }

        private LGSPUndoElemRemoved(IGraphElement elem, String name, LinkedList<Variable> vars)
        {
            _elem = elem;
            _name = name;
            _vars = vars;
        }

        public void DoUndo(LGSPGraphProcessingEnvironment procEnv)
        {
            if(procEnv.Graph is LGSPNamedGraph)
            {
                if(_elem is LGSPNode) ((LGSPNamedGraph)procEnv.graph).AddNode((LGSPNode)_elem, _name);
                else ((LGSPNamedGraph)procEnv.graph).AddEdge((LGSPEdge)_elem, _name);
            }
            else
            {
                if(_elem is LGSPNode) procEnv.graph.AddNode((LGSPNode)_elem);
                else procEnv.graph.AddEdge((LGSPEdge)_elem);
            }

            if(_vars != null)
            {
                foreach(Variable var in _vars)
                    procEnv.SetVariableValue(var.Name, _elem);
            }
        }
    }

    public enum UndoOperation
    {
        None,
        Assign,
        PutElement,
        RemoveElement,
        AssignElement
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoAttributeChanged : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public IGraphElement _elem;
        public AttributeType _attrType;
        public UndoOperation _undoOperation;
        public Object _value;
        public Object _keyOfValue;

        public LGSPUndoAttributeChanged(IGraphElement elem, AttributeType attrType,
                AttributeChangeType changeType, Object newValue, Object keyValue)
        {
            _elem = elem; _attrType = attrType;

            if (_attrType.Kind == AttributeKind.SetAttr)
            {
                if (changeType == AttributeChangeType.PutElement)
                {
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    if (dict.Contains(newValue))
                    {
                        _undoOperation = UndoOperation.None;
                    }
                    else
                    {
                        _undoOperation = UndoOperation.RemoveElement;
                        _value = newValue;
                    }
                }
                else if (changeType == AttributeChangeType.RemoveElement)
                {
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    if (dict.Contains(newValue))
                    {
                        _undoOperation = UndoOperation.PutElement;
                        _value = newValue;
                    }
                    else
                    {
                        _undoOperation = UndoOperation.None;
                    }
                }
                else // Assign
                {
                    Type keyType, valueType;
                    IDictionary dict = ContainerHelper.GetDictionaryTypes(
                        _elem.GetAttribute(_attrType.Name), out keyType, out valueType);
                    IDictionary clonedDict = ContainerHelper.NewDictionary(keyType, valueType, dict);
                    _undoOperation = UndoOperation.Assign;
                    _value = clonedDict;
                }
            }
            else if (_attrType.Kind == AttributeKind.ArrayAttr)
            {
                if (changeType == AttributeChangeType.PutElement)
                {
                    IList array = (IList)_elem.GetAttribute(_attrType.Name);
                    _undoOperation = UndoOperation.RemoveElement;
                    _keyOfValue = keyValue;
                }
                else if (changeType == AttributeChangeType.RemoveElement)
                {
                    IList array = (IList)_elem.GetAttribute(_attrType.Name);
                    _undoOperation = UndoOperation.PutElement;
                    if(keyValue == null)
                    {
                        _value = array[array.Count-1];
                    }
                    else
                    {
                        _value = array[(int)keyValue];
                        _keyOfValue = keyValue;
                    }
                }
                else if(changeType == AttributeChangeType.AssignElement)
                {
                    IList array = (IList)_elem.GetAttribute(_attrType.Name);
                    _undoOperation = UndoOperation.AssignElement;
                    _value = array[(int)keyValue];
                    _keyOfValue = keyValue;
                }
                else // Assign
                {
                    Type valueType;
                    IList array = ContainerHelper.GetListType(
                        _elem.GetAttribute(_attrType.Name), out valueType);
                    IList clonedArray = ContainerHelper.NewList(valueType, array);
                    _undoOperation = UndoOperation.Assign;
                    _value = clonedArray;
                }
            }
            else if(_attrType.Kind == AttributeKind.DequeAttr)
            {
                if(changeType == AttributeChangeType.PutElement)
                {
                    IDeque deque = (IDeque)_elem.GetAttribute(_attrType.Name);
                    _undoOperation = UndoOperation.RemoveElement;
                    _keyOfValue = keyValue;
                }
                else if(changeType == AttributeChangeType.RemoveElement)
                {
                    IDeque deque = (IDeque)_elem.GetAttribute(_attrType.Name);
                    _undoOperation = UndoOperation.PutElement;
                    if(keyValue == null)
                    {
                        _value = deque.Front;
                    }
                    else
                    {
                        _value = deque[(int)keyValue];
                        _keyOfValue = keyValue;
                    }
                }
                else if(changeType == AttributeChangeType.AssignElement)
                {
                    IDeque deque = (IDeque)_elem.GetAttribute(_attrType.Name);
                    _undoOperation = UndoOperation.AssignElement;
                    _value = deque[(int)keyValue];
                    _keyOfValue = keyValue;
                }
                else // Assign
                {
                    Type valueType;
                    IDeque deque = ContainerHelper.GetDequeType(
                        _elem.GetAttribute(_attrType.Name), out valueType);
                    IDeque clonedDeque = ContainerHelper.NewDeque(valueType, deque);
                    _undoOperation = UndoOperation.Assign;
                    _value = clonedDeque;
                }
            }
            else if(_attrType.Kind == AttributeKind.MapAttr)
            {
                if (changeType == AttributeChangeType.PutElement)
                {
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    if (dict.Contains(keyValue))
                    {
                        if (dict[keyValue] == newValue)
                        {
                            _undoOperation = UndoOperation.None;
                        }
                        else
                        {
                            _undoOperation = UndoOperation.PutElement;
                            _value = dict[keyValue];
                            _keyOfValue = keyValue;
                        }
                    }
                    else
                    {
                        _undoOperation = UndoOperation.RemoveElement;
                        _value = newValue;
                        _keyOfValue = keyValue;
                    }
                }
                else if (changeType == AttributeChangeType.RemoveElement)
                {
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    if (dict.Contains(keyValue))
                    {
                        _undoOperation = UndoOperation.PutElement;
                        _value = dict[keyValue];
                        _keyOfValue = keyValue;
                    }
                    else
                    {
                        _undoOperation = UndoOperation.None;
                    }
                }
                else if(changeType == AttributeChangeType.AssignElement)
                {
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    if(dict[keyValue] == newValue)
                    {
                        _undoOperation = UndoOperation.None;
                    }
                    else
                    {
                        _undoOperation = UndoOperation.AssignElement;
                        _value = dict[keyValue];
                        _keyOfValue = keyValue;
                    }
                }
                else // Assign
                {
                    Type keyType, valueType;
                    IDictionary dict = ContainerHelper.GetDictionaryTypes(
                        _elem.GetAttribute(_attrType.Name), out keyType, out valueType);
                    IDictionary clonedDict = ContainerHelper.NewDictionary(keyType, valueType, dict);
                    _undoOperation = UndoOperation.Assign;
                    _value = clonedDict;
                }
            }
            else // Primitve Type Assign
            {
                _undoOperation = UndoOperation.Assign;
                _value = _elem.GetAttribute(_attrType.Name);
            }
        }

        public void DoUndo(LGSPGraphProcessingEnvironment procEnv)
        {
            String attrName = _attrType.Name;
            if (_undoOperation == UndoOperation.PutElement)
            {
                if (_attrType.Kind == AttributeKind.SetAttr)
                {
                    ChangingElementAttribute(procEnv);
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    dict.Add(_value, null);
                }
                else if(_attrType.Kind == AttributeKind.MapAttr)
                {
                    ChangingElementAttribute(procEnv);
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    dict.Add(_keyOfValue, _value);
                }
                else if (_attrType.Kind == AttributeKind.ArrayAttr)
                {
                    ChangingElementAttribute(procEnv);
                    IList array = (IList)_elem.GetAttribute(_attrType.Name);
                    if(_keyOfValue == null)
                        array.Add(_value);
                    else
                        array.Insert((int)_keyOfValue, _value);
                }
                else //if(_attrType.Kind == AttributeKind.DequeAttr)
                {
                    ChangingElementAttribute(procEnv);
                    IDeque deque = (IDeque)_elem.GetAttribute(_attrType.Name);
                    if(_keyOfValue == null)
                        deque.EnqueueFront(_value);
                    else
                        deque.EnqueueAt((int)_keyOfValue, _value);
                }
            }
            else if (_undoOperation == UndoOperation.RemoveElement)
            {
                if (_attrType.Kind == AttributeKind.SetAttr)
                {
                    ChangingElementAttribute(procEnv);
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    dict.Remove(_value);
                }
                else if (_attrType.Kind == AttributeKind.MapAttr)
                {
                    ChangingElementAttribute(procEnv);
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    dict.Remove(_keyOfValue);
                }
                else if(_attrType.Kind == AttributeKind.ArrayAttr)
                {
                    ChangingElementAttribute(procEnv);
                    IList array = (IList)_elem.GetAttribute(_attrType.Name);
                    if(_keyOfValue == null)
                        array.RemoveAt(array.Count - 1);
                    else
                        array.RemoveAt((int)_keyOfValue);
                }
                else //if(_attrType.Kind == AttributeKind.DequeAttr)
                {
                    ChangingElementAttribute(procEnv);
                    IDeque deque = (IDeque)_elem.GetAttribute(_attrType.Name);
                    if(_keyOfValue == null)
                        deque.DequeueBack();
                    else
                        deque.DequeueAt((int)_keyOfValue);
                }
            }
            else if(_undoOperation == UndoOperation.AssignElement)
            {
                if(_attrType.Kind == AttributeKind.ArrayAttr)
                {
                    ChangingElementAttribute(procEnv);
                    IList array = (IList)_elem.GetAttribute(_attrType.Name);
                    array[(int)_keyOfValue] = _value;
                }
                else //if(_attrType.Kind == AttributeKind.DequeAttr)
                {
                    ChangingElementAttribute(procEnv);
                    IDeque deque = (IDeque)_elem.GetAttribute(_attrType.Name);
                    deque[(int)_keyOfValue] = _value;
                }
            }
            else if(_undoOperation == UndoOperation.Assign)
            {
                ChangingElementAttribute(procEnv);
                _elem.SetAttribute(attrName, _value);
            }
            // otherwise UndoOperation.None
        }

        private void ChangingElementAttribute(LGSPGraphProcessingEnvironment procEnv)
        {
            AttributeChangeType changeType;
            switch (_undoOperation)
            {
                case UndoOperation.Assign: changeType = AttributeChangeType.Assign; break;
                case UndoOperation.PutElement: changeType = AttributeChangeType.PutElement; break;
                case UndoOperation.RemoveElement: changeType = AttributeChangeType.RemoveElement; break;
                case UndoOperation.AssignElement: changeType = AttributeChangeType.AssignElement; break;
                default: throw new Exception("Internal error during transaction handling");
            }

            LGSPNode node = _elem as LGSPNode;
            if (node != null)
            {
                procEnv.graph.ChangingNodeAttribute(node, _attrType, changeType, _value, _keyOfValue);
            }
            else
            {
                LGSPEdge edge = (LGSPEdge)_elem;
                procEnv.graph.ChangingEdgeAttribute(edge, _attrType, changeType, _value, _keyOfValue);
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoElemRetyped : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public IGraphElement _oldElem;
        public IGraphElement _newElem;

        public LGSPUndoElemRetyped(IGraphElement oldElem, IGraphElement newElem)
        {
            _oldElem = oldElem;
            _newElem = newElem;
        }

        public void DoUndo(LGSPGraphProcessingEnvironment procEnv)
        {
            String name;
            if(procEnv.graph is LGSPNamedGraph && ((LGSPNamedGraph)procEnv.graph).ElemToName.TryGetValue(_newElem, out name))
            {
                LGSPNamedGraph lgspGraph = (LGSPNamedGraph)procEnv.graph;
                if(_newElem is INode)
                {
                    LGSPNode newNode = (LGSPNode)_newElem;
                    LGSPNode oldNode = (LGSPNode)_oldElem;
                    lgspGraph.ElemToName[oldNode] = name;
                    lgspGraph.RetypingNode(newNode, oldNode); // this will switch the variables
                    lgspGraph.ReplaceNode(newNode, oldNode);
                    lgspGraph.ElemToName.Remove(newNode);
                    lgspGraph.NameToElem[name] = oldNode;
                }
                else
                {
                    LGSPEdge newEdge = (LGSPEdge)_newElem;
                    LGSPEdge oldEdge = (LGSPEdge)_oldElem;
                    lgspGraph.ElemToName[oldEdge] = name;
                    lgspGraph.RetypingEdge(newEdge, oldEdge); // this will switch the variables
                    lgspGraph.ReplaceEdge(newEdge, oldEdge);
                    lgspGraph.ElemToName.Remove(newEdge);
                    lgspGraph.NameToElem[name] = oldEdge;
                }
            }
            else
            {
                LGSPGraph lgspGraph = procEnv.graph;
                if(_newElem is INode)
                {
                    LGSPNode newNode = (LGSPNode)_newElem;
                    LGSPNode oldNode = (LGSPNode)_oldElem;
                    lgspGraph.RetypingNode(newNode, oldNode); // this will switch the variables
                    lgspGraph.ReplaceNode(newNode, oldNode);
                }
                else
                {
                    LGSPEdge newEdge = (LGSPEdge)_newElem;
                    LGSPEdge oldEdge = (LGSPEdge)_oldElem;
                    lgspGraph.RetypingEdge(newEdge, oldEdge); // this will switch the variables
                    lgspGraph.ReplaceEdge(newEdge, oldEdge);
                }
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoElemRedirecting : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public LGSPEdge _edge;
        public LGSPNode _source;
        public LGSPNode _target;

        public LGSPUndoElemRedirecting(LGSPEdge edge, LGSPNode source, LGSPNode target)
        {
            _edge = edge;
            _source = source;
            _target = target;
        }

        public void DoUndo(LGSPGraphProcessingEnvironment procEnv)
        {
            _edge.lgspSource = _source;
            _edge.lgspTarget = _target;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoVisitedAlloc : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public int _visitorID;

        public LGSPUndoVisitedAlloc(int visitorID)
        {
            _visitorID = visitorID;
        }

        public void DoUndo(LGSPGraphProcessingEnvironment procEnv)
        {
            procEnv.graph.FreeVisitedFlagNonReset(_visitorID);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoVisitedFree : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public int _visitorID;
        
        public LGSPUndoVisitedFree(int visitorID)
        {
            _visitorID = visitorID;
        }

        public void DoUndo(LGSPGraphProcessingEnvironment procEnv)
        {
            procEnv.graph.ReallocateVisitedFlag(_visitorID);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoSettingVisited : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public IGraphElement _elem;
        public int _visitorID;
        public bool _oldValue;
        
        public LGSPUndoSettingVisited(IGraphElement elem, int visitorID, bool oldValue)
        {
            _elem = elem;
            _visitorID = visitorID;
            _oldValue = oldValue;
        }

        public void DoUndo(LGSPGraphProcessingEnvironment procEnv)
        {
            procEnv.graph.SetVisited(_elem, _visitorID, _oldValue);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoGraphChange : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public IGraph _oldGraph;

        public LGSPUndoGraphChange(IGraph oldGraph)
        {
            _oldGraph = oldGraph;
        }

        public void DoUndo(LGSPGraphProcessingEnvironment procEnv)
        {
            procEnv.ReturnFromSubgraph();
            procEnv.SwitchToSubgraph(_oldGraph);
        }
    }
}
