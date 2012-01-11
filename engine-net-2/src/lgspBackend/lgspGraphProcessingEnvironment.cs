/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// An implementation of the IGraphProcessingEnvironment, to be used with LGSPGraphs.
    /// </summary>
    public class LGSPGraphProcessingEnvironment : LGSPActionExecutionEnvironment, IGraphProcessingEnvironment
    {
        private LGSPTransactionManager transactionManager;
        private IRecorder recorder;
        private TextWriter emitWriter = Console.Out;
        public LGSPDeferredSequencesManager sequencesManager;
        
        private bool clearVariables = false;
        private IEdge currentlyRedirectedEdge;

        private IUserProxyForSequenceExecution userProxy;
        private IUserProxyForSequenceExecution compliantUserProxy = new CompliantUserProxyForSequenceExecution();

        protected Dictionary<IGraphElement, LinkedList<Variable>> ElementMap = new Dictionary<IGraphElement, LinkedList<Variable>>();
        protected Dictionary<String, Variable> VariableMap = new Dictionary<String, Variable>();


        public LGSPGraphProcessingEnvironment(LGSPGraph graph, LGSPActions actions)
            : base(graph, actions)
        {
            recorder = new Recorder(graph as LGSPNamedGraph, this);
            transactionManager = new LGSPTransactionManager(this);
            sequencesManager = new LGSPDeferredSequencesManager();
            SetClearVariables(true);
        }


        void RemovingNodeListener(INode node)
        {
            LGSPNode lgspNode = (LGSPNode)node;
            if((lgspNode.lgspFlags & (uint)LGSPElemFlags.HAS_VARIABLES) != 0)
            {
                foreach(Variable var in ElementMap[lgspNode])
                    VariableMap.Remove(var.Name);
                ElementMap.Remove(lgspNode);
                lgspNode.lgspFlags &= ~(uint)LGSPElemFlags.HAS_VARIABLES;
            }
        }

        void RemovingEdgeListener(IEdge edge)
        {
            if(edge == currentlyRedirectedEdge)
            {
                currentlyRedirectedEdge = null;
                return; // edge will be added again before other changes, keep the variables
            }

            LGSPEdge lgspEdge = (LGSPEdge)edge;
            if((lgspEdge.lgspFlags & (uint)LGSPElemFlags.HAS_VARIABLES) != 0)
            {
                foreach(Variable var in ElementMap[lgspEdge])
                    VariableMap.Remove(var.Name);
                ElementMap.Remove(lgspEdge);
                lgspEdge.lgspFlags &= ~(uint)LGSPElemFlags.HAS_VARIABLES;
            }
        }

        void RetypingNodeListener(INode oldNode, INode newNode)
        {
            LGSPNode oldLgspNode = (LGSPNode)oldNode;
            LGSPNode newLgspNode = (LGSPNode)newNode;
            if((oldLgspNode.lgspFlags & (uint)LGSPElemFlags.HAS_VARIABLES) != 0)
            {
                LinkedList<Variable> varList = ElementMap[oldLgspNode];
                foreach(Variable var in varList)
                    var.Value = newLgspNode;
                ElementMap.Remove(oldLgspNode);
                ElementMap[newLgspNode] = varList;
                newLgspNode.lgspFlags |= (uint)LGSPElemFlags.HAS_VARIABLES;
            }
        }

        void RetypingEdgeListener(IEdge oldEdge, IEdge newEdge)
        {
            LGSPEdge oldLgspEdge = (LGSPEdge)oldEdge;
            LGSPEdge newLgspEdge = (LGSPEdge)newEdge;
            if((oldLgspEdge.lgspFlags & (uint)LGSPElemFlags.HAS_VARIABLES) != 0)
            {
                LinkedList<Variable> varList = ElementMap[oldLgspEdge];
                foreach(Variable var in varList)
                    var.Value = newLgspEdge;
                ElementMap.Remove(oldLgspEdge);
                ElementMap[newLgspEdge] = varList;
                newLgspEdge.lgspFlags |= (uint)LGSPElemFlags.HAS_VARIABLES;
            }
        }

        void RedirectingEdgeListener(IEdge edge)
        {
            currentlyRedirectedEdge = edge;
        }

        void ClearGraphListener()
        {
            foreach(INode node in graph.Nodes)
            {
                LGSPNode lgspNode = (LGSPNode)node;
                if((lgspNode.lgspFlags & (uint)LGSPElemFlags.HAS_VARIABLES) != 0)
                {
                    foreach(Variable var in ElementMap[lgspNode])
                        VariableMap.Remove(var.Name);
                    ElementMap.Remove(lgspNode);
                    lgspNode.lgspFlags &= ~(uint)LGSPElemFlags.HAS_VARIABLES;
                }
            }

            foreach(IEdge edge in graph.Edges)
            {
                LGSPEdge lgspEdge = (LGSPEdge)edge;
                if((lgspEdge.lgspFlags & (uint)LGSPElemFlags.HAS_VARIABLES) != 0)
                {
                    foreach(Variable var in ElementMap[lgspEdge])
                        VariableMap.Remove(var.Name);
                    ElementMap.Remove(lgspEdge);
                    lgspEdge.lgspFlags &= ~(uint)LGSPElemFlags.HAS_VARIABLES;
                }
            }
        }

        public ITransactionManager TransactionManager
        { 
            get { return transactionManager; }
        }

        public IRecorder Recorder
        {
            get { return recorder; }
            set { recorder = value; }
        }

        public TextWriter EmitWriter
        {
            get { return emitWriter; }
            set { emitWriter = value; }
        }
        
        public void CloneGraphVariables(IGraph old, IGraph clone)
        {
            // TODO: implement
        }

        public override void Custom(params object[] args)
        {
            if(args.Length == 0) goto invalidCommand;

            bool newClearVariables;
            switch((String)args[0])
            {
                case "set_max_matches":
                    base.Custom(args);
                    return;

                case "adaptvariables":
                    if(args.Length != 2)
                        throw new ArgumentException("Usage: adaptvariables <bool>\n"
                                + "If <bool> == true, variables are cleared (nulled) if they contain\n"
                                + "graph elements which are removed from the graph, and rewritten to\n"
                                + "the new element on retypings. Saves from outdated and dangling\n"
                                + "variables at the cost of listening to node and edge removals and retypings.\n"
                                + "Dangerous! Disable this only if you don't work with variables.");

                    if(!bool.TryParse((String)args[1], out newClearVariables))
                        throw new ArgumentException("Illegal bool value specified: \"" + (String)args[1] + "\"");
                    SetClearVariables(newClearVariables);
                    return;
            }

        invalidCommand:
            string errorMsg = "";
            try
            {
                base.Custom(args);
            }
            catch(ArgumentException ex)
            {
                errorMsg = ex.Message;
            }

            throw new ArgumentException(errorMsg
                + "- adaptvariables: Sets whether variables are cleared if they contain\n"
                + "     elements which are removed from the graph, and rewritten to\n"
                + "     the new element on retypings.\n");
        }

        internal void SetClearVariables(bool newClearVariables)
        {
            if(newClearVariables == clearVariables)
                return;

            if(newClearVariables)
            {
                // start listening to remove events so we can clear variables if they occur
                graph.OnRemovingNode += RemovingNodeListener;
                graph.OnRemovingEdge += RemovingEdgeListener;
                graph.OnRetypingNode += RetypingNodeListener;
                graph.OnRetypingEdge += RetypingEdgeListener;
                graph.OnRedirectingEdge += RedirectingEdgeListener;
                graph.OnClearingGraph += ClearGraphListener;
            }
            else
            {
                // stop listening to remove events, we can't clear variables anymore when they happen
                graph.OnRemovingNode -= RemovingNodeListener;
                graph.OnRemovingEdge -= RemovingEdgeListener;
                graph.OnRetypingNode -= RetypingNodeListener;
                graph.OnRetypingEdge -= RetypingEdgeListener;
                graph.OnRedirectingEdge -= RedirectingEdgeListener;
                graph.OnClearingGraph -= ClearGraphListener;
            }
        }


        public int ApplyRewrite(RuleInvocationParameterBindings paramBindings, int which, int localMaxMatches, bool special, bool test)
        {
            int curMaxMatches = (localMaxMatches > 0) ? localMaxMatches : MaxMatches;

            object[] parameters;
            if(paramBindings.ArgumentExpressions.Length > 0)
            {
                parameters = paramBindings.Arguments;
                for(int i = 0; i < paramBindings.ArgumentExpressions.Length; i++)
                {
                    if(paramBindings.ArgumentExpressions[i] != null)
                        parameters[i] = paramBindings.ArgumentExpressions[i].Evaluate(this);
                }
            }
            else parameters = null;

            if(PerformanceInfo != null) PerformanceInfo.StartLocal();
            IMatches matches = paramBindings.Action.Match(this, curMaxMatches, parameters);
            if(PerformanceInfo != null) PerformanceInfo.StopMatch();

            Matched(matches, special);
            if(matches.Count == 0) return 0;

            if(PerformanceInfo != null) PerformanceInfo.MatchesFound += matches.Count;

            if(test) return matches.Count;

            Finishing(matches, special);

            if(PerformanceInfo != null) PerformanceInfo.StartLocal();
            object[] retElems = Replace(matches, which);
            for(int i = 0; i < paramBindings.ReturnVars.Length; i++)
                paramBindings.ReturnVars[i].SetVariableValue(retElems[i], this);
            if(PerformanceInfo != null) PerformanceInfo.StopRewrite();

            Finished(matches, special);

            return matches.Count;
        }


        #region Variables management

        public LinkedList<Variable> GetElementVariables(IGraphElement elem)
        {
            LinkedList<Variable> variableList;
            ElementMap.TryGetValue(elem, out variableList);
            return variableList;
        }

        public object GetVariableValue(String varName)
        {
            Variable var;
            VariableMap.TryGetValue(varName, out var);
            if(var == null) return null;
            return var.Value;
        }

        public INode GetNodeVarValue(string varName)
        {
            return (INode)GetVariableValue(varName);
        }

        /// <summary>
        /// Retrieves the LGSPNode for a variable name or null, if the variable isn't set yet or anymore.
        /// A InvalidCastException is thrown, if the variable is set and does not point to an LGSPNode object.
        /// </summary>
        /// <param name="varName">The variable name to lookup.</param>
        /// <returns>The according LGSPNode or null.</returns>
        public LGSPNode GetLGSPNodeVarValue(string varName)
        {
            return (LGSPNode)GetVariableValue(varName);
        }

        public IEdge GetEdgeVarValue(string varName)
        {
            return (IEdge)GetVariableValue(varName);
        }

        /// <summary>
        /// Retrieves the LGSPEdge for a variable name or null, if the variable isn't set yet or anymore.
        /// A InvalidCastException is thrown, if the variable is set and does not point to an LGSPEdge object.
        /// </summary>
        /// <param name="varName">The variable name to lookup.</param>
        /// <returns>The according LGSPEdge or null.</returns>
        public LGSPEdge GetLGSPEdgeVarValue(string varName)
        {
            return (LGSPEdge)GetVariableValue(varName);
        }

        /// <summary>
        /// Detaches the specified variable from the according graph element.
        /// If it was the last variable pointing to the element, the variable list for the element is removed.
        /// This function may only called on variables pointing to graph elements.
        /// </summary>
        /// <param name="var">Variable to detach.</param>
        private void DetachVariableFromElement(Variable var)
        {
            IGraphElement elem = (IGraphElement)var.Value;
            LinkedList<Variable> oldVarList = ElementMap[elem];
            oldVarList.Remove(var);
            if(oldVarList.Count == 0)
            {
                ElementMap.Remove(elem);

                LGSPNode oldNode = elem as LGSPNode;
                if(oldNode != null) oldNode.lgspFlags &= ~(uint)LGSPElemFlags.HAS_VARIABLES;
                else
                {
                    LGSPEdge oldEdge = (LGSPEdge)elem;
                    oldEdge.lgspFlags &= ~(uint)LGSPElemFlags.HAS_VARIABLES;
                }
            }
        }

        public void SetVariableValue(String varName, object val)
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
                node.lgspFlags |= (uint)LGSPElemFlags.HAS_VARIABLES;
            else
            {
                LGSPEdge edge = (LGSPEdge)elem;
                edge.lgspFlags |= (uint)LGSPElemFlags.HAS_VARIABLES;
            }
        }

        public IEnumerable<Variable> Variables
        {
            get
            {
                foreach(Variable var in VariableMap.Values)
                    yield return var;
            }
        }

        #endregion Variables management


        #region Variables of graph elements convenience

        public void AddNode(INode node, String varName)
        {
            AddNode((LGSPNode)node, varName);
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
            graph.AddNodeWithoutEvents(node, node.lgspType.TypeID);
            SetVariableValue(varName, node);
            graph.NodeAdded(node);
        }

        /// <summary>
        /// Adds a new node to the graph.
        /// TODO: Slow but provides a better interface...
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created node.</returns>
        protected INode AddINode(NodeType nodeType, String varName)
        {
            return AddNode(nodeType, varName);
        }

        public INode AddNode(NodeType nodeType, String varName)
        {
            return AddLGSPNode(nodeType, varName);
        }

        /// <summary>
        /// Adds a new LGSPNode to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created node.</returns>
        public LGSPNode AddLGSPNode(NodeType nodeType, String varName)
        {
            //            LGSPNode node = new LGSPNode(nodeType);
            LGSPNode node = (LGSPNode)nodeType.CreateNode();
            graph.AddNodeWithoutEvents(node, nodeType.TypeID);
            SetVariableValue(varName, node);
            graph.NodeAdded(node);
            return node;
        }

        /// <summary>
        /// Adds an existing IEdge object to the graph and assigns it to the given variable.
        /// The edge must not be part of any graph, yet!
        /// Source and target of the edge must already be part of the graph.
        /// </summary>
        /// <param name="edge">The edge to be added.</param>
        /// <param name="varName">The name of the variable.</param>
        public void AddEdge(IEdge edge, String varName)
        {
            AddEdge((LGSPEdge)edge, varName);
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
            graph.AddEdgeWithoutEvents(edge, edge.lgspType.TypeID);
            SetVariableValue(varName, edge);
            graph.EdgeAdded(edge);
        }

        /// <summary>
        /// Adds a new edge to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created edge.</returns>
        public IEdge AddEdge(EdgeType edgeType, INode source, INode target, String varName)
        {
            return AddEdge(edgeType, (LGSPNode)source, (LGSPNode)target, varName);
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
            LGSPEdge edge = (LGSPEdge)edgeType.CreateEdge(source, target);
            graph.AddEdgeWithoutEvents(edge, edgeType.TypeID);
            SetVariableValue(varName, edge);
            graph.EdgeAdded(edge);
            return edge;
        }

        #endregion Variables of graph elements convenience


        #region Variables of named graph elements convenience

        public void AddNode(INode node, String varName, String elemName)
        {
            LGSPNamedGraph namedGraph = (LGSPNamedGraph)graph;
            namedGraph.AddNode(node, elemName);
            SetVariableValue(varName, node);
        }

        public INode AddNode(NodeType nodeType, String varName, String elemName)
        {
            LGSPNamedGraph namedGraph = (LGSPNamedGraph)graph;
            INode node = namedGraph.AddNode(nodeType, elemName);
            SetVariableValue(varName, node);
            return node;
        }

        public void AddEdge(IEdge edge, String varName, String elemName)
        {
            LGSPNamedGraph namedGraph = (LGSPNamedGraph)graph;
            namedGraph.AddEdge(edge, elemName);
            SetVariableValue(varName, edge);
        }

        public IEdge AddEdge(EdgeType edgeType, INode source, INode target, String varName, String elemName)
        {
            LGSPNamedGraph namedGraph = (LGSPNamedGraph)graph;
            IEdge edge = namedGraph.AddEdge(edgeType, source, target, elemName);
            SetVariableValue(varName, edge);
            return edge;
        }

        #endregion Variables of named graph elements convenience


        #region Sequence handling

        public bool ApplyGraphRewriteSequence(Sequence sequence)
        {
            if(PerformanceInfo != null) PerformanceInfo.Start();

            bool res = sequence.Apply(this);

            if(PerformanceInfo != null) PerformanceInfo.Stop();
            return res;
        }

        public bool ApplyGraphRewriteSequence(String seqStr)
        {
            return ApplyGraphRewriteSequence(ParseSequence(seqStr));
        }

        public bool ValidateWithSequence(Sequence seq)
        {
            LGSPGraph old = graph;
            graph = (LGSPGraph)graph.Clone("clonedGraph");
            bool valid = seq.Apply(this);
            graph = old;
            return valid;
        }

        public bool ValidateWithSequence(String seqStr)
        {
            return ValidateWithSequence(ParseSequence(seqStr));
        }

        public Sequence ParseSequence(String seqStr)
        {
            List<string> warnings = new List<string>();
            Sequence seq = de.unika.ipd.grGen.libGr.sequenceParser.SequenceParser.ParseSequence(seqStr, curActions, warnings);
            foreach(string warning in warnings)
            {
                System.Console.Error.WriteLine(warning);
            }
            return seq;
        }


        public IUserProxyForSequenceExecution UserProxy
        {
            get { return userProxy; }
            set { userProxy = value; }
        }

        public IUserProxyForSequenceExecution CompliantUserProxy
        {
            get { return compliantUserProxy; }
        }

        #endregion Sequence handling
        

        #region Events

        public event EnterSequenceHandler OnEntereringSequence;
        public event ExitSequenceHandler OnExitingSequence;
        public event EndOfIterationHandler OnEndOfIteration;


        public void EnteringSequence(Sequence seq)
        {
            EnterSequenceHandler handler = OnEntereringSequence;
            if(handler != null) handler(seq);
        }

        public void ExitingSequence(Sequence seq)
        {
            ExitSequenceHandler handler = OnExitingSequence;
            if(handler != null) handler(seq);
        }

        public void EndOfIteration(bool continueLoop, Sequence seq)
        {
            EndOfIterationHandler handler = OnEndOfIteration;
            if(handler != null) handler(continueLoop, seq);
        }

        #endregion Events
    }
}
