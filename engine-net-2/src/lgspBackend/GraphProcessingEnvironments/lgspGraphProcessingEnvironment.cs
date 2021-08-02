/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.1
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.libGr.sequenceParser;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// An implementation of the IGraphProcessingEnvironment, to be used with LGSPGraphs.
    /// </summary>
    public class LGSPGraphProcessingEnvironment : LGSPSubactionAndOutputAdditionEnvironment, IGraphProcessingEnvironment
    {
        private readonly LGSPTransactionManager transactionManager;
        public readonly LGSPDeferredSequencesManager sequencesManager;
        
        private bool clearVariables = false;
        private IEdge currentlyRedirectedEdge;

        private IUserProxyForSequenceExecution userProxy;
        private IUserProxyForSequenceExecution compliantUserProxy = new CompliantUserProxyForSequenceExecution();

        protected readonly Dictionary<IGraphElement, LinkedList<Variable>> ElementMap = new Dictionary<IGraphElement, LinkedList<Variable>>();
        protected readonly Dictionary<String, Variable> VariableMap = new Dictionary<String, Variable>();
        protected readonly Dictionary<String, object> SpecialVariables = new Dictionary<String, object>();

        private readonly Dictionary<ITransientObject, long> transientObjectToUniqueId = new Dictionary<ITransientObject, long>();
        private readonly Dictionary<long, ITransientObject> uniqueIdToTransientObject = new Dictionary<long, ITransientObject>();
        
        // Source for assigning unique ids to internal transient class objects.
        private long transientObjectUniqueIdSource = 0;

        private long FetchTransientObjectUniqueId()
        {
            return transientObjectUniqueIdSource++;
        }

        readonly List<object[]> emptyList = new List<object[]>(); // performance optimization (for ApplyRewrite, empty list is only created once)


        public LGSPGraphProcessingEnvironment(LGSPGraph graph, LGSPActions actions)
            : base(graph, actions)
        {
            transactionManager = new LGSPTransactionManager(this);
            sequencesManager = new LGSPDeferredSequencesManager();
            SetClearVariables(true);
            FillCustomCommandDescriptions();
        }

        public override void Initialize(LGSPGraph graph, LGSPActions actions)
        {
            SetClearVariables(false);
            base.Initialize(graph, actions);
            SetClearVariables(true);
        }

        void RemovingNodeListener(INode node)
        {
            LGSPNode lgspNode = (LGSPNode)node;
            if((lgspNode.lgspFlags & (uint)LGSPElemFlags.HAS_VARIABLES) != 0)
            {
                foreach(Variable var in ElementMap[lgspNode])
                {
                    VariableMap.Remove(var.Name);
                }
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
                {
                    VariableMap.Remove(var.Name);
                }
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
                {
                    var.Value = newLgspNode;
                }
                ElementMap.Remove(oldLgspNode);
                ElementMap[newLgspNode] = varList;
                oldLgspNode.lgspFlags &= ~(uint)LGSPElemFlags.HAS_VARIABLES;
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
                {
                    var.Value = newLgspEdge;
                }
                ElementMap.Remove(oldLgspEdge);
                ElementMap[newLgspEdge] = varList;
                oldLgspEdge.lgspFlags &= ~(uint)LGSPElemFlags.HAS_VARIABLES;
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
                    {
                        VariableMap.Remove(var.Name);
                    }
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
                    {
                        VariableMap.Remove(var.Name);
                    }
                    ElementMap.Remove(lgspEdge);
                    lgspEdge.lgspFlags &= ~(uint)LGSPElemFlags.HAS_VARIABLES;
                }
            }
        }

        public ITransactionManager TransactionManager
        { 
            get { return transactionManager; }
        }
        
        public void CloneGraphVariables(IGraph old, IGraph clone)
        {
            // TODO: implement
        }

        private void FillCustomCommandDescriptions()
        {
            customCommandsToDescriptions.Add("adaptvariables",
                "- adaptvariables: Sets whether variables are cleared if they contain\n" +
                "     elements which are removed from the graph, and rewritten to\n" +
                "     the new element on retypings.\n");
        }

        public override void Custom(params object[] args)
        {
            if(args.Length == 0)
                throw new ArgumentException("No command given");

            String command = (String)args[0];
            switch(command)
            {
            case "set_max_matches":
                base.Custom(args);
                break;

            case "adaptvariables":
                {
                    if(args.Length != 2)
                        throw new ArgumentException("Usage: adaptvariables <bool>\n"
                                + "If <bool> == true, variables are cleared (nulled) if they contain\n"
                                + "graph elements which are removed from the graph, and rewritten to\n"
                                + "the new element on retypings. Saves from outdated and dangling\n"
                                + "variables at the cost of listening to node and edge removals and retypings.\n"
                                + "Dangerous! Disable this only if you don't work with variables.");

                    bool newClearVariables;
                    if(!bool.TryParse((String)args[1], out newClearVariables))
                        throw new ArgumentException("Illegal bool value specified: \"" + (String)args[1] + "\"");
                    SetClearVariables(newClearVariables);
                    break;
                }

            default:
                throw new ArgumentException("Unknown command: " + command);
            }
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

        public override IMatches Match(IAction action, object[] arguments, int localMaxMatches, bool special, List<FilterCall> filters, bool fireDebugEvents)
        {
            int curMaxMatches = (localMaxMatches > 0) ? localMaxMatches : MaxMatches;

#if DEBUGACTIONS || MATCHREWRITEDETAIL // spread over multiple files now, search for the corresponding defines to reactivate
            PerformanceInfo.StartLocal();
#endif
            IMatches matches = action.Match(this, curMaxMatches, arguments);
#if DEBUGACTIONS || MATCHREWRITEDETAIL
            PerformanceInfo.StopMatch();
#endif
            PerformanceInfo.MatchesFound += matches.Count;

            if(fireDebugEvents)
            {
                if(matches.Count > 0)
                    MatchedBeforeFiltering(matches);
            }

            for(int i = 0; i < filters.Count; ++i)
            {
                FilterCall filterCall = filters[i];
                if(filterCall is FilterCallWithLambdaExpression)
                {
                    FilterCallWithLambdaExpression lambdaExpressionFilterCall = (FilterCallWithLambdaExpression)filterCall;
                    Filter(matches, lambdaExpressionFilterCall);
                }
                else
                {
                    FilterCallWithArguments filterCallWithArguments = (FilterCallWithArguments)filterCall;
                    action.Filter(this, matches, filterCallWithArguments);
                }
            }

            if(fireDebugEvents)
            {
                if(matches.Count > 0) // ensure that Matched is only called when a match exists
                    MatchedAfterFiltering(matches, special);
            }

            return matches;
        }

        /// <summary>
        /// Filters the matches of a rule (all) call with a lambda expression filter (call).
        /// </summary>
        /// <param name="matches">The matches of the rule</param>
        /// <param name="filter">The lambda expression filter to apply</param>
        public void Filter(IMatches matches, FilterCallWithLambdaExpression filter)
        {
            if(filter.PlainName == "assign")
                FilterAssign(matches, filter);
            else if(filter.PlainName == "removeIf")
                FilterRemoveIf(matches, filter);
            else if(filter.PlainName == "assignStartWithAccumulateBy")
                FilterAssignStartWithAccumulateBy(matches, filter);
            else
                throw new Exception("Unknown lambda expression filter call (available are assign, removeIf, assignStartWithAccumulateBy)");
        }

        public void FilterAssign(IMatches matches, FilterCallWithLambdaExpression filterCall)
        {
            if(filterCall.arrayAccess != null)
            {
                List<IMatch> matchListCopy = new List<IMatch>();
                foreach(IMatch match in matches)
                {
                    matchListCopy.Add(match.Clone());
                }
                filterCall.arrayAccess.SetVariableValue(matchListCopy, this);
            }
            int index = 0;
            foreach(IMatch match in matches)
            {
                if(filterCall.index != null)
                    filterCall.index.SetVariableValue(index, this);
                filterCall.element.SetVariableValue(match, this);
                object result = filterCall.lambdaExpression.Evaluate(this);
                match.SetMember(filterCall.Entity, result);
                ++index;
            }
        }

        public void FilterRemoveIf(IMatches matches, FilterCallWithLambdaExpression filterCall)
        {
            List<IMatch> matchList = matches.ToList();
            if(filterCall.arrayAccess != null)
            {
                List<IMatch> matchListCopy = new List<IMatch>(matchList);
                filterCall.arrayAccess.SetVariableValue(matchListCopy, this);
            }
            for(int index = 0; index < matchList.Count; ++index)
            {
                if(filterCall.index != null)
                    filterCall.index.SetVariableValue(index, this);
                IMatch match = matchList[index];
                filterCall.element.SetVariableValue(match, this);
                object result = filterCall.lambdaExpression.Evaluate(this);
                if((bool)result)
                    matchList[index] = null;
            }
            matches.FromList();
        }

        public void FilterAssignStartWithAccumulateBy(IMatches matches, FilterCallWithLambdaExpression filterCall)
        {
            List<IMatch> matchListCopy = null;
            if(filterCall.initArrayAccess != null || filterCall.arrayAccess != null)
            {
                matchListCopy = new List<IMatch>();
                foreach(IMatch match in matches)
                {
                    matchListCopy.Add(match.Clone());
                }
            }
            if(filterCall.initArrayAccess != null)
                filterCall.initArrayAccess.SetVariableValue(matchListCopy, this);
            if(filterCall.arrayAccess != null)
                filterCall.arrayAccess.SetVariableValue(matchListCopy, this);

            filterCall.previousAccumulationAccess.SetVariableValue(filterCall.initExpression.Evaluate(this), this);

            int index = 0;
            foreach(IMatch match in matches)
            {
                if(filterCall.index != null)
                    filterCall.index.SetVariableValue(index, this);
                filterCall.element.SetVariableValue(match, this);
                object result = filterCall.lambdaExpression.Evaluate(this);
                match.SetMember(filterCall.Entity, result);
                ++index;
                filterCall.previousAccumulationAccess.SetVariableValue(result, this);
            }
        }

        public List<object[]> ApplyRewrite(IAction action, IGraph subgraph, object[] arguments, int which, 
            int localMaxMatches, bool special, bool test, List<FilterCall> filters, bool fireDebugEvents, out int numMatches)
        {
            if(subgraph != null)
                SwitchToSubgraph(subgraph);

            IMatches matches = Match(action, arguments, localMaxMatches, special, filters, fireDebugEvents);

            if(matches.Count == 0)
            {
                if(subgraph != null)
                    ReturnFromSubgraph();
                numMatches = 0;
                return emptyList;
            }

            if(test)
            {
                if(subgraph != null)
                    ReturnFromSubgraph();
                numMatches = matches.Count;
                return emptyList;
            }

#if DEBUGACTIONS || MATCHREWRITEDETAIL // spread over multiple files now, search for the corresponding defines to reactivate
            PerformanceInfo.StartLocal();
#endif
            List<object[]> retElemsList = Replace(matches, which, special, fireDebugEvents);
#if DEBUGACTIONS || MATCHREWRITEDETAIL
            PerformanceInfo.StopRewrite();
#endif
            if(fireDebugEvents)
                Finished(matches, special);

            if(subgraph != null)
                ReturnFromSubgraph();

            numMatches = matches.Count;
            return retElemsList;
        }

        public IMatches MatchForQuery(IAction action, IGraph subgraph, object[] arguments,
            int localMaxMatches, bool special, bool fireDebugEvents)
        {
            if(subgraph != null)
                SwitchToSubgraph(subgraph);

            IMatches matches = MatchForQuery(action, arguments, localMaxMatches, fireDebugEvents);

            //subrule debugging must be changed to allow this
            //if(matches.Count > 0) {// ensure that Matched is only called when a match exists
            //    procEnv.MatchedAfterFiltering(null, matches, null, special);
            //}

            if(subgraph != null)
                ReturnFromSubgraph();

            return matches;
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
            if(var == null)
                return null;
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
                if(oldNode != null)
                    oldNode.lgspFlags &= ~(uint)LGSPElemFlags.HAS_VARIABLES;
                else
                {
                    LGSPEdge oldEdge = (LGSPEdge)elem;
                    oldEdge.lgspFlags &= ~(uint)LGSPElemFlags.HAS_VARIABLES;
                }
            }
        }

        public void SetVariableValue(String varName, object val)
        {
            if(varName == null)
                return;

            Variable var;
            VariableMap.TryGetValue(varName, out var);

            if(var != null)
            {
                if(var.Value == val) // Variable already set to this element?
                    return;
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
                if(val == null)
                    return;

                var = new Variable(varName, val);
                VariableMap[varName] = var;
            }

            IGraphElement elem = val as IGraphElement;
            if(elem == null)
                return;

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
                {
                    yield return var;
                }
            }
        }

        public object this[string name]
        {
            get
            {
                return GetVariableValue(name);
            }

            set
            {
                SetVariableValue(name, value);
            }
        }

        #endregion Variables management


        #region Special variables management

        /// <summary>
        /// Retrieves the value of a special variable name.
        /// Special variables are used for GrGen-internal data storage (as of now only "this"-reference handling).
        /// </summary>
        /// <param name="name">The name of the special variable to read</param>
        /// <returns>The according value</returns>
        public object GetSpecialVariableValue(string name)
        {
            return SpecialVariables[name];
        }

        /// <summary>
        /// Sets the given special variable name to the given value.
        /// Special variables are used for GrGen-internal data storage (as of now only "this"-reference handling).
        /// </summary>
        /// <param name="name">The name of the special variable to write</param>
        /// <param name="val">The new value of the special variable</param>
        public void SetSpecialVariableValue(string name, object value)
        {
            SpecialVariables[name] = value;
        }

        /// <summary>
        /// Deletes the given special variable name.
        /// Special variables are used for GrGen-internal data storage (as of now only "this"-reference handling).
        /// </summary>
        /// <param name="name">The name of the special variable to delete</param>
        public void DeleteSpecialVariable(string name)
        {
            SpecialVariables.Remove(name);
        }

        #endregion Special variables management


        #region Transient Object id handling

        public long GetUniqueId(ITransientObject transientObject)
        {
            if(transientObject == null)
                return -1;

            if(!transientObjectToUniqueId.ContainsKey(transientObject))
            {
                long uniqueId = FetchTransientObjectUniqueId();
                transientObjectToUniqueId[transientObject] = uniqueId;
                uniqueIdToTransientObject[uniqueId] = transientObject;
            }
            return transientObjectToUniqueId[transientObject];
        }

        public ITransientObject GetTransientObject(long uniqueId)
        {
            ITransientObject transientObject;
            uniqueIdToTransientObject.TryGetValue(uniqueId, out transientObject);
            return transientObject;
        }

        #endregion Transient Object id handling


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
            PerformanceInfo.Start();

            bool res = sequence.Apply(this);

            PerformanceInfo.Stop();
            return res;
        }

        public bool ApplyGraphRewriteSequence(String seqStr)
        {
            return ApplyGraphRewriteSequence(ParseSequence(seqStr));
        }

        public object EvaluateGraphRewriteSequenceExpression(SequenceExpression sequenceExpression)
        {
            PerformanceInfo.Start();

            object res = sequenceExpression.Evaluate(this);

            PerformanceInfo.Stop();
            return res;
        }

        public object EvaluateGraphRewriteSequenceExpression(String seqExprStr)
        {
            return EvaluateGraphRewriteSequenceExpression(ParseSequenceExpression(seqExprStr));
        }

        public bool ValidateWithSequence(Sequence seq)
        {
            SwitchToSubgraph(graph.Clone("clonedGraph"));
            bool valid = seq.Apply(this);
            ReturnFromSubgraph();
            return valid;
        }

        public bool ValidateWithSequence(String seqStr)
        {
            return ValidateWithSequence(ParseSequence(seqStr));
        }

        public Sequence ParseSequence(String seqStr)
        {
            SequenceParserEnvironmentInterpreted parserEnv = new SequenceParserEnvironmentInterpreted(curActions);
            List<string> warnings = new List<string>();
            Sequence seq = SequenceParser.ParseSequence(seqStr, parserEnv, warnings);
            foreach(string warning in warnings)
            {
                System.Console.Error.WriteLine(warning);
            }
            return seq;
        }

        public SequenceExpression ParseSequenceExpression(String seqExprStr)
        {
            Dictionary<String, String> predefinedVariables = new Dictionary<string, string>();
            SequenceParserEnvironmentInterpreted parserEnv = new SequenceParserEnvironmentInterpreted(curActions);
            List<string> warnings = new List<string>();
            SequenceExpression seqExpr = SequenceParser.ParseSequenceExpression(seqExprStr, predefinedVariables, parserEnv, warnings);
            foreach(string warning in warnings)
            {
                System.Console.Error.WriteLine(warning);
            }
            return seqExpr;
        }


        public IUserProxyForSequenceExecution UserProxy
        {
            get { return userProxy ?? compliantUserProxy; }
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

        public void EnteringSequence(SequenceBase seq)
        {
            if(OnEntereringSequence != null)
                OnEntereringSequence(seq);
        }

        public void ExitingSequence(SequenceBase seq)
        {
            if(OnExitingSequence != null)
                OnExitingSequence(seq);
        }

        public void EndOfIteration(bool continueLoop, SequenceBase seq)
        {
            if(OnEndOfIteration != null)
                OnEndOfIteration(continueLoop, seq);
        }

        #endregion Events
    }
}
