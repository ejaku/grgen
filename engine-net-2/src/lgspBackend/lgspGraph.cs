/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

#define MONO_MULTIDIMARRAY_WORKAROUND       // not using multidimensional arrays is about 2% faster on .NET because of fewer bound checks
//#define OPCOST_WITH_GEO_MEAN
//#define CHECK_RINGLISTS

using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;
using System.Collections;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    public enum LGSPDir { In, Out };

    /// <summary>
    /// An implementation of the IGraph interface.
    /// </summary>
    public class LGSPGraph : BaseGraph
    {
        private static int graphID = 0;

        private String name;
        internal LGSPBackend backend = null;
        protected IGraphModel model;
        internal String modelAssemblyName;


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
        public int[] vstructs;
#else
        public int[, , ,] vstructs;
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

        public string[] nameOfSingleElementAdded = new string[1];

        protected static String GetNextGraphName() { return "lgspGraph_" + graphID++; }



        /// <summary>
        /// Constructs an LGSPGraph object with the given model and an automatically generated name.
        /// </summary>
        /// <param name="grmodel">The graph model.</param>
        public LGSPGraph(IGraphModel grmodel)
            : this(grmodel, GetNextGraphName())
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
        /// </summary>
        /// <param name="dataSource">The LGSPGraph object to get the data from</param>
        /// <param name="newName">Name of the copied graph.</param>
        /// <param name="oldToNewMap">A map of the old elements to the new elements after cloning.</param>
        public LGSPGraph(LGSPGraph dataSource, String newName, out IDictionary<IGraphElement, IGraphElement> oldToNewMap)
        {
            Copy(dataSource, newName, out oldToNewMap);
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="dataSource">The LGSPGraph object to get the data from</param>
        /// <param name="newName">Name of the copied graph.</param>
        public LGSPGraph(LGSPGraph dataSource, String newName)
        {
            IDictionary<IGraphElement, IGraphElement> oldToNewMap;
            Copy(dataSource, newName, out oldToNewMap);
        }

        /// <summary>
        /// Copy constructor helper.
        /// </summary>
        /// <param name="dataSource">The LGSPGraph object to get the data from</param>
        /// <param name="newName">Name of the copied graph.</param>
        /// <param name="oldToNewMap">A map of the old elements to the new elements after cloning,
        /// just forget about it if you don't need it.</param>
        private void Copy(LGSPGraph dataSource, String newName, out IDictionary<IGraphElement, IGraphElement> oldToNewMap)
        {
            model = dataSource.model;
            name = newName;

            InitializeGraph();

            if(dataSource.backend != null)
            {
                backend = dataSource.backend;
                modelAssemblyName = dataSource.modelAssemblyName;
            }

            oldToNewMap = new Dictionary<IGraphElement, IGraphElement>();

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

            /* TODO: remove when cloning of graph variables was implemented
             * foreach(KeyValuePair<IGraphElement, LinkedList<Variable>> kvp in dataSource.ElementMap)
            {
                IGraphElement newElem = oldToNewMap[kvp.Key];
                foreach(Variable var in kvp.Value)
                    SetVariableValue(var.Name, newElem);
            }*/

#if MONO_MULTIDIMARRAY_WORKAROUND
            dim0size = dataSource.dim0size;
            dim1size = dataSource.dim1size;
            dim2size = dataSource.dim2size;
            if(dataSource.vstructs != null)
                vstructs = (int[]) dataSource.vstructs.Clone();
#else
            if(dataSource.vstructs != null)
                vstructs = (int[ , , , ]) dataSource.vstructs.Clone();
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
            CheckNodeInGraph((LGSPNode)edge.Source);
            CheckNodeInGraph((LGSPNode)edge.Target);
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
        public virtual void AddNode(LGSPNode node)
        {
            AddNodeWithoutEvents(node, node.lgspType.TypeID);
            NodeAdded(node);
        }


        public override INode AddNode(NodeType nodeType)
        {
            return AddLGSPNode(nodeType);
        }

        /// <summary>
        /// Creates a new LGSPNode according to the given type and adds
        /// it to the graph.
        /// </summary>
        /// <param name="nodeType">The type for the new node.</param>
        /// <returns>The created node.</returns>
        public virtual LGSPNode AddLGSPNode(NodeType nodeType)
        {
            //            LGSPNode node = new LGSPNode(nodeType);
            LGSPNode node = (LGSPNode)nodeType.CreateNode();
            AddNodeWithoutEvents(node, nodeType.TypeID);
            NodeAdded(node);
            return node;
        }

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
        public virtual void AddEdge(LGSPEdge edge)
        {
            AddEdgeWithoutEvents(edge, edge.lgspType.TypeID);
            EdgeAdded(edge);
        }

        public override IEdge AddEdge(EdgeType edgeType, INode source, INode target)
        {
            return AddEdge(edgeType, (LGSPNode)source, (LGSPNode)target);
        }

        /// <summary>
        /// Adds a new edge to the graph.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <returns>The newly created edge.</returns>
        public virtual LGSPEdge AddEdge(EdgeType edgeType, LGSPNode source, LGSPNode target)
        {
//            LGSPEdge edge = new LGSPEdge(edgeType, source, target);
            LGSPEdge edge = (LGSPEdge) edgeType.CreateEdge(source, target);
            AddEdgeWithoutEvents(edge, edgeType.TypeID);
            EdgeAdded(edge);
            return edge;
        }

        internal void RemoveNodeWithoutEvents(LGSPNode node, int typeid)
        {
            node.lgspTypePrev.lgspTypeNext = node.lgspTypeNext;
            node.lgspTypeNext.lgspTypePrev = node.lgspTypePrev;
            node.lgspTypeNext = null;
            node.lgspTypePrev = null;

            nodesByTypeCounts[typeid]--;

            if(reuseOptimization)
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

            lgspEdge.lgspSource.RemoveOutgoing(lgspEdge);
            lgspEdge.lgspTarget.RemoveIncoming(lgspEdge);

            lgspEdge.lgspTypePrev.lgspTypeNext = lgspEdge.lgspTypeNext;
            lgspEdge.lgspTypeNext.lgspTypePrev = lgspEdge.lgspTypePrev;
            lgspEdge.lgspTypeNext = null;
            lgspEdge.lgspTypePrev = null;

            edgesByTypeCounts[lgspEdge.lgspType.TypeID]--;

            if(reuseOptimization)
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
        /// </summary>
        /// <param name="node">The node to be retyped.</param>
        /// <param name="newNodeType">The new type for the node.</param>
        /// <returns>The new node object representing the retyped node.</returns>
        public virtual LGSPNode Retype(LGSPNode node, NodeType newNodeType)
        {
            LGSPNode newNode = (LGSPNode) newNodeType.CreateNodeWithCopyCommons(node);
            RetypingNode(node, newNode);
            ReplaceNode(node, newNode);
            return newNode;
        }

        public override INode Retype(INode node, NodeType newNodeType)
        {
            return Retype((LGSPNode) node, newNodeType);
        }

        /// <summary>
        /// Retypes an edge by replacing it by a new edge of the given type.
        /// Source and target node as well as all attributes from common super classes are kept.
        /// </summary>
        /// <param name="edge">The edge to be retyped.</param>
        /// <param name="newEdgeType">The new type for the edge.</param>
        /// <returns>The new edge object representing the retyped edge.</returns>
        public virtual LGSPEdge Retype(LGSPEdge edge, EdgeType newEdgeType)
        {
            LGSPEdge newEdge = (LGSPEdge) newEdgeType.CreateEdgeWithCopyCommons(edge.lgspSource, edge.lgspTarget, edge);
            RetypingEdge(edge, newEdge);
            ReplaceEdge(edge, newEdge);
            return newEdge;
        }

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

            if(reuseOptimization)
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

            if(reuseOptimization)
                oldEdge.Recycle();

#if CHECK_RINGLISTS
            CheckTypeRinglistBroken(edgesByTypeHeads[newEdge.lgspType.TypeID]);
            CheckInOutRinglistsBroken(newEdge.lgspSource);
            CheckInOutRinglistsBroken(newEdge.lgspTarget);
#endif
        }

        /// <summary>
        /// Merges the source node into the target node,
        /// i.e. all edges incident to the source node are redirected to the target node, then the source node is deleted.
        /// </summary>
        /// <param name="target">The node which remains after the merge.</param>
        /// <param name="source">The node to be merged.</param>
        /// <param name="sourceName">The name of the node to be merged (used for debug display of redirected edges).</param>
        public void Merge(LGSPNode target, LGSPNode source, string sourceName)
        {
            if (source == target)
                return;

            while(source.lgspOuthead!=null)
            {
                if(source.lgspOuthead.Target==source)
                    RedirectSourceAndTarget(source.lgspOuthead, target, target, sourceName, sourceName);
                else
                    RedirectSource(source.lgspOuthead, target, sourceName);
            }
            while (source.lgspInhead != null)
            {
                RedirectTarget(source.lgspInhead, target, sourceName);
            }

            Remove(source);
        }

        /// <summary>
        /// Merges the source node into the target node,
        /// i.e. all edges incident to the source node are redirected to the target node, then the source node is deleted.
        /// </summary>
        /// <param name="target">The node which remains after the merge.</param>
        /// <param name="source">The node to be merged.</param>
        /// <param name="sourceName">The name of the node to be merged (used for debug display of redirected edges).</param>
        public override void Merge(INode target, INode source, string sourceName)
        {
            Merge((LGSPNode)target, (LGSPNode)source, sourceName);
        }

        /// <summary>
        /// Changes the source node of the edge from the old source to the given new source.
        /// </summary>
        /// <param name="edge">The edge to redirect.</param>
        /// <param name="newSource">The new source node of the edge.</param>
        /// <param name="oldSourceName">The name of the old source node (used for debug display of the new edge).</param>
        public void RedirectSource(LGSPEdge edge, LGSPNode newSource, string oldSourceName)
        {
            RedirectingEdge(edge);
            RemovingEdge(edge);
            edge.lgspSource.RemoveOutgoing(edge);
            newSource.AddOutgoing(edge);
            edge.lgspSource = newSource;
            nameOfSingleElementAdded[0] = "redirected from " + oldSourceName + " --> .";
            SettingAddedEdgeNames(nameOfSingleElementAdded);
            EdgeAdded(edge);
        }

        /// <summary>
        /// Changes the source node of the edge from the old source to the given new source.
        /// </summary>
        /// <param name="edge">The edge to redirect.</param>
        /// <param name="newSource">The new source node of the edge.</param>
        /// <param name="oldSourceName">The name of the old source node (used for debug display of the new edge).</param>
        public override void RedirectSource(IEdge edge, INode newSource, string oldSourceName)
        {
            RedirectSource((LGSPEdge)edge, (LGSPNode)newSource, oldSourceName);
        }
        
        /// <summary>
        /// Changes the target node of the edge from the old target to the given new target.
        /// </summary>
        /// <param name="edge">The edge to redirect.</param>
        /// <param name="newTarget">The new target node of the edge.</param>
        /// <param name="oldTargetName">The name of the old target node (used for debug display of the new edge).</param>
        public void RedirectTarget(LGSPEdge edge, LGSPNode newTarget, string oldTargetName)
        {
            RedirectingEdge(edge);
            RemovingEdge(edge);
            edge.lgspTarget.RemoveIncoming(edge);
            newTarget.AddIncoming(edge);
            edge.lgspTarget = newTarget;
            nameOfSingleElementAdded[0] = "redirected from . --> " + oldTargetName;
            SettingAddedEdgeNames(nameOfSingleElementAdded);
            EdgeAdded(edge);
        }

        /// <summary>
        /// Changes the target node of the edge from the old target to the given new target.
        /// </summary>
        /// <param name="edge">The edge to redirect.</param>
        /// <param name="newTarget">The new target node of the edge.</param>
        /// <param name="oldTargetName">The name of the old target node (used for debug display of the new edge).</param>
        public override void RedirectTarget(IEdge edge, INode newTarget, string oldTargetName)
        {
            RedirectTarget((LGSPEdge)edge, (LGSPNode)newTarget, oldTargetName);
        }

        /// <summary>
        /// Changes the source of the edge from the old source to the given new source,
        /// and changes the target node of the edge from the old target to the given new target.
        /// </summary>
        /// <param name="edge">The edge to redirect.</param>
        /// <param name="newSource">The new source node of the edge.</param>
        /// <param name="newTarget">The new target node of the edge.</param>
        /// <param name="oldSourceName">The name of the old source node (used for debug display of the new edge).</param>
        /// <param name="oldTargetName">The name of the old target node (used for debug display of the new edge).</param>
        public void RedirectSourceAndTarget(LGSPEdge edge, LGSPNode newSource, LGSPNode newTarget, string oldSourceName, string oldTargetName)
        {
            RedirectingEdge(edge);
            RemovingEdge(edge);
            edge.lgspSource.RemoveOutgoing(edge);
            newSource.AddOutgoing(edge);
            edge.lgspSource = newSource;
            edge.lgspTarget.RemoveIncoming(edge);
            newTarget.AddIncoming(edge);
            edge.lgspTarget = newTarget;
            nameOfSingleElementAdded[0] = "redirected from " + oldSourceName + " --> " + oldTargetName;
            SettingAddedEdgeNames(nameOfSingleElementAdded);
            EdgeAdded(edge);
        }

        /// <summary>
        /// Changes the source of the edge from the old source to the given new source,
        /// and changes the target node of the edge from the old target to the given new target.
        /// </summary>
        /// <param name="edge">The edge to redirect.</param>
        /// <param name="newSource">The new source node of the edge.</param>
        /// <param name="newTarget">The new target node of the edge.</param>
        /// <param name="oldSourceName">The name of the old source node (used for debug display of the new edge).</param>
        /// <param name="oldTargetName">The name of the old target node (used for debug display of the new edge).</param>
        public override void RedirectSourceAndTarget(IEdge edge, INode newSource, INode newTarget, string oldSourceName, string oldTargetName)
        {
            RedirectSourceAndTarget((LGSPEdge)edge, (LGSPNode)newSource, (LGSPNode)newTarget, oldSourceName, oldTargetName);
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
            vstructs = new int[numNodeTypes * numEdgeTypes * numNodeTypes * 2];
#else
            vstructs = new int[numNodeTypes, numEdgeTypes, numNodeTypes, 2];
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
//                                        int val = (float) Math.Log(outgoingVCount[edgeSuperTypeID, targetSuperTypeID]);     // > 1 im if
                                        int val = outgoingVCount[edgeSuperTypeID, targetSuperTypeID];
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
//                                        int val = (float) Math.Log(incomingVCount[edgeSuperTypeID, sourceSuperTypeID]);     // > 1 im if
                                        int val = incomingVCount[edgeSuperTypeID, sourceSuperTypeID];
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

        void CheckNodeInGraph(LGSPNode node)
        {
            LGSPNode head = nodesByTypeHeads[node.lgspType.TypeID];
            LGSPNode cur = head.lgspTypeNext;
            while(cur != head)
            {
                if(cur == node)
                    return;
                cur = cur.lgspTypeNext;
            }
            throw new Exception("Internal error: Node not in graph");
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
            }

invalidCommand:
            throw new ArgumentException("Possible commands:\n"
                + "- analyze: Analyzes the graph. The generated information can then be\n"
                + "     used by Actions implementations to optimize the pattern matching\n"
                + "- optimizereuse: Sets whether deleted elements may be reused in a rewrite\n"
                + "     (default: true)\n");
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

        /// <summary>
        /// Creates an empty graph using the same model and backend as the other.
        /// </summary>
        /// <param name="newName">Name of the new graph.</param>
        /// <returns>A new empty graph of the same model.</returns>
        public override IGraph CreateEmptyEquivalent(String newName)
        {
            return new LGSPGraph(this.model, newName);
        }

        /// <summary>
        /// Returns whether this graph is isomorph to that graph
        /// Each graph must be either unanalyzed or unchanged since the last analyze,
        /// otherwise results will be wrong!
        /// </summary>
        /// <param name="that">The other graph we check for isomorphy against</param>
        /// <returns>true if that is isomorph to this, false otherwise</returns>
        public override bool IsIsomorph(IGraph that)
        {
            return IsIsomorph(that, true);
        }

        /// <summary>
        /// Returns whether this graph is isomorph to that graph, neglecting the attribute values, only structurally
        /// Each graph must be either unanalyzed or unchanged since the last analyze,
        /// otherwise results will be wrong!
        /// </summary>
        /// <param name="that">The other graph we check for isomorphy against, neglecting attribute values</param>
        /// <returns>true if that is isomorph (regarding structure) to this, false otherwise</returns>
        public override bool HasSameStructure(IGraph that)
        {
            return IsIsomorph(that, false);
        }

        public bool IsIsomorph(IGraph that, bool includingAttributes)
        {
            // compare number of elements per type
            for(int i = 0; i < nodesByTypeCounts.Length; ++i)
                if(nodesByTypeCounts[i] != ((LGSPGraph)that).nodesByTypeCounts[i])
                    return false;
            for(int i = 0; i < edgesByTypeCounts.Length; ++i)
                if(edgesByTypeCounts[i] != ((LGSPGraph)that).edgesByTypeCounts[i])
                    return false;

            // ensure graphs are analyzed
            if(vstructs == null)
                AnalyzeGraph();
            if(((LGSPGraph)that).vstructs == null)
                ((LGSPGraph)that).AnalyzeGraph();

            // compare analyze statistics
            int numNodeTypes = Model.NodeModel.Types.Length;
            int numEdgeTypes = Model.EdgeModel.Types.Length;
            for(int sourceType = 0; sourceType < numNodeTypes; ++sourceType)
            {
                for(int edgeType = 0; edgeType < numEdgeTypes; ++edgeType)
                {
                    for(int targetType = 0; targetType < numNodeTypes; ++targetType)
                    {
                        for(int direction = 0; direction < 2; ++direction)
                        {
#if MONO_MULTIDIMARRAY_WORKAROUND
                            int vthis = vstructs[((sourceType * dim1size + edgeType) * dim2size + targetType) * 2 + direction];
                            int vthat = ((LGSPGraph)that).vstructs[((sourceType * dim1size + edgeType) * dim2size + targetType) * 2 + direction];
#else
                            int vthis = vstructs[sourceType, edgeType, targetType, direction];
                            int vthat = ((LGSPGraph)that).vstructs[sourceType, edgeType, targetType, direction];
#endif
                            if(Model.EdgeModel.Types[edgeType].Directedness != Directedness.Directed)
                            {
                                // for not directed edges the direction information is meaningless, even worse: random, so we must merge before comparing
#if MONO_MULTIDIMARRAY_WORKAROUND
                                vthis += vstructs[((targetType * dim1size + edgeType) * dim2size + sourceType) * 2 + 1];
                                vthat += ((LGSPGraph)that).vstructs[((targetType * dim1size + edgeType) * dim2size + sourceType) * 2 + 1];
#else
                                vthis += vstructs[targetType, edgeType, sourceType, 1];
                                vthat += ((LGSPGraph)that).vstructs[targetType, edgeType, sourceType, 1];
#endif
                                if(vthis != vthat)
                                    return false;

                                continue;
                            }
                            else
                            {
                                if(vthis != vthat)
                                    return false;
                            }
                        }
                    }
                }
            }

            // they were the same? then we must try to match that in this
            // for this we build an interpretation plan out of the graph
            LGSPMatcherGenerator matcherGen = new LGSPMatcherGenerator(this.model);
            PatternGraph patternGraph = matcherGen.BuildPatternGraph((LGSPGraph)that, includingAttributes);
            PlanGraph planGraph = matcherGen.GeneratePlanGraph(this, patternGraph, false, false);
            matcherGen.MarkMinimumSpanningArborescence(planGraph, patternGraph.name);
            SearchPlanGraph searchPlanGraph = matcherGen.GenerateSearchPlanGraph(planGraph);
            ScheduledSearchPlan scheduledSearchPlan = matcherGen.ScheduleSearchPlan(
                searchPlanGraph, patternGraph, false);
            InterpretationPlanBuilder builder = new InterpretationPlanBuilder(scheduledSearchPlan, searchPlanGraph);
            InterpretationPlan interpretationPlan = builder.BuildInterpretationPlan();

            // and execute the interpretation plan, matching that in this
            // that's sufficient for isomorphy because 
            // - element numbers are the same 
            // - we match only exact types
            return interpretationPlan.Execute(this);
        }

        public override void Check()
        {
            CheckTypeRinglistsBroken();
            CheckEmptyFlags();
            foreach(INode node in Nodes)
                CheckInOutRinglistsBroken((LGSPNode)node);
        }
    }
}
