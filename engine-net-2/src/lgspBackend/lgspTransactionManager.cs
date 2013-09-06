/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
    /// <summary>
    /// A class for managing graph transactions.
    /// </summary>
    public class LGSPTransactionManager : ITransactionManager
    {
        private List<IUndoItem> undoItems = new List<IUndoItem>();
        private IEdge currentlyRedirectedEdge;
        private bool recording = false;
        private bool paused = false; // only of interest if recording==true
        private bool reuseOptimizationBackup = false; // old value from graph, to be restored after outermost transaction completed
        private bool undoing = false;
        private bool wasVisitedFreeRecorded = false;
        private Dictionary<int, bool> visitedAllocationsWhilePaused = new Dictionary<int, bool>();
        private bool wasGraphChanged = false;
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
            procEnv.graph.OnRedirectingEdge += RedirectingEdge;
            procEnv.graph.OnVisitedAlloc += VisitedAlloc;
            procEnv.graph.OnVisitedFree += VisitedFree;
            procEnv.graph.OnSettingVisited += SettingVisited;
            procEnv.OnSwitchingToSubgraph += SwitchToGraph;
            procEnv.OnReturnedFromSubgraph += ReturnFromGraph;

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
            procEnv.graph.OnRedirectingEdge -= RedirectingEdge;
            procEnv.graph.OnVisitedAlloc -= VisitedAlloc;
            procEnv.graph.OnVisitedFree -= VisitedFree;
            procEnv.graph.OnSettingVisited -= SettingVisited;
            procEnv.OnSwitchingToSubgraph -= SwitchToGraph;
            procEnv.OnReturnedFromSubgraph -= ReturnFromGraph;

            recording = false;
            procEnv.graph.ReuseOptimization = reuseOptimizationBackup;
        }

        public int Start()
        {
            // possible extension: transactions within pauses, nesting of pauses with transactions, would require a stack of transactions
            if(paused)
                throw new Exception("Transaction handling is currently paused, can't start a transaction!");
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine(new String(' ', transactionLevel) + "Start");
            writer.Flush();
            ++transactionLevel;
#endif
            if(procEnv.Recorder != null)
                procEnv.Recorder.TransactionStart(undoItems.Count);

            if(!recording)
                SubscribeEvents();

            int count = undoItems.Count;
            undoItems.Add(new LGSPUndoTransactionStarted());
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
            visitedAllocationsWhilePaused.Clear();
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
            // otherwise we might need to undo it because a transaction enclosing this transaction failed
            if(transactionID == 0)
            {
                if(wasVisitedFreeRecorded && undoItems.Count>0)
                {
                    if(wasGraphChanged)
                    {
                        undoing = true;
                        procEnv.SwitchToSubgraph(procEnv.Graph);
                    }

                    for(int i = undoItems.Count - 1; i >= 0; --i)
                    {
                        IUndoItem curItem = undoItems[i];
                        if(curItem is LGSPUndoVisitedFree)
                            procEnv.graph.UnreserveVisitedFlag(((LGSPUndoVisitedFree)curItem)._visitorID);
                        else if(curItem is LGSPUndoGraphChange)
                            curItem.DoUndo(procEnv);
                    }
                    
                    if(wasGraphChanged)
                    {
                        procEnv.ReturnFromSubgraph();
                        undoing = false;
                    }
                }

                undoItems.Clear();
                UnsubscribeEvents();
                wasVisitedFreeRecorded = false;
                wasGraphChanged = false;
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
            if(wasGraphChanged)
                procEnv.SwitchToSubgraph(procEnv.Graph);

            while(undoItems.Count > transactionID)
            {
                IUndoItem lastItem = undoItems[undoItems.Count - 1];
#if LOG_TRANSACTION_HANDLING
                writer.Write(new String(' ', transactionLevel) + "rolling back " + undoItems.Count + " - ");
                if(lastItem is LGSPUndoTransactionStarted) {
                    writer.WriteLine("TransactionStarted");
                } else if(lastItem is LGSPUndoElemAdded) {
                    LGSPUndoElemAdded item = (LGSPUndoElemAdded)lastItem;
                    if(item._elem is INode) {
                        INode node = (INode)item._elem;
                        writer.WriteLine("ElementAdded: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(node) + ":" + node.Type.Name);
                    } else {
                        IEdge edge = (IEdge)item._elem;
                        writer.WriteLine("ElementAdded: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Source) + " -" + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge) + ":" + edge.Type.Name + " ->" + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Target));
                    }
                } else if(lastItem is LGSPUndoElemRemoved) {
                    LGSPUndoElemRemoved item = (LGSPUndoElemRemoved)lastItem;
                    if(item._elem is INode) {
                        INode node = (INode)item._elem;
                        writer.WriteLine("RemovingElement: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(node) + ":" + node.Type.Name);
                    } else {
                        IEdge edge = (IEdge)item._elem;
                        writer.WriteLine("RemovingElement: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Source) + " -" + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge) + ":" + edge.Type.Name + "-> " + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Target));
                    }
                } else if(lastItem is LGSPUndoAttributeChanged) {
                    LGSPUndoAttributeChanged item = (LGSPUndoAttributeChanged)lastItem;
                    writer.WriteLine("ChangingElementAttribute: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(item._elem) + ":" + item._elem.Type.Name + "." + item._attrType.Name);
                } else if(lastItem is LGSPUndoElemRetyped) {
                    LGSPUndoElemRetyped item = (LGSPUndoElemRetyped)lastItem;
                    writer.WriteLine("RetypingElement: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(item._newElem) + ":" + item._newElem.Type.Name + "<" + ((LGSPNamedGraph)procEnv.graph).GetElementName(item._oldElem) + ":" + item._oldElem.Type.Name + ">");
                } else if(lastItem is LGSPUndoElemRedirecting) {
                    LGSPUndoElemRedirecting item = (LGSPUndoElemRedirecting)lastItem;
                    writer.WriteLine("RedirectingEdge: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(item._edge) + " before undoing removal");
                } else if(lastItem is LGSPUndoVisitedAlloc) {
                    LGSPUndoVisitedAlloc item = (LGSPUndoVisitedAlloc)lastItem;
                    writer.WriteLine("VisitedAlloc: " + item._visitorID);
                } else if(lastItem is LGSPUndoVisitedFree) {
                    LGSPUndoVisitedFree item = (LGSPUndoVisitedFree)lastItem;
                    writer.WriteLine("VisitedFree: " + item._visitorID);
                } else if(lastItem is LGSPUndoSettingVisited) {
                    LGSPUndoSettingVisited item = (LGSPUndoSettingVisited)lastItem;
                    writer.WriteLine("SettingVisited: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(item._elem) + ".visited[" + item._visitorID + "]");
                } else if(lastItem is LGSPUndoGraphChange) {
                    LGSPUndoGraphChange item = (LGSPUndoGraphChange)lastItem;
                    writer.WriteLine("GraphChange: to previous " + item._oldGraph.Name);
                }
#endif
                if(wasGraphChanged) {
                    if(lastItem is LGSPUndoGraphChange) {
                        if(undoItems.Count - 2 >= 0 && undoItems[undoItems.Count - 2] is LGSPUndoGraphChange) {
                            undoItems.RemoveAt(undoItems.Count - 1);
                            continue; // skip graph change without effect to preceeding graph change
                        }
                    }
                }

                lastItem.DoUndo(procEnv);
                undoItems.RemoveAt(undoItems.Count - 1);
            }

            if(wasGraphChanged)
                procEnv.ReturnFromSubgraph();
            undoing = false;

            if(transactionID == 0)
            {
                UnsubscribeEvents();
                wasVisitedFreeRecorded = false;
                wasGraphChanged = false;
            }

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
            if(recording && !paused && !undoing)
                undoItems.Add(new LGSPUndoElemAdded(elem));

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
            if(recording && !paused && !undoing)
            {
                undoItems.Add(new LGSPUndoElemRemoved(elem, procEnv));
                if(Object.ReferenceEquals(elem, currentlyRedirectedEdge))
                {
                    LGSPEdge edge = (LGSPEdge)elem;
                    undoItems.Add(new LGSPUndoElemRedirecting(edge, edge.lgspSource, edge.lgspTarget));
                    currentlyRedirectedEdge = null;
                }
            }
        }

        public void ChangingElementAttribute(IGraphElement elem, AttributeType attrType,
                AttributeChangeType changeType, Object newValue, Object keyValue)
        {
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "ChangingElementAttribute: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(elem) + ":" + elem.Type.Name + "." + attrType.Name);
#endif
            if(recording && !paused && !undoing)
                undoItems.Add(new LGSPUndoAttributeChanged(elem, attrType, changeType, newValue, keyValue));
        }

        public void RetypingElement(IGraphElement oldElem, IGraphElement newElem)
        {
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "RetypingElement: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(newElem) + ":" + newElem.Type.Name + "<" + ((LGSPNamedGraph)procEnv.graph).GetElementName(oldElem) + ":" + oldElem.Type.Name + ">");
#endif
            if(recording && !paused && !undoing)
                undoItems.Add(new LGSPUndoElemRetyped(oldElem, newElem));
        }

        public void RedirectingEdge(IEdge edge)
        {
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "RedirectingEdge: " + " -" + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge) + "-> " );
#endif
            if(recording && !paused && !undoing)
                currentlyRedirectedEdge = edge;
        }

        public void VisitedAlloc(int visitorID)
        {
            if(recording && !paused && !undoing)
                undoItems.Add(new LGSPUndoVisitedAlloc(visitorID));
            if(paused)
                visitedAllocationsWhilePaused[visitorID] = true;

#if LOG_TRANSACTION_HANDLING
            writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "VisitedAlloc: " + visitorID);
#endif
        }

        public void VisitedFree(int visitorID)
        {
            if(recording && !paused && !undoing)
            {
                undoItems.Add(new LGSPUndoVisitedFree(visitorID));
                procEnv.graph.ReserveVisitedFlag(visitorID);
                wasVisitedFreeRecorded = true;
            }
            if(paused && !visitedAllocationsWhilePaused.ContainsKey(visitorID))
                throw new Exception("A vfree in a transaction pause can only free a visited flag that was allocated during that pause!");

#if LOG_TRANSACTION_HANDLING
            writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "VisitedFree: " + visitorID);
#endif
        }

        public void SettingVisited(IGraphElement elem, int visitorID, bool newValue)
        {
            if(recording && !paused && !undoing)
            {
                bool oldValue = procEnv.graph.IsVisited(elem, visitorID);
                if(newValue != oldValue)
                    undoItems.Add(new LGSPUndoSettingVisited(elem, visitorID, oldValue));
            }

#if LOG_TRANSACTION_HANDLING
            writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "SettingVisited: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(elem) + ".visited[" + visitorID + "] = " + (newValue ? "true" : "false"));
#endif
        }

        public void SwitchToGraph(IGraph newGraph)
        {
            IGraph oldGraph = procEnv.graph;
            if(recording && !undoing)
            {
                undoItems.Add(new LGSPUndoGraphChange(oldGraph));
                wasGraphChanged = true;
            }

#if LOG_TRANSACTION_HANDLING
            writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "SwitchToGraph: " + newGraph.Name + ", from " + oldGraph.Name);
#endif
        }

        public void ReturnFromGraph(IGraph oldGraph)
        {
            IGraph newGraph = procEnv.graph;
            if(recording && !undoing)
            {
                undoItems.Add(new LGSPUndoGraphChange(oldGraph));
                wasGraphChanged = true;
            }

#if LOG_TRANSACTION_HANDLING
            writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "ReturnFromGraph: " + newGraph.Name + ", back from " + oldGraph.Name);
#endif
        }

        public bool IsActive { get { return recording; } }
    }
}
