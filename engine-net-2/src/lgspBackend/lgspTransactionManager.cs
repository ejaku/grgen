/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
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
        private LinkedList<IUndoItem> undoItems = new LinkedList<IUndoItem>();
        private IEdge currentlyRedirectedEdge;
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
            procEnv.graph.OnRedirectingEdge += RedirectingEdge;

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

            recording = false;
            procEnv.graph.ReuseOptimization = reuseOptimizationBackup;
        }

        public int Start()
        {
            // TODO: allow transactions within pauses, nesting of pauses with transactions
            // this requires a stack of transactions; not difficult, but would eat a bit of performance,
            // so only to be done if there is demand by users (for the majority of tasks it should not be needed)
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
                } else if(undoItems.Last.Value is LGSPUndoElemRedirecting) {
                    LGSPUndoElemRedirecting item = (LGSPUndoElemRedirecting)undoItems.Last.Value;
                    writer.WriteLine("RedirectingEdge: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(item._edge) + " before undoing removal");
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
            if(recording && !paused && !undoing)
                undoItems.AddLast(new LGSPUndoElemAdded(elem));

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
                undoItems.AddLast(new LGSPUndoElemRemoved(elem, procEnv));
                if(Object.ReferenceEquals(elem, currentlyRedirectedEdge))
                {
                    LGSPEdge edge = (LGSPEdge)elem;
                    undoItems.AddLast(new LGSPUndoElemRedirecting(edge, edge.lgspSource, edge.lgspTarget));
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
                undoItems.AddLast(new LGSPUndoAttributeChanged(elem, attrType, changeType, newValue, keyValue));
        }

        public void RetypingElement(IGraphElement oldElem, IGraphElement newElem)
        {
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "RetypingElement: " + ((LGSPNamedGraph)procEnv.graph).GetElementName(newElem) + ":" + newElem.Type.Name + "<" + ((LGSPNamedGraph)procEnv.graph).GetElementName(oldElem) + ":" + oldElem.Type.Name + ">");
#endif
            if(recording && !paused && !undoing)
                undoItems.AddLast(new LGSPUndoElemRetyped(oldElem, newElem));
        }

        public void RedirectingEdge(IEdge edge)
        {
#if LOG_TRANSACTION_HANDLING
            writer.WriteLine((paused ? "" : new String(' ', transactionLevel)) + "RedirectingEdge: " + " -" + ((LGSPNamedGraph)procEnv.graph).GetElementName(edge) + "-> " );
#endif
            if(recording && !paused && !undoing)
                currentlyRedirectedEdge = edge;
        }

        public bool IsActive { get { return recording; } }
    }
}
