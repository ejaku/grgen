/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

#define MONO_MULTIDIMARRAY_WORKAROUND       // not using multidimensional arrays is about 2% faster on .NET because of fewer bound checks
//#define OPCOST_WITH_GEO_MEAN
//#define LOG_TRANSACTION_HANDLING
//#define CHECK_RINGLISTS

using System;
using System.Diagnostics;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;
using System.Collections;

namespace de.unika.ipd.grGen.lgsp
{
    public interface IUndoItem
    {
        void DoUndo(IGraph graph);

        IUndoItem Clone(Dictionary<IGraphElement, IGraphElement> oldToNewMap);
    }
    
    public class LGSPUndoElemAdded : IUndoItem
    {
        public IGraphElement _elem;

        public LGSPUndoElemAdded(IGraphElement elem) { _elem = elem; }

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

    public class LGSPUndoElemRemoved : IUndoItem
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
        RemoveElement
    }

    public class LGSPUndoAttributeChanged : IUndoItem
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
                    IDictionary dict = DictionaryHelper.GetDictionaryTypes(
                        _elem.GetAttribute(_attrType.Name), out keyType, out valueType);
                    IDictionary clonedDict = DictionaryHelper.NewDictionary(keyType, valueType, dict);
                    _undoOperation = UndoOperation.Assign;
                    _value = clonedDict;
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
                else // Assign
                {
                    Type keyType, valueType;
                    IDictionary dict = DictionaryHelper.GetDictionaryTypes(
                        _elem.GetAttribute(_attrType.Name), out keyType, out valueType);
                    IDictionary clonedDict = DictionaryHelper.NewDictionary(keyType, valueType, dict);
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
                else // AttributeKind.MapAttr
                {
                    ChangingElementAttribute(graph);
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    dict.Add(_keyOfValue, _value);
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
                else // AttributeKind.MapAttr
                {
                    ChangingElementAttribute(graph);
                    IDictionary dict = (IDictionary)_elem.GetAttribute(_attrType.Name);
                    dict.Remove(_keyOfValue);
                }
            }
            else if (_undoOperation == UndoOperation.Assign)
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

    public class LGSPUndoElemRetyped : IUndoItem
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
                lgspGraph.RetypingNode(newNode, oldNode);
                lgspGraph.ReplaceNode(newNode, oldNode);
            }
            else
            {
                LGSPEdge newEdge = (LGSPEdge) _newElem;
                LGSPEdge oldEdge = (LGSPEdge) _oldElem;
                lgspGraph.RetypingEdge(newEdge, oldEdge);
                lgspGraph.ReplaceEdge(newEdge, oldEdge);
            }
        }

        public IUndoItem Clone(Dictionary<IGraphElement, IGraphElement> oldToNewMap)
        {
            throw new Exception("Not implemented yet!");
//            return new LGSPUndoElemTypeChanged(oldToNewMap[_elem], _oldType, _oldAttrs == null ? null : (IAttributes) _oldAttrs.Clone());
        }
    }

    /// <summary>
    /// A class for managing graph transactions.
    /// </summary>
    public class LGSPTransactionManager : ITransactionManager
    {
        private LinkedList<IUndoItem> undoItems = new LinkedList<IUndoItem>();
        private bool recording = false;
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
            if(!recording)
            {
                recording = true;
                SubscribeEvents();
            }
            return undoItems.Count;
        }

        /// <summary>
        /// Removes the rollback data and stops this transaction
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
            if(transactionID == 0)
            {
                undoItems.Clear();
                recording = false;
                UnsubscribeEvents();
            }
            else
            {
                while(undoItems.Count > transactionID) undoItems.RemoveLast();
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
            undoing = true;
            while(undoItems.Count > transactionID)
            {
#if LOG_TRANSACTION_HANDLING
                writer.Write("rolling back " + undoItems.Count + " - ");
                if(undoItems.Last.Value is LGSPUndoElemAdded) {
                    LGSPUndoElemAdded item = (LGSPUndoElemAdded)undoItems.Last.Value;
                    writer.WriteLine("ElementAdded: " + graph.GetElementName(item._elem) + ":" + item._elem.Type.Name);
                } else if(undoItems.Last.Value is LGSPUndoElemRemoved) {
                    LGSPUndoElemRemoved item = (LGSPUndoElemRemoved)undoItems.Last.Value;
                    writer.WriteLine("RemovingElement: " + graph.GetElementName(item._elem) + ":" + item._elem.Type.Name);
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
            {
                recording = false;
                UnsubscribeEvents();
            }
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
            writer.WriteLine("ElementAdded: " + graph.GetElementName(elem) + ":" + elem.Type.Name);
#endif
        }

        public void RemovingElement(IGraphElement elem)
        {
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine("RemovingElement: " + graph.GetElementName(elem) + ":" + elem.Type.Name);
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
    
    public enum LGSPDir { In, Out };

    /// <summary>
    /// An implementation of the IGraph interface.
    /// </summary>
    public class LGSPGraph : BaseGraph
    {
        private static int actionID = 0;
        private static int graphID = 0;

        private String name;
        private LGSPBackend backend = null;
        private IGraphModel model;
        private String modelAssemblyName;

        private LGSPTransactionManager transactionManager;

        /// <summary>
        /// Currently associated LGSPActions object.
        /// This is needed to the current matchers while executing an exec statement on the RHS of a rule.
        /// </summary>
        public LGSPActions curActions = null;

        /// <summary>
        /// A currently associated actions object.
        /// </summary>
        public override BaseActions Actions { get { return curActions; } set { curActions = (LGSPActions) value; } }

        private bool reuseOptimization = true;

        /// <summary>
        /// If true (the default case), elements deleted during a rewrite
        /// may be reused in the same rewrite.
        /// As a result new elements may not be discriminable anymore from
        /// already deleted elements using object equality, hash maps, etc.
        /// In cases where this is needed this optimization should be disabled.
        /// </summary>
        public override bool ReuseOptimization
        {
            get { return reuseOptimization; }
            set { reuseOptimization = value; }
        }

#if MONO_MULTIDIMARRAY_WORKAROUND
        public int dim0size, dim1size, dim2size;  // dim3size is always 2
        public float[] vstructs;
#else
        public float[, , ,] vstructs;
#endif

        /// <summary>
        /// The number of compatible nodes in the graph for each type at the time of the last analysis.
        /// It is null, if no analysis has been executed, yet.
        /// </summary>
        public int[] nodeCounts;

        /// <summary>
        /// The number of compatible edges in the graph for each type at the time of the last analysis.
        /// It is null, if no analysis has been executed, yet.
        /// </summary>
        public int[] edgeCounts;

#if OPCOST_WITH_GEO_MEAN
        public float[] nodeLookupCosts, edgeLookupCosts;
#endif

        /// <summary>
        /// The mean out degree (independent of edge types) of the nodes of a graph for each node type
        /// at the time of the last analysis.
        /// It is null, if no analysis has been executed, yet.
        /// </summary>
        public float[] meanOutDegree;

        /// <summary>
        /// The mean in degree (independent of edge types) of the nodes of a graph for each node type
        /// at the time of the last analysis.
        /// It is null, if no analysis has been executed, yet.
        /// </summary>
        public float[] meanInDegree;
       
        /// <summary>
        /// An array containing one head of a doubly-linked ring-list for each node type indexed by the type ID.
        /// </summary>
        public LGSPNode[] nodesByTypeHeads;

        /// <summary>
        /// The number of nodes for each node type indexed by the type ID.
        /// </summary>
        public int[] nodesByTypeCounts;

        /// <summary>
        /// An array containing one head of a doubly-linked ring-list for each edge type indexed by the type ID.
        /// </summary>
        public LGSPEdge[] edgesByTypeHeads;

        /// <summary>
        /// The number of edges for each edge type indexed by the type ID.
        /// </summary>
        public int[] edgesByTypeCounts;

        public List<Pair<Dictionary<LGSPNode, LGSPNode>, Dictionary<LGSPEdge, LGSPEdge>>> atNegLevelMatchedElements;
        public List<Pair<Dictionary<LGSPNode, LGSPNode>, Dictionary<LGSPEdge, LGSPEdge>>> atNegLevelMatchedElementsGlobal;

        protected static String GetNextGraphName() { return "lgspGraph_" + graphID++; }

        /// <summary>
        /// Constructs an LGSPGraph object with the given model and an automatically generated name.
        /// </summary>
        /// <param name="grmodel">The graph model.</param>
        public LGSPGraph(IGraphModel grmodel) : this(grmodel, GetNextGraphName())
        {
        }

        /// <summary>
        /// Constructs an LGSPGraph object with the given model and name.
        /// </summary>
        /// <param name="grmodel">The graph model.</param>
        /// <param name="grname">The name for the graph.</param>
        public LGSPGraph(IGraphModel grmodel, String grname)
            : this(grname)
        {
            InitializeGraph(grmodel);
        }

        /// <summary>
        /// Constructs an LGSPGraph object.
        /// Deprecated.
        /// </summary>
        /// <param name="lgspBackend">The responsible backend object.</param>
        /// <param name="grmodel">The graph model.</param>
        /// <param name="grname">The name for the graph.</param>
        /// <param name="modelassemblyname">The name of the model assembly.</param>
        public LGSPGraph(LGSPBackend lgspBackend, IGraphModel grmodel, String grname, String modelassemblyname)
            : this(grmodel, grname)
        {
            backend = lgspBackend;
            modelAssemblyName = modelassemblyname;
        }

        /// <summary>
        /// Constructs an LGSPGraph object without initializing it.
        /// </summary>
        /// <param name="grname">The name for the graph.</param>
        protected LGSPGraph(String grname)
        {
            name = grname;

            atNegLevelMatchedElements = new List<Pair<Dictionary<LGSPNode, LGSPNode>, Dictionary<LGSPEdge, LGSPEdge>>>();
            atNegLevelMatchedElementsGlobal = new List<Pair<Dictionary<LGSPNode, LGSPNode>, Dictionary<LGSPEdge, LGSPEdge>>>();
        }

        /// <summary>
        /// Copy constructor.
        /// Open transaction data lost.
        /// </summary>
        /// <param name="dataSource">The LGSPGraph object to get the data from</param>
        /// <param name="newName">Name of the copied graph.</param>
        protected LGSPGraph(LGSPGraph dataSource, String newName)
        {
            model = dataSource.model;
            name = newName;

            InitializeGraph();

            if(dataSource.backend != null)
            {
                backend = dataSource.backend;
                modelAssemblyName = dataSource.modelAssemblyName;
            }

            Dictionary<IGraphElement, IGraphElement> oldToNewMap = new Dictionary<IGraphElement, IGraphElement>();

            for(int i = 0; i < dataSource.nodesByTypeHeads.Length; i++)
            {
                for(LGSPNode head = dataSource.nodesByTypeHeads[i], node = head.lgspTypePrev; node != head; node = node.lgspTypePrev)
                {
                    LGSPNode newNode = (LGSPNode) node.Clone();
                    AddNodeWithoutEvents(newNode, node.lgspType.TypeID);
                    oldToNewMap[node] = newNode;
                }
            }

            for(int i = 0; i < dataSource.edgesByTypeHeads.Length; i++)
            {
                for(LGSPEdge head = dataSource.edgesByTypeHeads[i], edge = head.lgspTypePrev; edge != head; edge = edge.lgspTypePrev)
                {
                    LGSPEdge newEdge = (LGSPEdge) edge.Clone((INode) oldToNewMap[edge.lgspSource], (INode) oldToNewMap[edge.lgspTarget]);
                    AddEdgeWithoutEvents(newEdge, newEdge.lgspType.TypeID);
                    oldToNewMap[edge] = newEdge;
                }
            }

            foreach(KeyValuePair<IGraphElement, LinkedList<Variable>> kvp in dataSource.ElementMap)
            {
                IGraphElement newElem = oldToNewMap[kvp.Key];
                foreach(Variable var in kvp.Value)
                    SetVariableValue(var.Name, newElem);
            }

#if MONO_MULTIDIMARRAY_WORKAROUND
            dim0size = dataSource.dim0size;
            dim1size = dataSource.dim1size;
            dim2size = dataSource.dim2size;
            if(dataSource.vstructs != null)
                vstructs = (float[]) dataSource.vstructs.Clone();
#else
            if(dataSource.vstructs != null)
                vstructs = (float[ , , , ]) dataSource.vstructs.Clone();
#endif
            if(dataSource.nodeCounts != null)
                nodeCounts = (int[]) dataSource.nodeCounts.Clone();
            if(dataSource.edgeCounts != null)
                edgeCounts = (int[]) dataSource.edgeCounts.Clone();
#if OPCOST_WITH_GEO_MEAN
            if(dataSource.nodeLookupCosts != null)
                nodeLookupCosts = (float[]) dataSource.nodeLookupCosts.Clone();
            if(dataSource.edgeLookupCosts != null)
                edgeLookupCosts = (float[]) dataSource.edgeLookupCosts.Clone();
#endif
            if(dataSource.meanInDegree != null)
                meanInDegree = (float[]) dataSource.meanInDegree.Clone();
            if(dataSource.meanOutDegree != null)
                meanOutDegree = (float[]) dataSource.meanOutDegree.Clone();
        }

        /// <summary>
        /// Initializes the graph with the given model.
        /// </summary>
        /// <param name="grmodel">The model for this graph.</param>
        protected void InitializeGraph(IGraphModel grmodel)
        {
            model = grmodel;

            modelAssemblyName = Assembly.GetAssembly(grmodel.GetType()).Location;

            InitializeGraph();
        }

        private void InitializeGraph()
        {
            transactionManager = new LGSPTransactionManager(this);

            nodesByTypeHeads = new LGSPNode[model.NodeModel.Types.Length];
            for(int i = 0; i < model.NodeModel.Types.Length; i++)
            {
                LGSPNode head = new LGSPNodeHead();
                head.lgspTypeNext = head;
                head.lgspTypePrev = head;
                nodesByTypeHeads[i] = head;
            }
            nodesByTypeCounts = new int[model.NodeModel.Types.Length];
            edgesByTypeHeads = new LGSPEdge[model.EdgeModel.Types.Length];
            for(int i = 0; i < model.EdgeModel.Types.Length; i++)
            {
                LGSPEdge head = new LGSPEdgeHead();
                head.lgspTypeNext = head;
                head.lgspTypePrev = head;
                edgesByTypeHeads[i] = head;
            }
            edgesByTypeCounts = new int[model.EdgeModel.Types.Length];

            // Reset variables
            ElementMap.Clear();
            VariableMap.Clear();

            // Reset statistical data
#if MONO_MULTIDIMARRAY_WORKAROUND
            dim0size = dim1size = dim2size = 0;
#endif
            vstructs = null;
            nodeCounts = null;
            edgeCounts = null;
#if OPCOST_WITH_GEO_MEAN
            nodeLookupCosts = null;
            edgeLookupCosts = null;
#endif
            meanInDegree = null;
            meanOutDegree = null;
        }

		/// <summary>
		/// A name associated with the graph.
		/// </summary>
        public override String Name { get { return name; } }

		/// <summary>
		/// The model associated with the graph.
		/// </summary>
        public override IGraphModel Model { get { return model; } }

		/// <summary>
		/// Returns the graph's transaction manager.
		/// For attribute changes using the transaction manager is the only way to include such changes in the transaction history!
		/// Don't forget to call Commit after a transaction is finished!
		/// </summary>
        public override ITransactionManager TransactionManager { get { return transactionManager; } }

        /// <summary>
        /// For persistent backends permanently destroys the graph
        /// </summary>
        public override void DestroyGraph()
        {
        }

        /// <summary>
        /// Loads a LGSPActions implementation
        /// </summary>
        /// <param name="actionFilename">Filename of a action file. This can be either a library (.dll) or source code (.cs)</param>
        /// <returns>A LGSPActions object as BaseActions</returns>
        public override BaseActions LoadActions(String actionFilename)
        {
            Assembly assembly;
            String assemblyName;

            String extension = Path.GetExtension(actionFilename);
            if(extension.Equals(".cs", StringComparison.OrdinalIgnoreCase))
            {
                CSharpCodeProvider compiler = new CSharpCodeProvider();
                CompilerParameters compParams = new CompilerParameters();
                compParams.ReferencedAssemblies.Add("System.dll");
                compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(IBackend)).Location);
                compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(LGSPActions)).Location);
                compParams.ReferencedAssemblies.Add(modelAssemblyName);

//                compParams.GenerateInMemory = true;
                compParams.CompilerOptions = "/optimize";
                compParams.OutputAssembly = String.Format("lgsp-action-assembly-{0:00000}.dll", actionID++);

                CompilerResults compResults = compiler.CompileAssemblyFromFile(compParams, actionFilename);
                if(compResults.Errors.HasErrors)
                {
                    String errorMsg = compResults.Errors.Count + " Errors:";
                    foreach(CompilerError error in compResults.Errors)
                        errorMsg += String.Format("\r\nLine: {0} - {1}", error.Line, error.ErrorText);
                    throw new ArgumentException("Illegal actions C# source code: " + errorMsg);
                }
                
                assembly = compResults.CompiledAssembly;
                assemblyName = compParams.OutputAssembly;
            }
            else if(extension.Equals(".dll", StringComparison.OrdinalIgnoreCase))
            {
                assembly = Assembly.LoadFrom(actionFilename);
                assemblyName = actionFilename;
                if(backend != null) backend.AddAssembly(assembly);          // TODO: still needed??
            }
            else
            {
                throw new ArgumentException("The action filename must be either a .cs or a .dll filename!");
            }

            Type actionsType = null;
            try
            {
                foreach(Type type in assembly.GetTypes())
                {
                    if(!type.IsClass || type.IsNotPublic) continue;
                    if(type.BaseType == typeof(LGSPActions))
                    {
                        if(actionsType != null)
                        {
                            throw new ArgumentException(
                                "The given action file contains more than one LGSPActions implementation!");
                        }
                        actionsType = type;
                    }
                }
            }
            catch(ReflectionTypeLoadException e)
            {
                String errorMsg = "";
                foreach(Exception ex in e.LoaderExceptions)
                    errorMsg += "- " + ex.Message + Environment.NewLine;
                if(errorMsg.Length == 0) errorMsg = e.Message;
                throw new ArgumentException(errorMsg);
            }
            if(actionsType == null)
                throw new ArgumentException("The given action file doesn't contain an LGSPActions implementation!");

            LGSPActions actions = (LGSPActions) Activator.CreateInstance(actionsType, this, modelAssemblyName, assemblyName);

            if(Model.MD5Hash != actions.ModelMD5Hash)
                throw new ArgumentException("The given action file has been compiled with another model assembly!");

            return actions;
        }

        /// <summary>
        /// Returns the number of nodes with the exact given node type.
        /// </summary>
        public override int GetNumExactNodes(NodeType nodeType)
        {
            return nodesByTypeCounts[nodeType.TypeID];
        }

        /// <summary>
        /// Returns the number of edges with the exact given edge type.
        /// </summary>
        public override int GetNumExactEdges(EdgeType edgeType)
        {
            return edgesByTypeCounts[edgeType.TypeID];
        }

        /// <summary>
        /// Enumerates all nodes with the exact given node type.
        /// </summary>
        public override IEnumerable<INode> GetExactNodes(NodeType nodeType)
        {
            LGSPNode head = nodesByTypeHeads[nodeType.TypeID];
            LGSPNode cur = head.lgspTypeNext;
            LGSPNode next;
            while(cur != head)
            {
                next = cur.lgspTypeNext;
                yield return cur;
                cur = next;
            }
        }

        /// <summary>
        /// Enumerates all edges with the exact given edge type.
        /// </summary>
        public override IEnumerable<IEdge> GetExactEdges(EdgeType edgeType)
        {
            LGSPEdge head = edgesByTypeHeads[edgeType.TypeID];
            LGSPEdge cur = head.lgspTypeNext;
            LGSPEdge next;
            while(cur != head)
            {
                next = cur.lgspTypeNext;
                yield return cur;
                cur = next;
            }
        }

        /// <summary>
        /// Returns the number of nodes compatible to the given node type.
        /// </summary>
        public override int GetNumCompatibleNodes(NodeType nodeType)
        {
            int num = 0;
            foreach(NodeType type in nodeType.SubOrSameTypes)
                num += nodesByTypeCounts[type.TypeID];

            return num;
        }

        /// <summary>
        /// Returns the number of edges compatible to the given edge type.
        /// </summary>
        public override int GetNumCompatibleEdges(EdgeType edgeType)
        {
            int num = 0;
            foreach(EdgeType type in edgeType.SubOrSameTypes)
                num += edgesByTypeCounts[type.TypeID];

            return num;
        }

        /// <summary>
        /// Enumerates all nodes compatible to the given node type.
        /// </summary>
        public override IEnumerable<INode> GetCompatibleNodes(NodeType nodeType)
        {
            foreach(NodeType type in nodeType.SubOrSameTypes)
            {
                LGSPNode head = nodesByTypeHeads[type.TypeID];
                LGSPNode cur = head.lgspTypeNext;
                LGSPNode next;
                while(cur != head)
                {
                    next = cur.lgspTypeNext;
                    yield return cur;
                    cur = next;
                }
            }
        }

        /// <summary>
        /// Enumerates all edges compatible to the given edge type.
        /// </summary>
        public override IEnumerable<IEdge> GetCompatibleEdges(EdgeType edgeType)
        {
            foreach(EdgeType type in edgeType.SubOrSameTypes)
            {
                LGSPEdge head = edgesByTypeHeads[type.TypeID];
                LGSPEdge cur = head.lgspTypeNext;
                LGSPEdge next;
                while(cur != head)
                {
                    next = cur.lgspTypeNext;
                    yield return cur;
                    cur = next;
                }
            }
        }

        /// <summary>
        /// Moves the type list head of the given node after the given node.
        /// Part of the "list trick".
        /// </summary>
        /// <param name="elem">The node.</param>
        public void MoveHeadAfter(LGSPNode elem)
        {
            if(elem.lgspType == null) return;       // elem is head
            LGSPNode head = nodesByTypeHeads[elem.lgspType.TypeID];

            head.lgspTypePrev.lgspTypeNext = head.lgspTypeNext;
            head.lgspTypeNext.lgspTypePrev = head.lgspTypePrev;

            elem.lgspTypeNext.lgspTypePrev = head;
            head.lgspTypeNext = elem.lgspTypeNext;
            head.lgspTypePrev = elem;
            elem.lgspTypeNext = head;
        }

        /// <summary>
        /// Moves the type list head of the given edge after the given edge.
        /// Part of the "list trick".
        /// </summary>
        /// <param name="elem">The edge.</param>
        public void MoveHeadAfter(LGSPEdge elem)
        {
            if(elem.lgspType == null) return;       // elem is head
            LGSPEdge head = edgesByTypeHeads[elem.lgspType.TypeID];

            head.lgspTypePrev.lgspTypeNext = head.lgspTypeNext;
            head.lgspTypeNext.lgspTypePrev = head.lgspTypePrev;

            elem.lgspTypeNext.lgspTypePrev = head;
            head.lgspTypeNext = elem.lgspTypeNext;
            head.lgspTypePrev = elem;
            elem.lgspTypeNext = head;
        }

        /// <summary>
        /// Adds an existing node to this graph.
        /// The graph may not already contain the node!
        /// The edge may not be connected to any other elements!
        /// Intended only for undo, clone, retyping and internal use!
        /// </summary>
        public void AddNodeWithoutEvents(LGSPNode node, int typeid)
        {
#if CHECK_RINGLISTS
            CheckNodeAlreadyInTypeRinglist(node);
#endif
            LGSPNode head = nodesByTypeHeads[typeid];
            LGSPNode oldTypeNext = head.lgspTypeNext;
            LGSPNode oldTypePrev = head.lgspTypePrev;
            head.lgspTypeNext.lgspTypePrev = node;
            node.lgspTypeNext = head.lgspTypeNext;
            node.lgspTypePrev = head;
            head.lgspTypeNext = node;

            nodesByTypeCounts[typeid]++;

#if CHECK_RINGLISTS
            CheckTypeRinglistBroken(head);
#endif
        }

        /// <summary>
        /// Adds an existing edge to this graph.
        /// The graph may not already contain the edge!
        /// The edge may not be connected to any other elements!
        /// Intended only for undo, clone, retyping and internal use!
        /// </summary>
        public void AddEdgeWithoutEvents(LGSPEdge edge, int typeid)
        {
#if CHECK_RINGLISTS
            CheckEdgeAlreadyInTypeRinglist(edge);
#endif
            LGSPEdge head = edgesByTypeHeads[typeid];
            head.lgspTypeNext.lgspTypePrev = edge;
            edge.lgspTypeNext = head.lgspTypeNext;
            edge.lgspTypePrev = head;
            head.lgspTypeNext = edge;

            edge.lgspSource.AddOutgoing(edge);
            edge.lgspTarget.AddIncoming(edge);

            edgesByTypeCounts[typeid]++;

#if CHECK_RINGLISTS
            CheckTypeRinglistBroken(head);
#endif
        }

        /// <summary>
        /// Adds an existing INode object to the graph and assigns it to the given variable.
        /// The node must not be part of any graph, yet!
        /// The node may not be connected to any other elements!
        /// </summary>
        /// <param name="node">The node to be added.</param>
        /// <param name="varName">The name of the variable.</param>
        public override void AddNode(INode node, String varName)
        {
            AddNode((LGSPNode) node, varName);
        }

        /// <summary>
        /// Adds an existing LGSPNode object to the graph.
        /// The node must not be part of any graph, yet!
        /// The node may not be connected to any other elements!
        /// </summary>
        /// <param name="node">The node to be added.</param>
        public override void AddNode(INode node)
        {
            AddNode((LGSPNode) node);
        }

        /// <summary>
        /// Adds an existing LGSPNode object to the graph.
        /// The node must not be part of any graph, yet!
        /// The node may not be connected to any other elements!
        /// </summary>
        /// <param name="node">The node to be added.</param>
        public void AddNode(LGSPNode node)
        {
            AddNodeWithoutEvents(node, node.lgspType.TypeID);
            NodeAdded(node);
        }

        /// <summary>
        /// Adds a new node to the graph.
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <returns>The newly created node.</returns>
        protected override INode AddINode(NodeType nodeType)
        {
            return AddNode(nodeType);
        }

        /// <summary>
        /// Creates a new LGSPNode according to the given type and adds
        /// it to the graph.
        /// </summary>
        /// <param name="nodeType">The type for the new node.</param>
        /// <returns>The created node.</returns>
        public new LGSPNode AddNode(NodeType nodeType)
        {
//            LGSPNode node = new LGSPNode(nodeType);
            LGSPNode node = (LGSPNode) nodeType.CreateNode();
            AddNodeWithoutEvents(node, nodeType.TypeID);
            NodeAdded(node);
            return node;
        }

        /// <summary>
        /// Adds an existing LGSPNode object to the graph and assigns it to the given variable.
        /// The node must not be part of any graph, yet!
        /// The node may not be connected to any other elements!
        /// </summary>
        /// <param name="node">The node to be added.</param>
        /// <param name="varName">The name of the variable.</param>
        public void AddNode(LGSPNode node, String varName)
        {
            AddNodeWithoutEvents(node, node.lgspType.TypeID);
            SetVariableValue(varName, node);
            NodeAdded(node);
        }

        /// <summary>
        /// Adds a new node to the graph.
        /// TODO: Slow but provides a better interface...
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
		/// <param name="varName">The name of the variable.</param>
		/// <returns>The newly created node.</returns>
        protected override INode AddINode(NodeType nodeType, String varName)
        {
            return AddNode(nodeType, varName);
        }

        /// <summary>
        /// Adds a new LGSPNode to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created node.</returns>
        public new LGSPNode AddNode(NodeType nodeType, String varName)
        {
//            LGSPNode node = new LGSPNode(nodeType);
            LGSPNode node = (LGSPNode) nodeType.CreateNode();
            AddNodeWithoutEvents(node, nodeType.TypeID);
            SetVariableValue(varName, node);
            NodeAdded(node);
            return node;
        }

        /// <summary>
        /// Adds an existing IEdge object to the graph and assigns it to the given variable.
        /// The edge must not be part of any graph, yet!
        /// Source and target of the edge must already be part of the graph.
        /// </summary>
        /// <param name="edge">The edge to be added.</param>
        /// <param name="varName">The name of the variable.</param>
        public override void AddEdge(IEdge edge, String varName)
        {
            AddEdge((LGSPEdge) edge, varName);
        }

        /// <summary>
        /// Adds an existing LGSPEdge object to the graph.
        /// The edge must not be part of any graph, yet!
        /// Source and target of the edge must already be part of the graph.
        /// </summary>
        /// <param name="edge">The edge to be added.</param>
        public override void AddEdge(IEdge edge)
        {
            AddEdge((LGSPEdge) edge);
        }

        /// <summary>
        /// Adds an existing LGSPEdge object to the graph.
        /// The edge must not be part of any graph, yet!
        /// Source and target of the edge must already be part of the graph.
        /// </summary>
        /// <param name="edge">The edge to be added.</param>
        public void AddEdge(LGSPEdge edge)
        {
            AddEdgeWithoutEvents(edge, edge.lgspType.TypeID);
            EdgeAdded(edge);
        }

        /// <summary>
        /// Adds a new edge to the graph.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <returns>The newly created edge.</returns>
        public LGSPEdge AddEdge(EdgeType edgeType, LGSPNode source, LGSPNode target)
        {
//            LGSPEdge edge = new LGSPEdge(edgeType, source, target);
            LGSPEdge edge = (LGSPEdge) edgeType.CreateEdge(source, target);
            AddEdgeWithoutEvents(edge, edgeType.TypeID);
            EdgeAdded(edge);
            return edge;
        }

        /// <summary>
        /// Adds a new edge to the graph.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <returns>The newly created edge.</returns>
        public override IEdge AddEdge(EdgeType edgeType, INode source, INode target)
        {
            return AddEdge(edgeType, (LGSPNode)source, (LGSPNode)target);
        }

        /// <summary>
        /// Adds an existing LGSPEdge object to the graph and assigns it to the given variable.
        /// The edge must not be part of any graph, yet!
        /// Source and target of the edge must already be part of the graph.
        /// </summary>
        /// <param name="edge">The edge to be added.</param>
        /// <param name="varName">The name of the variable.</param>
        public void AddEdge(LGSPEdge edge, String varName)
        {
            AddEdgeWithoutEvents(edge, edge.lgspType.TypeID);
            SetVariableValue(varName, edge);
            EdgeAdded(edge);
        }

        /// <summary>
        /// Adds a new edge to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created edge.</returns>
        public LGSPEdge AddEdge(EdgeType edgeType, LGSPNode source, LGSPNode target, String varName)
        {
//            LGSPEdge edge = new LGSPEdge(edgeType, source, target);
            LGSPEdge edge = (LGSPEdge) edgeType.CreateEdge(source, target);
            AddEdgeWithoutEvents(edge, edgeType.TypeID);
            SetVariableValue(varName, edge);
            EdgeAdded(edge);
            return edge;
        }

        /// <summary>
        /// Adds a new edge to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created edge.</returns>
        public override IEdge AddEdge(EdgeType edgeType, INode source, INode target, String varName)
        {
            return AddEdge(edgeType, (LGSPNode) source, (LGSPNode) target, varName);
        }

        internal void RemoveNodeWithoutEvents(LGSPNode node, int typeid)
        {
            node.lgspTypePrev.lgspTypeNext = node.lgspTypeNext;
            node.lgspTypeNext.lgspTypePrev = node.lgspTypePrev;
            node.lgspTypeNext = null;
            node.lgspTypePrev = null;

            nodesByTypeCounts[typeid]--;

            if(reuseOptimization && !TransactionManager.TransactionActive)
                node.Recycle();

#if CHECK_RINGLISTS
            CheckTypeRinglistBroken(nodesByTypeHeads[typeid]);
#endif
        }

        /// <summary>
        /// Removes the given node from the graph.
        /// </summary>
        public override void Remove(INode node)
        {
            LGSPNode lgspNode = (LGSPNode) node;
            if(lgspNode.HasOutgoing || lgspNode.HasIncoming)
                throw new ArgumentException("The given node still has edges left!");
            if(!lgspNode.Valid) return;          // node not in graph (anymore)

            RemovingNode(node);

            if((lgspNode.lgspFlags & (uint) LGSPElemFlags.HAS_VARIABLES) != 0)
            {
                foreach(Variable var in ElementMap[lgspNode])
                    VariableMap.Remove(var.Name);
                ElementMap.Remove(lgspNode);
                lgspNode.lgspFlags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
            }
            RemoveNodeWithoutEvents(lgspNode, lgspNode.lgspType.TypeID);
        }

        /// <summary>
        /// Removes the given edge from the graph.
        /// </summary>
        public override void Remove(IEdge edge)
        {
            LGSPEdge lgspEdge = (LGSPEdge) edge;
            if(!lgspEdge.Valid) return;          // edge not in graph (anymore)

            RemovingEdge(edge);

            if((lgspEdge.lgspFlags & (uint) LGSPElemFlags.HAS_VARIABLES) != 0)
            {
                foreach(Variable var in ElementMap[lgspEdge])
                    VariableMap.Remove(var.Name);
                ElementMap.Remove(lgspEdge);
                lgspEdge.lgspFlags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
            }
            lgspEdge.lgspSource.RemoveOutgoing(lgspEdge);
            lgspEdge.lgspTarget.RemoveIncoming(lgspEdge);

            lgspEdge.lgspTypePrev.lgspTypeNext = lgspEdge.lgspTypeNext;
            lgspEdge.lgspTypeNext.lgspTypePrev = lgspEdge.lgspTypePrev;
            lgspEdge.lgspTypeNext = null;
            lgspEdge.lgspTypePrev = null;

            edgesByTypeCounts[lgspEdge.lgspType.TypeID]--;

            if(reuseOptimization && !TransactionManager.TransactionActive)
                lgspEdge.Recycle();

#if CHECK_RINGLISTS
            CheckTypeRinglistBroken(edgesByTypeHeads[lgspEdge.lgspType.TypeID]);
#endif
        }

        /// <summary>
        /// Removes all edges from the given node.
        /// </summary>
        public override void RemoveEdges(INode node)
        {
            LGSPNode lnode = (LGSPNode) node;

            RemovingEdges(node);
            foreach(LGSPEdge edge in lnode.Outgoing)
                Remove(edge);
            foreach(LGSPEdge edge in lnode.Incoming)
                Remove(edge);
        }

        /// <summary>
        /// Reuses an LGSPNode object for a new node of the same type.
        /// This causes a RemovingEdges, a RemovingNode and a NodeAdded event and removes all edges
        /// and variables pointing to the old element.
        /// </summary>
        /// <param name="node">The LGSPNode object to be reused.</param>
        public void ReuseNode(LGSPNode node)
        {
            RemoveEdges(node);
            RemovingNode(node);

            if((node.lgspFlags & (uint) LGSPElemFlags.HAS_VARIABLES) != 0)
            {
                foreach(Variable var in ElementMap[node])
                    VariableMap.Remove(var.Name);
                ElementMap.Remove(node);
                node.lgspFlags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
            }

            node.ResetAllAttributes();

            NodeAdded(node);
        }

        /// <summary>
        /// Reuses an LGSPEdge object for a new edge of the same type and optionally changes the source and/or target.
        /// This causes a RemovingEdge and an EdgeAdded event and removes all variables pointing to the old element.
        /// </summary>
        /// <param name="edge">The LGSPEdge object to be reused.</param>
        /// <param name="newSource">The new source of the edge, or null if it is not to be changed.</param>
        /// <param name="newTarget">The new target of the edge, or null if it is not to be changed.</param>
        public void ReuseEdge(LGSPEdge edge, LGSPNode newSource, LGSPNode newTarget)
        {
            RemovingEdge(edge);

            if((edge.lgspFlags & (uint) LGSPElemFlags.HAS_VARIABLES) != 0)
            {
                foreach(Variable var in ElementMap[edge])
                    VariableMap.Remove(var.Name);
                ElementMap.Remove(edge);
                edge.lgspFlags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
            }

#if CHECK_RINGLISTS
            LGSPNode oldSource = edge.lgspSource;
            LGSPNode oldTarget = edge.lgspTarget;
#endif

            if(newSource != null)
            {
                // removeOutgoing
                if(edge == edge.lgspSource.lgspOuthead)
                {
                    edge.lgspSource.lgspOuthead = edge.lgspOutNext;
                    if(edge.lgspSource.lgspOuthead == edge)
                        edge.lgspSource.lgspOuthead = null;
                }
                edge.lgspOutPrev.lgspOutNext = edge.lgspOutNext;
                edge.lgspOutNext.lgspOutPrev = edge.lgspOutPrev;
                edge.lgspSource = newSource;

                // addOutgoing
                LGSPEdge outhead = newSource.lgspOuthead;
                if(outhead == null)
                {
                    newSource.lgspOuthead = edge;
                    edge.lgspOutNext = edge;
                    edge.lgspOutPrev = edge;
                }
                else
                {
                    outhead.lgspOutPrev.lgspOutNext = edge;
                    edge.lgspOutPrev = outhead.lgspOutPrev;
                    edge.lgspOutNext = outhead;
                    outhead.lgspOutPrev = edge;
                }
            }

            if(newTarget != null)
            {
                // removeIncoming
                if(edge == edge.lgspTarget.lgspInhead)
                {
                    edge.lgspTarget.lgspInhead = edge.lgspInNext;
                    if(edge.lgspTarget.lgspInhead == edge)
                        edge.lgspTarget.lgspInhead = null;
                }
                edge.lgspInPrev.lgspInNext = edge.lgspInNext;
                edge.lgspInNext.lgspInPrev = edge.lgspInPrev;
                edge.lgspTarget = newTarget;

                // addIncoming
                LGSPEdge inhead = newTarget.lgspInhead;
                if(inhead == null)
                {
                    newTarget.lgspInhead = edge;
                    edge.lgspInNext = edge;
                    edge.lgspInPrev = edge;
                }
                else
                {
                    inhead.lgspInPrev.lgspInNext = edge;
                    edge.lgspInPrev = inhead.lgspInPrev;
                    edge.lgspInNext = inhead;
                    inhead.lgspInPrev = edge;
                }
            }

            edge.ResetAllAttributes();

            EdgeAdded(edge);

#if CHECK_RINGLISTS
            CheckInOutRinglistsBroken(oldSource);
            CheckInOutRinglistsBroken(oldTarget);
            if(newSource!=null) CheckInOutRinglistsBroken(newSource);
            if(newTarget!=null) CheckInOutRinglistsBroken(newTarget);
            CheckTypeRinglistBroken(edgesByTypeHeads[edge.lgspType.TypeID]);
#endif
        }

        /// <summary>
        /// Removes all nodes and edges (including any variables pointing to them) from the graph.
        /// </summary>
        public override void Clear()
        {
            ClearingGraph();
            InitializeGraph();
        }

        /// <summary>
        /// Retypes a node by creating a new node of the given type.
        /// All incident edges as well as all attributes from common super classes are kept.
        /// WARNING: GetElementName will probably not return the same element name for the new node, yet! (TODO)
        /// </summary>
        /// <param name="node">The node to be retyped.</param>
        /// <param name="newNodeType">The new type for the node.</param>
        /// <returns>The new node object representing the retyped node.</returns>
        public LGSPNode Retype(LGSPNode node, NodeType newNodeType)
        {
            LGSPNode newNode = (LGSPNode) newNodeType.CreateNodeWithCopyCommons(node);

            RetypingNode(node, newNode);

            ReplaceNode(node, newNode);
            return newNode;
        }

        /// <summary>
        /// Retypes a node by creating a new node of the given type.
        /// All incident edges as well as all attributes from common super classes are kept.
        /// WARNING: GetElementName will probably not return the same element name for the new node, yet! (TODO)
        /// </summary>
        /// <param name="node">The node to be retyped.</param>
        /// <param name="newNodeType">The new type for the node.</param>
        /// <returns>The new node object representing the retyped node.</returns>
        public override INode Retype(INode node, NodeType newNodeType)
        {
            return Retype((LGSPNode) node, newNodeType);
        }

        /// <summary>
        /// Retypes an edge by replacing it by a new edge of the given type.
        /// Source and target node as well as all attributes from common super classes are kept.
        /// WARNING: GetElementName will probably not return the same element name for the new edge, yet! (TODO)
        /// </summary>
        /// <param name="edge">The edge to be retyped.</param>
        /// <param name="newEdgeType">The new type for the edge.</param>
        /// <returns>The new edge object representing the retyped edge.</returns>
        public LGSPEdge Retype(LGSPEdge edge, EdgeType newEdgeType)
        {
            LGSPEdge newEdge = (LGSPEdge) newEdgeType.CreateEdgeWithCopyCommons(edge.lgspSource, edge.lgspTarget, edge);

            RetypingEdge(edge, newEdge);

            ReplaceEdge(edge, newEdge);
            return newEdge;
        }

        /// <summary>
        /// Retypes an edge by replacing it by a new edge of the given type.
        /// Source and target node as well as all attributes from common super classes are kept.
        /// WARNING: GetElementName will probably not return the same element name for the new edge, yet! (TODO)
        /// </summary>
        /// <param name="edge">The edge to be retyped.</param>
        /// <param name="newEdgeType">The new type for the edge.</param>
        /// <returns>The new edge object representing the retyped edge.</returns>
        public override IEdge Retype(IEdge edge, EdgeType newEdgeType)
        {
            return Retype((LGSPEdge) edge, newEdgeType);
        }

        /// <summary>
        /// Replaces a given node by another one.
        /// All incident edges and variables are transferred to the new node.
        /// The attributes are not touched.
        /// This function is used for retyping.
        /// </summary>
        /// <param name="oldNode">The node to be replaced.</param>
        /// <param name="newNode">The replacement for the node.</param>
        public void ReplaceNode(LGSPNode oldNode, LGSPNode newNode)
        {
            if((oldNode.lgspFlags & (uint) LGSPElemFlags.HAS_VARIABLES) != 0)
            {
                LinkedList<Variable> varList = ElementMap[oldNode];
                foreach(Variable var in varList)
                    var.Value = newNode;
                ElementMap.Remove(oldNode);
                ElementMap[newNode] = varList;
                newNode.lgspFlags |= (uint) LGSPElemFlags.HAS_VARIABLES;
            }

            if(oldNode.lgspType != newNode.lgspType)
            {
                if(!oldNode.Valid)
                    throw new Exception("Fatal failure at retyping: The old node is not a member of the graph (any more).");

				oldNode.lgspTypePrev.lgspTypeNext = oldNode.lgspTypeNext;
				oldNode.lgspTypeNext.lgspTypePrev = oldNode.lgspTypePrev;
				nodesByTypeCounts[oldNode.lgspType.TypeID]--;

                AddNodeWithoutEvents(newNode, newNode.lgspType.TypeID);
            }
            else
            {
                newNode.lgspTypeNext = oldNode.lgspTypeNext;
                newNode.lgspTypePrev = oldNode.lgspTypePrev;
                oldNode.lgspTypeNext.lgspTypePrev = newNode;
                oldNode.lgspTypePrev.lgspTypeNext = newNode;
            }
            oldNode.lgspTypeNext = newNode;			// indicate replacement (checked in rewrite for hom nodes)
			oldNode.lgspTypePrev = null;			// indicate node is node valid anymore

            // Reassign all outgoing edges
            LGSPEdge outHead = oldNode.lgspOuthead;
            if(outHead != null)
            {
                LGSPEdge outCur = outHead;
                do
                {
                    outCur.lgspSource = newNode;
                    outCur = outCur.lgspOutNext;
                }
                while(outCur != outHead);
            }
            newNode.lgspOuthead = outHead;

            // Reassign all incoming edges
            LGSPEdge inHead = oldNode.lgspInhead;
            if(inHead != null)
            {
                LGSPEdge inCur = inHead;
                do
                {
                    inCur.lgspTarget = newNode;
                    inCur = inCur.lgspInNext;
                }
                while(inCur != inHead);
            }
            newNode.lgspInhead = inHead;

            if(reuseOptimization && !TransactionManager.TransactionActive)
                oldNode.Recycle();

#if CHECK_RINGLISTS
            CheckTypeRinglistBroken(nodesByTypeHeads[newNode.lgspType.TypeID]);
            CheckInOutRinglistsBroken(newNode);
#endif
        }

        /// <summary>
        /// Replaces a given edge by another one.
        /// Source and target node are transferred to the new edge,
        /// but the new edge must already have source and target set to these nodes.
        /// The new edge is added to the graph, the old edge is removed.
        /// A SettingEdgeType event is generated before.
        /// The attributes are not touched.
        /// This function is used for retyping.
        /// </summary>
        /// <param name="oldEdge">The edge to be replaced.</param>
        /// <param name="newEdge">The replacement for the edge.</param>
        public void ReplaceEdge(LGSPEdge oldEdge, LGSPEdge newEdge)
        {
            if((oldEdge.lgspFlags & (uint) LGSPElemFlags.HAS_VARIABLES) != 0)
            {
                LinkedList<Variable> varList = ElementMap[oldEdge];
                foreach(Variable var in varList)
                    var.Value = newEdge;
                ElementMap.Remove(oldEdge);
                ElementMap[newEdge] = varList;
                newEdge.lgspFlags |= (uint) LGSPElemFlags.HAS_VARIABLES;
            }

            if(oldEdge.lgspType != newEdge.lgspType)
            {
                if(!oldEdge.Valid)
                    throw new Exception("Fatal failure at retyping: The old edge is not a member of the graph (any more).");

                oldEdge.lgspTypePrev.lgspTypeNext = oldEdge.lgspTypeNext;
                oldEdge.lgspTypeNext.lgspTypePrev = oldEdge.lgspTypePrev;

                edgesByTypeCounts[oldEdge.lgspType.TypeID]--;

                LGSPEdge head = edgesByTypeHeads[newEdge.lgspType.TypeID];
                head.lgspTypeNext.lgspTypePrev = newEdge;
                newEdge.lgspTypeNext = head.lgspTypeNext;
                newEdge.lgspTypePrev = head;
                head.lgspTypeNext = newEdge;

                edgesByTypeCounts[newEdge.lgspType.TypeID]++;
            }
            else
            {
                newEdge.lgspTypeNext = oldEdge.lgspTypeNext;
                newEdge.lgspTypePrev = oldEdge.lgspTypePrev;
                oldEdge.lgspTypeNext.lgspTypePrev = newEdge;
                oldEdge.lgspTypePrev.lgspTypeNext = newEdge;
            }
			oldEdge.lgspTypeNext = newEdge;			// indicate replacement (checked in rewrite for hom edges)
			oldEdge.lgspTypePrev = null;			// indicate edge is node valid anymore

            // Reassign source node
            LGSPNode src = oldEdge.lgspSource;
            if(src.lgspOuthead == oldEdge)
                src.lgspOuthead = newEdge;

            if(oldEdge.lgspOutNext == oldEdge)  // the only outgoing edge?
            {
                newEdge.lgspOutNext = newEdge;
                newEdge.lgspOutPrev = newEdge;
            }
            else
            {
                LGSPEdge oldOutNext = oldEdge.lgspOutNext;
                LGSPEdge oldOutPrev = oldEdge.lgspOutPrev;
                oldOutNext.lgspOutPrev = newEdge;
                oldOutPrev.lgspOutNext = newEdge;
                newEdge.lgspOutNext = oldOutNext;
                newEdge.lgspOutPrev = oldOutPrev;
            }
            oldEdge.lgspOutNext = null;
            oldEdge.lgspOutPrev = null;

            // Reassign target node
            LGSPNode tgt = oldEdge.lgspTarget;
            if(tgt.lgspInhead == oldEdge)
                tgt.lgspInhead = newEdge;

            if(oldEdge.lgspInNext == oldEdge)   // the only incoming edge?
            {
                newEdge.lgspInNext = newEdge;
                newEdge.lgspInPrev = newEdge;
            }
            else
            {
                LGSPEdge oldInNext = oldEdge.lgspInNext;
                LGSPEdge oldInPrev = oldEdge.lgspInPrev;
                oldInNext.lgspInPrev = newEdge;
                oldInPrev.lgspInNext = newEdge;
                newEdge.lgspInNext = oldInNext;
                newEdge.lgspInPrev = oldInPrev;
            }
            oldEdge.lgspInNext = null;
            oldEdge.lgspInPrev = null;

            if(reuseOptimization && !TransactionManager.TransactionActive)
                oldEdge.Recycle();

#if CHECK_RINGLISTS
            CheckTypeRinglistBroken(edgesByTypeHeads[newEdge.lgspType.TypeID]);
            CheckInOutRinglistsBroken(newEdge.lgspSource);
            CheckInOutRinglistsBroken(newEdge.lgspTarget);
#endif
        }

        #region Visited flags management

        private class VisitorData
        {
            /// <summary>
            /// Specifies whether this visitor has already marked any nodes.
            /// </summary>
            public bool NodesMarked;

            /// <summary>
            /// Specifies whether this visitor has already marked any edges.
            /// </summary>
            public bool EdgesMarked;

            /// <summary>
            /// A hash map containing all visited elements (the values are not used).
            /// This is unused (and thus null), if the graph element flags are used for this visitor ID.
            /// </summary>
            public Dictionary<IGraphElement, bool> VisitedElements;
        }

        int numUsedVisitorIDs = 0;
        LinkedList<int> freeVisitorIDs = new LinkedList<int>();
        List<VisitorData> visitorDataList = new List<VisitorData>();

        /// <summary>
        /// Allocates a clean visited flag on the graph elements.
        /// If needed the flag is cleared on all graph elements, so this is an O(n) operation.
        /// </summary>
        /// <returns>A visitor ID to be used in
        /// visited conditions in patterns ("if { !visited(elem, id); }"),
        /// visited expressions in evals ("visited(elem, id) = true; b.flag = visited(elem, id) || c.flag; "}
        /// and calls to other visitor functions.</returns>
        public override int AllocateVisitedFlag()
        {
            int newID;
            if(freeVisitorIDs.Count != 0)
            {
                newID = freeVisitorIDs.First.Value;
                freeVisitorIDs.RemoveFirst();
            }
            else
            {
                newID = numUsedVisitorIDs;
                if(newID == visitorDataList.Count)
                    visitorDataList.Add(new VisitorData());
            }
            numUsedVisitorIDs++;

            ResetVisitedFlag(newID);

            return newID;
        }

        /// <summary>
        /// Frees a visited flag.
        /// This is an O(1) operation.
        /// It adds visitor flags supported by the element flags to the front of the list
        /// to prefer them when allocating a new one.
        /// </summary>
        /// <param name="visitorID">The ID of the visited flag to be freed.</param>
        public override void FreeVisitedFlag(int visitorID)
        {
            if(visitorID < (int) LGSPElemFlags.NUM_SUPPORTED_VISITOR_IDS)
                freeVisitorIDs.AddFirst(visitorID);
            else
                freeVisitorIDs.AddLast(visitorID);
            numUsedVisitorIDs--;
        }

        /// <summary>
        /// Resets the visited flag with the given ID on all graph elements, if necessary.
        /// </summary>
        /// <param name="visitorID">The ID of the visited flag.</param>
        public override void ResetVisitedFlag(int visitorID)
        {
            VisitorData data = visitorDataList[visitorID];
            if(visitorID < (int) LGSPElemFlags.NUM_SUPPORTED_VISITOR_IDS)     // id supported by flags?
            {
                if(data.NodesMarked)        // need to clear flag on nodes?
                {
                    for(int i = 0; i < nodesByTypeHeads.Length; i++)
                    {
                        for(LGSPNode head = nodesByTypeHeads[i], node = head.lgspTypePrev; node != head; node = node.lgspTypePrev)
                            node.lgspFlags &= ~((uint) LGSPElemFlags.IS_VISITED << visitorID);
                    }
                    data.NodesMarked = false;
                }
                if(data.EdgesMarked)        // need to clear flag on nodes?
                {
                    for(int i = 0; i < edgesByTypeHeads.Length; i++)
                    {
                        for(LGSPEdge head = edgesByTypeHeads[i], edge = head.lgspTypePrev; edge != head; edge = edge.lgspTypePrev)
                            edge.lgspFlags &= ~((uint) LGSPElemFlags.IS_VISITED << visitorID);
                    }
                    data.EdgesMarked = false;
                }
            }
            else                                                    // no, use hash map
            {
                data.NodesMarked = data.EdgesMarked = false;
                if(data.VisitedElements != null) data.VisitedElements.Clear();
                else data.VisitedElements = new Dictionary<IGraphElement, bool>();
            }
        }

        /// <summary>
        /// Sets the visited flag of the given graph element.
        /// </summary>
        /// <param name="elem">The graph element whose flag is to be set.</param>
        /// <param name="visitorID">The ID of the visited flag.</param>
        /// <param name="visited">True for visited, false for not visited.</param>
        public override void SetVisited(IGraphElement elem, int visitorID, bool visited)
        {
            VisitorData data = visitorDataList[visitorID];
            LGSPNode node = elem as LGSPNode;
            if(visitorID < (int) LGSPElemFlags.NUM_SUPPORTED_VISITOR_IDS)     // id supported by flags?
            {
                uint mask = (uint) LGSPElemFlags.IS_VISITED << visitorID;
                if(node != null)
                {
                    if(visited)
                    {
                        node.lgspFlags |= mask;
                        data.NodesMarked = true;
                    }
                    else node.lgspFlags &= ~mask;
                }
                else
                {
                    LGSPEdge edge = (LGSPEdge) elem;
                    if(visited)
                    {
                        edge.lgspFlags |= mask;
                        data.EdgesMarked = true;
                    }
                    else edge.lgspFlags &= ~mask;
                }
            }
            else                                                        // no, use hash map
            {
                if(visited)
                {
                    if(node != null)
                        data.NodesMarked = true;
                    else
                        data.EdgesMarked = true;
                    data.VisitedElements[elem] = true;
                }
                else data.VisitedElements.Remove(elem);
            }
        }

        /// <summary>
        /// Returns whether the given graph element has been visited.
        /// </summary>
        /// <param name="elem">The graph element to be examined.</param>
        /// <param name="visitorID">The ID of the visited flag.</param>
        /// <returns>True for visited, false for not visited.</returns>
        public override bool IsVisited(IGraphElement elem, int visitorID)
        {
            if(visitorID < (int) LGSPElemFlags.NUM_SUPPORTED_VISITOR_IDS)        // id supported by flags?
            {
                uint mask = (uint) LGSPElemFlags.IS_VISITED << visitorID;
                LGSPNode node = elem as LGSPNode;
                if(node != null)
                    return (node.lgspFlags & mask) != 0;
                else
                {
                    LGSPEdge edge = (LGSPEdge) elem;
                    return (edge.lgspFlags & mask) != 0;
                }
            }
            else                                                        // no, use hash map
            {
                VisitorData data = visitorDataList[visitorID];
                return data.VisitedElements.ContainsKey(elem);
            }
        }

        #endregion Visited flags management

        #region Names and Variables management

        /// <summary>
        /// Set it if a named graph is available, so that the nameof operator can return the persistent name instead of a hash code
        /// </summary>
        public NamedGraph NamedGraph { get { return namedGraph; } set { namedGraph = value; } }
        private NamedGraph namedGraph = null;

        protected Dictionary<IGraphElement, LinkedList<Variable>> ElementMap = new Dictionary<IGraphElement, LinkedList<Variable>>();
        protected Dictionary<String, Variable> VariableMap = new Dictionary<String, Variable>();

        /// <summary>
        /// Returns the name for the given element,
        /// i.e. the name defined by the named graph if a named graph is available,
        /// or a hash value string if only a lgpsGraph is available.
        /// </summary>
        /// <param name="elem">Element of which the name is to be found</param>
        /// <returns>The name of the given element</returns>
        public override String GetElementName(IGraphElement elem)
        {
            if(namedGraph != null) 
                return namedGraph.GetElementName(elem);
            else
                return "$" + elem.GetHashCode();
        }

        /// <summary>
        /// Returns a linked list of variables mapped to the given graph element
        /// or null, if no variable points to this element
        /// </summary>
        public override LinkedList<Variable> GetElementVariables(IGraphElement elem)
        {
            LinkedList<Variable> variableList;
            ElementMap.TryGetValue(elem, out variableList);
            return variableList;
        }

        /// <summary>
        /// Retrieves the object for a variable name or null, if the variable isn't set yet or anymore.
        /// </summary>
        /// <param name="varName">The variable name to lookup</param>
        /// <returns>The according object or null</returns>
        public override object GetVariableValue(String varName)
        {
            Variable var;
            VariableMap.TryGetValue(varName, out var);
            if(var == null) return null;
            return var.Value;
        }

        /// <summary>
        /// Retrieves the LGSPNode for a variable name or null, if the variable isn't set yet or anymore.
        /// A InvalidCastException is thrown, if the variable is set and does not point to an LGSPNode object.
        /// </summary>
        /// <param name="varName">The variable name to lookup.</param>
        /// <returns>The according LGSPNode or null.</returns>
        public new LGSPNode GetNodeVarValue(string varName)
        {
            return (LGSPNode) GetVariableValue(varName);
        }

        /// <summary>
        /// Retrieves the LGSPEdge for a variable name or null, if the variable isn't set yet or anymore.
        /// A InvalidCastException is thrown, if the variable is set and does not point to an LGSPEdge object.
        /// </summary>
        /// <param name="varName">The variable name to lookup.</param>
        /// <returns>The according LGSPEdge or null.</returns>
        public new LGSPEdge GetEdgeVarValue(string varName)
        {
            return (LGSPEdge) GetVariableValue(varName);
        }

        /// <summary>
        /// Detaches the specified variable from the according graph element.
        /// If it was the last variable pointing to the element, the variable list for the element is removed.
        /// This function may only called on variables pointing to graph elements.
        /// </summary>
        /// <param name="var">Variable to detach.</param>
        private void DetachVariableFromElement(Variable var)
        {
            IGraphElement elem = (IGraphElement) var.Value;
            LinkedList<Variable> oldVarList = ElementMap[elem];
            oldVarList.Remove(var);
            if(oldVarList.Count == 0)
            {
                ElementMap.Remove(elem);

                LGSPNode oldNode = elem as LGSPNode;
                if(oldNode != null) oldNode.lgspFlags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
                else
                {
                    LGSPEdge oldEdge = (LGSPEdge) elem;
                    oldEdge.lgspFlags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
                }
            }
        }

        /// <summary>
        /// Sets the value of the given variable to the given IGraphElement.
        /// If the variable name is null, this function does nothing.
        /// If elem is null, the variable is unset.
        /// </summary>
        /// <param name="varName">The name of the variable.</param>
        /// <param name="val">The new value of the variable or null to unset the variable.</param>
        public override void SetVariableValue(String varName, object val)
        {
            if(varName == null) return;

            Variable var;
            VariableMap.TryGetValue(varName, out var);

            if(var != null)
            {
                if(var.Value == val) return;     // Variable already set to this element?
                if(var.Value is IGraphElement)
                    DetachVariableFromElement(var);

                if(val == null)
                {
                    VariableMap.Remove(varName);
                    return;
                }
                var.Value = val;
            }
            else
            {
                if(val == null) return;

                var = new Variable(varName, val);
                VariableMap[varName] = var;
            }

            IGraphElement elem = val as IGraphElement;
            if(elem == null) return;

            LinkedList<Variable> newVarList;
            if(!ElementMap.TryGetValue(elem, out newVarList))
            {
                newVarList = new LinkedList<Variable>();
                ElementMap[elem] = newVarList;
            }
            newVarList.AddFirst(var);

            LGSPNode node = elem as LGSPNode;
            if(node != null)
                node.lgspFlags |= (uint) LGSPElemFlags.HAS_VARIABLES;
            else
            {
                LGSPEdge edge = (LGSPEdge) elem;
                edge.lgspFlags |= (uint) LGSPElemFlags.HAS_VARIABLES;
            }
        }

        #endregion Variables management

#if USE_SUB_SUPER_ENUMERATORS
        public void AnalyseGraph()
        {
            int numNodeTypes = Model.NodeModel.Types.Length;
            int numEdgeTypes = Model.EdgeModel.Types.Length;

            int[,] outgoingVCount = new int[numEdgeTypes, numNodeTypes];
            int[,] incomingVCount = new int[numEdgeTypes, numNodeTypes];

#if MONO_MULTIDIMARRAY_WORKAROUND
            dim0size = numNodeTypes;
            dim1size = numEdgeTypes;
            dim2size = numNodeTypes;
            vstructs = new float[numNodeTypes*numEdgeTypes*numNodeTypes*2];
#else
            vstructs = new float[numNodeTypes, numEdgeTypes, numNodeTypes, 2];
#endif
            nodeCounts = new int[numNodeTypes];
            edgeCounts = new int[numEdgeTypes];

            foreach(ITypeFramework nodeType in Model.NodeModel.Types)
            {
                foreach(ITypeFramework superType in nodeType.SuperOrSameTypes)
                    nodeCounts[superType.typeID] += nodes[superType.typeID].Count;

                for(LGSPNode nodeHead = nodes[nodeType.typeID].Head, node = nodeHead.next; node != nodeHead; node = node.next)
                {
                    // count outgoing v structures
                    outgoingVCount.Initialize();
                    for(LGSPEdge edgeHead = node.outgoing.Head, edge = edgeHead.outNode.next; edge != edgeHead; edge = edge.outNode.next)
                    {
                        ITypeFramework targetType = edge.target.type;
//                        outgoingVCount[edge.type.TypeID, nodeType.TypeID]++;

                        foreach(ITypeFramework edgeSuperType in edge.Type.SuperOrSameTypes)
                        {
                            int superTypeID = edgeSuperType.typeID;
                            foreach(ITypeFramework targetSuperType in targetType.SuperOrSameTypes)
                            {
                                outgoingVCount[superTypeID, targetSuperType.typeID]++;
                            }
                        }
                    }

                    // count incoming v structures
                    incomingVCount.Initialize();
                    for(LGSPEdge edgeHead = node.incoming.Head, edge = edgeHead.inNode.next; edge != edgeHead; edge = edge.inNode.next)
                    {
                        ITypeFramework sourceType = edge.source.type;
//                        incomingVCount[edge.type.TypeID, nodeType.TypeID]++;

                        foreach(ITypeFramework edgeSuperType in edge.Type.SuperOrSameTypes)
                        {
                            int superTypeID = edgeSuperType.typeID;
                            foreach(ITypeFramework sourceSuperType in sourceType.SuperOrSameTypes)
                            {
                                incomingVCount[superTypeID, sourceSuperType.typeID]++;
                            }
                        }
                    }

                    // finalize the counting and collect resulting local v-struct info

                    for(LGSPEdge edgeHead = node.outgoing.Head, edge = edgeHead.outNode.next; edge != edgeHead; edge = edge.outNode.next)
                    {
                        ITypeFramework targetType = edge.target.type;
                        int targetTypeID = targetType.typeID;

                        foreach(ITypeFramework edgeSuperType in edge.Type.SuperOrSameTypes)
                        {
                            int superTypeID = edgeSuperType.typeID;

                            foreach(ITypeFramework targetSuperType in targetType.SuperOrSameTypes)
                            {
                                if(outgoingVCount[superTypeID, targetSuperType.typeID] > 1)
                                {
                                    float val = (float) Math.Log(outgoingVCount[superTypeID, targetSuperType.typeID]);
                                    foreach(ITypeFramework nodeSuperType in nodeType.SuperOrSameTypes)
#if MONO_MULTIDIMARRAY_WORKAROUND
//                                        vstructs[((superTypeID * dim1size + nodeSuperType.TypeID) * dim2size + targetTypeID) * 2
//                                            + (int) LGSPDir.Out] += val;
                                        vstructs[((nodeSuperType.typeID * dim1size + superTypeID) * dim2size + targetTypeID) * 2
                                            + (int) LGSPDir.Out] += val;
#else
//                                        vstructs[superTypeID, nodeSuperType.TypeID, targetTypeID, (int) LGSPDir.Out] += val;
                                        vstructs[nodeSuperType.TypeID, superTypeID, targetTypeID, (int) LGSPDir.Out] += val;
#endif
                                    outgoingVCount[superTypeID, targetSuperType.typeID] = 0;
                                }
                            }
                        }
                    }

                    for(LGSPEdge edgeHead = node.incoming.Head, edge = edgeHead.inNode.next; edge != edgeHead; edge = edge.inNode.next)
                    {
                        ITypeFramework sourceType = edge.source.type;
                        int sourceTypeID = sourceType.typeID;

                        foreach(ITypeFramework edgeSuperType in edge.Type.SuperOrSameTypes)
                        {
                            int superTypeID = edgeSuperType.typeID;
                            foreach(ITypeFramework sourceSuperType in sourceType.SuperOrSameTypes)
                            {
                                if(incomingVCount[superTypeID, sourceSuperType.typeID] > 1)
                                {
                                    float val = (float) Math.Log(incomingVCount[superTypeID, sourceSuperType.typeID]);

                                    foreach(ITypeFramework nodeSuperType in nodeType.SuperOrSameTypes)
#if MONO_MULTIDIMARRAY_WORKAROUND
//                                        vstructs[((superTypeID * dim1size + nodeSuperType.TypeID) * dim2size + sourceTypeID) * 2
//                                            + (int) LGSPDir.In] += val;
                                        vstructs[((nodeSuperType.typeID * dim1size + superTypeID) * dim2size + sourceTypeID) * 2
                                            + (int) LGSPDir.In] += val;
#else
//                                        vstructs[superTypeID, nodeSuperType.TypeID, sourceTypeID, (int) LGSPDir.In] += val;
                                        vstructs[nodeSuperType.TypeID, superTypeID, sourceTypeID, (int) LGSPDir.In] += val;
#endif
                                    incomingVCount[superTypeID, sourceSuperType.typeID] = 0;
                                }
                            }
                        }
                    }
                }
            }

            nodeLookupCosts = new float[numNodeTypes];
            for(int i = 0; i < numNodeTypes; i++)
            {
                if(nodeCounts[i] <= 1)
                    nodeLookupCosts[i] = 0.00001F;                              // TODO: check this value (>0 because of preset elements)
                else
                    nodeLookupCosts[i] = (float) Math.Log(nodeCounts[i]);
            }

            // Calculate edgeCounts
            foreach(ITypeFramework edgeType in Model.EdgeModel.Types)
            {
                edgeCounts[edgeType.typeID] += edges[edgeType.typeID].Count;
                foreach(ITypeFramework superType in edgeType.SuperTypes)
                    edgeCounts[superType.typeID] += edges[superType.typeID].Count;
            }

            edgeLookupCosts = new float[numEdgeTypes];
            for(int i = 0; i < numEdgeTypes; i++)
            {
                if(edgeCounts[i] <= 1)
                    edgeLookupCosts[i] = 0.00001F;                              // TODO: check this value (>0 because of preset elements)
                else
                    edgeLookupCosts[i] = (float) Math.Log(edgeCounts[i]);
            }
        }
#elif OPCOST_WITH_GEO_MEAN
        public void AnalyzeGraph()
        {
            int numNodeTypes = Model.NodeModel.Types.Length;
            int numEdgeTypes = Model.EdgeModel.Types.Length;

            int[,] outgoingVCount = new int[numEdgeTypes, numNodeTypes];
            int[,] incomingVCount = new int[numEdgeTypes, numNodeTypes];

#if MONO_MULTIDIMARRAY_WORKAROUND
            dim0size = numNodeTypes;
            dim1size = numEdgeTypes;
            dim2size = numNodeTypes;
            vstructs = new float[numNodeTypes*numEdgeTypes*numNodeTypes*2];
#else
            vstructs = new float[numNodeTypes, numEdgeTypes, numNodeTypes, 2];
#endif
            nodeCounts = new int[numNodeTypes];
            edgeCounts = new int[numEdgeTypes];
            nodeIncomingCount = new float[numNodeTypes];
            nodeOutgoingCount = new float[numNodeTypes];

            foreach(ITypeFramework nodeType in Model.NodeModel.Types)
            {
                foreach(ITypeFramework superType in nodeType.superOrSameTypes)
                    nodeCounts[superType.typeID] += nodesByTypeCounts[nodeType.typeID];

                for(LGSPNode nodeHead = nodesByTypeHeads[nodeType.typeID], node = nodeHead.typeNext; node != nodeHead; node = node.typeNext)
                {
                    //
                    // count outgoing v structures
                    //

                    for(int i = 0; i < numEdgeTypes; i++)
                        for(int j = 0; j < numNodeTypes; j++)
                            outgoingVCount[i, j] = 0;

                    LGSPEdge outhead = node.outhead;
                    if(outhead != null)
                    {
                        LGSPEdge edge = outhead;
                        do
                        {
                            ITypeFramework targetType = edge.target.type;
                            nodeOutgoingCount[nodeType.typeID]++;
                            foreach(ITypeFramework edgeSuperType in edge.type.superOrSameTypes)
                            {
                                int superTypeID = edgeSuperType.typeID;
                                foreach(ITypeFramework targetSuperType in targetType.superOrSameTypes)
                                {
                                    outgoingVCount[superTypeID, targetSuperType.typeID]++;
                                }
                            }
                            edge = edge.outNext;
                        }
                        while(edge != outhead);
                    }

                    //
                    // count incoming v structures
                    //

                    for(int i = 0; i < numEdgeTypes; i++)
                        for(int j = 0; j < numNodeTypes; j++)
                            incomingVCount[i, j] = 0;

                    LGSPEdge inhead = node.inhead;
                    if(inhead != null)
                    {
                        LGSPEdge edge = inhead;
                        do
                        {
                            ITypeFramework sourceType = edge.source.type;
                            nodeIncomingCount[nodeType.typeID]++;
                            foreach(ITypeFramework edgeSuperType in edge.type.superOrSameTypes)
                            {
                                int superTypeID = edgeSuperType.typeID;
                                foreach(ITypeFramework sourceSuperType in sourceType.superOrSameTypes)
                                {
                                    incomingVCount[superTypeID, sourceSuperType.typeID]++;
                                }
                            }
                            edge = edge.inNext;
                        }
                        while(edge != inhead);
                    }

                    //
                    // finalize the counting and collect resulting local v-struct info
                    //

                    if(outhead != null)
                    {
                        LGSPEdge edge = outhead;
                        do
                        {
                            ITypeFramework targetType = edge.target.type;
                            int targetTypeID = targetType.typeID;

                            foreach(ITypeFramework edgeSuperType in edge.type.superOrSameTypes)
                            {
                                int edgeSuperTypeID = edgeSuperType.typeID;

                                foreach(ITypeFramework targetSuperType in targetType.superOrSameTypes)
                                {
                                    int targetSuperTypeID = targetSuperType.typeID;
                                    if(outgoingVCount[edgeSuperTypeID, targetSuperTypeID] > 1)
                                    {
                                        float val = (float) Math.Log(outgoingVCount[edgeSuperTypeID, targetSuperTypeID]);
                                        foreach(ITypeFramework nodeSuperType in nodeType.superOrSameTypes)
                                        {
#if MONO_MULTIDIMARRAY_WORKAROUND
                                            vstructs[((nodeSuperType.typeID * dim1size + edgeSuperTypeID) * dim2size + targetSuperTypeID) * 2
                                                + (int) LGSPDir.Out] += val;
#else
                                            vstructs[nodeSuperType.TypeID, edgeSuperTypeID, targetSuperTypeID, (int) LGSPDir.Out] += val;
#endif
                                        }
                                        outgoingVCount[edgeSuperTypeID, targetSuperTypeID] = 0;
                                    }
                                }
                            }
                            edge = edge.outNext;
                        }
                        while(edge != outhead);
                    }

                    if(inhead != null)
                    {
                        LGSPEdge edge = inhead;
                        do
                        {
                            ITypeFramework sourceType = edge.source.type;
                            int sourceTypeID = sourceType.typeID;

                            foreach(ITypeFramework edgeSuperType in edge.type.superOrSameTypes)
                            {
                                int edgeSuperTypeID = edgeSuperType.typeID;
                                foreach(ITypeFramework sourceSuperType in sourceType.superOrSameTypes)
                                {
                                    int sourceSuperTypeID = sourceSuperType.typeID;
                                    if(incomingVCount[edgeSuperTypeID, sourceSuperTypeID] > 1)
                                    {
                                        float val = (float) Math.Log(incomingVCount[edgeSuperTypeID, sourceSuperTypeID]);

                                        foreach(ITypeFramework nodeSuperType in nodeType.superOrSameTypes)
#if MONO_MULTIDIMARRAY_WORKAROUND
                                            vstructs[((nodeSuperType.typeID * dim1size + edgeSuperTypeID) * dim2size + sourceSuperTypeID) * 2
                                                + (int) LGSPDir.In] += val;
#else
                                            vstructs[nodeSuperType.TypeID, edgeSuperTypeID, sourceSuperTypeID, (int) LGSPDir.In] += val;
#endif
                                        incomingVCount[edgeSuperTypeID, sourceSuperTypeID] = 0;
                                    }
                                }
                            }
                            edge = edge.inNext;
                        }
                        while(edge != inhead);
                    }
                }
            }

            nodeLookupCosts = new float[numNodeTypes];
            for(int i = 0; i < numNodeTypes; i++)
            {
                if(nodeCounts[i] <= 1)
                    nodeLookupCosts[i] = 0;
                else
                    nodeLookupCosts[i] = (float) Math.Log(nodeCounts[i]);
            }

            // Calculate edgeCounts
            foreach(ITypeFramework edgeType in Model.EdgeModel.Types)
            {
                foreach(ITypeFramework superType in edgeType.superOrSameTypes)
                    edgeCounts[superType.typeID] += edgesByTypeCounts[edgeType.typeID];
            }

            edgeLookupCosts = new float[numEdgeTypes];
            for(int i = 0; i < numEdgeTypes; i++)
            {
                if(edgeCounts[i] <= 1)
                    edgeLookupCosts[i] = 0;
                else
                    edgeLookupCosts[i] = (float) Math.Log(edgeCounts[i]);
            }
        }
#else
        /// <summary>
        /// Analyzes the graph.
        /// The calculated data is used to generate good searchplans for the current graph.
        /// </summary>
        public void AnalyzeGraph()
        {
            int numNodeTypes = Model.NodeModel.Types.Length;
            int numEdgeTypes = Model.EdgeModel.Types.Length;

            int[,] outgoingVCount = new int[numEdgeTypes, numNodeTypes];
            int[,] incomingVCount = new int[numEdgeTypes, numNodeTypes];

#if MONO_MULTIDIMARRAY_WORKAROUND
            dim0size = numNodeTypes;
            dim1size = numEdgeTypes;
            dim2size = numNodeTypes;
            vstructs = new float[numNodeTypes * numEdgeTypes * numNodeTypes * 2];
#else
            vstructs = new float[numNodeTypes, numEdgeTypes, numNodeTypes, 2];
#endif
            nodeCounts = new int[numNodeTypes];
            edgeCounts = new int[numEdgeTypes];
            meanInDegree = new float[numNodeTypes];
            meanOutDegree = new float[numNodeTypes];

#if SCHNELLERER_ANSATZ_NUR_ANGEFANGEN
            foreach(ITypeFramework edgeType in Model.EdgeModel.Types)
            {
                /*                foreach(ITypeFramework superType in nodeType.superOrSameTypes)
                                    nodeCounts[superType.typeID] += nodesByTypeCounts[nodeType.typeID];*/

                for(LGSPEdge edgeHead = edgesByTypeHeads[edgeType.typeID], edge = edgeHead.typeNext; edge != edgeHead; edge = edge.typeNext)
                {
                    ITypeFramework sourceType = edge.source.type;
                    ITypeFramework targetType = edge.target.type;

#if MONO_MULTIDIMARRAY_WORKAROUND
                    vstructs[((sourceType.typeID * dim1size + edgeType.typeID) * dim2size + targetType.typeID) * 2 + (int) LGSPDir.Out] += val;
#else
                    vstructs[nodeSuperType.TypeID, edgeSuperTypeID, targetSuperTypeID, (int) LGSPDir.Out] += val;
#endif
                }
            }
#endif

            foreach(NodeType nodeType in Model.NodeModel.Types)
            {
                foreach(NodeType superType in nodeType.SuperOrSameTypes)
                    nodeCounts[superType.TypeID] += nodesByTypeCounts[nodeType.TypeID];

                for(LGSPNode nodeHead = nodesByTypeHeads[nodeType.TypeID], node = nodeHead.lgspTypeNext; node != nodeHead; node = node.lgspTypeNext)
                {
                    //
                    // count outgoing v structures
                    //

                    for(int i = 0; i < numEdgeTypes; i++)
                        for(int j = 0; j < numNodeTypes; j++)
                            outgoingVCount[i, j] = 0;

                    LGSPEdge outhead = node.lgspOuthead;
                    if(outhead != null)
                    {
                        LGSPEdge edge = outhead;
                        do
                        {
                            NodeType targetType = edge.lgspTarget.lgspType;
                            meanOutDegree[nodeType.TypeID]++;
                            foreach(EdgeType edgeSuperType in edge.lgspType.superOrSameTypes)
                            {
                                int superTypeID = edgeSuperType.TypeID;
                                foreach(NodeType targetSuperType in targetType.SuperOrSameTypes)
                                {
                                    outgoingVCount[superTypeID, targetSuperType.TypeID]++;
                                }
                            }
                            edge = edge.lgspOutNext;
                        }
                        while(edge != outhead);
                    }

                    //
                    // count incoming v structures
                    //

                    for(int i = 0; i < numEdgeTypes; i++)
                        for(int j = 0; j < numNodeTypes; j++)
                            incomingVCount[i, j] = 0;

                    LGSPEdge inhead = node.lgspInhead;
                    if(inhead != null)
                    {
                        LGSPEdge edge = inhead;
                        do
                        {
                            NodeType sourceType = edge.lgspSource.lgspType;
                            meanInDegree[nodeType.TypeID]++;
                            foreach(EdgeType edgeSuperType in edge.lgspType.superOrSameTypes)
                            {
                                int superTypeID = edgeSuperType.TypeID;
                                foreach(NodeType sourceSuperType in sourceType.superOrSameTypes)
                                {
                                    incomingVCount[superTypeID, sourceSuperType.TypeID]++;
                                }
                            }
                            edge = edge.lgspInNext;
                        }
                        while(edge != inhead);
                    }

                    //
                    // finalize the counting and collect resulting local v-struct info
                    //

                    if(outhead != null)
                    {
                        LGSPEdge edge = outhead;
                        do
                        {
                            NodeType targetType = edge.lgspTarget.lgspType;
                            int targetTypeID = targetType.TypeID;

                            foreach(EdgeType edgeSuperType in edge.lgspType.superOrSameTypes)
                            {
                                int edgeSuperTypeID = edgeSuperType.TypeID;

                                foreach(NodeType targetSuperType in targetType.superOrSameTypes)
                                {
                                    int targetSuperTypeID = targetSuperType.TypeID;
                                    if(outgoingVCount[edgeSuperTypeID, targetSuperTypeID] > 0)
                                    {
//                                        float val = (float) Math.Log(outgoingVCount[edgeSuperTypeID, targetSuperTypeID]);     // > 1 im if
                                        float val = outgoingVCount[edgeSuperTypeID, targetSuperTypeID];
                                        foreach(NodeType nodeSuperType in nodeType.superOrSameTypes)
                                        {
#if MONO_MULTIDIMARRAY_WORKAROUND
                                            vstructs[((nodeSuperType.TypeID * dim1size + edgeSuperTypeID) * dim2size + targetSuperTypeID) * 2
                                                + (int) LGSPDir.Out] += val;
#else
                                            vstructs[nodeSuperType.TypeID, edgeSuperTypeID, targetSuperTypeID, (int) LGSPDir.Out] += val;
#endif
                                        }
                                        outgoingVCount[edgeSuperTypeID, targetSuperTypeID] = 0;
                                    }
                                }
                            }
                            edge = edge.lgspOutNext;
                        }
                        while(edge != outhead);
                    }

                    if(inhead != null)
                    {
                        LGSPEdge edge = inhead;
                        do
                        {
                            NodeType sourceType = edge.lgspSource.lgspType;
                            int sourceTypeID = sourceType.TypeID;

                            foreach(EdgeType edgeSuperType in edge.lgspType.superOrSameTypes)
                            {
                                int edgeSuperTypeID = edgeSuperType.TypeID;
                                foreach(NodeType sourceSuperType in sourceType.superOrSameTypes)
                                {
                                    int sourceSuperTypeID = sourceSuperType.TypeID;
                                    if(incomingVCount[edgeSuperTypeID, sourceSuperTypeID] > 0)
                                    {
//                                        float val = (float) Math.Log(incomingVCount[edgeSuperTypeID, sourceSuperTypeID]);     // > 1 im if
                                        float val = incomingVCount[edgeSuperTypeID, sourceSuperTypeID];
                                        foreach(NodeType nodeSuperType in nodeType.superOrSameTypes)
#if MONO_MULTIDIMARRAY_WORKAROUND
                                            vstructs[((nodeSuperType.TypeID * dim1size + edgeSuperTypeID) * dim2size + sourceSuperTypeID) * 2
                                                + (int) LGSPDir.In] += val;
#else
                                            vstructs[nodeSuperType.TypeID, edgeSuperTypeID, sourceSuperTypeID, (int) LGSPDir.In] += val;
#endif
                                        incomingVCount[edgeSuperTypeID, sourceSuperTypeID] = 0;
                                    }
                                }
                            }
                            edge = edge.lgspInNext;
                        }
                        while(edge != inhead);
                    }
                }

                int numCompatibleNodes = nodeCounts[nodeType.TypeID];
                if(numCompatibleNodes != 0)
                {
                    meanOutDegree[nodeType.TypeID] /= numCompatibleNodes;
                    meanInDegree[nodeType.TypeID] /= numCompatibleNodes;
                }
            }


/*            // Calculate nodeCounts
            foreach(ITypeFramework nodeType in Model.NodeModel.Types)
            {
                foreach(ITypeFramework superType in edgeType.superOrSameTypes)
                    nodeCounts[superType.typeID] += nodesByTypeCounts[nodeType.typeID];
            }*/

            // Calculate edgeCounts
            foreach(EdgeType edgeType in Model.EdgeModel.Types)
            {
                foreach(EdgeType superType in edgeType.superOrSameTypes)
                    edgeCounts[superType.TypeID] += edgesByTypeCounts[edgeType.TypeID];
            }
        }
#endif

        /// <summary>
        /// Checks if the matching state flags in the graph are not set, as they should be in case no matching is undereway
        /// </summary>
        public void CheckEmptyFlags()
        {
            foreach (NodeType nodeType in Model.NodeModel.Types)
            {
                for (LGSPNode nodeHead = nodesByTypeHeads[nodeType.TypeID], node = nodeHead.lgspTypeNext; node != nodeHead; node = node.lgspTypeNext)
                {
                    if (node.lgspFlags != 0 && node.lgspFlags != 1)
                        throw new Exception("Internal error: No matching underway, but matching state still in graph.");
                }
            }

            foreach (EdgeType edgeType in Model.EdgeModel.Types)
            {
                for (LGSPEdge edgeHead = edgesByTypeHeads[edgeType.TypeID], edge = edgeHead.lgspTypeNext; edge != edgeHead; edge = edge.lgspTypeNext)
                {
                    if (edge.lgspFlags != 0 && edge.lgspFlags != 1)
                        throw new Exception("Internal error: No matching underway, but matching state still in graph.");
                }
            }
        }

#if CHECK_RINGLISTS
        /// <summary>
        /// Checks if the given node is already available in its type ringlist
        /// </summary>
        public void CheckNodeAlreadyInTypeRinglist(LGSPNode node)
        {
            LGSPNode head = nodesByTypeHeads[node.lgspType.TypeID];
            LGSPNode cur = head.lgspTypeNext;
            while(cur != head)
            {
                if(cur == node) throw new Exception("Internal error: Node already available in ringlist");
                cur = cur.lgspTypeNext;
            }
            cur = head.lgspTypePrev;
            while(cur != head)
            {
                if(cur == node) throw new Exception("Internal error: Node already available in ringlist");
                cur = cur.lgspTypePrev;
            }
        }

        /// <summary>
        /// Checks if the given edge is already available in its type ringlist
        /// </summary>
        public void CheckEdgeAlreadyInTypeRinglist(LGSPEdge edge)
        {
            LGSPEdge head = edgesByTypeHeads[edge.lgspType.TypeID];
            LGSPEdge cur = head.lgspTypeNext;
            while(cur != head)
            {
                if(cur == edge) throw new Exception("Internal error: Edge already available in ringlist");
                cur = cur.lgspTypeNext;
            }
            cur = head.lgspTypePrev;
            while(cur != head)
            {
                if(cur == edge) throw new Exception("Internal error: Edge already available in ringlist");
                cur = cur.lgspTypePrev;
            }
        }

        /// <summary>
        /// Checks whether the type ringlist starting at the given head node is broken.
        /// Use for debugging purposes.
        /// </summary>
        /// <param name="node">The node head to be checked.</param>
        public void CheckTypeRinglistBroken(LGSPNode head)
        {
            LGSPNode headNext = head.lgspTypeNext;
            if(headNext == null) throw new Exception("Internal error: Ringlist broken");
            LGSPNode cur = headNext;
            LGSPNode next;
            while(cur != head)
            {
                if(cur != cur.lgspTypeNext.lgspTypePrev) throw new Exception("Internal error: Ringlist out of order");
                next = cur.lgspTypeNext;
                if(next == null) throw new Exception("Internal error: Ringlist broken");
                if(next == headNext) throw new Exception("Internal error: Ringlist loops bypassing head");
                cur = next;
            }

            LGSPNode headPrev = head.lgspTypePrev;
            if(headPrev == null) throw new Exception("Internal error: Ringlist broken");
            cur = headPrev;
            LGSPNode prev;
            while(cur != head)
            {
                if(cur != cur.lgspTypePrev.lgspTypeNext) throw new Exception("Internal error: Ringlist out of order");
                prev = cur.lgspTypePrev;
                if(prev == null) throw new Exception("Internal error: Ringlist broken");
                if(prev == headPrev) throw new Exception("Internal error: Ringlist loops bypassing head");
                cur = prev;
            }
        }

        /// <summary>
        /// Checks whether the type ringlist starting at the given head edge is broken.
        /// Use for debugging purposes.
        /// </summary>
        /// <param name="node">The edge head to be checked.</param>
        public void CheckTypeRinglistBroken(LGSPEdge head)
        {
            LGSPEdge headNext = head.lgspTypeNext;
            if(headNext == null) throw new Exception("Internal error: Ringlist broken");
            LGSPEdge cur = headNext;
            LGSPEdge next;
            while(cur != head)
            {
                if(cur != cur.lgspTypeNext.lgspTypePrev) throw new Exception("Internal error: Ringlist out of order");
                next = cur.lgspTypeNext;
                if(next == null) throw new Exception("Internal error: Ringlist broken");
                if(next == headNext) throw new Exception("Internal error: Ringlist loops bypassing head");
                cur = next;
            }

            LGSPEdge headPrev = head.lgspTypePrev;
            if(headPrev == null) throw new Exception("Internal error: type Ringlist broken");
            cur = headPrev;
            LGSPEdge prev;
            while(cur != head)
            {
                if(cur != cur.lgspTypePrev.lgspTypeNext) throw new Exception("Internal error: Ringlist out of order");
                prev = cur.lgspTypePrev;
                if(prev == null) throw new Exception("Internal error: Ringlist broken");
                if(prev == headPrev) throw new Exception("Internal error: Ringlist loops bypassing head");
                cur = prev;
            }
        }

        /// <summary>
        /// Checks whether the type ringlists are broken.
        /// Use for debugging purposes.
        /// </summary>
        public void CheckTypeRinglistsBroken()
        {
            foreach(LGSPNode head in nodesByTypeHeads)
                CheckTypeRinglistBroken(head);
            foreach(LGSPEdge head in edgesByTypeHeads)
                CheckTypeRinglistBroken(head);
        }
                    
        /// <summary>
        /// Checks whether the incoming or outgoing ringlists of the given node are broken.
        /// Use for debugging purposes.
        /// </summary>
        /// <param name="node">The node to be checked.</param>
        public void CheckInOutRinglistsBroken(LGSPNode node)
        {
            LGSPEdge inHead = node.lgspInhead;
            if(inHead != null)
            {
                LGSPEdge headNext = inHead.lgspInNext;
                if(headNext == null) throw new Exception("Internal error: in Ringlist broken");
                LGSPEdge cur = headNext;
                LGSPEdge next;
                while(cur != inHead)
                {
                    if(cur != cur.lgspInNext.lgspInPrev) throw new Exception("Internal error: Ringlist out of order");
                    next = cur.lgspInNext;
                    if(next == null) throw new Exception("Internal error: Ringlist broken");
                    if(next == headNext) throw new Exception("Internal error: Ringlist loops bypassing head");
                    cur = next;
                }
            }
            if(inHead != null)
            {
                LGSPEdge headPrev = inHead.lgspInPrev;
                if(headPrev == null) throw new Exception("Internal error: Ringlist broken");
                LGSPEdge cur = headPrev;
                LGSPEdge prev;
                while(cur != inHead)
                {
                    if(cur != cur.lgspInPrev.lgspInNext) throw new Exception("Internal error: Ringlist out of order");
                    prev = cur.lgspInPrev;
                    if(prev == null) throw new Exception("Internal error: Ringlist broken");
                    if(prev == headPrev) throw new Exception("Internal error: Ringlist loops bypassing head");
                    cur = prev;
                }
            }
            LGSPEdge outHead = node.lgspOuthead;
            if(outHead != null)
            {
                LGSPEdge headNext = outHead.lgspOutNext;
                if(headNext == null) throw new Exception("Internal error: Ringlist broken");
                LGSPEdge cur = headNext;
                LGSPEdge next;
                while(cur != outHead)
                {
                    if(cur != cur.lgspOutNext.lgspOutPrev) throw new Exception("Internal error: Ringlist out of order");
                    next = cur.lgspOutNext;
                    if(next == null) throw new Exception("Internal error: Ringlist broken");
                    if(next == headNext) throw new Exception("Internal error: Ringlist loops bypassing head");
                    cur = next;
                }
            }
            if(outHead != null)
            {
                LGSPEdge headPrev = outHead.lgspOutPrev;
                if(headPrev == null) throw new Exception("Internal error: Ringlist broken");
                LGSPEdge cur = headPrev;
                LGSPEdge prev;
                while(cur != outHead)
                {
                    if(cur != cur.lgspOutPrev.lgspOutNext) throw new Exception("Internal error: Ringlist out of order");
                    prev = cur.lgspOutPrev;
                    if(prev == null) throw new Exception("Internal error: Ringlist broken");
                    if(prev == headPrev) throw new Exception("Internal error: Ringlist loops bypassing head");
                    cur = prev;
                }
            }
        }
#endif

        /// <summary>
        /// Mature a graph.
        /// This method should be invoked after adding all nodes and edges to the graph.
        /// The backend may implement analyses on the graph to speed up matching etc.
        /// The graph may not be modified by this function.
        /// </summary>
        public override void Mature()
        {
            // Nothing to be done here, yet
        }

        /// <summary>
        /// Does graph-backend dependent stuff.
        /// </summary>
        /// <param name="args">Any kind of paramteres for the stuff to do</param>
        public override void Custom(params object[] args)
        {
            if(args.Length == 0) goto invalidCommand;

            switch((String) args[0])
            {
                case "analyze":
                case "analyze_graph":
                {
                    int startticks = Environment.TickCount;
                    AnalyzeGraph();
                    Console.WriteLine("Graph '{0}' analyzed in {1} ms.", name, Environment.TickCount - startticks);
                    return;
                }

                case "optimizereuse":
                    if(args.Length != 2)
                        throw new ArgumentException("Usage: optimizereuse <bool>\n"
                                + "If <bool> == true, deleted elements may be reused in a rewrite.\n"
                                + "As a result new elements may not be discriminable anymore from\n"
                                + "already deleted elements using object equality, hash maps, etc.");

                    if(!bool.TryParse((String) args[1], out reuseOptimization))
                        throw new ArgumentException("Illegal bool value specified: \"" + (String) args[1] + "\"");
                    return;

                case "set_max_matches":
                    if(args.Length != 2)
                        throw new ArgumentException("Usage: set_max_matches <integer>\nIf <integer> <= 0, all matches will be matched.");

                    int newMaxMatches;
                    if(!int.TryParse((String) args[1], out newMaxMatches))
                        throw new ArgumentException("Illegal integer value specified: \"" + (String) args[1] + "\"");
                    MaxMatches = newMaxMatches;
                    return;
            }

invalidCommand:
            throw new ArgumentException("Possible commands:\n"
                + "- analyze: Analyzes the graph. The generated information can then be\n"
                + "     used by Actions implementations to optimize the pattern matching\n"
                + "- optimizereuse: Sets whether deleted elements may be reused in a rewrite\n"
                + "     (default: true)\n"
                + "- set_max_matches: Sets the maximum number of matches to be found\n"
                + "     during matching\n");
        }

        /// <summary>
        /// Duplicates a graph.
        /// The new graph will use the same model and backend as the other
        /// Open transaction data will not be cloned.
        /// </summary>
        /// <param name="newName">Name of the new graph.</param>
        /// <returns>A new graph with the same structure as this graph.</returns>
        public override IGraph Clone(String newName)
        {
            return new LGSPGraph(this, newName);
        }
    }
}
