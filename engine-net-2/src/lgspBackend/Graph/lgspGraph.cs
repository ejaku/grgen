/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

//#define CHECK_ISOCOMPARE_CANONIZATION_AGREE
//#define CHECK_RINGLISTS
//#define CHECK_VISITED_FLAGS_CLEAR_ON_FREE

using System;
using System.Collections.Generic;
using System.Reflection;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// An implementation of the IGraph interface.
    /// </summary>
    public class LGSPGraph : BaseGraph
    {
        // counter for ids, used for naming and to determine the age
        private static int graphIDSource = 0;

        protected static String GetGraphName()
        {
            return "lgspGraph_" + graphIDSource;
        }
        private static int GetGraphId()
        {
            return graphIDSource;
        }
        private static void NextGraphIdAndName()
        {
            ++graphIDSource;
        }

        private readonly int graphID;
        public override int GraphId { get { return graphID; } }

        private String name;

        protected readonly IGraphModel model;
        internal readonly String modelAssemblyName;

        protected static IDictionary<IGraphElement, IGraphElement> tmpOldToNewMap; // workaround to hide map parameter passing in copy constructor

        private readonly IIndexSet indices;
        private readonly LGSPUniquenessEnsurer uniquenessEnsurer; // not null if unique ids for nodes/edges were requested
        private readonly LGSPGlobalVariables globalVariables;

        // Used as storage space for the name for the SettingAddedEdgeNames event, in case of redirection
        public readonly string[] nameOfSingleElementAdded = new string[1];


        private static bool reuseOptimization = true;
        public static int poolSize = 10; // intitialize with default pool size, can be changed but won't be applied after per-element-type pool creation

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

        
        long changesCounter = 0;

        /// <summary>
        /// Returns a counter of the number of changes that occured since the graph was created.
        /// If it's different since last time you visited, the graph has changed (but it may be back again in the original state).
        /// Only graph structure changes are counted, attribute changes are not included.
        /// </summary>
        public override long ChangesCounter
        {
            get { return changesCounter; }
        }


        /// <summary>
        /// Stores the statistics about the last analyze pass of the graph
        /// </summary>
        public readonly LGSPGraphStatistics statistics;

        public long changesCounterAtLastAnalyze = -1;

        /// <summary>
        /// Analyzes the graph.
        /// The calculated data is used to generate good searchplans for the current graph.
        /// </summary>
        public void AnalyzeGraph()
        {
            if(changesCounterAtLastAnalyze == changesCounter)
                return;
            changesCounterAtLastAnalyze = changesCounter;

            statistics.AnalyzeGraph(this);
        }

        /// <summary>
        /// Normally null, contains some data which allows for efficient graph comparison,
        /// in case this is a constant graph which was used for isomorphy checking
        /// </summary>
        public GraphMatchingState matchingState;

        String canonicalRepresentation = null;

        public long changesCounterAtLastCanonize = -1;


        /// <summary>
        /// An array containing one head of a doubly-linked ring-list for each node type indexed by the type ID.
        /// </summary>
        public readonly LGSPNode[] nodesByTypeHeads;

        /// <summary>
        /// The number of nodes for each node type indexed by the type ID.
        /// </summary>
        public readonly int[] nodesByTypeCounts;

        /// <summary>
        /// An array containing one head of a doubly-linked ring-list for each edge type indexed by the type ID.
        /// </summary>
        public readonly LGSPEdge[] edgesByTypeHeads;

        /// <summary>
        /// The number of edges for each edge type indexed by the type ID.
        /// </summary>
        public readonly int[] edgesByTypeCounts;


        /// <summary>
        /// a list with the isomorphy spaces, each contains in a dictionary the elements matched locally 
        /// used in case the is-matched-bits in the graph elements are not sufficient (during non-parallel matching)
        /// </summary>
        public readonly List<Dictionary<IGraphElement, IGraphElement>> inIsoSpaceMatchedElements
            = new List<Dictionary<IGraphElement, IGraphElement>>();

        /// <summary>
        /// a list with the isomorphy spaces, each contains in a dictionary the elements matched globally 
        /// used in case the is-matched-bits in the graph elements are not sufficient (during non-parallel matching)
        /// </summary>
        public readonly List<Dictionary<IGraphElement, IGraphElement>> inIsoSpaceMatchedElementsGlobal
            = new List<Dictionary<IGraphElement, IGraphElement>>();


        public object graphMatchingLock = new object();

        /// <summary>
        /// a list which stores for each parallel matcher thread, per graph element the flags with the matching state
        /// each flag encodes a bounded amount of the is-matched-bits for the lowest isomorphy spaces,
        /// the flag is a reduced version of the flags bitvector available in each graph element in case of single threaded matching
        /// (the unique id of the graph elements is used as index into the flags array)
        /// </summary>
        public List<List<ushort>> flagsPerThreadPerElement;
        
        /// <summary>
        /// a list which stores for each matcher thread the isomorphy spaces, each contains in a dictionary the elements matched locally 
        /// used in case the is-matched-bits in the per-thread flags array are not sufficient, employed during parallelized matching
        /// the outermost list is read concurrently, so its dimension/content must be fixed before matching begins
        /// </summary>
        public List<List<Dictionary<IGraphElement, IGraphElement>>> perThreadInIsoSpaceMatchedElements;

        /// <summary>
        /// a list which stores for each matcher thread the isomorphy spaces, each contains in a dictionary the elements matched globally
        /// used in case the is-matched-bits in the per-thread flags array are not sufficient, employed during parallelized matching
        /// the outermost list is read concurrently, so its dimension/content must be fixed before matching begins
        /// </summary>
        public List<List<Dictionary<IGraphElement, IGraphElement>>> perThreadInIsoSpaceMatchedElementsGlobal;

        protected readonly Dictionary<String, String> customCommandsToDescriptions;


        /// <summary>
        /// Constructs an LGSPGraph object with the given model, the given global variables, and an automatically generated name.
        /// </summary>
        /// <param name="grmodel">The graph model.</param>
        /// <param name="globalVars">The global variables.</param>
        public LGSPGraph(IGraphModel grmodel, IGlobalVariables globalVars)
            : this(grmodel, globalVars, GetGraphName())
        {
        }

        /// <summary>
        /// Constructs an LGSPGraph object with the given model, the given global variables, and the given name.
        /// </summary>
        /// <param name="grmodel">The graph model.</param>
        /// <param name="globalVars">The global variables.</param>
        /// <param name="grname">The name for the graph.</param>
        public LGSPGraph(IGraphModel grmodel, IGlobalVariables globalVars, String grname)
        {
            model = grmodel;
            modelAssemblyName = Assembly.GetAssembly(grmodel.GetType()).Location;
            globalVariables = (LGSPGlobalVariables)globalVars;

            graphID = GetGraphId();
            NextGraphIdAndName();
            name = grname;

            InitializeGraph(out nodesByTypeHeads, out nodesByTypeCounts,
                out edgesByTypeHeads, out edgesByTypeCounts,
                out uniquenessEnsurer, out indices,
                out statistics, out customCommandsToDescriptions);
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="dataSource">The LGSPGraph object to get the data from</param>
        /// <param name="newName">Name of the copied graph.</param>
        /// <param name="oldToNewMap">A map of the old elements to the new elements after cloning.</param>
        public LGSPGraph(LGSPGraph dataSource, String newName, out IDictionary<IGraphElement, IGraphElement> oldToNewMap)
        {
            model = dataSource.model;
            modelAssemblyName = dataSource.modelAssemblyName;
            globalVariables = dataSource.globalVariables;

            graphID = GetGraphId();
            NextGraphIdAndName();
            name = newName;

            InitializeGraph(out nodesByTypeHeads, out nodesByTypeCounts,
                out edgesByTypeHeads, out edgesByTypeCounts,
                out uniquenessEnsurer, out indices,
                out statistics, out customCommandsToDescriptions);

            statistics.Copy(dataSource);

            oldToNewMap = new Dictionary<IGraphElement, IGraphElement>();

            for(int i = 0; i < dataSource.nodesByTypeHeads.Length; i++)
            {
                for(LGSPNode head = dataSource.nodesByTypeHeads[i], node = head.lgspTypePrev; node != head; node = node.lgspTypePrev)
                {
                    LGSPNode newNode = (LGSPNode)node.Clone();
                    AddNodeWithoutEvents(newNode, node.lgspType.TypeID);
                    oldToNewMap[node] = newNode;
                }
            }

            for(int i = 0; i < dataSource.edgesByTypeHeads.Length; i++)
            {
                for(LGSPEdge head = dataSource.edgesByTypeHeads[i], edge = head.lgspTypePrev; edge != head; edge = edge.lgspTypePrev)
                {
                    LGSPEdge newEdge = (LGSPEdge)edge.Clone((INode)oldToNewMap[edge.lgspSource], (INode)oldToNewMap[edge.lgspTarget]);
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

            model.FillIndexSetAsClone(this, dataSource, oldToNewMap);
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="dataSource">The LGSPGraph object to get the data from</param>
        /// <param name="newName">Name of the copied graph.</param>
        public LGSPGraph(LGSPGraph dataSource, String newName)
            : this(dataSource, newName, out tmpOldToNewMap)
        {
            tmpOldToNewMap = null;
        }

        private void InitializeGraph(out LGSPNode[] nodesByTypeHeads, out int[] nodesByTypeCounts, 
            out LGSPEdge[] edgesByTypeHeads, out int[] edgesByTypeCounts,
            out LGSPUniquenessEnsurer uniquenessEnsurer, out IIndexSet indices,
            out LGSPGraphStatistics statistics, out Dictionary<string, string> customCommandsToDescriptions)
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

            uniquenessEnsurer = (LGSPUniquenessEnsurer)model.CreateUniquenessHandler(this);
            indices = model.CreateIndexSet(this);

            statistics = new LGSPGraphStatistics(model);

            customCommandsToDescriptions = new Dictionary<string, string>();
            FillCustomCommandDescriptions();
        }

        public void EnsureSufficientIsomorphySpacesForParallelizedMatchingAreAvailable(int numberOfThreads)
        {
            if(!model.AreFunctionsParallelized && !(model.BranchingFactorForEqualsAny > 1))
            {
                ConsoleUI.errorOutWriter.WriteLine("A parallelized matcher or graph comparison function requires a parallelized model (declared by \"for function[parallelize=true];\" for parallelized matchers (requiring functions) or \"for equalsAny[parallelize=2];\" (or higher constant) for graph comparison functions).");
                ConsoleUI.errorOutWriter.WriteLine("(In case of the former: the (external) functions and function methods are then available in versions supporting parallel execution.)");
                ConsoleUI.errorOutWriter.WriteLine("The model does not support parallelization - it seems it was overwritten by a model from a generation run without parallelization.");
                throw new NotImplementedException("Parallelized matching not supported.");
            }

            // we assume that all perThread members are initialized the same
            if(flagsPerThreadPerElement == null)
            {
                flagsPerThreadPerElement = new List<List<ushort>>(numberOfThreads);
                perThreadInIsoSpaceMatchedElements = new List<List<Dictionary<IGraphElement, IGraphElement>>>(numberOfThreads);
                perThreadInIsoSpaceMatchedElementsGlobal = new List<List<Dictionary<IGraphElement, IGraphElement>>>(numberOfThreads);
            }
            if(flagsPerThreadPerElement.Count < numberOfThreads)
            {
                int additionalNumberOfThreads = numberOfThreads - flagsPerThreadPerElement.Count;
                for(int i = 0; i < additionalNumberOfThreads; ++i)
                {
                    List<ushort> flags = new List<ushort>();
                    flagsPerThreadPerElement.Add(flags);
                    List<Dictionary<IGraphElement, IGraphElement>> inIsoSpaceMatchedElements = new List<Dictionary<IGraphElement, IGraphElement>>();
                    perThreadInIsoSpaceMatchedElements.Add(inIsoSpaceMatchedElements);
                    List<Dictionary<IGraphElement, IGraphElement>> inIsoSpaceMatchedElementsGlobal = new List<Dictionary<IGraphElement, IGraphElement>>();
                    perThreadInIsoSpaceMatchedElementsGlobal.Add(inIsoSpaceMatchedElementsGlobal);
                }
                uniquenessEnsurer.InitialFillFlags(additionalNumberOfThreads, numberOfThreads);
            }
        }

        public void AllocateFurtherIsomorphySpaceNestingLevelForParallelizedMatching(int threadId)
        {
            Dictionary<IGraphElement, IGraphElement> inIsoSpaceMatchedElements = new Dictionary<IGraphElement, IGraphElement>();
            perThreadInIsoSpaceMatchedElements[threadId].Add(inIsoSpaceMatchedElements);
            Dictionary<IGraphElement, IGraphElement> inIsoSpaceMatchedElementsGlobal = new Dictionary<IGraphElement, IGraphElement>();
            perThreadInIsoSpaceMatchedElementsGlobal[threadId].Add(inIsoSpaceMatchedElementsGlobal);
        }

		/// <summary>
		/// A name associated with the graph.
		/// </summary>
        public override String Name 
        { 
            get { return name; }
            set { name = value; }
        }

		/// <summary>
		/// The model associated with the graph.
		/// </summary>
        public override IGraphModel Model
        {
            get { return model; }
        }

        /// <summary>
        /// The indices associated with the graph.
        /// </summary>
        public override IIndexSet Indices
        {
            get { return indices; }
        }

        /// <summary>
        /// The uniqueness handler associated with the graph.
        /// </summary>
        public override IUniquenessHandler UniquenessHandler
        {
            get { return uniquenessEnsurer; }
        }

        /// <summary>
        /// The global variables of the graph rewrite system; convenience access to save parameter passing.
        /// </summary>
        public override IGlobalVariables GlobalVariables
        {
            get { return globalVariables; }
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
            {
                num += nodesByTypeCounts[type.TypeID];
            }
            return num;
        }

        /// <summary>
        /// Returns the number of edges compatible to the given edge type.
        /// </summary>
        public override int GetNumCompatibleEdges(EdgeType edgeType)
        {
            int num = 0;
            foreach(EdgeType type in edgeType.SubOrSameTypes)
            {
                num += edgesByTypeCounts[type.TypeID];
            }
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
        /// Gets the graph element for the given unique id.
        /// Only available if the unique index was declared in the model.
        /// </summary>
        /// <param name="unique">The unique if of a graph element.</param>
        /// <returns>The graph element for the given unique id or null, if there is no graph element with this unique id.</returns>
        public override IGraphElement GetGraphElement(int unique)
        {
            LGSPUniquenessIndex index = uniquenessEnsurer as LGSPUniquenessIndex;
            if(index==null)
                throw new NotSupportedException("Can't fetch graph element by unique id because no unique index was declared");

            // check range
            if(unique >= index.index.Count || unique < 0)
                return null;

            return index.index[unique];
        }

        /// <summary>
        /// Gets the node for the given unique id.
        /// Only available if the unique index was declared in the model.
        /// </summary>
        /// <param name="unique">The unique if of a node.</param>
        /// <returns>The node for the given unique id or null, if there is no node with this unique id.</returns>
        public override INode GetNode(int unique)
        {
            return GetGraphElement(unique) as INode;
        }

        /// <summary>
        /// Gets the edge for the given id.
        /// Only available if the unique index was declared in the model.
        /// </summary>
        /// <param name="unique">The unique if of a edge.</param>
        /// <returns>The edge for the given unique id or null, if there is no edge with this unique id.</returns>
        public override IEdge GetEdge(int unique)
        {
            return GetGraphElement(unique) as IEdge;
        }


        /// <summary>
        /// Moves the type list head of the given node after the given node.
        /// Part of the "list trick".
        /// </summary>
        /// <param name="elem">The node.</param>
        public void MoveHeadAfter(LGSPNode elem)
        {
            if(elem.lgspType == null)
                return;       // elem is head
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
            if(elem.lgspType == null)
                return;       // elem is head
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

            ++nodesByTypeCounts[typeid];

            ++changesCounter;

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

            ++edgesByTypeCounts[typeid];

            ++changesCounter;

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

            --nodesByTypeCounts[typeid];

            ++changesCounter;

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
            if(!lgspNode.Valid)
                return;          // node not in graph (anymore)

            RemovingNode(node);
            RemoveNodeWithoutEvents(lgspNode, lgspNode.lgspType.TypeID);
        }

        /// <summary>
        /// Removes the given edge from the graph.
        /// </summary>
        public override void Remove(IEdge edge)
        {
            LGSPEdge lgspEdge = (LGSPEdge) edge;
            if(!lgspEdge.Valid)
                return;          // edge not in graph (anymore)

            RemovingEdge(edge);

            lgspEdge.lgspSource.RemoveOutgoing(lgspEdge);
            lgspEdge.lgspTarget.RemoveIncoming(lgspEdge);

            lgspEdge.lgspTypePrev.lgspTypeNext = lgspEdge.lgspTypeNext;
            lgspEdge.lgspTypeNext.lgspTypePrev = lgspEdge.lgspTypePrev;
            lgspEdge.lgspTypeNext = null;
            lgspEdge.lgspTypePrev = null;

            --edgesByTypeCounts[lgspEdge.lgspType.TypeID];

            ++changesCounter;

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
            {
                Remove(edge);
            }
            foreach(LGSPEdge edge in lnode.Incoming)
            {
                Remove(edge);
            }
        }

        /// <summary>
        /// Removes all nodes and edges (including any variables pointing to them) from the graph.
        /// </summary>
        public override void Clear()
        {
            ClearingGraph();

            for(int i = 0; i < model.NodeModel.Types.Length; ++i)
            {
                LGSPNode head = nodesByTypeHeads[i];
                head.lgspTypeNext = head;
                head.lgspTypePrev = head;
                nodesByTypeCounts[i] = 0;
            }
            for(int i = 0; i < model.EdgeModel.Types.Length; ++i)
            {
                LGSPEdge head = edgesByTypeHeads[i];
                head.lgspTypeNext = head;
                head.lgspTypePrev = head;
                edgesByTypeCounts[i] = 0;
            }

            statistics.ResetStatisticalData();

            ++changesCounter;
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

            ++changesCounter;

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

            ++changesCounter;

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
            if(source == target)
                return;

            while(source.lgspOuthead!=null)
            {
                if(source.lgspOuthead.Target==source)
                    RedirectSourceAndTarget(source.lgspOuthead, target, target, sourceName, sourceName);
                else
                    RedirectSource(source.lgspOuthead, target, sourceName);
            }
            while(source.lgspInhead != null)
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
            ++changesCounter;
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
            ++changesCounter;
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
            ++changesCounter;
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
            /// This is unused, if the dictionary is used for this visitor ID.
            /// </summary>
            public bool NodesMarked = false;

            /// <summary>
            /// Specifies whether this visitor has already marked any edges.
            /// This is unused, if the dictionary is used for this visitor ID.
            /// </summary>
            public bool EdgesMarked = false;

            /// <summary>
            /// A hash map containing all visited elements (in the keys, the dictionary values are not used).
            /// This is unused (and thus null), if the graph element flags are used for this visitor ID.
            /// </summary>
            public Dictionary<IGraphElement, bool> VisitedElements;

            /// <summary>
            /// Tells whether this flag was reserved by the transaction manager,
            /// preventing it from getting handed out in a valloc again
            /// (because the flag was freed during a transaction,
            /// and might not be allocatable again during transaction rollback without reservation.)
            /// </summary>
            public bool IsReserved = false;

            /// <summary>
            /// Tells whether the reserved flag is to be unreserved again.
            /// </summary>
            public bool ToBeUnreserved = false;
        }

        List<VisitorData> availableVisitors = new List<VisitorData>();
        Deque<int> indicesOfFreeVisitors = new Deque<int>();

        /// <summary>
        /// Allocates a visited flag on the graph elements.
        /// </summary>
        /// <returns>A visitor ID to be used in
        /// visited conditions in patterns ("if { !elem.visited[id]; }"),
        /// visited expressions in evals ("elem.visited[id] = true; b.flag = elem.visited[id] || c.flag; "}
        /// and calls to other visitor functions.</returns>
        public override int AllocateVisitedFlag()
        {
            int newID;
            if(indicesOfFreeVisitors.Count > 0)
                newID = indicesOfFreeVisitors.Dequeue();
            else
            {
                availableVisitors.Add(new VisitorData());
                newID = availableVisitors.Count - 1;
                if(newID >= (int)LGSPElemFlags.NUM_SUPPORTED_VISITOR_IDS)
                    availableVisitors[newID].VisitedElements = new Dictionary<IGraphElement, bool>();
                else
                    availableVisitors[newID].NodesMarked = availableVisitors[newID].EdgesMarked = false;
            }
            VisitedAlloc(newID);
            return newID;
        }

        /// <summary>
        /// Frees a visited flag.
        /// This is a safe but O(n) operation, as it resets the visited flag in the graph.
        /// </summary>
        /// <param name="visitorID">The ID of the visited flag to be freed.</param>
        public override void FreeVisitedFlag(int visitorID)
        {
            ResetVisitedFlag(visitorID);
            FreeVisitedFlagNonReset(visitorID);
        }

        /// <summary>
        /// Frees a clean visited flag.
        /// This is an O(1) but potentially unsafe operation.
        /// Attention! A marked element stays marked, so a later allocation hands out a dirty visited flag! 
        /// Use only if you can ensure that all elements of that flag are unmarked before calling.
        /// </summary>
        /// <param name="visitorID">The ID of the visited flag to be freed.</param>
        public override void FreeVisitedFlagNonReset(int visitorID)
        {
            if(visitorID < (int)LGSPElemFlags.NUM_SUPPORTED_VISITOR_IDS)     // id supported by flags?
            {
#if CHECK_VISITED_FLAGS_CLEAR_ON_FREE
                for(int i = 0; i < nodesByTypeHeads.Length; ++i)
                {
                    for(LGSPNode head = nodesByTypeHeads[i], node = head.lgspTypePrev; node != head; node = node.lgspTypePrev)
                    {
                        if((node.lgspFlags & ((uint)LGSPElemFlags.IS_VISITED << visitorID)) == ((uint)LGSPElemFlags.IS_VISITED << visitorID))
                            throw new Exception("Nodes are still marked on vfree!");
                    }
                }
                for(int i = 0; i < edgesByTypeHeads.Length; ++i)
                {
                    for(LGSPEdge head = edgesByTypeHeads[i], edge = head.lgspTypePrev; edge != head; edge = edge.lgspTypePrev)
                    {
                        if((edge.lgspFlags & ((uint)LGSPElemFlags.IS_VISITED << visitorID)) == ((uint)LGSPElemFlags.IS_VISITED << visitorID))
                            throw new Exception("Edges are still marked on vfree!");
                    }
                }
#endif
            }
            else
            {
                // the check for the dictionary visited flags is cheap, so we always do this (in contrast to the graph element visited flags, we only check them in case a debug ifdef is set)
                if(availableVisitors[visitorID].VisitedElements.Count > 0)
                    throw new Exception("Elements are still marked on vfree!");
            }

            if(!availableVisitors[visitorID].IsReserved)
                VisitedFree(visitorID); // the transaction manager may reserve the flag during this notification

            if(!availableVisitors[visitorID].IsReserved || availableVisitors[visitorID].ToBeUnreserved)
            {
                // prefer graph element visitors over dictionary based visitors
                if(visitorID < (int)LGSPElemFlags.NUM_SUPPORTED_VISITOR_IDS)
                    indicesOfFreeVisitors.EnqueueFront(visitorID);
                else
                    indicesOfFreeVisitors.Enqueue(visitorID);

                availableVisitors[visitorID].IsReserved = false;
                availableVisitors[visitorID].ToBeUnreserved = false;
            }
        }

        /// <summary>
        /// Resets the visited flag with the given ID on all graph elements, if necessary.
        /// </summary>
        /// <param name="visitorID">The ID of the visited flag.</param>
        public override void ResetVisitedFlag(int visitorID)
        {
            VisitorData visitor = availableVisitors[visitorID];
            if(visitorID < (int) LGSPElemFlags.NUM_SUPPORTED_VISITOR_IDS)     // id supported by flags?
            {
                uint mask = (uint) LGSPElemFlags.IS_VISITED << visitorID;
                if(visitor.NodesMarked)        // need to clear flag on nodes?
                {
                    for(int i = 0; i < nodesByTypeHeads.Length; ++i)
                    {
                        for(LGSPNode head = nodesByTypeHeads[i], node = head.lgspTypePrev; node != head; node = node.lgspTypePrev)
                        {
                            if((node.lgspFlags & mask) != 0)
                                SettingVisited(node, visitorID, false);
                            node.lgspFlags &= ~mask;
                        }
                    }
                    visitor.NodesMarked = false;
                }
                if(visitor.EdgesMarked)        // need to clear flag on edges?
                {
                    for(int i = 0; i < edgesByTypeHeads.Length; ++i)
                    {
                        for(LGSPEdge head = edgesByTypeHeads[i], edge = head.lgspTypePrev; edge != head; edge = edge.lgspTypePrev)
                        {
                            if((edge.lgspFlags & mask) != 0)
                                SettingVisited(edge, visitorID, false);
                            edge.lgspFlags &= ~mask;
                        }
                    }
                    visitor.EdgesMarked = false;
                }
            }
            else                                                    // no, use hash map
            {
                foreach(KeyValuePair<IGraphElement, bool> kvp in visitor.VisitedElements)
                {
                    SettingVisited(kvp.Key, visitorID, false);
                }
                visitor.VisitedElements.Clear();
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
            SettingVisited(elem, visitorID, visited);
            
            VisitorData visitor = availableVisitors[visitorID];
            if(visitorID < (int) LGSPElemFlags.NUM_SUPPORTED_VISITOR_IDS)     // id supported by flags?
            {
                uint mask = (uint)LGSPElemFlags.IS_VISITED << visitorID;
                LGSPNode node = elem as LGSPNode;
                if(node != null)
                {
                    if(visited)
                    {
                        node.lgspFlags |= mask;
                        visitor.NodesMarked = true;
                    }
                    else
                        node.lgspFlags &= ~mask;
                }
                else
                {
                    LGSPEdge edge = (LGSPEdge) elem;
                    if(visited)
                    {
                        edge.lgspFlags |= mask;
                        visitor.EdgesMarked = true;
                    }
                    else
                        edge.lgspFlags &= ~mask;
                }
            }
            else                                                        // no, use hash map
            {
                if(visited)
                    visitor.VisitedElements[elem] = true;
                else
                    visitor.VisitedElements.Remove(elem);
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
                return availableVisitors[visitorID].VisitedElements.ContainsKey(elem);
        }

        /// <summary>
        /// Returns the ids of the allocated visited flags.
        /// </summary>
        /// <returns>A dynamic array of the visitor ids allocated.</returns>
        public override List<int> GetAllocatedVisitedFlags()
        {
            List<int> result = new List<int>(availableVisitors.Count - indicesOfFreeVisitors.Count);
            for(int i = 0; i < availableVisitors.Count; ++i)
            {
                if(indicesOfFreeVisitors.Contains(i))
                    continue;
                if(availableVisitors[i].IsReserved)
                    continue;

                result.Add(i);
            }
            return result;
        }

        /// <summary>
        /// Called by the transaction manager just when it gets notified about a vfree.
        /// The visited flag freed must be reserved until the transaction finished,
        /// cause otherwise it might be impossible for the transaction manager to roll the vfree back with a valloc;
        /// that may happen if the flag is handed out again in a succeeding valloc, during a transaction pause.
        /// </summary>
        /// <param name="visitorID"></param>
        public void ReserveVisitedFlag(int visitorID)
        {
            availableVisitors[visitorID].IsReserved = true;
        }

        /// <summary>
        /// Called by the transaction manager on flags it reserved from getting handed out again during valloc,
        /// when the transaction finished and it is safe to return those flags again on valloc.
        /// </summary>
        /// <param name="visitorID"></param>
        public void UnreserveVisitedFlag(int visitorID)
        {
            availableVisitors[visitorID].ToBeUnreserved = true;
            FreeVisitedFlagNonReset(visitorID);
        }

        /// <summary>
        /// Called by the transaction manager on flags it reserved from getting handed out again during valloc,
        /// when the transaction is rolled back, and the vfree is undone by a realloc.
        /// </summary>
        /// <param name="visitorID"></param>
        public void ReallocateVisitedFlag(int visitorID)
        {
            if(!availableVisitors[visitorID].IsReserved)
                throw new Exception("Visited flag to reallocate was not reserved!");

            availableVisitors[visitorID].IsReserved = false;

            VisitedAlloc(visitorID);
        }

        #endregion Visited flags management


        #region Visited flag for internal use management

        /// <summary>
        /// Sets the internal-use visited flag of the given graph element.
        /// (Used for computing reachability.)
        /// </summary>
        /// <param name="elem">The graph element whose flag is to be set.</param>
        /// <param name="visited">True for visited, false for not visited.</param>
        public override void SetInternallyVisited(IGraphElement elem, bool visited)
        {
            LGSPNode node = elem as LGSPNode;
            if(visited)
            {
                if(node != null)
                    node.lgspFlags |= (uint)LGSPElemFlags.IS_VISITED_INTERNALLY;
                else
                    (elem as LGSPEdge).lgspFlags |= (uint)LGSPElemFlags.IS_VISITED_INTERNALLY;
            }
            else
            {
                if(node != null)
                    node.lgspFlags &= ~(uint)LGSPElemFlags.IS_VISITED_INTERNALLY;
                else
                    (elem as LGSPEdge).lgspFlags &= ~(uint)LGSPElemFlags.IS_VISITED_INTERNALLY;
            }
        }

        /// <summary>
        /// Returns whether the given graph element has been internally visited.
        /// (Used for computing reachability.)
        /// </summary>
        /// <param name="elem">The graph element whose flag is to be retrieved.</param>
        /// <returns>True for visited, false for not visited.</returns>
        public override bool IsInternallyVisited(IGraphElement elem)
        {
            LGSPNode node = elem as LGSPNode;
            if(node != null)
                return (node.lgspFlags & (uint)LGSPElemFlags.IS_VISITED_INTERNALLY) == (uint)LGSPElemFlags.IS_VISITED_INTERNALLY;
            else
                return ((elem as LGSPEdge).lgspFlags & (uint)LGSPElemFlags.IS_VISITED_INTERNALLY) == (uint)LGSPElemFlags.IS_VISITED_INTERNALLY;
        }

        /// <summary>
        /// Sets the internal-use visited flag of the given graph element.
        /// (Used for computing reachability when employed from a parallelized matcher executed by the thread pool.)
        /// </summary>
        /// <param name="elem">The graph element whose flag is to be set.</param>
        /// <param name="visited">True for visited, false for not visited.</param>
        /// <param name="threadId">The id of the thread which marks the graph element.</param>
        public override void SetInternallyVisited(IGraphElement elem, bool visited, int threadId)
        {
            List<ushort> flagsPerElement = flagsPerThreadPerElement[threadId];
            LGSPNode node = elem as LGSPNode;
            if(visited)
            {
                if(node != null)
                    flagsPerElement[node.uniqueId] |= (ushort)LGSPElemFlagsParallel.IS_VISITED_INTERNALLY;
                else
                    flagsPerElement[(elem as LGSPEdge).uniqueId] |= (ushort)LGSPElemFlagsParallel.IS_VISITED_INTERNALLY;
            }
            else
            {
                if(node != null)
                    flagsPerElement[node.uniqueId] &= (ushort)~LGSPElemFlagsParallel.IS_VISITED_INTERNALLY;
                else
                {
                    flagsPerElement[(elem as LGSPEdge).uniqueId] &= (ushort)~LGSPElemFlagsParallel.IS_VISITED_INTERNALLY;
                }
            }
        }

        /// <summary>
        /// Returns whether the given graph element has been internally visited.
        /// (Used for computing reachability when employed from a parallelized matcher executed by the thread pool.)
        /// </summary>
        /// <param name="elem">The graph element whose flag is to be retrieved.</param>
        /// <param name="threadId">The id of the thread which queries the marking of the graph element.</param>
        /// <returns>True for visited, false for not visited.</returns>
        public override bool IsInternallyVisited(IGraphElement elem, int threadId)
        {
            List<ushort> flagsPerElement = flagsPerThreadPerElement[threadId];
            LGSPNode node = elem as LGSPNode;
            if(node != null)
                return (flagsPerElement[node.uniqueId] & (ushort)LGSPElemFlagsParallel.IS_VISITED_INTERNALLY) == (ushort)LGSPElemFlagsParallel.IS_VISITED_INTERNALLY;
            else
                return (flagsPerElement[(elem as LGSPEdge).uniqueId] & (ushort)LGSPElemFlagsParallel.IS_VISITED_INTERNALLY) == (ushort)LGSPElemFlagsParallel.IS_VISITED_INTERNALLY;
        }

        #endregion Visited flag for internal use management


        /// <summary>
        /// Checks if the matching state flags in the graph are not set, as they should be in case no matching is undereway
        /// </summary>
        public void CheckEmptyFlags()
        {
            foreach(NodeType nodeType in Model.NodeModel.Types)
            {
                for(LGSPNode nodeHead = nodesByTypeHeads[nodeType.TypeID], node = nodeHead.lgspTypeNext; node != nodeHead; node = node.lgspTypeNext)
                {
                    if(node.lgspFlags != 0 && node.lgspFlags != 1)
                        throw new Exception("Internal error: No matching underway, but matching state still in graph.");
                }
            }

            foreach(EdgeType edgeType in Model.EdgeModel.Types)
            {
                for(LGSPEdge edgeHead = edgesByTypeHeads[edgeType.TypeID], edge = edgeHead.lgspTypeNext; edge != edgeHead; edge = edge.lgspTypeNext)
                {
                    if(edge.lgspFlags != 0 && edge.lgspFlags != 1)
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
                if(cur == node)
                    throw new Exception("Internal error: Node already available in ringlist");
                cur = cur.lgspTypeNext;
            }
            cur = head.lgspTypePrev;
            while(cur != head)
            {
                if(cur == node)
                    throw new Exception("Internal error: Node already available in ringlist");
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
                if(cur == edge)
                    throw new Exception("Internal error: Edge already available in ringlist");
                cur = cur.lgspTypeNext;
            }
            cur = head.lgspTypePrev;
            while(cur != head)
            {
                if(cur == edge)
                    throw new Exception("Internal error: Edge already available in ringlist");
                cur = cur.lgspTypePrev;
            }
        }

        /// <summary>
        /// Checks whether the type ringlist starting at the given head node is broken.
        /// Use for debugging purposes.
        /// </summary>
        /// <param name="head">The node head to be checked.</param>
        public void CheckTypeRinglistBroken(LGSPNode head)
        {
            LGSPNode headNext = head.lgspTypeNext;
            if(headNext == null)
                throw new Exception("Internal error: Ringlist broken");
            LGSPNode cur = headNext;
            LGSPNode next;
            while(cur != head)
            {
                if(cur != cur.lgspTypeNext.lgspTypePrev)
                    throw new Exception("Internal error: Ringlist out of order");
                next = cur.lgspTypeNext;
                if(next == null)
                    throw new Exception("Internal error: Ringlist broken");
                if(next == headNext)
                    throw new Exception("Internal error: Ringlist loops bypassing head");
                cur = next;
            }

            LGSPNode headPrev = head.lgspTypePrev;
            if(headPrev == null)
                throw new Exception("Internal error: Ringlist broken");
            cur = headPrev;
            LGSPNode prev;
            while(cur != head)
            {
                if(cur != cur.lgspTypePrev.lgspTypeNext)
                    throw new Exception("Internal error: Ringlist out of order");
                prev = cur.lgspTypePrev;
                if(prev == null)
                    throw new Exception("Internal error: Ringlist broken");
                if(prev == headPrev)
                    throw new Exception("Internal error: Ringlist loops bypassing head");
                cur = prev;
            }
        }

        /// <summary>
        /// Checks whether the type ringlist starting at the given head edge is broken.
        /// Use for debugging purposes.
        /// </summary>
        /// <param name="head">The edge head to be checked.</param>
        public void CheckTypeRinglistBroken(LGSPEdge head)
        {
            LGSPEdge headNext = head.lgspTypeNext;
            if(headNext == null)
                throw new Exception("Internal error: Ringlist broken");
            LGSPEdge cur = headNext;
            LGSPEdge next;
            while(cur != head)
            {
                if(cur != cur.lgspTypeNext.lgspTypePrev)
                    throw new Exception("Internal error: Ringlist out of order");
                next = cur.lgspTypeNext;
                if(next == null)
                    throw new Exception("Internal error: Ringlist broken");
                if(next == headNext)
                    throw new Exception("Internal error: Ringlist loops bypassing head");
                cur = next;
            }

            LGSPEdge headPrev = head.lgspTypePrev;
            if(headPrev == null)
                throw new Exception("Internal error: type Ringlist broken");
            cur = headPrev;
            LGSPEdge prev;
            while(cur != head)
            {
                if(cur != cur.lgspTypePrev.lgspTypeNext)
                    throw new Exception("Internal error: Ringlist out of order");
                prev = cur.lgspTypePrev;
                if(prev == null)
                    throw new Exception("Internal error: Ringlist broken");
                if(prev == headPrev)
                    throw new Exception("Internal error: Ringlist loops bypassing head");
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
            {
                CheckTypeRinglistBroken(head);
            }
            foreach(LGSPEdge head in edgesByTypeHeads)
            {
                CheckTypeRinglistBroken(head);
            }
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
                if(headNext == null)
                    throw new Exception("Internal error: in Ringlist broken");
                LGSPEdge cur = headNext;
                LGSPEdge next;
                while(cur != inHead)
                {
                    if(cur != cur.lgspInNext.lgspInPrev)
                        throw new Exception("Internal error: Ringlist out of order");
                    next = cur.lgspInNext;
                    if(next == null)
                        throw new Exception("Internal error: Ringlist broken");
                    if(next == headNext)
                        throw new Exception("Internal error: Ringlist loops bypassing head");
                    cur = next;
                }
            }
            if(inHead != null)
            {
                LGSPEdge headPrev = inHead.lgspInPrev;
                if(headPrev == null)
                    throw new Exception("Internal error: Ringlist broken");
                LGSPEdge cur = headPrev;
                LGSPEdge prev;
                while(cur != inHead)
                {
                    if(cur != cur.lgspInPrev.lgspInNext)
                        throw new Exception("Internal error: Ringlist out of order");
                    prev = cur.lgspInPrev;
                    if(prev == null)
                        throw new Exception("Internal error: Ringlist broken");
                    if(prev == headPrev)
                        throw new Exception("Internal error: Ringlist loops bypassing head");
                    cur = prev;
                }
            }
            LGSPEdge outHead = node.lgspOuthead;
            if(outHead != null)
            {
                LGSPEdge headNext = outHead.lgspOutNext;
                if(headNext == null)
                    throw new Exception("Internal error: Ringlist broken");
                LGSPEdge cur = headNext;
                LGSPEdge next;
                while(cur != outHead)
                {
                    if(cur != cur.lgspOutNext.lgspOutPrev)
                        throw new Exception("Internal error: Ringlist out of order");
                    next = cur.lgspOutNext;
                    if(next == null)
                        throw new Exception("Internal error: Ringlist broken");
                    if(next == headNext)
                        throw new Exception("Internal error: Ringlist loops bypassing head");
                    cur = next;
                }
            }
            if(outHead != null)
            {
                LGSPEdge headPrev = outHead.lgspOutPrev;
                if(headPrev == null)
                    throw new Exception("Internal error: Ringlist broken");
                LGSPEdge cur = headPrev;
                LGSPEdge prev;
                while(cur != outHead)
                {
                    if(cur != cur.lgspOutPrev.lgspOutNext)
                        throw new Exception("Internal error: Ringlist out of order");
                    prev = cur.lgspOutPrev;
                    if(prev == null)
                        throw new Exception("Internal error: Ringlist broken");
                    if(prev == headPrev)
                        throw new Exception("Internal error: Ringlist loops bypassing head");
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

        private void FillCustomCommandDescriptions()
        {
            customCommandsToDescriptions.Add("analyze",
                "- analyze: Analyzes the graph. The generated information can then be\n" +
                "     used by Actions implementations to optimize the pattern matching.\n");
            customCommandsToDescriptions.Add("analyze_graph",
                "- analyze_graph: Analyzes the graph. The generated information can then be\n" +
                "     used by Actions implementations to optimize the pattern matching.\n");
            customCommandsToDescriptions.Add("statistics",
                "- statistics save <filename>: Writes the statistics of the last analyze\n" +
                "     to the specified statistics file (the graph must have been analyzed before)\n" +
                "     To be used with the statistics compiler/shell option, to directly emit\n" +
                "     matchers adapted to that class of graphs.\n");
            customCommandsToDescriptions.Add("optimizereuse",
                "- optimizereuse: Sets whether deleted elements may be reused in a rewrite.\n" +
                "     Defaults to: true.\n");
        }

        /// <summary>
        /// The graph-backend dependent commands that are available, and a description of each command.
        /// </summary>
        public override IDictionary<String, String> CustomCommandsAndDescriptions
        {
            get { return customCommandsToDescriptions; }
        }

        /// <summary>
        /// Does graph-backend dependent stuff.
        /// </summary>
        /// <param name="args">Any kind of parameters for the stuff to do; first parameter has to be the command</param>
        public override void Custom(params object[] args)
        {
            if(args.Length == 0)
                throw new ArgumentException("No command given");

            String command = (String)args[0];
            switch(command)
            {
            case "analyze":
            case "analyze_graph":
                {
                    int startticks = Environment.TickCount;
                    AnalyzeGraph();
                    ConsoleUI.outWriter.WriteLine("Graph '{0}' analyzed in {1} ms.", name, Environment.TickCount - startticks);
                    return;
                }

            case "statistics":
                {
                    if(args.Length != 3 || (string)(args[1]) != "save")
                    {
                        throw new ArgumentException("Usage: statistics save \"<filepath>\"\n"
                                + "Writes statistics resulting from analysis to file.");
                    }
                    if(statistics.nodeCounts == null)
                        throw new ArgumentException("The graph is not analyzed yet.");

                    GraphStatisticsParserSerializer parserSerializer = new GraphStatisticsParserSerializer(statistics);
                    parserSerializer.Serialize((string)args[2]);

                    ConsoleUI.outWriter.WriteLine("Statistics about graph written to {0}.", args[2]);
                    return;
                }

            case "optimizereuse":
                if(args.Length != 2)
                {
                    throw new ArgumentException("Usage: optimizereuse <bool>\n"
                            + "If <bool> == true, deleted elements may be reused in a rewrite.\n"
                            + "As a result new elements may not be discriminable anymore from\n"
                            + "already deleted elements using object equality, hash maps, etc.");
                }
                if(!bool.TryParse((String) args[1], out reuseOptimization))
                    throw new ArgumentException("Illegal bool value specified: \"" + (String)args[1] + "\"");
                return;

            case "optimizereuse_poolsize":
                if(args.Length != 2)
                {
                    throw new ArgumentException("Usage: optimizereuse_poolsize <int>\n"
                            + "Sets the pool size for the reusing of deleted elements.\n"
                            + "Changes are not going to be taken care of after the pool size was applied\n"
                            + "(per element type) (and are only going to be applied in case of optimizereuse).\n");
                }
                if(!int.TryParse((String)args[1], out poolSize))
                    throw new ArgumentException("Illegal int value specified: \"" + (String)args[1] + "\"");
                return;

            default:
                throw new ArgumentException("Unknown command: " + command);
            }
        }

        /// <summary>
        /// Duplicates a graph.
        /// The new graph will use the same model as the other
        /// Open transaction data will not be cloned.
        /// </summary>
        /// <param name="newName">Name of the new graph.</param>
        /// <returns>A new graph with the same structure as this graph.</returns>
        public override IGraph Clone(String newName)
        {
            return new LGSPGraph(this, newName);
        }

        /// <summary>
        /// Duplicates a graph.
        /// The new graph will use the same model as the other.
        /// Open transaction data will not be cloned.
        /// </summary>
        /// <param name="newName">Name of the new graph.</param>
        /// <param name="oldToNewMap">A map of the old elements to the corresponding new elements after cloning.</param>
        /// <returns>A new graph with the same structure as this graph.</returns>
        public override IGraph Clone(String newName, out IDictionary<IGraphElement, IGraphElement> oldToNewMap)
        {
            return new LGSPGraph(this, newName, out oldToNewMap);
        }

        /// <summary>
        /// Duplicates a graph, assigning names.
        /// The new graph will use the same model as the other
        /// Open transaction data will not be cloned.
        /// </summary>
        /// <returns>A new named graph with the same structure as this graph.</returns>
        public override INamedGraph CloneAndAssignNames()
        {
            return new LGSPNamedGraph(this);
        }

        /// <summary>
        /// Creates an empty graph using the same model as the other.
        /// </summary>
        /// <param name="newName">Name of the new graph.</param>
        /// <returns>A new empty graph of the same model.</returns>
        public override IGraph CreateEmptyEquivalent(String newName)
        {
            return new LGSPGraph(this.model, this.globalVariables, newName);
        }

        /// <summary>
        /// Returns whether this graph is isomorph to that graph (including the attribute values)
        /// If a graph changed only in attribute values since the last comparison, results will be wrong!
        /// (Do a fake node insert and removal to ensure the graph is recognized as having changed.)
        /// </summary>
        /// <param name="that">The other graph we check for isomorphy against</param>
        /// <returns>true if that is isomorph to this, false otherwise</returns>
        public override bool IsIsomorph(IGraph that)
        {
            lock(graphMatchingLock)
            {
                if(this.matchingState == null)
                    this.matchingState = new GraphMatchingState(this);
                if(((LGSPGraph)that).matchingState == null)
                    ((LGSPGraph)that).matchingState = new GraphMatchingState((LGSPGraph)that);

                bool result = matchingState.IsIsomorph(this, (LGSPGraph)that, true);
#if CHECK_ISOCOMPARE_CANONIZATION_AGREE
                bool otherResult = this.Canonize() == that.Canonize();
                if(result != otherResult)
                {
                    List<string> thisArg = new List<string>();
                    thisArg.Add("this.grs");
                    Porter.Export((INamedGraph)this, thisArg);
                    List<string> thatArg = new List<string>();
                    thatArg.Add("that.grs");
                    Porter.Export((INamedGraph)that, thatArg);
                    throw new Exception("Potential internal error: Isomorphy and Canonization disagree");
                }
#endif
                return result;
            }
        }

        /// <summary>
        /// Returns whether this graph is isomorph to any of the set of graphs given (including the attribute values)
        /// If a graph changed only in attribute values since the last comparison, results will be wrong!
        /// (Do a fake node insert and removal to ensure the graph is recognized as having changed.)
        /// Don't call from a parallelized matcher!
        /// </summary>
        /// <param name="graphsToCheckAgainst">The other graphs we check for isomorphy against</param>
        /// <returns>true if any of the graphs given is isomorph to this, false otherwise</returns>
        public override bool IsIsomorph(IDictionary<IGraph, SetValueType> graphsToCheckAgainst)
        {
            return GetIsomorph(graphsToCheckAgainst) != null;
        }

        /// <summary>
        /// Returns the graph from the set of graphs given that is isomorphic to this graph (including the attribute values), or null if no such graph exists
        /// If a graph changed only in attribute values since the last comparison, results will be wrong!
        /// (Do a fake node insert and removal to ensure the graph is recognized as having changed.)
        /// Don't call from a parallelized matcher!
        /// </summary>
        /// <param name="graphsToCheckAgainst">The other graph we check for isomorphy against</param>
        /// <returns>The isomorphic graph from graphsToCheckAgainst, null if no such graph exists</returns>
        public override IGraph GetIsomorph(IDictionary<IGraph, SetValueType> graphsToCheckAgainst)
        {
            if(graphsToCheckAgainst.Count == 0)
                return null;
            if(graphsToCheckAgainst.ContainsKey(this))
                return this;

            lock(graphMatchingLock)
            {
                if(this.matchingState == null)
                    this.matchingState = new GraphMatchingState(this);

                if(Environment.ProcessorCount == 1 || graphsToCheckAgainst.Count == 1 || model.BranchingFactorForEqualsAny < 2)
                {
                    foreach(IGraph that in graphsToCheckAgainst.Keys)
                    {
                        if(((LGSPGraph)that).matchingState == null)
                            ((LGSPGraph)that).matchingState = new GraphMatchingState((LGSPGraph)that);
                        if(matchingState.IsIsomorph(this, (LGSPGraph)that, true))
                            return that;
                    }
                    return null;
                }
                else
                {
                    ++this.matchingState.numChecks;
                    GraphMatchingState.EnsureIsAnalyzed(this);

                    int numWorkerThreads = WorkerPool.EnsurePoolSize(Math.Min(model.BranchingFactorForEqualsAny, 64));
                    this.EnsureSufficientIsomorphySpacesForParallelizedMatchingAreAvailable(numWorkerThreads);

                    matchingState.graphToCheck = this;
                    matchingState.graphsToCheckAgainstIterator = graphsToCheckAgainst.GetEnumerator();
                    matchingState.iterationLock = 0;
                    matchingState.includingAttributes_ = true;
                    matchingState.wasIso = false;
                    matchingState.graphThatWasIso = null;

                    WorkerPool.Task = matchingState.IsIsomorph;
                    WorkerPool.StartWork(Math.Min(numWorkerThreads, graphsToCheckAgainst.Count));
                    WorkerPool.WaitForWorkDone();

                    matchingState.graphToCheck = null;
                    matchingState.graphsToCheckAgainstIterator = null;
                    return matchingState.graphThatWasIso;
                }
            }
        }


        /// <summary>
        /// Returns whether this graph is isomorph to that graph, neglecting the attribute values, only structurally
        /// <param name="that">The other graph we check for isomorphy against, neglecting attribute values</param>
        /// <returns>true if that is isomorph (regarding structure) to this, false otherwise</returns>
        /// </summary>
        public override bool HasSameStructure(IGraph that)
        {
            lock(graphMatchingLock)
            {
                if(this.matchingState == null)
                    this.matchingState = new GraphMatchingState(this);
                if(((LGSPGraph)that).matchingState == null)
                    ((LGSPGraph)that).matchingState = new GraphMatchingState((LGSPGraph)that);

                bool result = matchingState.IsIsomorph(this, (LGSPGraph)that, false);
#if CHECK_ISOCOMPARE_CANONIZATION_AGREE
                bool otherResult = this.Canonize() == that.Canonize();
                if(result != otherResult)
                {
                    List<string> thisArg = new List<string>();
                    thisArg.Add("this.grs");
                    Porter.Export((INamedGraph)this, thisArg);
                    List<string> thatArg = new List<string>();
                    thatArg.Add("that.grs");
                    Porter.Export((INamedGraph)that, thatArg);
                    throw new Exception("Potential internal error: Isomorphy (without attributes) and Canonization disagree");
                }
#endif
                return result;
            }
        }

        /// <summary>
        /// Returns whether this graph is isomorph to any of the set of graphs given, neglecting the attribute values, only structurally
        /// Don't call from a parallelized matcher!
        /// </summary>
        /// <param name="graphsToCheckAgainst">The other graphs we check for isomorphy against, neglecting attribute values</param>
        /// <returns>true if any of the graphs given is isomorph (regarding structure) to this, false otherwise</returns>
        public override bool HasSameStructure(IDictionary<IGraph, SetValueType> graphsToCheckAgainst)
        {
            return GetSameStructure(graphsToCheckAgainst) != null;
        }

        /// <summary>
        /// Returns the graph from the set of graphs given that is isomorphic to this graph (neglecting the attribute values, only structurally), or null if no such graph exists
        /// Don't call from a parallelized matcher!
        /// </summary>
        /// <param name="graphsToCheckAgainst">The other graphs we check for isomorphy against, neglecting attribute values</param>
        /// <returns>The isomorphic graph from graphsToCheckAgainst (regarding structure), null if no such graph exists</returns>
        public override IGraph GetSameStructure(IDictionary<IGraph, SetValueType> graphsToCheckAgainst)
        {
            if(graphsToCheckAgainst.Count == 0)
                return null;
            if(graphsToCheckAgainst.ContainsKey(this))
                return this;

            lock(graphMatchingLock)
            {
                if(this.matchingState == null)
                    this.matchingState = new GraphMatchingState(this);

                if(Environment.ProcessorCount == 1 || graphsToCheckAgainst.Count == 1 || model.BranchingFactorForEqualsAny < 2)
                {
                    foreach(IGraph that in graphsToCheckAgainst.Keys)
                    {
                        if(((LGSPGraph)that).matchingState == null)
                            ((LGSPGraph)that).matchingState = new GraphMatchingState((LGSPGraph)that);
                        if(matchingState.IsIsomorph(this, (LGSPGraph)that, false))
                            return that;
                    }
                    return null;
                }
                else
                {
                    ++this.matchingState.numChecks;
                    GraphMatchingState.EnsureIsAnalyzed(this);

                    int numWorkerThreads = WorkerPool.EnsurePoolSize(Math.Min(model.BranchingFactorForEqualsAny, 64));
                    this.EnsureSufficientIsomorphySpacesForParallelizedMatchingAreAvailable(numWorkerThreads);

                    matchingState.graphToCheck = this;
                    matchingState.graphsToCheckAgainstIterator = graphsToCheckAgainst.GetEnumerator();
                    matchingState.iterationLock = 0;
                    matchingState.includingAttributes_ = false;
                    matchingState.wasIso = false;
                    matchingState.graphThatWasIso = null;

                    WorkerPool.Task = matchingState.IsIsomorph;
                    WorkerPool.StartWork(Math.Min(numWorkerThreads, graphsToCheckAgainst.Count));
                    WorkerPool.WaitForWorkDone();

                    matchingState.graphToCheck = null;
                    matchingState.graphsToCheckAgainstIterator = null;
                    return matchingState.graphThatWasIso;
                }
            }
        }

        /// <summary>
        /// Returns a canonical representation of the graph as a string
        /// </summary>
        /// <returns>a canonical representation of the graph as a string</returns>
        public override string Canonize()
        {
            if(changesCounterAtLastCanonize != changesCounter)
            {
                SimpleGraphCanonizer canonizer = new SimpleGraphCanonizer();
                canonicalRepresentation = canonizer.Canonize(this);
                changesCounterAtLastCanonize = changesCounter;
            }

            return canonicalRepresentation;
        }

        public override void Check()
        {
            CheckTypeRinglistsBroken();
            CheckEmptyFlags();
            foreach(INode node in Nodes)
            {
                CheckInOutRinglistsBroken((LGSPNode)node);
            }
        }

        public override string ToString()
        {
            return "LGSPGraph " + Name + " id " + graphID + " @ " + ChangesCounter;
        }
    }
}
