/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Text;
using de.unika.ipd.grGen.libConsoleAndOS;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.libGrPersistenceProviderSQLite
{
    /// <summary>
    /// A component that carries out a garbage collection of the host graph, finally removing unreachable deleted entities and compactifying containers (by overwriting event change histories by the current state).
    /// </summary>
    internal class HostGraphCleaner
    {
        // garbage collection depth first search state items appear on the depth first search stack that is used to mark the elements (marking phase of a mark and sweep like garbage collection algorithm)
        abstract class GcDfsStateItem // potential todo: using a struct would avoid memory allocations and .NET garbage collection cycles (not sure whether it's worthwhile)
        {
        }

        class GcDfsStateItemElement : GcDfsStateItem
        {
            internal GcDfsStateItemElement(IAttributeBearer element)
            {
                this.referencesContainedInAttributes = GetReferencesContainedInAttributes(element).GetEnumerator();
            }

            internal IEnumerator<object> referencesContainedInAttributes; // potential todo: not using an own enumerator with yield would avoid memory allocations and garbage collection cycles (not sure whether it's worthwhile, the code is simpler/cleaner this way)
        }

        class GcDfsStateItemGraph : GcDfsStateItem
        {
            internal GcDfsStateItemGraph(IGraph graph)
            {
                this.graphElements = GetGraphElements(graph).GetEnumerator();
            }

            internal IEnumerator<IGraphElement> graphElements; // potential todo: not using an own enumerator with yield would avoid memory allocations and garbage collection cycles (not sure whether it's worthwhile, the code is simpler/cleaner this way)
        }


        // prepared statements for handling nodes (assuming available node related tables)
        SQLiteCommand[] deleteNodeCommands; // per-type
        Dictionary<String, SQLiteCommand>[] deleteNodeContainerCommands; // per-type, per-container-attribute

        // prepared statements for handling edges (assuming available edge related tables)
        SQLiteCommand[] deleteEdgeCommands; // per-type
        Dictionary<String, SQLiteCommand>[] deleteEdgeContainerCommands; // per-type, per-container-attribute

        // prepared statements for handling graphs
        SQLiteCommand deleteGraphCommand; // topology

        // prepared statements for handling objects (assuming available object related tables)
        SQLiteCommand deleteObjectCommand; // topology
        SQLiteCommand[] deleteObjectCommands; // per-type
        Dictionary<String, SQLiteCommand>[] deleteObjectContainerCommands; // per-type, per-container-attribute

        Dictionary<object, SetValueType> visited = new Dictionary<object, SetValueType>(); // later todo: implement visited flags in all entities, use them instead of a visited dictionary
        Stack<GcDfsStateItem> gcDfsStateItems = new Stack<GcDfsStateItem>(); // explicit stack to avoid a stack overrun from too many recursive calls during depth-first traversal

        PersistenceProviderSQLite persistenceProvider;


        internal HostGraphCleaner(PersistenceProviderSQLite persistenceProvider)
        {
            this.persistenceProvider = persistenceProvider;
        }

        // TODO: maybe lazy initialization...
        private void PrepareStatementsForGraphModifications()
        {
            deleteGraphCommand = persistenceProvider.PrepareTopologyDelete("graphs", "graphId");

            deleteNodeCommands = new SQLiteCommand[persistenceProvider.graph.Model.NodeModel.Types.Length];
            foreach(NodeType nodeType in persistenceProvider.graph.Model.NodeModel.Types)
            {
                deleteNodeCommands[nodeType.TypeID] = PrepareDelete(nodeType, "nodeId");
            }
            deleteNodeContainerCommands = new Dictionary<String, SQLiteCommand>[persistenceProvider.graph.Model.NodeModel.Types.Length];
            foreach(NodeType nodeType in persistenceProvider.graph.Model.NodeModel.Types)
            {
                deleteNodeContainerCommands[nodeType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in nodeType.AttributeTypes)
                {
                    if(!PersistenceProviderSQLite.IsContainerType(attributeType))
                        continue;
                    deleteNodeContainerCommands[nodeType.TypeID][attributeType.Name] = PrepareContainerDelete(nodeType, "nodeId", attributeType);
                }
            }

            deleteEdgeCommands = new SQLiteCommand[persistenceProvider.graph.Model.EdgeModel.Types.Length];
            foreach(EdgeType edgeType in persistenceProvider.graph.Model.EdgeModel.Types)
            {
                deleteEdgeCommands[edgeType.TypeID] = PrepareDelete(edgeType, "edgeId");
            }
            deleteEdgeContainerCommands = new Dictionary<String, SQLiteCommand>[persistenceProvider.graph.Model.EdgeModel.Types.Length];
            foreach(EdgeType edgeType in persistenceProvider.graph.Model.EdgeModel.Types)
            {
                deleteEdgeContainerCommands[edgeType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in edgeType.AttributeTypes)
                {
                    if(!PersistenceProviderSQLite.IsContainerType(attributeType))
                        continue;
                    deleteEdgeContainerCommands[edgeType.TypeID][attributeType.Name] = PrepareContainerDelete(edgeType, "edgeId", attributeType);
                }
            }

            deleteObjectCommand = persistenceProvider.PrepareTopologyDelete("objects", "objectId");
            deleteObjectCommands = new SQLiteCommand[persistenceProvider.graph.Model.ObjectModel.Types.Length];
            foreach(ObjectType objectType in persistenceProvider.graph.Model.ObjectModel.Types)
            {
                deleteObjectCommands[objectType.TypeID] = PrepareDelete(objectType, "objectId");
            }
            deleteObjectContainerCommands = new Dictionary<String, SQLiteCommand>[persistenceProvider.graph.Model.ObjectModel.Types.Length];
            foreach(ObjectType objectType in persistenceProvider.graph.Model.ObjectModel.Types)
            {
                deleteObjectContainerCommands[objectType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in objectType.AttributeTypes)
                {
                    if(!PersistenceProviderSQLite.IsContainerType(attributeType))
                        continue;
                    deleteObjectContainerCommands[objectType.TypeID][attributeType.Name] = PrepareContainerDelete(objectType, "objectId", attributeType);
                }
            }
        }

        internal void Cleanup()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            PrepareStatementsForGraphModifications();

            // references to nodes/edges from different graphs are not supported as of now in import/export, because persistent names only hold in the current graph, keep this in db persistence handling
            // but the user could assign references to elements from another graph to an attribute, so todo: maybe relax that constraint, requiring a model supporting graphof, and a global post-patching step after everything was read locally, until a check that the user doesn't assign references to elements from another graph is implemented
            // another issue with multiple graphs: names may be assigned in a different graph than the original graph when a node/edge (reference) is getting emitted
            // another issue with zombie elements: names may be assigned when a zombie node/edge (reference) is getting emitted

            // find all entities reachable from the host graph - corresponds to the mark phase of a garbage collector - later todo: use visited flags instead of visited maps
            MarkReachableEntities(); // on in-memory representation, so only most current container state is taken into account (as it should be) (thus zombie nodes/edges only being referenced in history but nowehere in current state are purged later on)

            // cleaning pass - similar to the sweep phase of a garbage collector - remove the graphs/objects from the database that are not reachable in-memory (unused) from the host graph on
            int numContainersCompactified = 0;
            int numNodesRemoved = 0, numEdgesRemoved = 0;
            int numGraphsPurged = 0;
            int numObjectsPurged = 0;
            int numZombieNodesPurged = 0;
            int numZombieEdgesPurged = 0;
            using(persistenceProvider.transaction = persistenceProvider.connection.BeginTransaction())
            {
                try
                {
                    // compactify before purging so that no graph/object zombie objects are needed at next run when non-existing graphs/objects that only exist in container change history are referenced
                    numContainersCompactified = CompactifyContainerChangeHistory();

                    // cleaning pass - similar to the sweep phase of a garbage collector - remove the graphs/objects from the database that are not reachable in-memory (unused) from the host graph on
                    numGraphsPurged = PurgeUnreachableGraphs(out numNodesRemoved, out numEdgesRemoved); // after all references to the graphs are known, we can purge the ones not in use; node/edge references are not a prerequisite for this (it only must be ensured all of them were instantiated before) (containers which may also contain graph references)
                    numObjectsPurged = PurgeUnreachableObjects(); // after all references to the objects are known, we can purge the ones not in use
                    numZombieNodesPurged = PurgeUnreachableZombieNodes();
                    numZombieEdgesPurged = PurgeUnreachableZombieEdges();

                    persistenceProvider.transaction.Commit();
                    persistenceProvider.transaction = null;
                }
                catch
                {
                    persistenceProvider.transaction.Rollback();
                    persistenceProvider.transaction = null;
                    throw;
                }
            }

            UnmarkReachableEntities();

            // zombies can only appear for nodes and edges, graphs and objects are garbage collected (after the read process), so zombies can not appear (neither can they for value types)
            ReportRemainingZombiesAsDanglingReferences(); // in an upcoming memory/semantic model they will be allowed, then nothing is reported/removed (they are not a programming error then but regular elements)

            //todo: vacuum the underlying database once in while, to be detected when exactly, maybe only on user request; analyze might be also helpful

            stopwatch.Stop();
            ConsoleUI.outWriter.WriteLine("Compactified {0} containers and purged unreachable entities in the form of {1} zombie nodes, {2} zombie edges, {3} internal class objects, {4} graphs with {5} nodes and {6} edges in {7} ms.",
                numContainersCompactified, numZombieNodesPurged, numZombieEdgesPurged, numObjectsPurged, numGraphsPurged, numNodesRemoved, numEdgesRemoved, stopwatch.ElapsedMilliseconds);
        }

        private void ReportRemainingZombiesAsDanglingReferences()
        {
            // zombies are graph elements that existed in the past in a graph but do not exist anymore in the graph in the current state, they are to be distinguished into:
            // - container change history zombie existing internally for technical reasons without programmer intervention that should never be visible externally (neither internally after container change history replay, they must be supported temporarily during change history replay)
            // - externally visible dangling-reference-zombies, when the semantic model forbids use after deletion from the graph (but the programmer does not set the references to null before deletion/removes them from containers), then they are dangling references to not existing graph elements (with undefined behaviour, to be reported as a zombie as a debugging/development aide, the program could also crash, or reuse them in another context) (deletion means deletion) (retyping removes the old referenced object and adds a new one, keeping graph context and shared attributes, name, unique identifier, but not object identity/reference (neither memory reference nor database identifier)),
            // - out-of-graph-elements when it allows use after removal from the graph they are simply references to graph elements that are not contained in a graph anymore (but still allow access to their attributes) (that will be garbage collected when the last reference to them vanishes) (also externally visible) (deletion means removal) (upcoming additional semantic model of the in-memory graph, only supported internally by the persistent graph)

            // after cleanup (purging and container compactification), walk entire heap (nodes,edges,objects(,graphs)) and report zombies, i.e. dangling references, found in the attributes of the entities
            // potential performance todo: by type, then attribute type check is only needed by type instead of per instance
            foreach(KeyValuePair<long, INode> dbidToNode in persistenceProvider.DbIdToNode)
            {
                long dbid = dbidToNode.Key;
                INode node = dbidToNode.Value;

                foreach(AttributeType attributeType in node.Type.AttributeTypes)
                {
                    if(ContainsGraphElementReferences(attributeType))
                    {
                        object attributeValue = node.GetAttribute(attributeType.Name);
                        if(attributeValue == null)
                            continue;

                        if(PersistenceProviderSQLite.IsContainerType(attributeType))
                            ReportReferencesIfDangling(node, attributeType, attributeValue);
                        else
                            ReportReferenceIfDangling(node, attributeType, attributeValue);
                    }
                }
            }

            foreach(KeyValuePair<long, IEdge> dbidToEdge in persistenceProvider.DbIdToEdge)
            {
                long dbid = dbidToEdge.Key;
                IEdge edge = dbidToEdge.Value;

                foreach(AttributeType attributeType in edge.Type.AttributeTypes)
                {
                    if(ContainsGraphElementReferences(attributeType))
                    {
                        object attributeValue = edge.GetAttribute(attributeType.Name);
                        if(attributeValue == null)
                            continue;

                        if(PersistenceProviderSQLite.IsContainerType(attributeType))
                            ReportReferencesIfDangling(edge, attributeType, attributeValue);
                        else
                            ReportReferenceIfDangling(edge, attributeType, attributeValue);
                    }
                }
            }

            foreach(KeyValuePair<long, IObject> dbidToObject in persistenceProvider.DbIdToObject)
            {
                long dbid = dbidToObject.Key;
                IObject @object = dbidToObject.Value;

                foreach(AttributeType attributeType in @object.Type.AttributeTypes)
                {
                    if(ContainsGraphElementReferences(attributeType))
                    {
                        object attributeValue = @object.GetAttribute(attributeType.Name);
                        if(attributeValue == null)
                            continue;

                        if(PersistenceProviderSQLite.IsContainerType(attributeType))
                            ReportReferencesIfDangling(@object, attributeType, attributeValue);
                        else
                            ReportReferenceIfDangling(@object, attributeType, attributeValue);
                    }
                }
            }
        }

        private void ReportReferenceIfDangling(IAttributeBearer owner, AttributeType attributeType, object value)
        {
            //Debug.Assert(ContainsGraphElementReferences(attributeType));
            ReportReferenceIfDangling(owner, attributeType, (IGraphElement)value);
        }

        private void ReportReferencesIfDangling(IAttributeBearer owner, AttributeType attributeType, object container)
        {
            //Debug.Assert(ContainsGraphElementReferences(attributeType));
            if(attributeType.Kind == AttributeKind.SetAttr)
            {
                IDictionary set = (IDictionary)container;
                foreach(DictionaryEntry entry in set)
                {
                    IGraphElement graphElement = (IGraphElement)entry.Key;
                    ReportReferenceIfDangling(owner, attributeType, graphElement);
                }
            }
            else if(attributeType.Kind == AttributeKind.MapAttr)
            {
                IDictionary map = (IDictionary)container;
                if(PersistenceProviderSQLite.IsGraphElementType(attributeType.KeyType) && PersistenceProviderSQLite.IsGraphElementType(attributeType.ValueType))
                {
                    foreach(DictionaryEntry entry in map)
                    {
                        IGraphElement graphElementKey = (IGraphElement)entry.Key;
                        IGraphElement graphElementValue = (IGraphElement)entry.Value;
                        ReportReferenceIfDangling(owner, attributeType, graphElementKey);
                        ReportReferenceIfDangling(owner, attributeType, graphElementValue);
                    }
                }
                else if(PersistenceProviderSQLite.IsGraphElementType(attributeType.KeyType))
                {
                    foreach(DictionaryEntry entry in map)
                    {
                        IGraphElement graphElementKey = (IGraphElement)entry.Key;
                        ReportReferenceIfDangling(owner, attributeType, graphElementKey);
                    }
                }
                else
                {
                    foreach(DictionaryEntry entry in map)
                    {
                        object key = entry.Key;
                        IGraphElement graphElementValue = (IGraphElement)entry.Value;
                        ReportReferenceIfDangling(owner, attributeType, graphElementValue);
                    }
                }
            }
            else if(attributeType.Kind == AttributeKind.ArrayAttr)
            {
                IList array = (IList)container;
                foreach(object entry in array)
                {
                    IGraphElement graphElement = (IGraphElement)entry;
                    ReportReferenceIfDangling(owner, attributeType, graphElement);
                }
            }
            else
            {
                IDeque deque = (IDeque)container;
                foreach(object entry in deque)
                {
                    IGraphElement graphElement = (IGraphElement)entry;
                    ReportReferenceIfDangling(owner, attributeType, graphElement);
                }
            }
        }

        private void ReportReferenceIfDangling(IAttributeBearer owner, AttributeType attributeType, IGraphElement graphElementReferenced) // potential future todo: enrich by information about the exact position (key/value/index) of the element in the container
        {
            INode node = graphElementReferenced as INode;
            IEdge edge = graphElementReferenced as IEdge;
            if(node != null && PersistenceProviderSQLite.GetContainingGraph(node) == null) // GetContainingGraph(node) == null means zombie node, i.e. a reference to it is a dangling reference
            {
                long dbid = persistenceProvider.NodeToDbId[node];
                ReportDanglingReference(owner, attributeType, dbid, true);
            }
            else if(edge != null && PersistenceProviderSQLite.GetContainingGraph(edge) == null) // see comment above
            {
                long dbid = persistenceProvider.EdgeToDbId[edge];
                ReportDanglingReference(owner, attributeType, dbid, false);
            }
        }

        private static void ReportDanglingReference(IAttributeBearer owner, AttributeType attributeType, long dbid, bool isNode)
        {
            string graphElementReferencedKind = isNode ? "node" : "edge";
            string danglingReferencePart = " contains a dangling reference to a(n) " + graphElementReferencedKind + " (" + graphElementReferencedKind + " dbid=" + dbid + ")";
            if(owner is IGraphElement)
            {
                string ownerKind = owner is INode ? "node" : "edge";
                IGraphElement owningGraphElement = (IGraphElement)owner;
                INamedGraph containingGraph = PersistenceProviderSQLite.GetContainingGraph(owningGraphElement);
                string ownerName = containingGraph != null ? containingGraph.GetElementName(owningGraphElement) : "zombie-" + ownerKind;
                string graphNamePart = containingGraph != null ? " of the graph " + containingGraph.Name : " out of graph";
                string pathPart = " of the " + ownerKind + " " + ownerName + graphNamePart;
                if(PersistenceProviderSQLite.IsContainerType(attributeType))
                {
                    ConsoleUI.errorOutWriter.WriteLine("Warning: the container attribute " + attributeType.Name + pathPart
                        + danglingReferencePart + " - you deleted or retyped a(n) " + graphElementReferencedKind + " without removing the reference to it from the container (also beware of bogus names)!");
                }
                else
                {
                    ConsoleUI.errorOutWriter.WriteLine("Warning: the attribute " + attributeType.Name + pathPart
                        + danglingReferencePart + " - you deleted or retyped a(n) " + graphElementReferencedKind + " without setting the attribute to null (also beware of bogus names)!");
                }
            }
            else
            {
                IObject owningObject = (IObject)owner;
                string ownerName = owningObject.GetObjectName();
                string pathPart = " of the internal class object " + ownerName;
                if(PersistenceProviderSQLite.IsContainerType(attributeType))
                {
                    ConsoleUI.errorOutWriter.WriteLine("Warning: the container attribute " + attributeType.Name + pathPart
                        + danglingReferencePart + " - you deleted or retyped a(n) " + graphElementReferencedKind + " without removing the reference to it from the container (also beware of bogus names)!");
                }
                else
                {
                    ConsoleUI.errorOutWriter.WriteLine("Warning: the attribute " + attributeType.Name + pathPart
                        + danglingReferencePart + " - you deleted or retyped a(n) " + graphElementReferencedKind + " without setting the attribute to null (also beware of bogus names)!");
                }
            }
        }

        private void MarkReachableEntities()
        {
            gcDfsStateItems.Push(new GcDfsStateItemGraph(persistenceProvider.host));
            visited.Add(persistenceProvider.host, null);

        processTopOfStack:
            while(gcDfsStateItems.Count > 0)
            {
                if(gcDfsStateItems.Peek() is GcDfsStateItemGraph)
                {
                    GcDfsStateItemGraph graphItem = (GcDfsStateItemGraph)gcDfsStateItems.Peek();
                    while(graphItem.graphElements.MoveNext())
                    {
                        IGraphElement graphElement = graphItem.graphElements.Current;
                        if(visited.ContainsKey(graphElement))
                            continue;
                        visited.Add(graphElement, null);
                        if(!ContainsReferences(graphElement.Type))
                            continue;
                        gcDfsStateItems.Push(new GcDfsStateItemElement(graphElement));
                        goto processTopOfStack;
                    }
                    gcDfsStateItems.Pop();
                }
                else
                {
                    GcDfsStateItemElement elementItem = (GcDfsStateItemElement)gcDfsStateItems.Peek();
                    while(elementItem.referencesContainedInAttributes.MoveNext())
                    {
                        object referencedElementFromAttributeOfElement = elementItem.referencesContainedInAttributes.Current;
                        if(visited.ContainsKey(referencedElementFromAttributeOfElement))
                            continue;
                        visited.Add(referencedElementFromAttributeOfElement, null);

                        if(referencedElementFromAttributeOfElement is IGraph)
                        {
                            IGraph referencedGraph = (IGraph)referencedElementFromAttributeOfElement;

                            gcDfsStateItems.Push(new GcDfsStateItemGraph(referencedGraph));
                            goto processTopOfStack;
                        }
                        else
                        {
                            IAttributeBearer referencedElement = (IAttributeBearer)referencedElementFromAttributeOfElement; // node/edge/internal object

                            // when references are contained in the attributes of the nestedElement, inspect the contained references (unlikely todo: dynamic check (i.e. reference not null)? - left to the handling of the element as such)
                            if(!ContainsReferences(referencedElement.Type))
                                continue;

                            // todo: contained only required under certain circumstances, ensure they hold
                            if(referencedElement.Type is GraphElementType)
                            {
                                IGraph containingGraph = PersistenceProviderSQLite.GetContainingGraph((IGraphElement)referencedElement);
                                if(containingGraph != null && !visited.ContainsKey(containingGraph)) // zombie elements are existing out-of-graph, just references to graph elements
                                {
                                    visited.Add(containingGraph, null);
                                    gcDfsStateItems.Push(new GcDfsStateItemGraph(containingGraph)); // instead of the element from the containing graph, push the entire graph, should be ok because the element will be added by the containing graph...
                                    visited.Remove(referencedElement); // ...but this requires unmarking of the element, otherwise it would not be inspected when the containing graph is handled (but this would be required, because of the contained references, see check before)
                                    goto processTopOfStack;
                                }
                            }

                            gcDfsStateItems.Push(new GcDfsStateItemElement(referencedElement));
                            goto processTopOfStack;
                        }
                    }
                    gcDfsStateItems.Pop();
                }
            }
        }

        // visits all attributes of the element, yield returns only the ones that really contain references -- but neither pays attention to the visited status of the reference from the attribute, nor sets it
        private static IEnumerable<object> GetReferencesContainedInAttributes(IAttributeBearer element)
        {
            foreach(AttributeType attributeType in element.Type.AttributeTypes)
            {
                if(!ContainsReferences(attributeType))
                    continue;
                if(PersistenceProviderSQLite.IsContainerType(attributeType))
                {
                    if(element.GetAttribute(attributeType.Name) == null)
                        yield break;
                    if(attributeType.Kind == AttributeKind.SetAttr)
                    {
                        foreach(DictionaryEntry entry in (IDictionary)element.GetAttribute(attributeType.Name))
                        {
                            yield return entry.Key;
                        }
                    }
                    else if(attributeType.Kind == AttributeKind.MapAttr)
                    {
                        if(IsReference(attributeType.KeyType) && IsReference(attributeType.ValueType))
                        {
                            foreach(DictionaryEntry entry in (IDictionary)element.GetAttribute(attributeType.Name))
                            {
                                yield return entry.Key;
                                if(entry.Value != null)
                                    yield return entry.Value;
                            }
                        }
                        else if(IsReference(attributeType.KeyType))
                        {
                            foreach(DictionaryEntry entry in (IDictionary)element.GetAttribute(attributeType.Name))
                            {
                                yield return entry.Key;
                            }
                        }
                        else
                        {
                            foreach(DictionaryEntry entry in (IDictionary)element.GetAttribute(attributeType.Name))
                            {
                                if(entry.Value != null)
                                    yield return entry.Value;
                            }
                        }
                    }
                    else if(attributeType.Kind == AttributeKind.ArrayAttr)
                    {
                        foreach(object containedElement in (IList)element.GetAttribute(attributeType.Name))
                        {
                            if(containedElement != null)
                                yield return containedElement;
                        }
                    }
                    else
                    {
                        foreach(object containedElement in (IDeque)element.GetAttribute(attributeType.Name))
                        {
                            if(containedElement != null)
                                yield return containedElement;
                        }
                    }
                }
                else
                {
                    object containedElement = element.GetAttribute(attributeType.Name);
                    if(containedElement != null)
                        yield return containedElement;
                }
            }
        }

        // yield returns all graph elements of the graph
        // does not take into account whether they may contain references because statically reference types are used for some attributes, or dynamically references are really contained in the attributes,
        // neither pays attention to the visited status of the graph element from the graph, nor sets it
        // alternative: PotentiallyContainingReferences - yield return only the ones that could contain references 
        // alternative: pay attention here to the visited status, set it, and return only graph elements that potentially contain references
        private static IEnumerable<IGraphElement> GetGraphElements(IGraph graph)
        {
            // potential performance todo: first all elements without references, then all elements with references
            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                foreach(INode node in graph.GetExactNodes(nodeType))
                {
                    yield return node;
                }
            }
            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                foreach(IEdge edge in graph.GetExactEdges(edgeType))
                {
                    yield return edge;
                }
            }
        }

        private static bool ContainsReferences(InheritanceType inheritanceType)
        {
            foreach(AttributeType attributeType in inheritanceType.AttributeTypes)
            {
                if(ContainsReferences(attributeType))
                    return true;
            }
            return false;
        }

        private static bool ContainsReferences(AttributeType attributeType)
        {
            if(IsReference(attributeType))
                return true;
            if(PersistenceProviderSQLite.IsContainerType(attributeType))
            {
                if(attributeType.Kind == AttributeKind.MapAttr)
                {
                    if(IsReference(attributeType.KeyType))
                        return true;
                }
                if(IsReference(attributeType.ValueType))
                    return true;
            }
            return false;
        }

        private static bool IsReference(AttributeType attributeType)
        {
            if(PersistenceProviderSQLite.IsGraphElementType(attributeType))
                return true;
            if(PersistenceProviderSQLite.IsGraphType(attributeType))
                return true;
            if(PersistenceProviderSQLite.IsObjectType(attributeType))
                return true;
            return false;
        }

        private static bool ContainsGraphElementReferences(AttributeType attributeType)
        {
            if(PersistenceProviderSQLite.IsGraphElementType(attributeType))
                return true;
            if(PersistenceProviderSQLite.IsContainerType(attributeType))
            {
                if(attributeType.Kind == AttributeKind.MapAttr)
                {
                    if(PersistenceProviderSQLite.IsGraphElementType(attributeType.KeyType))
                        return true;
                }
                if(PersistenceProviderSQLite.IsGraphElementType(attributeType.ValueType))
                    return true;
            }
            return false;
        }

        private void UnmarkReachableEntities()
        {
            visited.Clear();
        }

        private int PurgeUnreachableGraphs(out int nodesRemoved, out int edgesRemoved)
        {
            nodesRemoved = 0;
            edgesRemoved = 0;

            List<KeyValuePair<long, INamedGraph>> graphsToBeDeleted = new List<KeyValuePair<long, INamedGraph>>();
            foreach(KeyValuePair<long, INamedGraph> dbIdToGraph in persistenceProvider.DbIdToGraph)
            {
                INamedGraph graph = dbIdToGraph.Value;
                if(!visited.ContainsKey(graph))
                    graphsToBeDeleted.Add(dbIdToGraph);
            }
            foreach(KeyValuePair<long, INamedGraph> dbidToGraph in graphsToBeDeleted) // maybe batch delete needed performance wise, maybe recursive delete with complex SQL statemente needed performance wise - but multiple commands within a single transaction should be also fast, and are more modular
            {
                INamedGraph graph = dbidToGraph.Value;
                long dbid = dbidToGraph.Key;

                // the RemovingXXX methods remove the topology entry, the type entry with the non-container attributes, and the containers attribute stored in extra tables
                // maybe todo: encapsulate in RemovingGraph
                foreach(IEdge edge in graph.Edges)
                {
                    persistenceProvider.RemovingEdge(edge);
                    RemoveEdge(edge);
                    ++edgesRemoved;
                }
                foreach(INode node in graph.Nodes)
                {
                    persistenceProvider.RemovingNode(node);
                    RemoveNode(node);
                    ++nodesRemoved;
                }

                SQLiteCommand deleteGraphTopologyCommand = deleteGraphCommand;
                deleteGraphTopologyCommand.Parameters.Clear();
                deleteGraphTopologyCommand.Parameters.AddWithValue("@graphId", dbid);
                deleteGraphTopologyCommand.Transaction = persistenceProvider.transaction;
                int rowsAffected = deleteGraphTopologyCommand.ExecuteNonQuery();

                persistenceProvider.RemoveGraphFromDbIdMapping(graph);
            }

            return graphsToBeDeleted.Count;
        }

        private int PurgeUnreachableObjects()
        {
            List<KeyValuePair<long, IObject>> objectsToBeDeleted = new List<KeyValuePair<long, IObject>>();
            foreach(KeyValuePair<long, IObject> dbIdToObject in persistenceProvider.DbIdToObject)
            {
                IObject @object = dbIdToObject.Value;
                if(!visited.ContainsKey(@object))
                    objectsToBeDeleted.Add(dbIdToObject);
            }
            foreach(KeyValuePair<long, IObject> dbidToObject in objectsToBeDeleted) // maybe batch delete needed performance wise, maybe recursive delete with complex SQL statemente needed performance wise - but multiple commands within a single transaction should be also fast, and are more modular
            {
                IObject @object = dbidToObject.Value;
                long dbid = dbidToObject.Key;

                // the RemovingObject method removes the topology entry, the type entry with the non-container attributes, and the container attributes stored in extra tables
                RemovingObject(@object);
            }
            return objectsToBeDeleted.Count;
        }

        private int PurgeUnreachableZombieNodes()
        {
            List<KeyValuePair<long, INode>> nodesToBeDeleted = new List<KeyValuePair<long, INode>>();
            foreach(KeyValuePair<long, INode> dbIdToNode in persistenceProvider.DbIdToNode)
            {
                INode node = dbIdToNode.Value;
                if(!visited.ContainsKey(node))
                    nodesToBeDeleted.Add(dbIdToNode);
            }
            foreach(KeyValuePair<long, INode> dbidToNode in nodesToBeDeleted) // maybe batch delete needed performance wise, maybe recursive delete with complex SQL statemente needed performance wise - but multiple commands within a single transaction should be also fast, and are more modular
            {
                INode node = dbidToNode.Value;
                long dbid = dbidToNode.Key;

                // the RemoveNode method removes the type entry with the non-container attributes, and the container attributes stored in extra tables -- it does not remove the topology entry, it is assumed it does not exist anymore
                RemoveNode(node);
            }
            return nodesToBeDeleted.Count;
        }

        private int PurgeUnreachableZombieEdges()
        {
            List<KeyValuePair<long, IEdge>> edgesToBeDeleted = new List<KeyValuePair<long, IEdge>>();
            foreach(KeyValuePair<long, IEdge> dbIdToEdge in persistenceProvider.DbIdToEdge)
            {
                IEdge edge = dbIdToEdge.Value;
                if(!visited.ContainsKey(edge))
                    edgesToBeDeleted.Add(dbIdToEdge);
            }
            foreach(KeyValuePair<long, IEdge> dbidToEdge in edgesToBeDeleted) // maybe batch delete needed performance wise, maybe recursive delete with complex SQL statemente needed performance wise - but multiple commands within a single transaction should be also fast, and are more modular
            {
                IEdge edge = dbidToEdge.Value;
                long dbid = dbidToEdge.Key;

                // the RemoveEdge method removes the type entry with the non-container attributes, and the container attributes stored in extra tables -- it does not remove the topology entry, it is assumed it does not exist anymore
                RemoveEdge(edge);
            }
            return edgesToBeDeleted.Count;
        }

        private int CompactifyContainerChangeHistory()
        {
            int numContainersCompactified = 0;

            foreach(KeyValuePair<long, INode> dbidToNode in persistenceProvider.DbIdToNode)
            {
                long dbid = dbidToNode.Key;
                INode node = dbidToNode.Value;

                if(!visited.ContainsKey(node))
                    continue; // purged altogether later on

                foreach(AttributeType attributeType in node.Type.AttributeTypes)
                {
                    if(PersistenceProviderSQLite.IsContainerType(attributeType))
                    {
                        if(!persistenceProvider.modifiedContainers.IsMarked(node, attributeType.Name))
                            continue; // container unchanged, i.e. only initialized and added to

                        object container = node.GetAttribute(attributeType.Name);
                        PurgeContainerEntries(node, attributeType);
                        persistenceProvider.WriteContainerEntries(container, attributeType, node);

                        ++numContainersCompactified;
                    }
                }
            }

            foreach(KeyValuePair<long, IEdge> dbidToEdge in persistenceProvider.DbIdToEdge)
            {
                long dbid = dbidToEdge.Key;
                IEdge edge = dbidToEdge.Value;

                if(!visited.ContainsKey(edge))
                    continue; // purged altogether later on

                foreach(AttributeType attributeType in edge.Type.AttributeTypes)
                {
                    if(PersistenceProviderSQLite.IsContainerType(attributeType))
                    {
                        if(!persistenceProvider.modifiedContainers.IsMarked(edge, attributeType.Name))
                            continue; // container unchanged, i.e. only initialized and added to

                        object container = edge.GetAttribute(attributeType.Name);
                        PurgeContainerEntries(edge, attributeType);
                        persistenceProvider.WriteContainerEntries(container, attributeType, edge);

                        ++numContainersCompactified;
                    }
                }
            }

            foreach(KeyValuePair<long, IObject> dbidToObject in persistenceProvider.DbIdToObject)
            {
                long dbid = dbidToObject.Key;
                IObject @object = dbidToObject.Value;

                if(!visited.ContainsKey(@object))
                    continue; // purged altogether later on

                foreach(AttributeType attributeType in @object.Type.AttributeTypes)
                {
                    if(PersistenceProviderSQLite.IsContainerType(attributeType))
                    {
                        if(!persistenceProvider.modifiedContainers.IsMarked(@object, attributeType.Name))
                            continue; // container unchanged, i.e. only initialized and added to

                        object container = @object.GetAttribute(attributeType.Name);
                        PurgeContainerEntries(@object, attributeType);
                        persistenceProvider.WriteContainerEntries(container, attributeType, @object);

                        ++numContainersCompactified;
                    }
                }
            }

            return numContainersCompactified;
        }

        private void RemoveNode(INode node)
        {
            SQLiteCommand deleteNodeCommand = deleteNodeCommands[node.Type.TypeID];
            deleteNodeCommand.Parameters.Clear();
            deleteNodeCommand.Parameters.AddWithValue("@nodeId", persistenceProvider.NodeToDbId[node]);
            deleteNodeCommand.Transaction = persistenceProvider.transaction;
            int rowsAffected = deleteNodeCommand.ExecuteNonQuery();

            foreach(AttributeType attributeType in node.Type.AttributeTypes)
            {
                if(!PersistenceProviderSQLite.IsContainerType(attributeType))
                    continue;
                SQLiteCommand deleteNodeContainerCommand = deleteNodeContainerCommands[node.Type.TypeID][attributeType.Name];
                deleteNodeContainerCommand.Parameters.Clear();
                deleteNodeContainerCommand.Parameters.AddWithValue("@nodeId", persistenceProvider.NodeToDbId[node]);
                deleteNodeContainerCommand.Transaction = persistenceProvider.transaction;
                rowsAffected = deleteNodeContainerCommand.ExecuteNonQuery();
            }

            persistenceProvider.RemoveNodeFromDbIdMapping(node);
        }

        private void RemoveEdge(IEdge edge)
        {
            SQLiteCommand deleteEdgeCommand = deleteEdgeCommands[edge.Type.TypeID];
            deleteEdgeCommand.Parameters.Clear();
            deleteEdgeCommand.Parameters.AddWithValue("@edgeId", persistenceProvider.EdgeToDbId[edge]);
            deleteEdgeCommand.Transaction = persistenceProvider.transaction;
            int rowsAffected = deleteEdgeCommand.ExecuteNonQuery();

            foreach(AttributeType attributeType in edge.Type.AttributeTypes)
            {
                if(!PersistenceProviderSQLite.IsContainerType(attributeType))
                    continue;
                SQLiteCommand deleteEdgeContainerCommand = deleteEdgeContainerCommands[edge.Type.TypeID][attributeType.Name];
                deleteEdgeContainerCommand.Parameters.Clear();
                deleteEdgeContainerCommand.Parameters.AddWithValue("@edgeId", persistenceProvider.EdgeToDbId[edge]);
                deleteEdgeContainerCommand.Transaction = persistenceProvider.transaction;
                rowsAffected = deleteEdgeContainerCommand.ExecuteNonQuery();
            }

            persistenceProvider.RemoveEdgeFromDbIdMapping(edge);
        }

        private void RemovingObject(IObject @object)
        {
            // maybe todo: introduce foreign key constraints to the database data model, with on delete cascade
            SQLiteCommand deleteObjectTopologyCommand = this.deleteObjectCommand;
            deleteObjectTopologyCommand.Parameters.Clear();
            deleteObjectTopologyCommand.Parameters.AddWithValue("@objectId", persistenceProvider.ObjectToDbId[@object]);
            deleteObjectTopologyCommand.Transaction = persistenceProvider.transaction;
            int rowsAffected = deleteObjectTopologyCommand.ExecuteNonQuery();

            SQLiteCommand deleteObjectCommand = deleteObjectCommands[@object.Type.TypeID];
            deleteObjectCommand.Parameters.Clear();
            deleteObjectCommand.Parameters.AddWithValue("@objectId", persistenceProvider.ObjectToDbId[@object]);
            deleteObjectCommand.Transaction = persistenceProvider.transaction;
            rowsAffected = deleteObjectCommand.ExecuteNonQuery();

            foreach(AttributeType attributeType in @object.Type.AttributeTypes)
            {
                if(!PersistenceProviderSQLite.IsContainerType(attributeType))
                    continue;
                SQLiteCommand deleteObjectContainerCommand = deleteObjectContainerCommands[@object.Type.TypeID][attributeType.Name];
                deleteObjectContainerCommand.Parameters.Clear();
                deleteObjectContainerCommand.Parameters.AddWithValue("@objectId", persistenceProvider.ObjectToDbId[@object]);
                deleteObjectContainerCommand.Transaction = persistenceProvider.transaction;
                rowsAffected = deleteObjectContainerCommand.ExecuteNonQuery();
            }

            persistenceProvider.RemoveObjectFromDbIdMapping(@object);
        }

        private void PurgeContainerEntries(IAttributeBearer owningElement, AttributeType attributeType)
        {
            // maybe todo: code de-duplication by merging nodes/edges/objects handling even further, issue: type ids are only unique per type model (but first let's introduce graph types with attributes)
            if(owningElement is INode)
            {
                INode node = (INode)owningElement;
                SQLiteCommand deleteNodeContainerCommand = deleteNodeContainerCommands[node.Type.TypeID][attributeType.Name];
                deleteNodeContainerCommand.Parameters.Clear();
                deleteNodeContainerCommand.Parameters.AddWithValue("@nodeId", persistenceProvider.NodeToDbId[node]);
                deleteNodeContainerCommand.Transaction = persistenceProvider.transaction;
                int rowsAffected = deleteNodeContainerCommand.ExecuteNonQuery();
            }
            else if(owningElement is IEdge)
            {
                IEdge edge = (IEdge)owningElement;
                SQLiteCommand deleteEdgeContainerCommand = deleteEdgeContainerCommands[edge.Type.TypeID][attributeType.Name];
                deleteEdgeContainerCommand.Parameters.Clear();
                deleteEdgeContainerCommand.Parameters.AddWithValue("@edgeId", persistenceProvider.EdgeToDbId[edge]);
                deleteEdgeContainerCommand.Transaction = persistenceProvider.transaction;
                int rowsAffected = deleteEdgeContainerCommand.ExecuteNonQuery();
            }
            else
            {
                IObject @object = (IObject)owningElement;
                SQLiteCommand deleteObjectContainerCommand = deleteObjectContainerCommands[@object.Type.TypeID][attributeType.Name];
                deleteObjectContainerCommand.Parameters.Clear();
                deleteObjectContainerCommand.Parameters.AddWithValue("@objectId", persistenceProvider.ObjectToDbId[@object]);
                deleteObjectContainerCommand.Transaction = persistenceProvider.transaction;
                int rowsAffected = deleteObjectContainerCommand.ExecuteNonQuery();
            }
        }

        private SQLiteCommand PrepareDelete(InheritanceType type, String idName)
        {
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(type.Package, type.Name);
            StringBuilder command = new StringBuilder();
            command.Append("DELETE FROM ");
            command.Append(tableName);
            command.Append(" WHERE ");
            command.Append(idName);
            command.Append("==");
            command.Append("@" + idName);

            return new SQLiteCommand(command.ToString(), persistenceProvider.connection);
        }

        private SQLiteCommand PrepareContainerDelete(InheritanceType type, string ownerIdColumnName, AttributeType attributeType)
        {
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(type.Package, type.Name, attributeType.Name);

            StringBuilder command = new StringBuilder();
            command.Append("DELETE FROM ");
            command.Append(tableName);
            command.Append(" WHERE ");
            command.Append(ownerIdColumnName);
            command.Append("==");
            command.Append("@" + ownerIdColumnName);

            return new SQLiteCommand(command.ToString(), persistenceProvider.connection);
        }
    }
}
