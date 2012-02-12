/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

//#define LOG_TRANSACTION_HANDLING
//#define CHECK_RINGLISTS

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
                    IDictionary dict = DictionaryListHelper.GetDictionaryTypes(
                        _elem.GetAttribute(_attrType.Name), out keyType, out valueType);
                    IDictionary clonedDict = DictionaryListHelper.NewDictionary(keyType, valueType, dict);
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
                    IList array = DictionaryListHelper.GetListType(
                        _elem.GetAttribute(_attrType.Name), out valueType);
                    IList clonedArray = DictionaryListHelper.NewList(valueType, array);
                    _undoOperation = UndoOperation.Assign;
                    _value = clonedArray;
                }
            }
            else if (_attrType.Kind == AttributeKind.MapAttr)
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
                    IDictionary dict = DictionaryListHelper.GetDictionaryTypes(
                        _elem.GetAttribute(_attrType.Name), out keyType, out valueType);
                    IDictionary clonedDict = DictionaryListHelper.NewDictionary(keyType, valueType, dict);
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
                else //if (_attrType.Kind == AttributeKind.ArrayAttr)
                {
                    ChangingElementAttribute(procEnv);
                    IList array = (IList)_elem.GetAttribute(_attrType.Name);
                    if(_keyOfValue == null)
                        array.Add(_value);
                    else
                        array.Insert((int)_keyOfValue, _value);
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
                else //if(_attrType.Kind == AttributeKind.ArrayAttr)
                {
                    ChangingElementAttribute(procEnv);
                    IList array = (IList)_elem.GetAttribute(_attrType.Name);
                    if(_keyOfValue == null)
                        array.RemoveAt(array.Count - 1);
                    else
                        array.RemoveAt((int)_keyOfValue);
                }
            }
            else if(_undoOperation == UndoOperation.AssignElement)
            {
                ChangingElementAttribute(procEnv);
                IList array = (IList)_elem.GetAttribute(_attrType.Name);
                array[(int)_keyOfValue] = _value;
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

    /// <summary>
    /// A class for managing graph transactions.
    /// </summary>
    public class LGSPTransactionManager : ITransactionManager
    {
        private LinkedList<IUndoItem> undoItems = new LinkedList<IUndoItem>();
        private bool recording = false;
        private bool paused = false; // only of interest if recording==true
        private bool reuseOptimizationBackup = false; // old value from graph, to be restored after outermost transaction completed
        private bool undoing = false;
        private LGSPGraphProcessingEnvironment procEnv;

#if LOG_TRANSACTION_HANDLING
        private StreamWriter writer;
        private int transactionLevel = 0;
#endif

        public LGSPTransactionManager(LGSPGraphProcessingEnvironment procEnv)
        {
            this.procEnv = procEnv;

#if LOG_TRANSACTION_HANDLING
            writer = new StreamWriter(procEnv.graph.Name + "_transaction_log.txt");
#endif
        }

        private void SubscribeEvents()
        {
            procEnv.graph.OnNodeAdded += ElementAdded;
            procEnv.graph.OnEdgeAdded += ElementAdded;
            procEnv.graph.OnRemovingNode += RemovingElement;
            procEnv.graph.OnRemovingEdge += RemovingElement;
            procEnv.graph.OnChangingNodeAttribute += ChangingElementAttribute;
            procEnv.graph.OnChangingEdgeAttribute += ChangingElementAttribute;
            procEnv.graph.OnRetypingNode += RetypingElement;
            procEnv.graph.OnRetypingEdge += RetypingElement;

            recording = true;
            paused = false;
            reuseOptimizationBackup = procEnv.graph.ReuseOptimization;
            procEnv.graph.ReuseOptimization = false; // reusing destroys the graph on rollback, so disable it when handling transactions
        }

        private void UnsubscribeEvents()
        {
            procEnv.graph.OnNodeAdded -= ElementAdded;
            procEnv.graph.OnEdgeAdded -= ElementAdded;
            procEnv.graph.OnRemovingNode -= RemovingElement;
            procEnv.graph.OnRemovingEdge -= RemovingElement;
            procEnv.graph.OnChangingNodeAttribute -= ChangingElementAttribute;
            procEnv.graph.OnChangingEdgeAttribute -= ChangingElementAttribute;
            procEnv.graph.OnRetypingNode -= RetypingElement;
            procEnv.graph.OnRetypingEdge -= RetypingElement;

            recording = false;
            procEnv.graph.ReuseOptimization = reuseOptimizationBackup;
        }

        public int StartTransaction()
        {
            // TODO: allow transactions within pauses, nesting of pauses with transactions
            // this requires a stack of transactions; not difficult, but would eat a bit of performance,
            // so only to be done if there is demand by users (for the majority of tasks it should not be needed)
            if(paused)
                throw new Exception("Transaction handling is currently paused, can't start a transaction!");
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine(new String(' ', transactionLevel) + "StartTransaction");
            writer.Flush();
            ++transactionLevel;
#endif
            if(procEnv.Recorder != null)
                procEnv.Recorder.TransactionStart(undoItems.Count);

            if(!recording)
                SubscribeEvents();

            int count = undoItems.Count;
            undoItems.AddLast(new LGSPUndoTransactionStarted());
            return count;
        }

        public void Pause()
        {
            if(paused)
                throw new Exception("Transaction handling is already paused, can't pause again!");
            if(!recording)
                throw new Exception("No transaction underway to pause!");
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine(new String(' ', transactionLevel) + "PauseTransaction");
            writer.Flush();
#endif
            paused = true;
        }

        public void Resume()
        {
            if(!paused)
                throw new Exception("Transaction handling is not paused, can't resume!");
            if(!recording)
                throw new Exception("No transaction underway to resume!");
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine(new String(' ', transactionLevel) + "ResumeTransaction");
            writer.Flush();
#endif
            paused = false;
        }


        public void Commit(int transactionID)
        {
            if(paused)
                throw new Exception("Transaction handling is currently paused, can't commit!");
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine(new String(' ', transactionLevel) + "Commit to " + transactionID);
            writer.Flush();
            --transactionLevel;
#endif
#if CHECK_RINGLISTS
            procEnv.graph.CheckTypeRinglistsBroken();
#endif
            if(procEnv.Recorder != null)
                procEnv.Recorder.TransactionCommit(transactionID);

            // removes rollback information only if the transaction is outermost
            // otherwise we might need do undo it because a transaction enclosing this transaction failed
            if(transactionID == 0)
            {
                undoItems.Clear();
                UnsubscribeEvents();
            }
        }

        public void Rollback(int transactionID)
        {
            if(paused)
                throw new Exception("Transaction handling is currently paused, can't roll back!");
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine(new String(' ', transactionLevel) + "Rollback to " + transactionID);
            writer.Flush();
#endif      
            if(procEnv.Recorder != null)
                procEnv.Recorder.TransactionRollback(transactionID, true);

            undoing = true;
            while(undoItems.Count > transactionID)
            {
#if LOG_TRANSACTION_HANDLING
                writer.Write(new String(' ', transactionLevel) + "rolling back " + undoItems.Count + " - ");
                if(undoItems.Last.Value is LGSPUndoTransactionStarted) {
                    writer.WriteLine("TransactionStarted");
                } else if(undoItems.Last.Value is LGSPUndoElemAdded) {
                    LGSPUndoElemAdded item = (LGSPUndoElemAdded)undoItems.Last.Value;
                    if(item._elem is INode) {
                        INode node = (INode)item._elem;
                        writer.WriteLine("ElementAdded: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(node) + ":" + node.Type.Name);
                    } else {
                        IEdge edge = (IEdge)item._elem;
                        writer.WriteLine("ElementAdded: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Source) + " -" + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge) + ":" + edge.Type.Name + " ->" + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Target));
                    }
                } else if(undoItems.Last.Value is LGSPUndoElemRemoved) {
                    LGSPUndoElemRemoved item = (LGSPUndoElemRemoved)undoItems.Last.Value;
                    if(item._elem is INode) {
                        INode node = (INode)item._elem;
                        writer.WriteLine("RemovingElement: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(node) + ":" + node.Type.Name);
                    } else {
                        IEdge edge = (IEdge)item._elem;
                        writer.WriteLine("RemovingElement: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Source) + " -" + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge) + ":" + edge.Type.Name + "-> " + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Target));
                    }
                } else if(undoItems.Last.Value is LGSPUndoAttributeChanged) {
                    LGSPUndoAttributeChanged item = (LGSPUndoAttributeChanged)undoItems.Last.Value;
                    writer.WriteLine("ChangingElementAttribute: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(item._elem) + ":" + item._elem.Type.Name + "." + item._attrType.Name);
                } else if(undoItems.Last.Value is LGSPUndoElemRetyped) {
                    LGSPUndoElemRetyped item = (LGSPUndoElemRetyped)undoItems.Last.Value;
                    writer.WriteLine("RetypingElement: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(item._newElem) + ":" + item._newElem.Type.Name + "<" + ((LGSPNamedGraph)procEnv.graph).GetElementName(item._oldElem) + ":" + item._oldElem.Type.Name + ">");
                }
#endif
                undoItems.Last.Value.DoUndo(procEnv);
                undoItems.RemoveLast();
            }
            undoing = false;

            if(transactionID == 0)
                UnsubscribeEvents();

            if(procEnv.Recorder != null)
                procEnv.Recorder.TransactionRollback(transactionID, false);

#if LOG_TRANSACTION_HANDLING
            --transactionLevel;
            writer.Flush();
#endif
#if CHECK_RINGLISTS
            procEnv.graph.CheckTypeRinglistsBroken();
#endif
        }

        public void ElementAdded(IGraphElement elem)
        {
            if(recording && !paused && !undoing) undoItems.AddLast(new LGSPUndoElemAdded(elem));
#if LOG_TRANSACTION_HANDLING
            if(elem is INode) {
                INode node = (INode)elem;
                writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "ElementAdded: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(node) + ":" + node.Type.Name);
            } else {
                IEdge edge = (IEdge)elem;
                writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "ElementAdded: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Source) + " -" + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge) + ":" + edge.Type.Name + "-> " + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Target));
            }
#endif
        }

        public void RemovingElement(IGraphElement elem)
        {
#if LOG_TRANSACTION_HANDLING
            if(elem is INode) {
                INode node = (INode)elem;
                writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "RemovingElement: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(node) + ":" + node.Type.Name);
            } else {
                IEdge edge = (IEdge)elem;
                writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "RemovingElement: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Source) + " -" + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge) + ":" + edge.Type.Name + "-> " + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Target));
            }
#endif
            if(recording && !paused && !undoing) undoItems.AddLast(new LGSPUndoElemRemoved(elem, procEnv));
        }

        public void ChangingElementAttribute(IGraphElement elem, AttributeType attrType,
                AttributeChangeType changeType, Object newValue, Object keyValue)
        {
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "ChangingElementAttribute: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(elem) + ":" + elem.Type.Name + "." + attrType.Name);
#endif
            if(recording && !paused && !undoing) undoItems.AddLast(new LGSPUndoAttributeChanged(elem, attrType, changeType, newValue, keyValue));
        }

        public void RetypingElement(IGraphElement oldElem, IGraphElement newElem)
        {
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "RetypingElement: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(newElem) + ":" + newElem.Type.Name + "<" + ((LGSPNamedGraph)procEnv.graph).GetElementName(oldElem) + ":" + oldElem.Type.Name + ">");
#endif
            if(recording && !paused && !undoing) undoItems.AddLast(new LGSPUndoElemRetyped(oldElem, newElem));
        }

        public bool TransactionActive { get { return recording; } }
    }
}
