/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

//#define LOG_TRANSACTION_HANDLING
//#define CHECK_RINGLISTS

using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// A class for managing graph transactions.
    /// </summary>
    public class LGSPTransactionManager : ITransactionManager
    {
        private readonly List<IUndoItem> undoItems = new List<IUndoItem>();

        private IEdge currentlyRedirectedEdge;
        private bool recording = false;
        private bool paused = false; // only of interest if recording==true
        private bool reuseOptimizationBackup = false; // old value from graph, to be restored after outermost transaction completed
        private bool undoing = false;
        private bool wasVisitedFreeRecorded = false;
        private readonly Dictionary<int, bool> visitedAllocationsWhilePaused = new Dictionary<int, bool>();
        private bool wasGraphChanged = false;
        private readonly LGSPGraphProcessingEnvironment procEnv;

#if LOG_TRANSACTION_HANDLING
        private System.IO.StreamWriter writer;
        private int transactionLevel = 0;
#endif

        public LGSPTransactionManager(LGSPGraphProcessingEnvironment procEnv)
        {
            this.procEnv = procEnv;

#if LOG_TRANSACTION_HANDLING
            writer = new System.IO.StreamWriter(procEnv.graph.Name + "_transaction_log.txt");
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
            procEnv.graph.OnChangingObjectAttribute += ChangingElementAttribute;
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
            procEnv.graph.OnChangingObjectAttribute -= ChangingElementAttribute;
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
                if(lastItem is LGSPUndoTransactionStarted)
                    writer.WriteLine("TransactionStarted");
                else if(lastItem is LGSPUndoElemAdded)
                {
                    LGSPUndoElemAdded item = (LGSPUndoElemAdded)lastItem;
                    if(item._elem is INode)
                    {
                        INode node = (INode)item._elem;
                        writer.WriteLine("ElementAdded: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(node) + ":" + node.Type.Name);
                    }
                    else if(item._elem is IEdge)
                    {
                        IEdge edge = (IEdge)item._elem;
                        writer.WriteLine("ElementAdded: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Source) + " -" + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge) + ":" + edge.Type.Name + " ->" + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Target));
                    }
                    else
                    {
                        IObject obj = (IObject)item._elem;
                        writer.WriteLine("ElementAdded: hash" + obj.GetHashCode() + ":" + obj.Type.Name);
                    }
                }
                else if(lastItem is LGSPUndoElemRemoved)
                {
                    LGSPUndoElemRemoved item = (LGSPUndoElemRemoved)lastItem;
                    if(item._elem is INode)
                    {
                        INode node = (INode)item._elem;
                        writer.WriteLine("RemovingElement: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(node) + ":" + node.Type.Name);
                    }
                    else if(item._elem is IEdge)
                    {
                        IEdge edge = (IEdge)item._elem;
                        writer.WriteLine("RemovingElement: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Source) + " -" + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge) + ":" + edge.Type.Name + "-> " + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Target));
                    }
                    else
                    {
                        IObject obj = (IObject)item._elem;
                        writer.WriteLine("RemovingElement: hash" + obj.GetHashCode() + ":" + obj.Type.Name);
                    }
                }
                else if(lastItem is LGSPUndoAttributeChanged)
                {
                    LGSPUndoAttributeChanged item = (LGSPUndoAttributeChanged)lastItem;
                    if(item._elem is IGraphElement)
                        writer.WriteLine("ChangingElementAttribute: " + ((LGSPNamedGraph)procEnv.graph).GetElementName((IGraphElement)item._elem) + ":" + item._elem.Type.Name + "." + item._attrType.Name);
                    else
                        writer.WriteLine("ChangingElementAttribute: hash" + ((IObject)item._elem).GetHashCode() + ":" + item._elem.Type.Name + "." + item._attrType.Name);
                }
                else if(lastItem is LGSPUndoElemRetyped)
                {
                    LGSPUndoElemRetyped item = (LGSPUndoElemRetyped)lastItem;
                    writer.WriteLine("RetypingElement: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(item._newElem) + ":" + item._newElem.Type.Name + "<" + ((LGSPNamedGraph)procEnv.graph).GetElementName(item._oldElem) + ":" + item._oldElem.Type.Name + ">");
                }
                else if(lastItem is LGSPUndoElemRedirecting)
                {
                    LGSPUndoElemRedirecting item = (LGSPUndoElemRedirecting)lastItem;
                    writer.WriteLine("RedirectingEdge: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(item._edge) + " before undoing removal");
                }
                else if(lastItem is LGSPUndoVisitedAlloc)
                {
                    LGSPUndoVisitedAlloc item = (LGSPUndoVisitedAlloc)lastItem;
                    writer.WriteLine("VisitedAlloc: " + item._visitorID);
                }
                else if(lastItem is LGSPUndoVisitedFree)
                {
                    LGSPUndoVisitedFree item = (LGSPUndoVisitedFree)lastItem;
                    writer.WriteLine("VisitedFree: " + item._visitorID);
                }
                else if(lastItem is LGSPUndoSettingVisited)
                {
                    LGSPUndoSettingVisited item = (LGSPUndoSettingVisited)lastItem;
                    writer.WriteLine("SettingVisited: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(item._elem) + ".visited[" + item._visitorID + "]");
                }
                else if(lastItem is LGSPUndoGraphChange)
                {
                    LGSPUndoGraphChange item = (LGSPUndoGraphChange)lastItem;
                    writer.WriteLine("GraphChange: to previous " + item._oldGraph.Name);
                }
#endif
                if(wasGraphChanged)
                {
                    if(lastItem is LGSPUndoGraphChange)
                    {
                        if(undoItems.Count - 2 >= 0 && undoItems[undoItems.Count - 2] is LGSPUndoGraphChange)
                        {
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
                undoItems.Add(new LGSPUndoElemAdded(elem, procEnv));

#if LOG_TRANSACTION_HANDLING
            if(elem is INode)
            {
                INode node = (INode)elem;
                writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "ElementAdded: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(node) + ":" + node.Type.Name);
            }
            else
            {
                IEdge edge = (IEdge)elem;
                writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "ElementAdded: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Source) + " -" + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge) + ":" + edge.Type.Name + "-> " + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Target));
            }
#endif
        }

        public void RemovingElement(IGraphElement elem)
        {
#if LOG_TRANSACTION_HANDLING
            if(elem is INode)
            {
                INode node = (INode)elem;
                writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "RemovingElement: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(node) + ":" + node.Type.Name);
            }
            else
            {
                IEdge edge = (IEdge)elem;
                writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "RemovingElement: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Source) + " -" + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge) + ":" + edge.Type.Name + "-> " + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge.Target));
            }
#endif
            if(recording && !paused && !undoing)
            {
                undoItems.Add(new LGSPUndoElemRemoved(elem, procEnv));
                if(object.ReferenceEquals(elem, currentlyRedirectedEdge))
                {
                    LGSPEdge edge = (LGSPEdge)elem;
                    undoItems.Add(new LGSPUndoElemRedirecting(edge, edge.lgspSource, edge.lgspTarget, procEnv));
                    currentlyRedirectedEdge = null;
                }
            }
        }

        public void ChangingElementAttribute(IAttributeBearer owner, AttributeType attrType,
                AttributeChangeType changeType, object newValue, object keyValue)
        {
#if LOG_TRANSACTION_HANDLING
            if(owner is IGraphElement)
                writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "ChangingElementAttribute: " + ((LGSPNamedGraph)procEnv.graph).GetElementName((IGraphElement)owner) + ":" + owner.Type.Name + "." + attrType.Name);
            else
                writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "ChangingElementAttribute: hash" + ((IObject)owner).GetHashCode() + ":" + owner.Type.Name + "." + attrType.Name);
#endif
            if(recording && !paused && !undoing)
                undoItems.Add(new LGSPUndoAttributeChanged(owner, attrType, changeType, newValue, keyValue, procEnv));
        }

        public void RetypingElement(IGraphElement oldElem, IGraphElement newElem)
        {
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "RetypingElement: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(newElem) + ":" + newElem.Type.Name + "<" + ((LGSPNamedGraph)procEnv.graph).GetElementName(oldElem) + ":" + oldElem.Type.Name + ">");
#endif
            if(recording && !paused && !undoing)
                undoItems.Add(new LGSPUndoElemRetyped(oldElem, newElem, procEnv));
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
                    undoItems.Add(new LGSPUndoSettingVisited(elem, visitorID, oldValue, procEnv));
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

        public void ExternalTypeChanged(IUndoItem item)
        {
            if(recording && !paused && !undoing)
                undoItems.Add(item);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < undoItems.Count; ++i)
            {
                IUndoItem item = undoItems[i];
                sb.Append(item.ToString());
                sb.Append(" @");
                sb.Append(i.ToString());
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
