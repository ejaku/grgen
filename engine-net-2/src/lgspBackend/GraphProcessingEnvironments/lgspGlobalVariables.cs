/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Threading;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// An implementation of the IGlobalVariables, to be used with LGSPGraphProcessingEnvironments.
    /// </summary>
    public class LGSPGlobalVariables : IGlobalVariables
    {
        private bool clearVariables = false;
        private IEdge currentlyRedirectedEdge;

        protected readonly Dictionary<IGraphElement, LinkedList<Variable>> ElementMap = new Dictionary<IGraphElement, LinkedList<Variable>>();
        protected readonly Dictionary<String, Variable> VariableMap = new Dictionary<String, Variable>();

        private readonly Dictionary<ITransientObject, long> transientObjectToUniqueId = new Dictionary<ITransientObject, long>();
        private readonly Dictionary<long, ITransientObject> uniqueIdToTransientObject = new Dictionary<long, ITransientObject>();
        
        // Source for assigning unique ids to internal transient class objects.
        private long transientObjectUniqueIdSource = 0;

        private long FetchTransientObjectUniqueId()
        {
            return transientObjectUniqueIdSource++;
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

        void ClearGraphListener(IGraph graph)
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
        
        public void CloneGraphVariables(IGraph old, IGraph clone)
        {
            // TODO: implement / or remove method? there is only one global variables object, where to store clone/variables clones (or replace old ones? but then not only clone)?
        }

        public void FillCustomCommandDescriptions(Dictionary<String, String> customCommandsToDescriptions)
        {
            customCommandsToDescriptions.Add("adaptvariables",
                "- adaptvariables: Sets whether variables are cleared if they contain\n" +
                "     elements which are removed from the graph, and rewritten to\n" +
                "     the new element on retypings.\n");
        }

        public void Custom(IGraph graph, params object[] args)
        {
            if(args.Length == 0)
                throw new ArgumentException("No command given");

            String command = (String)args[0];
            switch(command)
            {
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
                    SetClearVariables(newClearVariables, graph);
                    break;
                }

            default:
                throw new ArgumentException("Unknown command: " + command);
            }
        }

        internal void SetClearVariables(bool newClearVariables, IGraph graph)
        {
            if(newClearVariables == clearVariables)
                return;

            clearVariables = newClearVariables;

            if(newClearVariables)
                StartListening(graph); // start listening to remove events so we can clear variables if they occur
            else
                StopListening(graph); // stop listening to remove events, we can't clear variables anymore when they happen
        }

        internal void StartListening(IGraph graph)
        {
            if(!clearVariables)
                return;

            graph.OnRemovingNode += RemovingNodeListener;
            graph.OnRemovingEdge += RemovingEdgeListener;
            graph.OnRetypingNode += RetypingNodeListener;
            graph.OnRetypingEdge += RetypingEdgeListener;
            graph.OnRedirectingEdge += RedirectingEdgeListener;
            graph.OnClearingGraph += ClearGraphListener;
        }

        internal void StopListening(IGraph graph)
        {
            if(!clearVariables)
                return;

            graph.OnRemovingNode -= RemovingNodeListener;
            graph.OnRemovingEdge -= RemovingEdgeListener;
            graph.OnRetypingNode -= RetypingNodeListener;
            graph.OnRetypingEdge -= RetypingEdgeListener;
            graph.OnRedirectingEdge -= RedirectingEdgeListener;
            graph.OnClearingGraph -= ClearGraphListener;
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

        /// <summary>
        /// Source for assigning unique ids to internal class objects.
        /// </summary>
        private long objectUniqueIdSource = 0;

        public long FetchObjectUniqueId()
        {
            long uniqueId = Interlocked.Increment(ref objectUniqueIdSource);
            return uniqueId - 1;
        }

        public long FetchObjectUniqueId(long idToObtain)
        {
            // not possible to check against this condition and throw an exception -- requests may come out of order
            if(idToObtain < objectUniqueIdSource)
            {
                return idToObtain;
            }

            while(objectUniqueIdSource != idToObtain)
            {
                ++objectUniqueIdSource;
            }
            ++objectUniqueIdSource;

            return idToObtain;
        }

        public void ResetObjectUniqueIdSource()
        {
            objectUniqueIdSource = 0;
        }
    }
}
