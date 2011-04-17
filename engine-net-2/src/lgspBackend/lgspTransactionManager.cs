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
        void DoUndo(IGraph graph);

        IUndoItem Clone(Dictionary<IGraphElement, IGraphElement> oldToNewMap);
    }


    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoTransactionStarted : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public LGSPUndoTransactionStarted()
        {
        }

        public void DoUndo(IGraph graph)
        {
            // nothing to do, this is only needed to distinguish the outermost transaction
            // from the nested transactions to know when to remove the rollback information
            // if nothing happened from opening the first transaction to opening the transaction of interest,
            // they would be indistinguishable as the number of undo items did not change
        }

        public IUndoItem Clone(Dictionary<IGraphElement, IGraphElement> oldToNewMap)
        {
            return new LGSPUndoTransactionStarted();
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

        public void DoUndo(IGraph graph)
        {
            if(_elem is INode) graph.Remove((INode) _elem);
            else graph.Remove((IEdge) _elem);
        }

        public IUndoItem Clone(Dictionary<IGraphElement, IGraphElement> oldToNewMap)
        {
            return new LGSPUndoElemAdded(oldToNewMap[_elem]);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class LGSPUndoElemRemoved : IUndoItem
    ////////////////////////////////////////////////////////////////////////////////
    {
        public IGraphElement _elem;
        public String _name;
        public LinkedList<Variable> _vars;

        public LGSPUndoElemRemoved(IGraphElement elem, IGraph graph)
        {
            _elem = elem;
            _vars = graph.GetElementVariables(_elem);
            if(graph is NamedGraph) _name = ((NamedGraph) graph).GetElementName(_elem);
            else _name = null;
        }

        private LGSPUndoElemRemoved(IGraphElement elem, String name, LinkedList<Variable> vars)
        {
            _elem = elem;
            _name = name;
            _vars = vars;
        }

        public void DoUndo(IGraph graph)
        {
            if(_elem is LGSPNode) ((LGSPGraph) graph).AddNode((LGSPNode) _elem);
            else ((LGSPGraph) graph).AddEdge((LGSPEdge) _elem);

            if(graph is NamedGraph)
                ((NamedGraph) graph).SetElementName(_elem, _name);

            if(_vars != null)
            {
                foreach(Variable var in _vars)
                    graph.SetVariableValue(var.Name, _elem);
            }
        }

        public IUndoItem Clone(Dictionary<IGraphElement, IGraphElement> oldToNewMap)
        {
            LinkedList<Variable> newVars = new LinkedList<Variable>();
            foreach(Variable var in _vars)
            {
                object newVal;
                IGraphElement elem = var.Value as IGraphElement;
                if(elem != null) newVal = oldToNewMap[elem];
                else newVal = var.Value;
                newVars.AddLast(new Variable(var.Name, newVal));
            }
            return new LGSPUndoElemRemoved(oldToNewMap[_elem], _name, newVars);
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

        public void DoUndo(IGraph graph)
        {
            String attrName = _attrType.Name;
            if (_undoOperation == UndoOperation.PutElement)
            {
                if (_attrType.Kind == AttributeKind.SetAttr)
                {
                    ChangingElementAttribute(graph);
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    dict.Add(_value, null);
                }
                else if(_attrType.Kind == AttributeKind.MapAttr)
                {
                    ChangingElementAttribute(graph);
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    dict.Add(_keyOfValue, _value);
                }
                else //if (_attrType.Kind == AttributeKind.ArrayAttr)
                {
                    ChangingElementAttribute(graph);
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
                    ChangingElementAttribute(graph);
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    dict.Remove(_value);
                }
                else if (_attrType.Kind == AttributeKind.MapAttr)
                {
                    ChangingElementAttribute(graph);
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    dict.Remove(_keyOfValue);
                }
                else //if(_attrType.Kind == AttributeKind.ArrayAttr)
                {
                    ChangingElementAttribute(graph);
                    IList array = (IList)_elem.GetAttribute(_attrType.Name);
                    if(_keyOfValue == null)
                        array.RemoveAt(array.Count - 1);
                    else
                        array.RemoveAt((int)_keyOfValue);
                }
            }
            else if(_undoOperation == UndoOperation.AssignElement)
            {
                ChangingElementAttribute(graph);
                IList array = (IList)_elem.GetAttribute(_attrType.Name);
                array[(int)_keyOfValue] = _value;
            }
            else if(_undoOperation == UndoOperation.Assign)
            {
                ChangingElementAttribute(graph);
                _elem.SetAttribute(attrName, _value);
            }
            // otherwise UndoOperation.None
        }

        private void ChangingElementAttribute(IGraph graph)
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
                graph.ChangingNodeAttribute(node, _attrType, changeType, _value, _keyOfValue);
            }
            else
            {
                LGSPEdge edge = (LGSPEdge)_elem;
                graph.ChangingEdgeAttribute(edge, _attrType, changeType, _value, _keyOfValue);
            }
        }

        public IUndoItem Clone(Dictionary<IGraphElement, IGraphElement> oldToNewMap)
        {
            throw new Exception("Not implemented yet!");
            //return new LGSPUndoAttributeChanged(oldToNewMap[_elem], _attrType, _oldValue, _newValue);
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

        public void DoUndo(IGraph graph)
        {
            LGSPGraph lgspGraph = (LGSPGraph) graph;

            LGSPNode newNode = _newElem as LGSPNode;
            if(newNode != null)
            {
                LGSPNode oldNode = (LGSPNode) _oldElem;
                if(lgspGraph.NamedGraph != null)
                    lgspGraph.NamedGraph.Retyping(newNode, oldNode);
                lgspGraph.RetypingNode(newNode, oldNode);
                lgspGraph.ReplaceNode(newNode, oldNode);
                if(lgspGraph.NamedGraph != null)
                    lgspGraph.NamedGraph.Retyped(newNode, oldNode);
            }
            else
            {
                LGSPEdge newEdge = (LGSPEdge) _newElem;
                LGSPEdge oldEdge = (LGSPEdge) _oldElem;
                if(lgspGraph.NamedGraph != null)
                    lgspGraph.NamedGraph.Retyping(newEdge, oldEdge);
                lgspGraph.RetypingEdge(newEdge, oldEdge);
                lgspGraph.ReplaceEdge(newEdge, oldEdge);
                if(lgspGraph.NamedGraph != null)
                    lgspGraph.NamedGraph.Retyped(newEdge, oldEdge);
            }
        }

        public IUndoItem Clone(Dictionary<IGraphElement, IGraphElement> oldToNewMap)
        {
            throw new Exception("Not implemented yet!");
//            return new LGSPUndoElemTypeChanged(oldToNewMap[_elem], _oldType, _oldAttrs == null ? null : (IAttributes) _oldAttrs.Clone());
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
        private bool reuseOptimizationBackup = false;
        private bool undoing = false;
        private LGSPGraph graph;

#if LOG_TRANSACTION_HANDLING
        private StreamWriter writer;
#endif

        public LGSPTransactionManager(LGSPGraph lgspgraph)
        {
            graph = lgspgraph;

#if LOG_TRANSACTION_HANDLING
            writer = new StreamWriter(graph.Name + "_transaction_log.txt");
#endif
        }

        private void SubscribeEvents()
        {
            graph.OnNodeAdded += new NodeAddedHandler(ElementAdded);
            graph.OnEdgeAdded += new EdgeAddedHandler(ElementAdded);
            graph.OnRemovingNode += new RemovingNodeHandler(RemovingElement);
            graph.OnRemovingEdge += new RemovingEdgeHandler(RemovingElement);
            graph.OnChangingNodeAttribute += new ChangingNodeAttributeHandler(ChangingElementAttribute);
            graph.OnChangingEdgeAttribute += new ChangingEdgeAttributeHandler(ChangingElementAttribute);
            graph.OnRetypingNode += new RetypingNodeHandler(RetypingElement);
            graph.OnRetypingEdge += new RetypingEdgeHandler(RetypingElement);

            recording = true;
            reuseOptimizationBackup = graph.ReuseOptimization;
            graph.ReuseOptimization = false; // reusing destroys the graph on rollback, so disable it when handling transactions
        }

        private void UnsubscribeEvents()
        {
            graph.OnNodeAdded -= new NodeAddedHandler(ElementAdded);
            graph.OnEdgeAdded -= new EdgeAddedHandler(ElementAdded);
            graph.OnRemovingNode -= new RemovingNodeHandler(RemovingElement);
            graph.OnRemovingEdge -= new RemovingEdgeHandler(RemovingElement);
            graph.OnChangingNodeAttribute -= new ChangingNodeAttributeHandler(ChangingElementAttribute);
            graph.OnChangingEdgeAttribute -= new ChangingEdgeAttributeHandler(ChangingElementAttribute);
            graph.OnRetypingNode -= new RetypingNodeHandler(RetypingElement);
            graph.OnRetypingEdge -= new RetypingEdgeHandler(RetypingElement);

            recording = false;
            graph.ReuseOptimization = reuseOptimizationBackup;
        }

        /// <summary>
        /// Starts a transaction
        /// </summary>
        /// <returns>A transaction ID to be used with Commit or Rollback</returns>
        public int StartTransaction()
        {
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine("StartTransaction");
#endif
            if(graph.Recorder != null)
                graph.Recorder.TransactionStart(undoItems.Count);

            if(!recording)
                SubscribeEvents();

            int count = undoItems.Count;
            undoItems.AddLast(new LGSPUndoTransactionStarted());
            return count;
        }

        /// <summary>
        /// Commits the changes during a transaction
        /// (removes rollback information only if the transaction is outermost)
        /// </summary>
        /// <param name="transactionID">Transaction ID returned by a StartTransaction call</param>
        public void Commit(int transactionID)
        {
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine("Commit to " + transactionID);
            writer.Flush();
#endif
#if CHECK_RINGLISTS
            graph.CheckTypeRinglistsBroken();
#endif
            if(graph.Recorder != null)
                graph.Recorder.TransactionCommit(transactionID);

            if(transactionID == 0)
            {
                undoItems.Clear();
                UnsubscribeEvents();
            }
        }

        /// <summary>
        /// Undoes all changes during a transaction
        /// </summary>
        /// <param name="transactionID">The ID of the transaction to be rollbacked</param>
        public void Rollback(int transactionID)
        {
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine("Rollback to " + transactionID);
#endif
            if(graph.Recorder != null)
                graph.Recorder.TransactionRollback(transactionID, true);

            undoing = true;
            while(undoItems.Count > transactionID)
            {
#if LOG_TRANSACTION_HANDLING
                writer.Write("rolling back " + undoItems.Count + " - ");
                if(undoItems.Last.Value is LGSPUndoTransactionStarted) {
                    writer.WriteLine("TransactionStarted");
                } else if(undoItems.Last.Value is LGSPUndoElemAdded) {
                    LGSPUndoElemAdded item = (LGSPUndoElemAdded)undoItems.Last.Value;
                    if(item._elem is INode) {
                        INode node = (INode)item._elem;
                        writer.WriteLine("ElementAdded: " + graph.GetElementName(node) + ":" + node.Type.Name);
                    } else {
                        IEdge edge = (IEdge)item._elem;
                        writer.WriteLine("ElementAdded: " + graph.GetElementName(edge.Source) + " -" + graph.GetElementName(edge) + ":" + edge.Type.Name + " ->" + graph.GetElementName(edge.Target));
                    }
                } else if(undoItems.Last.Value is LGSPUndoElemRemoved) {
                    LGSPUndoElemRemoved item = (LGSPUndoElemRemoved)undoItems.Last.Value;
                    if(item._elem is INode) {
                        INode node = (INode)item._elem;
                        writer.WriteLine("RemovingElement: " + graph.GetElementName(node) + ":" + node.Type.Name);
                    } else {
                        IEdge edge = (IEdge)item._elem;
                        writer.WriteLine("RemovingElement: " + graph.GetElementName(edge.Source) + " -"+ graph.GetElementName(edge) + ":" + edge.Type.Name + "-> " + graph.GetElementName(edge.Target));
                    }
                } else if(undoItems.Last.Value is LGSPUndoAttributeChanged) {
                    LGSPUndoAttributeChanged item = (LGSPUndoAttributeChanged)undoItems.Last.Value;
                    writer.WriteLine("ChangingElementAttribute: " + graph.GetElementName(item._elem) + ":" + item._elem.Type.Name + "." + item._attrType.Name);
                } else if(undoItems.Last.Value is LGSPUndoElemRetyped) {
                    LGSPUndoElemRetyped item = (LGSPUndoElemRetyped)undoItems.Last.Value;
                    writer.WriteLine("RetypingElement: " + graph.GetElementName(item._newElem) + ":" + item._newElem.Type.Name + "<" + graph.GetElementName(item._oldElem)+ ":" + item._oldElem.Type.Name + ">");
                }
#endif
                undoItems.Last.Value.DoUndo(graph);
                undoItems.RemoveLast();
            }
            undoing = false;
      
            if(transactionID == 0)
                UnsubscribeEvents();

            if(graph.Recorder != null)
                graph.Recorder.TransactionRollback(transactionID, false);

#if LOG_TRANSACTION_HANDLING
            writer.Flush();
#endif
#if CHECK_RINGLISTS
            graph.CheckTypeRinglistsBroken();
#endif
        }

        public void ElementAdded(IGraphElement elem)
        {
            if(recording && !undoing) undoItems.AddLast(new LGSPUndoElemAdded(elem));
#if LOG_TRANSACTION_HANDLING
            if(elem is INode) {
                INode node = (INode)elem;
                writer.WriteLine("ElementAdded: " + graph.GetElementName(node) + ":" + node.Type.Name);
            } else {
                IEdge edge = (IEdge)elem;
                writer.WriteLine("ElementAdded: " + graph.GetElementName(edge.Source) + " -" + graph.GetElementName(edge) + ":" + edge.Type.Name + "-> " + graph.GetElementName(edge.Target));
            }
#endif
        }

        public void RemovingElement(IGraphElement elem)
        {
#if LOG_TRANSACTION_HANDLING
            if(elem is INode) {
                INode node = (INode)elem;
                writer.WriteLine("RemovingElement: " + graph.GetElementName(node) + ":" + node.Type.Name);
            } else {
                IEdge edge = (IEdge)elem;
                writer.WriteLine("RemovingElement: " + graph.GetElementName(edge.Source) + " -" + graph.GetElementName(edge) + ":" + edge.Type.Name + "-> " + graph.GetElementName(edge.Target));
            }
#endif
            if(recording && !undoing) undoItems.AddLast(new LGSPUndoElemRemoved(elem, graph));
        }

        public void ChangingElementAttribute(IGraphElement elem, AttributeType attrType,
                AttributeChangeType changeType, Object newValue, Object keyValue)
        {
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine("ChangingElementAttribute: " + graph.GetElementName(elem) + ":" + elem.Type.Name + "." + attrType.Name);
#endif
            if(recording && !undoing) undoItems.AddLast(new LGSPUndoAttributeChanged(elem, attrType, changeType, newValue, keyValue));
        }

        public void RetypingElement(IGraphElement oldElem, IGraphElement newElem)
        {
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine("RetypingElement: " + graph.GetElementName(newElem) + ":" + newElem.Type.Name+ "<" + graph.GetElementName(oldElem) + ":" + oldElem.Type.Name + ">");
#endif
            if(recording && !undoing) undoItems.AddLast(new LGSPUndoElemRetyped(oldElem, newElem));
        }

        public bool TransactionActive { get { return recording; } }

/*        public ITransactionManager Clone(Dictionary<IGraphElement, IGraphElement> oldToNewMap)
        {
            LGSPTransactionManager newTM = new LGSPTransactionManager();
            newTM.recording = recording;
            newTM.undoing = undoing;                              
            foreach(IUndoItem undoItem in undoItems)
            {
                IUndoItem newUndoItem = undoItem.Clone(oldToNewMap);
                newTM.undoItems.AddLast(newUndoItem);
            }
        }*/
    }
}
