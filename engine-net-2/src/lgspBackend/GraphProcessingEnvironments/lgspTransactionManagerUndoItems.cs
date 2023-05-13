/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoTransactionStarted : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public LGSPUndoTransactionStarted()
        {
        }

        public void DoUndo(IGraphProcessingEnvironment procEnv)
        {
            // nothing to do, this is only needed to distinguish the outermost transaction
            // from the nested transactions to know when to remove the rollback information
            // if nothing happened from opening the first transaction to opening the transaction of interest,
            // they would be indistinguishable as the number of undo items did not change
        }

        public override string ToString()
        {
            return "Transaction::start()";
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoElemAdded : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public readonly IGraphElement _elem;
        public readonly String _name; // for ToString only
        public readonly String _sourceName; // for ToString only
        public readonly String _targetName; // for ToString only

        public LGSPUndoElemAdded(IGraphElement elem, LGSPGraphProcessingEnvironment procEnv)
        {
            _elem = elem;
            if(procEnv.graph is LGSPNamedGraph)
            {
                _name = ((LGSPNamedGraph)procEnv.graph).GetElementName(_elem);
                if(_elem is IEdge)
                {
                    _sourceName = ((LGSPNamedGraph)procEnv.graph).GetElementName(((IEdge)_elem).Source);
                    _targetName = ((LGSPNamedGraph)procEnv.graph).GetElementName(((IEdge)_elem).Target);
                }
            }
            else
            {
                _name = "?";
                if(_elem is IEdge)
                {
                    _sourceName = "?";
                    _targetName = "?";
                }
            }
        }

        public void DoUndo(IGraphProcessingEnvironment procEnv)
        {
            if(_elem is INode)
                procEnv.Graph.Remove((INode) _elem);
            else
                procEnv.Graph.Remove((IEdge) _elem);
        }

        public override string ToString()
        {
            if(_elem is INode)
                return "rem(" + _name + ":" + _elem.Type.Name + ")";
            else
                return "rem(-" + _name + ":" + _elem.Type.Name + "->" + SourceTarget() + ")";
        }

        private string SourceTarget()
        {
            if(_sourceName == null || _targetName == null)
                return ", ?, ?";
            else
                return ", " + _sourceName + ", " + _targetName;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoElemRemoved : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public readonly IGraphElement _elem;
        public readonly String _name;
        public readonly LinkedList<Variable> _vars;
        public readonly IGraph _graph; // for ToString only

        public LGSPUndoElemRemoved(IGraphElement elem, LGSPGraphProcessingEnvironment procEnv)
        {
            _elem = elem;
            _vars = procEnv.GetElementVariables(_elem);
            if(procEnv.graph is LGSPNamedGraph)
                _name = ((LGSPNamedGraph)procEnv.graph).GetElementName(_elem);
            else
                _name = null;
            _graph = procEnv.graph;
        }

        private LGSPUndoElemRemoved(IGraphElement elem, String name, LinkedList<Variable> vars)
        {
            _elem = elem;
            _name = name;
            _vars = vars;
        }

        public void DoUndo(IGraphProcessingEnvironment procEnv)
        {
            LGSPGraphProcessingEnvironment procEnv_ = (LGSPGraphProcessingEnvironment)procEnv;
            if(procEnv.Graph is LGSPNamedGraph)
            {
                if(_elem is LGSPNode)
                    ((LGSPNamedGraph)procEnv_.graph).AddNode((LGSPNode)_elem, _name);
                else
                    ((LGSPNamedGraph)procEnv_.graph).AddEdge((LGSPEdge)_elem, _name);
            }
            else
            {
                if(_elem is LGSPNode)
                    procEnv_.graph.AddNode((LGSPNode)_elem);
                else
                    procEnv_.graph.AddEdge((LGSPEdge)_elem);
            }

            if(_vars != null)
            {
                foreach(Variable var in _vars)
                {
                    procEnv_.SetVariableValue(var.Name, _elem);
                }
            }
        }

        public override string ToString()
        {
            if(_elem is INode)
                return _name != null ? "add(" + _name + ":" + _elem.Type.Name + ")" : "add(?:" + _elem.Type.Name + ")";
            else
                return _name != null ? "add(-" + _name + ":" + _elem.Type.Name + "->" + SourceTarget() + ")" : "add(-?:" + _elem.Type.Name + "->" + SourceTarget() + ")";
        }

        private string SourceTarget()
        {
            if(!(_graph is INamedGraph) || ((IEdge)_elem).Source == null || ((IEdge)_elem).Target == null)
                return ", ?, ?";
            else
                return ", " + ((INamedGraph)_graph).GetElementName(((IEdge)_elem).Source) + ", " + ((INamedGraph)_graph).GetElementName(((IEdge)_elem).Target);
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
        public readonly IAttributeBearer _elem;
        public readonly AttributeType _attrType;
        public readonly UndoOperation _undoOperation;
        public readonly object _value;
        public readonly object _keyOfValue;
        public readonly String _name; // for ToString only
        public readonly IGraph _graph; // for ToString only

        public LGSPUndoAttributeChanged(IAttributeBearer elem, AttributeType attrType,
                AttributeChangeType changeType, object newValue, object keyValue, 
                LGSPGraphProcessingEnvironment procEnv)
        {
            _elem = elem;
            _attrType = attrType;
            if(procEnv.graph is LGSPNamedGraph)
            {
                if(_elem is IGraphElement)
                    _name = ((LGSPNamedGraph)procEnv.graph).GetElementName((IGraphElement)_elem);
                else
                    _name = "hash" + _elem.GetHashCode();
            }
            else
                _name = "?";
            _graph = procEnv.graph;

            if(_attrType.Kind == AttributeKind.SetAttr)
            {
                if(changeType == AttributeChangeType.PutElement)
                {
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    if(dict.Contains(newValue))
                        _undoOperation = UndoOperation.None;
                    else
                    {
                        _undoOperation = UndoOperation.RemoveElement;
                        _value = newValue;
                    }
                }
                else if(changeType == AttributeChangeType.RemoveElement)
                {
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    if(dict.Contains(newValue))
                    {
                        _undoOperation = UndoOperation.PutElement;
                        _value = newValue;
                    }
                    else
                        _undoOperation = UndoOperation.None;
                }
                else // Assign
                {
                    Type keyType;
                    Type valueType;
                    IDictionary dict = ContainerHelper.GetDictionaryTypes(
                        _elem.GetAttribute(_attrType.Name), out keyType, out valueType);
                    IDictionary clonedDict = ContainerHelper.NewDictionary(keyType, valueType, dict);
                    _undoOperation = UndoOperation.Assign;
                    _value = clonedDict;
                }
            }
            else if(_attrType.Kind == AttributeKind.ArrayAttr)
            {
                if(changeType == AttributeChangeType.PutElement)
                {
                    IList array = (IList)_elem.GetAttribute(_attrType.Name);
                    _undoOperation = UndoOperation.RemoveElement;
                    _keyOfValue = keyValue;
                }
                else if(changeType == AttributeChangeType.RemoveElement)
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
                        _value = deque.Front;
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
                if(changeType == AttributeChangeType.PutElement)
                {
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    if(dict.Contains(keyValue))
                    {
                        if(dict[keyValue] == newValue)
                            _undoOperation = UndoOperation.None;
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
                else if(changeType == AttributeChangeType.RemoveElement)
                {
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    if(dict.Contains(keyValue))
                    {
                        _undoOperation = UndoOperation.PutElement;
                        _value = dict[keyValue];
                        _keyOfValue = keyValue;
                    }
                    else
                        _undoOperation = UndoOperation.None;
                }
                else if(changeType == AttributeChangeType.AssignElement)
                {
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    if(dict[keyValue] == newValue)
                        _undoOperation = UndoOperation.None;
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

        public void DoUndo(IGraphProcessingEnvironment procEnv)
        {
            String attrName = _attrType.Name;
            LGSPGraphProcessingEnvironment procEnv_ = (LGSPGraphProcessingEnvironment)procEnv;
            if(_undoOperation == UndoOperation.PutElement)
            {
                if(_attrType.Kind == AttributeKind.SetAttr)
                {
                    ChangingElementAttribute(procEnv_);
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    dict.Add(_value, null);
                }
                else if(_attrType.Kind == AttributeKind.MapAttr)
                {
                    ChangingElementAttribute(procEnv_);
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    dict.Add(_keyOfValue, _value);
                }
                else if(_attrType.Kind == AttributeKind.ArrayAttr)
                {
                    ChangingElementAttribute(procEnv_);
                    IList array = (IList)_elem.GetAttribute(_attrType.Name);
                    if(_keyOfValue == null)
                        array.Add(_value);
                    else
                        array.Insert((int)_keyOfValue, _value);
                }
                else //if(_attrType.Kind == AttributeKind.DequeAttr)
                {
                    ChangingElementAttribute(procEnv_);
                    IDeque deque = (IDeque)_elem.GetAttribute(_attrType.Name);
                    if(_keyOfValue == null)
                        deque.EnqueueFront(_value);
                    else
                        deque.EnqueueAt((int)_keyOfValue, _value);
                }
            }
            else if(_undoOperation == UndoOperation.RemoveElement)
            {
                if(_attrType.Kind == AttributeKind.SetAttr)
                {
                    ChangingElementAttribute(procEnv_);
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    dict.Remove(_value);
                }
                else if(_attrType.Kind == AttributeKind.MapAttr)
                {
                    ChangingElementAttribute(procEnv_);
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    dict.Remove(_keyOfValue);
                }
                else if(_attrType.Kind == AttributeKind.ArrayAttr)
                {
                    ChangingElementAttribute(procEnv_);
                    IList array = (IList)_elem.GetAttribute(_attrType.Name);
                    if(_keyOfValue == null)
                        array.RemoveAt(array.Count - 1);
                    else
                        array.RemoveAt((int)_keyOfValue);
                }
                else //if(_attrType.Kind == AttributeKind.DequeAttr)
                {
                    ChangingElementAttribute(procEnv_);
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
                    ChangingElementAttribute(procEnv_);
                    IList array = (IList)_elem.GetAttribute(_attrType.Name);
                    array[(int)_keyOfValue] = _value;
                }
                else if(_attrType.Kind == AttributeKind.DequeAttr)
                {
                    ChangingElementAttribute(procEnv_);
                    IDeque deque = (IDeque)_elem.GetAttribute(_attrType.Name);
                    deque[(int)_keyOfValue] = _value;
                }
                else //if(_attrType.Kind == AttributeKind.MapAttr)
                {
                    ChangingElementAttribute(procEnv_);
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    dict[_keyOfValue] = _value;
                }
            }
            else if(_undoOperation == UndoOperation.Assign)
            {
                ChangingElementAttribute(procEnv_);
                _elem.SetAttribute(attrName, _value);
            }
            // otherwise UndoOperation.None
        }

        private void ChangingElementAttribute(LGSPGraphProcessingEnvironment procEnv)
        {
            AttributeChangeType changeType;
            switch(_undoOperation)
            {
            case UndoOperation.Assign: changeType = AttributeChangeType.Assign; break;
            case UndoOperation.PutElement: changeType = AttributeChangeType.PutElement; break;
            case UndoOperation.RemoveElement: changeType = AttributeChangeType.RemoveElement; break;
            case UndoOperation.AssignElement: changeType = AttributeChangeType.AssignElement; break;
            default: throw new Exception("Internal error during transaction handling");
            }

            if(_elem is LGSPNode)
                procEnv.graph.ChangingNodeAttribute((LGSPNode)_elem, _attrType, changeType, _value, _keyOfValue);
            else if(_elem is LGSPEdge)
                procEnv.graph.ChangingEdgeAttribute((LGSPEdge)_elem, _attrType, changeType, _value, _keyOfValue);
            else
                procEnv.graph.ChangingObjectAttribute((LGSPObject)_elem, _attrType, changeType, _value, _keyOfValue);
        }

        public override string ToString()
        {
            String attrName = _attrType.Name;
            if(_undoOperation == UndoOperation.PutElement)
            {
                if(_attrType.Kind == AttributeKind.SetAttr)
                    return NameDotAttribute() + ".add(" + EmitHelper.ToStringAutomatic(_value, _graph, false, null, null) + ")";
                else if(_attrType.Kind == AttributeKind.MapAttr)
                    return NameDotAttribute() + ".add(" + EmitHelper.ToStringAutomatic(_keyOfValue, _graph, false, null, null) + ", " + EmitHelper.ToStringAutomatic(_value, _graph, false, null, null) + ")";
                else if(_attrType.Kind == AttributeKind.ArrayAttr)
                {
                    if(_keyOfValue == null)
                        return NameDotAttribute() + ".add(" + EmitHelper.ToStringAutomatic(_value, _graph, false, null, null) + ")";
                    else
                        return NameDotAttribute() + ".add(" + EmitHelper.ToStringAutomatic(_keyOfValue, _graph, false, null, null) + ", " + EmitHelper.ToStringAutomatic(_value, _graph, false, null, null) + ")";
                }
                else //if(_attrType.Kind == AttributeKind.DequeAttr)
                {
                    if(_keyOfValue == null)
                        return NameDotAttribute() + ".add(" + EmitHelper.ToStringAutomatic(_value, _graph, false, null, null) + ")";
                    else
                        return NameDotAttribute() + ".add(" + EmitHelper.ToStringAutomatic(_keyOfValue, _graph, false, null, null) + ", " + EmitHelper.ToStringAutomatic(_value, _graph, false, null, null) + ")";
                }
            }
            else if(_undoOperation == UndoOperation.RemoveElement)
            {
                if(_attrType.Kind == AttributeKind.SetAttr)
                    return NameDotAttribute() + ".rem(" + EmitHelper.ToStringAutomatic(_value, _graph, false, null, null) + ")";
                else if(_attrType.Kind == AttributeKind.MapAttr)
                    return NameDotAttribute() + ".rem(" + EmitHelper.ToStringAutomatic(_keyOfValue, _graph, false, null, null) + ")";
                else if(_attrType.Kind == AttributeKind.ArrayAttr)
                {
                    if(_keyOfValue == null)
                        return NameDotAttribute() + ".rem()";
                    else
                        return NameDotAttribute() + ".rem(" + EmitHelper.ToStringAutomatic(_keyOfValue, _graph, false, null, null) + ")";
                }
                else //if(_attrType.Kind == AttributeKind.DequeAttr)
                {
                    if(_keyOfValue == null)
                        return NameDotAttribute() + ".rem()";
                    else
                        return NameDotAttribute() + ".rem(" + EmitHelper.ToStringAutomatic(_keyOfValue, _graph, false, null, null) + ")";
                }
            }
            else if(_undoOperation == UndoOperation.AssignElement)
            {
                if(_attrType.Kind == AttributeKind.ArrayAttr)
                    return NameDotAttribute() + "[" + EmitHelper.ToStringAutomatic(_keyOfValue, _graph, false, null, null) + "] = " + EmitHelper.ToStringAutomatic(_value, _graph, false, null, null);
                else //if(_attrType.Kind == AttributeKind.DequeAttr)
                    return NameDotAttribute() + "[" + EmitHelper.ToStringAutomatic(_keyOfValue, _graph, false, null, null) + "] = " + EmitHelper.ToStringAutomatic(_value, _graph, false, null, null);
            }
            else if(_undoOperation == UndoOperation.Assign)
                return NameDotAttribute() + " = " + EmitHelper.ToStringAutomatic(_value, _graph, false, null, null);
            return "nop (idempotent action)";
        }

        private String NameDotAttribute()
        {
            if(_elem is INode)
                return _name + "." + _attrType.Name;
            else
                return "-" + _name + "." + _attrType.Name + "->";
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoElemRetyped : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public readonly IGraphElement _oldElem;
        public readonly IGraphElement _newElem;
        public readonly String _name; // for ToString only

        public LGSPUndoElemRetyped(IGraphElement oldElem, IGraphElement newElem, LGSPGraphProcessingEnvironment procEnv)
        {
            _oldElem = oldElem;
            _newElem = newElem;
            if(procEnv.graph is LGSPNamedGraph)
                _name = ((LGSPNamedGraph)procEnv.graph).GetElementName(newElem);
            else
                _name = "?";
        }

        public void DoUndo(IGraphProcessingEnvironment procEnv)
        {
            String name;
            if(procEnv.Graph is LGSPNamedGraph && ((LGSPNamedGraph)procEnv.Graph).ElemToName.TryGetValue(_newElem, out name))
            {
                LGSPNamedGraph lgspGraph = (LGSPNamedGraph)procEnv.Graph;
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
                LGSPGraph lgspGraph = (LGSPGraph)procEnv.Graph;
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

        public override string ToString()
        {
            if(_oldElem is INode)
                return "retype(" + _name + ":" + _oldElem.Type.Name + ")";
            else
                return "retype(-" + _name + ":" + _oldElem.Type.Name + "->)";
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoElemRedirecting : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public readonly LGSPEdge _edge;
        public readonly LGSPNode _source;
        public readonly LGSPNode _target;
        public readonly String _name; // for ToString only
        public readonly IGraph _graph; // for ToString only

        public LGSPUndoElemRedirecting(LGSPEdge edge, LGSPNode source, LGSPNode target, LGSPGraphProcessingEnvironment procEnv)
        {
            _edge = edge;
            _source = source;
            _target = target;
            if(procEnv.graph is LGSPNamedGraph)
                _name = ((LGSPNamedGraph)procEnv.graph).GetElementName(_edge);
            else
                _name = "?";
            _graph = procEnv.graph;
        }

        public void DoUndo(IGraphProcessingEnvironment procEnv)
        {
            _edge.lgspSource = _source;
            _edge.lgspTarget = _target;
        }

        public override string ToString()
        {
            return "redirect(-" + _name + ":" + _edge.Type.Name + "->" + SourceTarget() + ")";
        }

        private string SourceTarget()
        {
            if(!(_graph is INamedGraph))
                return ", ?, ?";
            else
                return ", " + ((INamedGraph)_graph).GetElementName(_source) + ", " + ((INamedGraph)_graph).GetElementName(_target);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoVisitedAlloc : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public readonly int _visitorID;

        public LGSPUndoVisitedAlloc(int visitorID)
        {
            _visitorID = visitorID;
        }

        public void DoUndo(IGraphProcessingEnvironment procEnv)
        {
            procEnv.Graph.FreeVisitedFlagNonReset(_visitorID);
        }

        public override string ToString()
        {
            return "vfree(" + _visitorID + ")";
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoVisitedFree : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public readonly int _visitorID;
        
        public LGSPUndoVisitedFree(int visitorID)
        {
            _visitorID = visitorID;
        }

        public void DoUndo(IGraphProcessingEnvironment procEnv)
        {
            ((LGSPGraphProcessingEnvironment)procEnv).graph.ReallocateVisitedFlag(_visitorID);
        }

        public override string ToString()
        {
            return "valloc(" + _visitorID + ")";
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoSettingVisited : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public readonly IGraphElement _elem;
        public readonly int _visitorID;
        public readonly bool _oldValue;
        public readonly String _name; // for ToString only

        public LGSPUndoSettingVisited(IGraphElement elem, int visitorID, bool oldValue, LGSPGraphProcessingEnvironment procEnv)
        {
            _elem = elem;
            _visitorID = visitorID;
            _oldValue = oldValue;
            if(procEnv.graph is LGSPNamedGraph)
                _name = ((LGSPNamedGraph)procEnv.graph).GetElementName(_elem);
            else
                _name = "?";
        }

        public void DoUndo(IGraphProcessingEnvironment procEnv)
        {
            procEnv.Graph.SetVisited(_elem, _visitorID, _oldValue);
        }

        public override string ToString()
        {
            if(_elem is INode)
                return _name + ":" + _elem.Type.Name + ".visited[" + _visitorID + "] = " + _oldValue;
            else
                return "-" + _name + ":" + _elem.Type.Name + "->.visited[" + _visitorID + "] = " + _oldValue;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoGraphChange : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public readonly IGraph _oldGraph;

        public LGSPUndoGraphChange(IGraph oldGraph)
        {
            _oldGraph = oldGraph;
        }

        public void DoUndo(IGraphProcessingEnvironment procEnv)
        {
            procEnv.ReturnFromSubgraph();
            procEnv.SwitchToSubgraph(_oldGraph);
        }

        public override string ToString()
        {
            return "in " + _oldGraph.Name;
        }
    }
}
