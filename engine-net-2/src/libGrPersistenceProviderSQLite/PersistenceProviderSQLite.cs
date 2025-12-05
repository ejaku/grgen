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
    /// An implementation of the IPersistenceProvider that allows to persist changes to a named graph to an SQLite database
    /// (so a kind of named graph DAO; utilizing a package reference to the official SQLite ADO.NET driver).
    /// (Design idea: everything reachable from the host graph is stored in the database, in contrast to all graphs/nodes/edges/objects created at runtime, with the elements visible due to graph processing events lying in between)
    /// This class handles graph change events directly, storing modifications of the graph to the database, while delegating model initialization/updating, host graph reading, and host graph cleaning to appropriate task classes.
    /// Some database infrastructure / persistence provider helper/shared code is available in the PersistenceProviderSQLiteBase this class inherits from.
    /// </summary>
    public class PersistenceProviderSQLite : PersistenceProviderSQLiteBase, IPersistenceProvider
    {
        internal class AttributesMarkingState
        {
            internal AttributesMarkingState()
            {
                markedAttributes = new Dictionary<IAttributeBearer, Dictionary<string, SetValueType>>();
            }

            internal bool IsMarked(IAttributeBearer owner, String attribute)
            {
                if(!markedAttributes.ContainsKey(owner))
                    return false;
                return markedAttributes[owner].ContainsKey(attribute);
            }

            internal void Mark(IAttributeBearer owner, String attribute)
            {
                if(!markedAttributes.ContainsKey(owner))
                    markedAttributes.Add(owner, new Dictionary<string, SetValueType>());
                markedAttributes[owner][attribute] = null;
            }

            private Dictionary<IAttributeBearer, Dictionary<String, SetValueType>> markedAttributes;
        }

        // prepared statements for handling nodes (assuming available node related tables)
        SQLiteCommand createNodeCommand; // topology
        SQLiteCommand[] createNodeCommands; // per-type
        Dictionary<String, SQLiteCommand>[] updateNodeCommands; // per-type, per-non-container-attribute
        Dictionary<String, SQLiteCommand>[] updateNodeContainerCommands; // per-type, per-container-attribute (inserting container updating commands)
        SQLiteCommand deleteNodeCommand; // topology

        // prepared statements for handling edges (assuming available edge related tables)
        SQLiteCommand createEdgeCommand; // topology
        SQLiteCommand[] createEdgeCommands; // per-type
        Dictionary<String, SQLiteCommand>[] updateEdgeCommands; // per-type, per-non-container-attribute
        Dictionary<String, SQLiteCommand>[] updateEdgeContainerCommands; // per-type, per-container-attribute (inserting container updating commands)
        SQLiteCommand deleteEdgeCommand; // topology

        // database edge redirections, due to a node retype requiring an adaptation to the new node id, or a domain object/application layer edge redirect
        SQLiteCommand updateEdgeSourceCommand; // topology
        SQLiteCommand updateEdgeTargetCommand; // topology
        SQLiteCommand redirectEdgeCommand; // topology

        // prepared statements for handling graphs
        SQLiteCommand createGraphCommand;
        internal static long HOST_GRAPH_ID = 0;

        // prepared statements for handling objects (assuming available object related tables)
        SQLiteCommand createObjectCommand; // topology
        SQLiteCommand[] createObjectCommands; // per-type
        Dictionary<String, SQLiteCommand>[] updateObjectCommands; // per-type, per-non-container-attribute
        Dictionary<String, SQLiteCommand>[] updateObjectContainerCommands; // per-type, per-container-attribute (inserting container updating commands)

        Stack<INamedGraph> graphs; // the stack of graphs getting processed, the first entry being the host graph
        internal INamedGraph host; // I'd prefer to use a property accessing the stack, but this would require to create an array...
        internal INamedGraph graph { get { return graphs.Peek(); } } // the current graph

        IGraphProcessingEnvironment procEnv; // the graph processing environment to switch the current graph in case of to-subgraph switches

        IEdge edgeGettingRedirected;

        internal readonly AttributeType IntegerAttributeType = new AttributeType(null, null, AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));

        internal AttributesMarkingState initializedContainers;
        internal AttributesMarkingState modifiedContainers;


        public PersistenceProviderSQLite()
            : base()
        {
        }

        #region IPersistenceProvider

        public void Open(string connectionParameters, string persistentGraphParameters)
        {
            this.connectionParameters = connectionParameters;
            this.persistentGraphParameters = persistentGraphParameters;

            connection = new SQLiteConnection(connectionParameters);
            connection.Open();
            ConsoleUI.outWriter.WriteLine("persistence provider \"libGrPersistenceProviderSQLite.dll\" connected to database with \"{0}\" (additional parameters: {1}).", connectionParameters, persistentGraphParameters ?? "");
        }

        public void ReadPersistentGraphAndRegisterToListenToGraphModifications(INamedGraph hostGraph)
        {
            if(hostGraph.NumNodes != 0 || hostGraph.NumEdges != 0)
                throw new Exception("The graph must be empty!");

            // the following constraint is not required in grs import/export, but that one is flawed, based on an assumption that often holds but does not hold in the general case:
            // a graph element reference (ugly duckling) belongs to the current graph (this breaks when the user assigns a node/edge from another graph to a node/edge valued attribute in a node/edge of the current graph, or to an internal class object reachable from multiple graphs) (or when this happens automatically, e.g. with inducedSubgraph/definedSubgraph)
            // only since graphof is a general handling possible (yet to be implemented in the grs import/export)
            // here in the persistent graph we enfore the general handling (ironically, this is less needed here due to global node/edge ids in the database, but the entire runtime graph containing the node/edge may not be known, gets only known by the assignment of a graph element to a graph element attribute)
            if(ModelContainsGraphElementReferences(hostGraph.Model) && !hostGraph.Model.GraphElementsReferenceContainingGraph)
                throw new Exception("When the nodes/edges/objects in the graph model contain node or edge references (i.e. node/edge typed attributes), the graph elements must reference the graph they are contained in (add a node edge graph; declaration to the model)!");

            // the persistent graph depends on/requires memory safety (parially because it's not possible in another way (with node/edge references), partially because the implementation stays simpler (regarding container change history handling))
            hostGraph.ReuseOptimization = false; // general TODO: potentially dangerous optimization, to be enabled explicitly by the user for performance-critical things, change default

            graphs = new Stack<INamedGraph>();
            graphs.Push(hostGraph);
            host = hostGraph;

            initializedContainers = new AttributesMarkingState();
            modifiedContainers = new AttributesMarkingState();

            new ModelInitializerAndUpdater(this).CreateSchemaIfNotExistsOrAdaptToCompatibleChanges();
            new HostGraphReader(this).ReadCompleteGraph();
            PrepareStatementsForGraphModifications(); // Cleanup may carry out graph modifications (even before we are notified about graph changes after registering the handlers)
            new HostGraphCleaner(this).Cleanup();
            RegisterPersistenceHandlers();

            initializedContainers = null;
            modifiedContainers = null;
        }

        // TODO: a deregister would make sense so event handlers can be removed when the action environment is changed (but in this case the persistent graph will be released, so no urgent action needed)
        public void RegisterToListenToProcessingEnvironmentEvents(IGraphProcessingEnvironment procEnv)
        {
            this.procEnv = procEnv; // not supported: changes to referenced graphs outside of event control; potentially possible but not wanted: listen to changes to all graphs

            procEnv.OnSwitchingToSubgraph += SwitchToSubgraphHandler;
            procEnv.OnReturnedFromSubgraph += ReturnFromSubgraphHandler;
            procEnv.OnSpawnSequences += SpawnSequencesHandler;
        }

        public void Close()
        {
            connection.Close();
        }

        #endregion IPersistenceProvider

        #region Graph modification handling preparations

        // TODO: maybe lazy initialization...
        private void PrepareStatementsForGraphModifications()
        {
            INodeModel nodeModel = graph.Model.NodeModel;
            IEdgeModel edgeModel = graph.Model.EdgeModel;
            IObjectModel objectModel = graph.Model.ObjectModel;

            createGraphCommand = PrepareGraphInsert();

            createNodeCommand = PrepareNodeInsert();
            createNodeCommands = new SQLiteCommand[nodeModel.Types.Length];
            foreach(NodeType nodeType in nodeModel.Types)
            {
                createNodeCommands[nodeType.TypeID] = PrepareInsert(nodeType, "nodeId");
            }
            createEdgeCommand = PrepareEdgeInsert();
            createEdgeCommands = new SQLiteCommand[edgeModel.Types.Length];
            foreach(EdgeType edgeType in edgeModel.Types)
            {
                createEdgeCommands[edgeType.TypeID] = PrepareInsert(edgeType, "edgeId");
            }

            createObjectCommand = PrepareObjectInsert();
            createObjectCommands = new SQLiteCommand[objectModel.Types.Length];
            foreach(ObjectType objectType in objectModel.Types)
            {
                createObjectCommands[objectType.TypeID] = PrepareInsert(objectType, "objectId");
            }

            updateEdgeSourceCommand = PrepareUpdateEdgeSource();
            updateEdgeTargetCommand = PrepareUpdateEdgeTarget();
            redirectEdgeCommand = PrepareRedirectEdge();

            updateNodeCommands = new Dictionary<String, SQLiteCommand>[nodeModel.Types.Length];
            foreach(NodeType nodeType in nodeModel.Types)
            {
                updateNodeCommands[nodeType.TypeID] = new Dictionary<String, SQLiteCommand>(nodeType.NumAttributes);
                foreach(AttributeType attributeType in nodeType.AttributeTypes)
                {
                    if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
                        continue;
                    updateNodeCommands[nodeType.TypeID][attributeType.Name] = PrepareUpdate(nodeType, "nodeId", attributeType);
                }
            }
            updateNodeContainerCommands = new Dictionary<String, SQLiteCommand>[nodeModel.Types.Length];
            foreach(NodeType nodeType in nodeModel.Types)
            {
                updateNodeContainerCommands[nodeType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in nodeType.AttributeTypes)
                {
                    if(!IsContainerType(attributeType))
                        continue;
                    updateNodeContainerCommands[nodeType.TypeID][attributeType.Name] = PrepareContainerUpdatingInsert(nodeType, "nodeId", attributeType);
                }
            }
            updateEdgeCommands = new Dictionary<String, SQLiteCommand>[edgeModel.Types.Length];
            foreach(EdgeType edgeType in edgeModel.Types)
            {
                updateEdgeCommands[edgeType.TypeID] = new Dictionary<String, SQLiteCommand>(edgeType.NumAttributes);
                foreach(AttributeType attributeType in edgeType.AttributeTypes)
                {
                    if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
                        continue;
                    updateEdgeCommands[edgeType.TypeID][attributeType.Name] = PrepareUpdate(edgeType, "edgeId", attributeType);
                }
            }
            updateEdgeContainerCommands = new Dictionary<String, SQLiteCommand>[edgeModel.Types.Length];
            foreach(EdgeType edgeType in edgeModel.Types)
            {
                updateEdgeContainerCommands[edgeType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in edgeType.AttributeTypes)
                {
                    if(!IsContainerType(attributeType))
                        continue;
                    updateEdgeContainerCommands[edgeType.TypeID][attributeType.Name] = PrepareContainerUpdatingInsert(edgeType, "edgeId", attributeType);
                }
            }

            updateObjectCommands = new Dictionary<String, SQLiteCommand>[objectModel.Types.Length];
            foreach(ObjectType objectType in objectModel.Types)
            {
                updateObjectCommands[objectType.TypeID] = new Dictionary<String, SQLiteCommand>(objectType.NumAttributes);
                foreach(AttributeType attributeType in objectType.AttributeTypes)
                {
                    if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
                        continue;
                    updateObjectCommands[objectType.TypeID][attributeType.Name] = PrepareUpdate(objectType, "objectId", attributeType);
                }
            }
            updateObjectContainerCommands = new Dictionary<String, SQLiteCommand>[objectModel.Types.Length];
            foreach(ObjectType objectType in objectModel.Types)
            {
                updateObjectContainerCommands[objectType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in objectType.AttributeTypes)
                {
                    if(!IsContainerType(attributeType))
                        continue;
                    updateObjectContainerCommands[objectType.TypeID][attributeType.Name] = PrepareContainerUpdatingInsert(objectType, "objectId", attributeType);
                }
            }

            deleteNodeCommand = PrepareTopologyDelete("nodes", "nodeId");
            deleteEdgeCommand = PrepareTopologyDelete("edges", "edgeId");
        }

        private SQLiteCommand PrepareGraphInsert()
        {
            String tableName = "graphs";
            StringBuilder columnNames = new StringBuilder();
            StringBuilder parameterNames = new StringBuilder();
            // the id of the concept (graphId) being the same as the primary key is defined by the dbms
            AddInsertParameter(columnNames, parameterNames, "typeId");
            AddInsertParameter(columnNames, parameterNames, "name");
            StringBuilder command = new StringBuilder();
            command.Append("INSERT INTO ");
            command.Append(tableName);
            command.Append("(");
            command.Append(columnNames.ToString());
            command.Append(")");
            command.Append(" VALUES");
            command.Append("(");
            command.Append(parameterNames.ToString());
            command.Append(")");

            return new SQLiteCommand(command.ToString(), connection);
        }

        private SQLiteCommand PrepareNodeInsert()
        {
            String tableName = "nodes";
            StringBuilder columnNames = new StringBuilder();
            StringBuilder parameterNames = new StringBuilder();
            // the id of the concept (nodeId) being the same as the primary key is defined by the dbms
            AddInsertParameter(columnNames, parameterNames, "typeId");
            AddInsertParameter(columnNames, parameterNames, "graphId");
            AddInsertParameter(columnNames, parameterNames, "name");
            StringBuilder command = new StringBuilder();
            command.Append("INSERT INTO ");
            command.Append(tableName);
            command.Append("(");
            command.Append(columnNames.ToString());
            command.Append(")");
            command.Append(" VALUES");
            command.Append("(");
            command.Append(parameterNames.ToString());
            command.Append(")");

            return new SQLiteCommand(command.ToString(), connection);
        }

        private SQLiteCommand PrepareEdgeInsert()
        {
            String tableName = "edges";
            StringBuilder columnNames = new StringBuilder();
            StringBuilder parameterNames = new StringBuilder();
            // the id of the concept (edgeId) being the same as the primary key is defined by the dbms
            AddInsertParameter(columnNames, parameterNames, "typeId");
            AddInsertParameter(columnNames, parameterNames, "sourceNodeId");
            AddInsertParameter(columnNames, parameterNames, "targetNodeId");
            AddInsertParameter(columnNames, parameterNames, "graphId");
            AddInsertParameter(columnNames, parameterNames, "name");
            StringBuilder command = new StringBuilder();
            command.Append("INSERT INTO ");
            command.Append(tableName);
            command.Append("(");
            command.Append(columnNames.ToString());
            command.Append(")");
            command.Append(" VALUES");
            command.Append("(");
            command.Append(parameterNames.ToString());
            command.Append(")");

            return new SQLiteCommand(command.ToString(), connection);
        }

        private SQLiteCommand PrepareObjectInsert()
        {
            String tableName = "objects";
            StringBuilder columnNames = new StringBuilder();
            StringBuilder parameterNames = new StringBuilder();
            // the id of the concept (objectId) being the same as the primary key is defined by the dbms
            AddInsertParameter(columnNames, parameterNames, "typeId");
            AddInsertParameter(columnNames, parameterNames, "name");
            StringBuilder command = new StringBuilder();
            command.Append("INSERT INTO ");
            command.Append(tableName);
            command.Append("(");
            command.Append(columnNames.ToString());
            command.Append(")");
            command.Append(" VALUES");
            command.Append("(");
            command.Append(parameterNames.ToString());
            command.Append(")");

            return new SQLiteCommand(command.ToString(), connection);
        }

        private SQLiteCommand PrepareInsert(InheritanceType type, string idName)
        {
            String tableName = GetUniqueTableName(type.Package, type.Name);
            StringBuilder columnNames = new StringBuilder();
            StringBuilder parameterNames = new StringBuilder();
            AddInsertParameter(columnNames, parameterNames, idName);
            foreach(AttributeType attributeType in type.AttributeTypes)
            {
                if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
                    continue;
                AddInsertParameter(columnNames, parameterNames, GetUniqueColumnName(attributeType.Name));
            }
            StringBuilder command = new StringBuilder();
            command.Append("INSERT INTO ");
            command.Append(tableName);
            command.Append("(");
            command.Append(columnNames.ToString());
            command.Append(")");
            command.Append(" VALUES");
            command.Append("(");
            command.Append(parameterNames.ToString());
            command.Append(")");

            return new SQLiteCommand(command.ToString(), connection);
        }

        private SQLiteCommand PrepareContainerUpdatingInsert(InheritanceType type, string ownerIdColumnName, AttributeType attributeType)
        {
            String tableName = GetUniqueTableName(type.Package, type.Name, attributeType.Name);
            StringBuilder columnNames = new StringBuilder();
            StringBuilder parameterNames = new StringBuilder();
            // the id of the container element (entryId) being the same as the primary key is defined by the dbms
            AddInsertParameter(columnNames, parameterNames, ownerIdColumnName);
            AddInsertParameter(columnNames, parameterNames, "command");
            AddInsertParameter(columnNames, parameterNames, "value");
            if(attributeType.Kind != AttributeKind.SetAttr)
                AddInsertParameter(columnNames, parameterNames, "key");

            StringBuilder command = new StringBuilder();
            command.Append("INSERT INTO ");
            command.Append(tableName);
            command.Append("(");
            command.Append(columnNames.ToString());
            command.Append(")");
            command.Append(" VALUES");
            command.Append("(");
            command.Append(parameterNames.ToString());
            command.Append(")");

            return new SQLiteCommand(command.ToString(), connection);
        }

        private SQLiteCommand PrepareUpdateEdgeSource()
        {
            StringBuilder command = new StringBuilder();
            command.Append("UPDATE ");
            command.Append("edges");
            command.Append(" SET ");
            command.Append("sourceNodeId");
            command.Append(" = ");
            command.Append("@" + "sourceNodeId");
            command.Append(" WHERE ");
            command.Append("edgeId");
            command.Append(" == ");
            command.Append("@" + "edgeId");

            return new SQLiteCommand(command.ToString(), connection);
        }

        private SQLiteCommand PrepareUpdateEdgeTarget()
        {
            StringBuilder command = new StringBuilder();
            command.Append("UPDATE ");
            command.Append("edges");
            command.Append(" SET ");
            command.Append("targetNodeId");
            command.Append(" = ");
            command.Append("@" + "targetNodeId");
            command.Append(" WHERE ");
            command.Append("edgeId");
            command.Append(" == ");
            command.Append("@" + "edgeId");

            return new SQLiteCommand(command.ToString(), connection);
        }

        private SQLiteCommand PrepareRedirectEdge()
        {
            StringBuilder command = new StringBuilder();
            command.Append("UPDATE ");
            command.Append("edges");
            command.Append(" SET ");
            command.Append("sourceNodeId");
            command.Append(" = ");
            command.Append("@" + "sourceNodeId");
            command.Append(", ");
            command.Append("targetNodeId");
            command.Append(" = ");
            command.Append("@" + "targetNodeId");
            command.Append(" WHERE ");
            command.Append("edgeId");
            command.Append(" == ");
            command.Append("@" + "edgeId");

            return new SQLiteCommand(command.ToString(), connection);
        }

        internal SQLiteCommand PrepareUpdate(InheritanceType type, string idName, AttributeType attributeType)
        {
            String tableName = GetUniqueTableName(type.Package, type.Name);
            StringBuilder command = new StringBuilder();
            command.Append("UPDATE ");
            command.Append(tableName);
            command.Append(" SET ");
            command.Append(GetUniqueColumnName(attributeType.Name));
            command.Append(" = ");
            command.Append("@" + GetUniqueColumnName(attributeType.Name));
            if(idName != null)
            {
                command.Append(" WHERE ");
                command.Append(idName);
                command.Append(" == ");
                command.Append("@" + idName);
            }

            return new SQLiteCommand(command.ToString(), connection);
        }

        internal SQLiteCommand PrepareTopologyDelete(String tableName, String idName)
        {
            StringBuilder command = new StringBuilder();
            command.Append("DELETE FROM ");
            command.Append(tableName);
            command.Append(" WHERE ");
            command.Append(idName);
            command.Append("==");
            command.Append("@" + idName);

            return new SQLiteCommand(command.ToString(), connection);
        }

        #endregion Graph modification handling preparations

        #region Listen to graph changes in order to persist them and related code

        private void RegisterPersistenceHandlers()
        {
            graph.OnNodeAdded += NodeAdded;
            graph.OnEdgeAdded += EdgeAdded;
            //graph.OnObjectCreated += ObjectCreated; not used, the database representation of objects is created when they are assigned to an element reachable from the host graph

            graph.OnRemovingNode += RemovingNode;
            graph.OnRemovingEdge += RemovingEdge;
            //graph.OnRemovingEdges += RemovingEdges; unnecessary, conveys only additional information, each edge is reported to be removed with RemovingEdge

            graph.OnClearingGraph += ClearingGraph;

            graph.OnChangingNodeAttribute += ChangingNodeAttribute;
            graph.OnChangingEdgeAttribute += ChangingEdgeAttribute;
            graph.OnChangingObjectAttribute += ChangingObjectAttribute;
            graph.OnChangedNodeAttribute += ChangedNodeAttribute;
            graph.OnChangedEdgeAttribute += ChangedEdgeAttribute;

            graph.OnRetypingNode += RetypingNode;
            graph.OnRetypingEdge += RetypingEdge;

            graph.OnRedirectingEdge += RedirectingEdge;
        }

        private void DeregisterPersistenceHandlers()
        {
            graph.OnNodeAdded -= NodeAdded;
            graph.OnEdgeAdded -= EdgeAdded;
            //graph.OnObjectCreated -= ObjectCreated; not used, the database representation of objects is created when they are assigned to an element reachable from the host graph

            graph.OnRemovingNode -= RemovingNode;
            graph.OnRemovingEdge -= RemovingEdge;
            //graph.OnRemovingEdges -= RemovingEdges; unnecessary, conveys only additional information, each edge is reported to be removed with RemovingEdge

            graph.OnClearingGraph -= ClearingGraph;

            graph.OnChangingNodeAttribute -= ChangingNodeAttribute;
            graph.OnChangingEdgeAttribute -= ChangingEdgeAttribute;
            graph.OnChangingObjectAttribute -= ChangingObjectAttribute;
            graph.OnChangedNodeAttribute -= ChangedNodeAttribute;
            graph.OnChangedEdgeAttribute -= ChangedEdgeAttribute;

            graph.OnRetypingNode -= RetypingNode;
            graph.OnRetypingEdge -= RetypingEdge;

            graph.OnRedirectingEdge -= RedirectingEdge;
        }

        // the following event handlers expect events for the graph returned by ReadPersistentGraphAndRegisterToListenToGraphModifications

        public void NodeAdded(INode node)
        {
            WriteNodeBaseEntry(node, graph);
            AddReferencesToDatabase(node); // pre-run adding graphs and objects if needed, otherwise completion would run into re-entry issues (during command builing the command would be filled and executed (indirectly) another time, wreaking havoc on the parameters of the command)
            CompleteNodeEntry(node);
        }

        public void WriteNodeBaseEntry(INode node, INamedGraph graph)
        {
            SQLiteCommand addNodeTopologyCommand = createNodeCommand;
            addNodeTopologyCommand.Parameters.Clear();
            addNodeTopologyCommand.Parameters.AddWithValue("@typeId", TypeNameToDbId[node.Type.PackagePrefixedName]);
            addNodeTopologyCommand.Parameters.AddWithValue("@graphId", GraphToDbId[graph]);
            addNodeTopologyCommand.Parameters.AddWithValue("@name", graph.GetElementName(node));
            addNodeTopologyCommand.Transaction = transaction;
            int rowsAffected = addNodeTopologyCommand.ExecuteNonQuery();
            long rowId = connection.LastInsertRowId;
            AddNodeWithDbIdToDbIdMapping(node, rowId);
        }

        public void CompleteNodeEntry(INode node)
        {
            SQLiteCommand addNodeCommand = createNodeCommands[node.Type.TypeID];
            addNodeCommand.Parameters.Clear();
            addNodeCommand.Parameters.AddWithValue("@nodeId", NodeToDbId[node]);
            foreach(AttributeType attributeType in node.Type.AttributeTypes)
            {
                if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
                    continue;
                object value = node.GetAttribute(attributeType.Name);
                addNodeCommand.Parameters.AddWithValue("@" + GetUniqueColumnName(attributeType.Name), ValueOrIdOfReferencedElement(value, attributeType));
            }
            addNodeCommand.Transaction = transaction;
            int rowsAffected = addNodeCommand.ExecuteNonQuery();

            WriteContainerEntries(node);
        }

        public void EdgeAdded(IEdge edge)
        {
            if(edge == edgeGettingRedirected)
            {
                redirectEdgeCommand.Parameters.Clear();
                redirectEdgeCommand.Parameters.AddWithValue("@edgeId", EdgeToDbId[edge]);
                redirectEdgeCommand.Parameters.AddWithValue("@sourceNodeId", NodeToDbId[edge.Source]); // not yet, but somewhen nodes from another graph may be used, then it might be necessary to add their graph as needed
                redirectEdgeCommand.Parameters.AddWithValue("@targetNodeId", NodeToDbId[edge.Target]);
                redirectEdgeCommand.Transaction = transaction;
                int rowsAffected = redirectEdgeCommand.ExecuteNonQuery();

                edgeGettingRedirected = null;
                return;
            }

            WriteEdgeBaseEntry(edge, graph);
            AddReferencesToDatabase(edge); // pre-run adding graphs and objects if needed, otherwise completion would run into re-entry issues (during command builing the command would be filled and executed (indirectly) another time, wreaking havoc on the parameters of the command)
            CompleteEdgeEntry(edge);
        }

        public void WriteEdgeBaseEntry(IEdge edge, INamedGraph graph)
        {
            SQLiteCommand addEdgeTopologyCommand = createEdgeCommand;
            addEdgeTopologyCommand.Parameters.Clear();
            addEdgeTopologyCommand.Parameters.AddWithValue("@typeId", TypeNameToDbId[edge.Type.PackagePrefixedName]);
            addEdgeTopologyCommand.Parameters.AddWithValue("@sourceNodeId", NodeToDbId[edge.Source]);
            addEdgeTopologyCommand.Parameters.AddWithValue("@targetNodeId", NodeToDbId[edge.Target]);
            addEdgeTopologyCommand.Parameters.AddWithValue("@graphId", GraphToDbId[graph]);
            addEdgeTopologyCommand.Parameters.AddWithValue("@name", graph.GetElementName(edge));
            addEdgeTopologyCommand.Transaction = transaction;
            int rowsAffected = addEdgeTopologyCommand.ExecuteNonQuery();
            long rowId = connection.LastInsertRowId;
            AddEdgeWithDbIdToDbIdMapping(edge, rowId);
        }

        public void CompleteEdgeEntry(IEdge edge)
        {
            SQLiteCommand addEdgeCommand = createEdgeCommands[edge.Type.TypeID];
            addEdgeCommand.Parameters.Clear();
            addEdgeCommand.Parameters.AddWithValue("@edgeId", EdgeToDbId[edge]);
            foreach(AttributeType attributeType in edge.Type.AttributeTypes)
            {
                if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
                    continue;
                object value = edge.GetAttribute(attributeType.Name);
                addEdgeCommand.Parameters.AddWithValue("@" + GetUniqueColumnName(attributeType.Name), ValueOrIdOfReferencedElement(value, attributeType));
            }
            addEdgeCommand.Transaction = transaction;
            int rowsAffected = addEdgeCommand.ExecuteNonQuery();

            WriteContainerEntries(edge);
        }

        public void RemovingNode(INode node)
        {
            // remove the node from the topology table and thus the graph, but keep its per-type entry so its references stay intact, to be deleted during garbage collection when no references exist anymore
            SQLiteCommand deleteNodeTopologyCommand = this.deleteNodeCommand;
            deleteNodeTopologyCommand.Parameters.Clear();
            deleteNodeTopologyCommand.Parameters.AddWithValue("@nodeId", NodeToDbId[node]);
            deleteNodeTopologyCommand.Transaction = transaction;
            int rowsAffected = deleteNodeTopologyCommand.ExecuteNonQuery();
        }

        public void RemovingEdge(IEdge edge)
        {
            if(edge == edgeGettingRedirected) // todo: this method is also used internally, maybe it is better split into a version without this header because of this
                return;

            // remove the edge from the topology table and thus the graph, but keep its per-type entry so its references stay intact, to be deleted during garbage collection when no references exist anymore
            SQLiteCommand deleteEdgeTopologyCommand = this.deleteEdgeCommand;
            deleteEdgeTopologyCommand.Parameters.Clear();
            deleteEdgeTopologyCommand.Parameters.AddWithValue("@edgeId", EdgeToDbId[edge]);
            deleteEdgeTopologyCommand.Transaction = transaction;
            int rowsAffected = deleteEdgeTopologyCommand.ExecuteNonQuery();
        }

        public void ClearingGraph(IGraph graph)
        {
            // TODO: implement optimized batch version - likely not needed when transactions are used
            foreach(IEdge edge in graph.Edges)
            {
                RemovingEdge(edge);
            }
            foreach(INode node in graph.Nodes)
            {
                RemovingNode(node);
            }
        }

        public void ChangingNodeAttribute(INode node, AttributeType attrType,
                AttributeChangeType changeType, object newValue, object keyValue)
        {
            if(!IsSupportedAttributeType(attrType))
                return;

            if(IsGraphType(attrType))
                AddGraphAsNeeded((INamedGraph)newValue);
            else if(IsObjectType(attrType))
                AddObjectAsNeeded((IObject)newValue);
            else if(IsGraphElementType(attrType))
                AddGraphAsNeeded(GetContainingGraph((IGraphElement)newValue));

            if(IsContainerType(attrType))
                WriteContainerChange(node, attrType, changeType, newValue, keyValue);
            else
            {
                SQLiteCommand updateNodeCommand = updateNodeCommands[node.Type.TypeID][attrType.Name];
                updateNodeCommand.Parameters.Clear();
                updateNodeCommand.Parameters.AddWithValue("@nodeId", NodeToDbId[node]);
                updateNodeCommand.Parameters.AddWithValue("@" + GetUniqueColumnName(attrType.Name), ValueOrIdOfReferencedElement(newValue, attrType));

                updateNodeCommand.Transaction = transaction;
                int rowsAffected = updateNodeCommand.ExecuteNonQuery();
            }
        }

        public void ChangingEdgeAttribute(IEdge edge, AttributeType attrType,
                AttributeChangeType changeType, object newValue, object keyValue)
        {
            if(!IsSupportedAttributeType(attrType))
                return;

            if(IsGraphType(attrType))
                AddGraphAsNeeded((INamedGraph)newValue);
            else if(IsObjectType(attrType))
                AddObjectAsNeeded((IObject)newValue);
            else if(IsGraphElementType(attrType))
                AddGraphAsNeeded(GetContainingGraph((IGraphElement)newValue));

            if(IsContainerType(attrType))
                WriteContainerChange(edge, attrType, changeType, newValue, keyValue);
            else
            {
                SQLiteCommand updateEdgeCommand = updateEdgeCommands[edge.Type.TypeID][attrType.Name];
                updateEdgeCommand.Parameters.Clear();
                updateEdgeCommand.Parameters.AddWithValue("@edgeId", EdgeToDbId[edge]);
                updateEdgeCommand.Parameters.AddWithValue("@" + GetUniqueColumnName(attrType.Name), ValueOrIdOfReferencedElement(newValue, attrType));

                updateEdgeCommand.Transaction = transaction;
                int rowsAffected = updateEdgeCommand.ExecuteNonQuery();
            }
        }

        public void ChangingObjectAttribute(IObject obj, AttributeType attrType,
                AttributeChangeType changeType, object newValue, object keyValue)
        {
            if(!ObjectToDbId.ContainsKey(obj))
                return; // object not known to the graph means we receive an update notification for an object that is not reachable (yet) from the graph -- to be ignored, when the object becomes known, the by-then current attribute will be written

            if(!IsSupportedAttributeType(attrType))
                return;

            if(IsGraphType(attrType))
                AddGraphAsNeeded((INamedGraph)newValue);
            else if(IsObjectType(attrType))
                AddObjectAsNeeded((IObject)newValue);
            else if(IsGraphElementType(attrType))
                AddGraphAsNeeded(GetContainingGraph((IGraphElement)newValue));

            if(IsContainerType(attrType))
                WriteContainerChange(obj, attrType, changeType, newValue, keyValue);
            else
            {
                SQLiteCommand updateObjectCommand = updateObjectCommands[obj.Type.TypeID][attrType.Name];
                updateObjectCommand.Parameters.Clear();
                updateObjectCommand.Parameters.AddWithValue("@objectId", ObjectToDbId[obj]);
                updateObjectCommand.Parameters.AddWithValue("@" + GetUniqueColumnName(attrType.Name), ValueOrIdOfReferencedElement(newValue, attrType));

                updateObjectCommand.Transaction = transaction;
                int rowsAffected = updateObjectCommand.ExecuteNonQuery();
            }
        }

        public void ChangedNodeAttribute(INode node, AttributeType attrType)
        {
        }

        public void ChangedEdgeAttribute(IEdge edge, AttributeType attrType)
        {
        }

        public void RetypingNode(INode oldNode, INode newNode)
        {
            // remove old entry and insert new entry -- this way, stale references that appear in-memory in this situation can be also detected on database level
            // (accepted price compared to just updating the old topology entry with the new type id, i.e. esp. keeping the old node id: performance reduction due to the insert/delete of the topology entry, and esp. the incident edges patching)
            RemovingNode(oldNode);
            NodeAdded(newNode);

            // update all incident edges to the new node id
            foreach(IEdge outgoingEdge in oldNode.Outgoing)
            {
                SQLiteCommand updateEdgeSourceCommand = this.updateEdgeSourceCommand;
                updateEdgeSourceCommand.Parameters.Clear();
                updateEdgeSourceCommand.Parameters.AddWithValue("@edgeId", EdgeToDbId[outgoingEdge]);
                updateEdgeSourceCommand.Parameters.AddWithValue("@sourceNodeId", NodeToDbId[newNode]);
                updateEdgeSourceCommand.Transaction = transaction;
                int rowsAffected = updateEdgeSourceCommand.ExecuteNonQuery();
            }
            foreach(IEdge incomingEdge in oldNode.Incoming)
            {
                SQLiteCommand updateEdgeTargetCommand = this.updateEdgeTargetCommand;
                updateEdgeTargetCommand.Parameters.Clear();
                updateEdgeTargetCommand.Parameters.AddWithValue("@edgeId", EdgeToDbId[incomingEdge]);
                updateEdgeTargetCommand.Parameters.AddWithValue("@targetNodeId", NodeToDbId[newNode]);
                updateEdgeTargetCommand.Transaction = transaction;
                int rowsAffected = updateEdgeTargetCommand.ExecuteNonQuery();
            }
        }

        public void RetypingEdge(IEdge oldEdge, IEdge newEdge)
        {
            // remove old entry and insert new entry -- this way, stale references that appear in-memory in this situation can be also detected on database level
            // (accepted price compared to just updating the old topology entry with the new type id, i.e. esp. keeping the old edge id: performance reduction due to the insert/delete of the topology entry)
            RemovingEdge(oldEdge);
            EdgeAdded(newEdge);
        }

        public void RedirectingEdge(IEdge edge)
        {
            // edge is going to be removed and readded thereafter, in order to keep its database id, we just redirect it
            edgeGettingRedirected = edge;
        }

        internal static INamedGraph GetContainingGraph(IGraphElement graphElement)
        {
            if(graphElement == null)
                return null;
            else
                return (INamedGraph)((IContained)graphElement).GetContainingGraph();
        }

        private void AddGraphAsNeeded(INamedGraph graph)
        {
            if(graph == null)
                return;
            if(!GraphToDbId.ContainsKey(graph))
                WriteGraph(graph);
        }

        private void WriteGraph(INamedGraph graph)
        {
            SQLiteCommand addGraphTopologyCommand = createGraphCommand;
            addGraphTopologyCommand.Parameters.Clear();
            addGraphTopologyCommand.Parameters.AddWithValue("@typeId", TypeNameToDbId["graph"]);
            addGraphTopologyCommand.Parameters.AddWithValue("@name", graph.Name);
            addGraphTopologyCommand.Transaction = transaction;
            int rowsAffected = addGraphTopologyCommand.ExecuteNonQuery();
            long rowId = connection.LastInsertRowId;
            AddGraphWithDbIdToDbIdMapping(graph, rowId);

            // pass 1 - create ids (in topology tables)
            foreach(INode node in graph.Nodes)
            {
                WriteNodeBaseEntry(node, graph);
            }
            foreach(IEdge edge in graph.Edges)
            {
                WriteEdgeBaseEntry(edge, graph);
            }

            // pass 2 - write attributes (in full element tables) (due to existing ids, now references to our own nodes/edges can be filled)
            foreach(INode node in graph.Nodes)
            {
                AddReferencesToDatabase(node); // pre-run adding graphs and objects if needed, otherwise completion would run into re-entry issues (during command builing the command would be filled and executed (indirectly) another time, wreaking havoc on the parameters of the command)
                CompleteNodeEntry(node);
            }
            foreach(IEdge edge in graph.Edges)
            {
                AddReferencesToDatabase(edge); // pre-run adding graphs and objects if needed, otherwise completion would run into re-entry issues (during command builing the command would be filled and executed (indirectly) another time, wreaking havoc on the parameters of the command)
                CompleteEdgeEntry(edge);
            }
        }

        private void AddObjectAsNeeded(IObject obj)
        {
            if(obj == null)
                return;
            if(!ObjectToDbId.ContainsKey(obj))
            {
                WriteObjectBaseEntry(obj);
                AddReferencesToDatabase(obj);
                CompleteObjectEntry(obj);
            }
        }

        // add the references contained in the attributes of the root object given as input argument, assuming the input object itself was already handled
        // this first step could be saved, when the input object were not to be added to the database before, merging it with AddObjectsAndReferencesToDatabase - at the price of complexity, a node/edge add would be carried out, an object add would be left to this method (not sure whether this tradeoff is worthwhile, tbd/todo somewhen later)
        private void AddReferencesToDatabase(IAttributeBearer root)
        {
            Stack<IAttributeBearer> todos = null; // DFS worklist for objects

            AddObjectsToTodosAndReferencesToDatabase(root, ref todos);

            if(todos != null)
                AddObjectsAndReferencesToDatabase(todos);
        }

        private void AddObjectsAndReferencesToDatabase(Stack<IAttributeBearer> todos)
        {
            while(todos.Count > 0)
            {
                IAttributeBearer current = todos.Pop();

                bool objectWritten = false;
                if(current is IObject)
                {
                    if(!ObjectToDbId.ContainsKey((IObject)current))
                    {
                        WriteObjectBaseEntry((IObject)current);
                        objectWritten = true;
                    }
                }

                AddObjectsToTodosAndReferencesToDatabase(current, ref todos);

                if(objectWritten)
                    CompleteObjectEntry((IObject)current);
            }
        }

        private void AddObjectsToTodosAndReferencesToDatabase(IAttributeBearer current, ref Stack<IAttributeBearer> todos)
        {
            foreach(AttributeType attributeType in current.Type.AttributeTypes)
            {
                if(IsContainerType(attributeType))
                    AddObjectsToTodosAndReferencesToDatabaseFromContainer(current, attributeType, ref todos);
                else
                {
                    object val = current.GetAttribute(attributeType.Name);
                    AddObjectsToTodosAndReferencesToDatabase(attributeType, val, ref todos);
                }
            }
        }

        private void AddObjectsToTodosAndReferencesToDatabaseFromContainer(IAttributeBearer current, AttributeType attributeType, ref Stack<IAttributeBearer> todos)
        {
            if(attributeType.Kind == AttributeKind.SetAttr)
            {
                object val = current.GetAttribute(attributeType.Name);
                IDictionary set = (IDictionary)val;
                if(set != null)
                {
                    foreach(DictionaryEntry entry in set)
                    {
                        AddObjectsToTodosAndReferencesToDatabase(attributeType.ValueType, entry.Key, ref todos);
                    }
                }
            }
            else if(attributeType.Kind == AttributeKind.MapAttr)
            {
                object val = current.GetAttribute(attributeType.Name);
                IDictionary map = (IDictionary)val;
                if(map != null)
                {
                    foreach(DictionaryEntry entry in map)
                    {
                        AddObjectsToTodosAndReferencesToDatabase(attributeType.KeyType, entry.Key, ref todos);
                        AddObjectsToTodosAndReferencesToDatabase(attributeType.ValueType, entry.Value, ref todos);
                    }
                }
            }
            else if(attributeType.Kind == AttributeKind.ArrayAttr)
            {
                object val = current.GetAttribute(attributeType.Name);
                IList array = (IList)val;
                if(array != null)
                {
                    foreach(object entry in array)
                    {
                        AddObjectsToTodosAndReferencesToDatabase(attributeType.ValueType, entry, ref todos);
                    }
                }
            }
            else if(attributeType.Kind == AttributeKind.DequeAttr)
            {
                object val = current.GetAttribute(attributeType.Name);
                IDeque deque = (IDeque)val;
                if(deque != null)
                {
                    foreach(object entry in deque)
                    {
                        AddObjectsToTodosAndReferencesToDatabase(attributeType.ValueType, entry, ref todos);
                    }
                }
            }
        }

        private void AddObjectsToTodosAndReferencesToDatabase(AttributeType attributeType, object val, ref Stack<IAttributeBearer> todos)
        {
            if(IsGraphType(attributeType))
            {
                AddGraphAsNeeded((INamedGraph)val);
            }
            else if(IsObjectType(attributeType))
            {
                IObject obj = (IObject)val;
                if(obj != null)
                {
                    if(!ObjectToDbId.ContainsKey(obj))
                    {
                        if(todos == null)
                            todos = new Stack<IAttributeBearer>();
                        todos.Push(obj);
                    }
                }
            }
            else if(IsGraphElementType(attributeType))
            {
                if(val != null)
                    AddGraphAsNeeded(GetContainingGraph((IGraphElement)val));
            }
        }

        private void WriteObjectBaseEntry(IObject obj)
        {
            SQLiteCommand addObjectTopologyCommand = createObjectCommand;
            addObjectTopologyCommand.Parameters.Clear();
            addObjectTopologyCommand.Parameters.AddWithValue("@typeId", TypeNameToDbId[obj.Type.PackagePrefixedName]);
            addObjectTopologyCommand.Parameters.AddWithValue("@name", obj.GetObjectName());
            addObjectTopologyCommand.Transaction = transaction;
            int rowsAffected = addObjectTopologyCommand.ExecuteNonQuery();
            long rowId = connection.LastInsertRowId;
            AddObjectWithDbIdToDbIdMapping(obj, rowId);
        }

        private void CompleteObjectEntry(IObject obj)
        {
            SQLiteCommand addObjectCommand = createObjectCommands[obj.Type.TypeID];
            addObjectCommand.Parameters.Clear();
            addObjectCommand.Parameters.AddWithValue("@objectId", ObjectToDbId[obj]);
            foreach(AttributeType attributeType in obj.Type.AttributeTypes)
            {
                if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
                    continue;
                object val = obj.GetAttribute(attributeType.Name);
                addObjectCommand.Parameters.AddWithValue("@" + GetUniqueColumnName(attributeType.Name), ValueOrIdOfReferencedElement(val, attributeType));
            }
            addObjectCommand.Transaction = transaction;
            int rowsAffected = addObjectCommand.ExecuteNonQuery();

            WriteContainerEntries(obj);
        }

        private void WriteContainerChange(IAttributeBearer owningElement, AttributeType attributeType,
                AttributeChangeType changeType, object newValue, object keyValue)
        {
            // todo: split by type into functions instead of using ? in order to split by type?
            switch(changeType)
            {
                case AttributeChangeType.Assign:
                    {
                        object container = newValue;
                        WriteContainerEntries(container, attributeType, owningElement);
                        break;
                    }
                case AttributeChangeType.PutElement:
                    {
                        SQLiteCommand updatingInsert = GetUpdateContainerCommand(owningElement, attributeType);
                        string owningElementIdColumnName;
                        long owningElementId = GetDbIdAndColumnName(owningElement, out owningElementIdColumnName);
                        long entryId = attributeType.Kind==AttributeKind.SetAttr
                                ? ExecuteUpdatingInsert(updatingInsert, attributeType.ValueType, owningElementId, owningElementIdColumnName, ContainerCommand.PutElement, newValue)
                                : ExecuteUpdatingInsert(updatingInsert, attributeType.ValueType, attributeType.Kind == AttributeKind.MapAttr ? attributeType.KeyType : IntegerAttributeType, owningElementId, owningElementIdColumnName, ContainerCommand.PutElement, newValue, keyValue);
                        break;
                    }
                case AttributeChangeType.RemoveElement:
                    {
                        SQLiteCommand updatingInsert = GetUpdateContainerCommand(owningElement, attributeType);
                        string owningElementIdColumnName;
                        long owningElementId = GetDbIdAndColumnName(owningElement, out owningElementIdColumnName);
                        long entryId = attributeType.Kind == AttributeKind.SetAttr
                                ? ExecuteUpdatingInsert(updatingInsert, attributeType.ValueType, owningElementId, owningElementIdColumnName, ContainerCommand.RemoveElement, newValue)
                                : ExecuteUpdatingInsert(updatingInsert, attributeType.ValueType, attributeType.Kind == AttributeKind.MapAttr ? attributeType.KeyType : IntegerAttributeType, owningElementId, owningElementIdColumnName, ContainerCommand.RemoveElement, newValue, keyValue);
                            break;
                    }
                case AttributeChangeType.AssignElement:
                    {
                        SQLiteCommand updatingInsert = GetUpdateContainerCommand(owningElement, attributeType);
                        string owningElementIdColumnName;
                        long owningElementId = GetDbIdAndColumnName(owningElement, out owningElementIdColumnName);
                        long entryId = ExecuteUpdatingInsert(updatingInsert, attributeType.ValueType, attributeType.Kind == AttributeKind.MapAttr ? attributeType.KeyType : IntegerAttributeType, owningElementId, owningElementIdColumnName, ContainerCommand.AssignElement, newValue, keyValue);
                        break;
                    }
            }
        }

        private SQLiteCommand GetUpdateContainerCommand(IAttributeBearer owningElement, AttributeType attributeType)
        {
            if(owningElement is INode)
                return updateNodeContainerCommands[owningElement.Type.TypeID][attributeType.Name];
            else if(owningElement is IEdge)
                return updateEdgeContainerCommands[owningElement.Type.TypeID][attributeType.Name];
            else if(owningElement is IObject)
                return updateObjectContainerCommands[owningElement.Type.TypeID][attributeType.Name];
            throw new Exception("Unsupported owning element type");
        }

        private void WriteContainerEntries(IAttributeBearer owningElement)
        {
            foreach(AttributeType attributeType in owningElement.Type.AttributeTypes)
            {
                if(!IsContainerType(attributeType))
                    continue;

                WriteContainerEntries(owningElement.GetAttribute(attributeType.Name), attributeType, owningElement);
            }
        }

        internal void WriteContainerEntries(object container, AttributeType attributeType, IAttributeBearer owningElement)
        {
            if(attributeType.Kind == AttributeKind.SetAttr)
                WriteSetEntries((IDictionary)container, attributeType, owningElement);
            else if(attributeType.Kind == AttributeKind.MapAttr)
                WriteMapEntries((IDictionary)container, attributeType, owningElement);
            else if(attributeType.Kind == AttributeKind.ArrayAttr)
                WriteArrayEntries((IList)container, attributeType, owningElement);
            else if(attributeType.Kind == AttributeKind.DequeAttr)
                WriteDequeEntries((IDeque)container, attributeType, owningElement);
            else
                throw new Exception("Unsupported container type");
        }

        private void WriteSetEntries(IDictionary set, AttributeType attributeType, IAttributeBearer owningElement)
        {
            string owningElementIdColumnName;
            long owningElementId = GetDbIdAndColumnName(owningElement, out owningElementIdColumnName);

            // new entire container
            SQLiteCommand updatingInsert = GetUpdateContainerCommand(owningElement, attributeType);
            if(set == null)
            {
                long entryId = ExecuteUpdatingInsertNullValue(updatingInsert, owningElementId, owningElementIdColumnName, ContainerCommand.AssignNull);
            }
            else
            {
                long entryId = ExecuteUpdatingInsertNullValue(updatingInsert, owningElementId, owningElementIdColumnName, ContainerCommand.AssignEmptyContainer);

                // add all container entries - explode complete container into series of adds, i.e. put-elements
                foreach(DictionaryEntry entry in set)
                {
                    entryId = ExecuteUpdatingInsert(updatingInsert, attributeType.ValueType, owningElementId, owningElementIdColumnName, ContainerCommand.PutElement, entry.Key);
                }
            }
        }

        private void WriteMapEntries(IDictionary map, AttributeType attributeType, IAttributeBearer owningElement)
        {
            string owningElementIdColumnName;
            long owningElementId = GetDbIdAndColumnName(owningElement, out owningElementIdColumnName);

            // new entire container
            SQLiteCommand updatingInsert = GetUpdateContainerCommand(owningElement, attributeType);
            if(map == null)
            {
                long entryId = ExecuteUpdatingInsertNullValueAndKey(updatingInsert, owningElementId, owningElementIdColumnName, ContainerCommand.AssignNull);
            }
            else
            {
                long entryId = ExecuteUpdatingInsertNullValueAndKey(updatingInsert, owningElementId, owningElementIdColumnName, ContainerCommand.AssignEmptyContainer);

                // add all container entries - explode complete container into series of adds, i.e. put-elements
                foreach(DictionaryEntry entry in map)
                {
                    entryId = ExecuteUpdatingInsert(updatingInsert, attributeType.ValueType, attributeType.KeyType, owningElementId, owningElementIdColumnName, ContainerCommand.PutElement, entry.Value, entry.Key);
                }
            }
        }

        private void WriteArrayEntries(IList array, AttributeType attributeType, IAttributeBearer owningElement)
        {
            string owningElementIdColumnName;
            long owningElementId = GetDbIdAndColumnName(owningElement, out owningElementIdColumnName);

            // new entire container
            SQLiteCommand updatingInsert = GetUpdateContainerCommand(owningElement, attributeType);
            if(array == null)
            {
                long entryId = ExecuteUpdatingInsertNullValueAndKey(updatingInsert, owningElementId, owningElementIdColumnName, ContainerCommand.AssignNull); ;
            }
            else
            {
                long entryId = ExecuteUpdatingInsertNullValueAndKey(updatingInsert, owningElementId, owningElementIdColumnName, ContainerCommand.AssignEmptyContainer);

                // add all container entries - explode complete container into series of adds, i.e. put-elements
                foreach(object entry in array)
                {
                    entryId = ExecuteUpdatingInsert(updatingInsert, attributeType.ValueType, IntegerAttributeType, owningElementId, owningElementIdColumnName, ContainerCommand.PutElement, entry, null);
                }
            }
        }

        private void WriteDequeEntries(IDeque deque, AttributeType attributeType, IAttributeBearer owningElement)
        {
            string owningElementIdColumnName;
            long owningElementId = GetDbIdAndColumnName(owningElement, out owningElementIdColumnName);

            // new entire container
            SQLiteCommand updatingInsert = GetUpdateContainerCommand(owningElement, attributeType);
            if(deque == null)
            {
                long entryId = ExecuteUpdatingInsertNullValueAndKey(updatingInsert, owningElementId, owningElementIdColumnName, ContainerCommand.AssignNull); ;
            }
            else
            {
                long entryId = ExecuteUpdatingInsertNullValueAndKey(updatingInsert, owningElementId, owningElementIdColumnName, ContainerCommand.AssignEmptyContainer);

                // add all container entries - explode complete container into series of adds, i.e. put-elements
                foreach(object entry in deque)
                {
                    entryId = ExecuteUpdatingInsert(updatingInsert, attributeType.ValueType, IntegerAttributeType, owningElementId, owningElementIdColumnName, ContainerCommand.PutElement, entry, null);
                }
            }
        }

        private long ExecuteUpdatingInsert(SQLiteCommand updatingInsert, AttributeType valueAttributeType, long owningElementId, string owningElementIdColumnName, ContainerCommand command, object value)
        {
            if(IsGraphType(valueAttributeType))
                AddGraphAsNeeded((INamedGraph)value);
            else if(IsObjectType(valueAttributeType))
                AddObjectAsNeeded((IObject)value);
            else if(IsGraphElementType(valueAttributeType))
                AddGraphAsNeeded(GetContainingGraph((IGraphElement)value));

            updatingInsert.Parameters.Clear();
            updatingInsert.Parameters.AddWithValue("@" + owningElementIdColumnName, owningElementId);
            updatingInsert.Parameters.AddWithValue("@command", command);
            object valueOrId = ValueOrIdOfReferencedElement(value, valueAttributeType);
            updatingInsert.Parameters.AddWithValue("@value", value != null ? valueOrId : DBNull.Value);
            updatingInsert.Transaction = transaction;
            int rowsAffected = updatingInsert.ExecuteNonQuery();
            return connection.LastInsertRowId;
        }

        private long ExecuteUpdatingInsertNullValue(SQLiteCommand updatingInsert, long owningElementId, string owningElementIdColumnName, ContainerCommand command)
        {
            updatingInsert.Parameters.Clear();
            updatingInsert.Parameters.AddWithValue("@" + owningElementIdColumnName, owningElementId);
            updatingInsert.Parameters.AddWithValue("@command", command);
            updatingInsert.Parameters.AddWithValue("@value", DBNull.Value);
            updatingInsert.Transaction = transaction;
            int rowsAffected = updatingInsert.ExecuteNonQuery();
            return connection.LastInsertRowId;
        }

        private long ExecuteUpdatingInsert(SQLiteCommand updatingInsert, AttributeType valueAttributeType, AttributeType keyAttributeType, long owningElementId, string owningElementIdColumnName, ContainerCommand command, object value, object key)
        {
            if(IsGraphType(keyAttributeType))
                AddGraphAsNeeded((INamedGraph)key);
            else if(IsObjectType(keyAttributeType))
                AddObjectAsNeeded((IObject)key);
            else if(IsGraphElementType(keyAttributeType))
                AddGraphAsNeeded(GetContainingGraph((IGraphElement)key));

            if(IsGraphType(valueAttributeType))
                AddGraphAsNeeded((INamedGraph)value);
            else if(IsObjectType(valueAttributeType))
                AddObjectAsNeeded((IObject)value);
            else if(IsGraphElementType(valueAttributeType))
                AddGraphAsNeeded(GetContainingGraph((IGraphElement)value));

            updatingInsert.Parameters.Clear();
            updatingInsert.Parameters.AddWithValue("@" + owningElementIdColumnName, owningElementId);
            updatingInsert.Parameters.AddWithValue("@command", command);
            object valueValueOrId = ValueOrIdOfReferencedElement(value, valueAttributeType);
            updatingInsert.Parameters.AddWithValue("@value", value != null ? valueValueOrId : DBNull.Value);
            object keyValueOrId = ValueOrIdOfReferencedElement(key, keyAttributeType);
            updatingInsert.Parameters.AddWithValue("@key", key != null ? keyValueOrId : DBNull.Value);
            updatingInsert.Transaction = transaction;
            int rowsAffected = updatingInsert.ExecuteNonQuery();
            return connection.LastInsertRowId;
        }

        private long ExecuteUpdatingInsertNullValueAndKey(SQLiteCommand updatingInsert, long owningElementId, string owningElementIdColumnName, ContainerCommand command)
        {
            updatingInsert.Parameters.Clear();
            updatingInsert.Parameters.AddWithValue("@" + owningElementIdColumnName, owningElementId);
            updatingInsert.Parameters.AddWithValue("@command", command);
            updatingInsert.Parameters.AddWithValue("@value", DBNull.Value);
            updatingInsert.Parameters.AddWithValue("@key", DBNull.Value);
            updatingInsert.Transaction = transaction;
            int rowsAffected = updatingInsert.ExecuteNonQuery();
            return connection.LastInsertRowId;
        }

        private long GetDbId(IAttributeBearer element)
        {
            string columnName;
            if(element == null)
                return -1;
            else
                return GetDbIdAndColumnName(element, out columnName);
        }

        private long GetDbIdAndColumnName(IAttributeBearer element, out string columnName)
        {
            long dbid;
            INode node = element as INode;
            if(node != null)
            {
                dbid = NodeToDbId[node];
                columnName = "nodeId";
            }
            else
            {
                IEdge edge = element as IEdge;
                if(edge != null)
                {
                    dbid = EdgeToDbId[edge];
                    columnName = "edgeId";
                }
                else
                {
                    IObject obj = element as IObject;
                    dbid = ObjectToDbId[obj];
                    columnName = "objectId";
                }
            }
            return dbid;
        }

        private object ValueOrIdOfReferencedElement(object newValue, AttributeType attrType)
        {
            if(IsGraphType(attrType))
            {
                return newValue != null ? (object)GraphToDbId[(INamedGraph)newValue] : (object)DBNull.Value;
            }

            if(IsObjectType(attrType))
            {
                return newValue != null ? (object)ObjectToDbId[(IObject)newValue] : (object)DBNull.Value;
            }

            if(IsGraphElementType(attrType))
            {
                if(newValue == null)
                    return (object)DBNull.Value;
                else if(attrType.Kind == AttributeKind.NodeAttr)
                    return (object)NodeToDbId[(INode)newValue];
                else //if(attrType.Kind == AttributeKind.EdgeAttr)
                    return (object)EdgeToDbId[(IEdge)newValue];
            }

            return newValue;
        }

        #endregion Listen to graph changes in order to persist them and related code

        #region Listen to graph processing changes with influence on persisting graph changes (from the graph processing environments)

        // subgraph processing is based on the assumption that all graph processing occurs in the graph switched-to, and no external changes occur to a graph (besides initial loading)
        public void SwitchToSubgraphHandler(IGraph graph)
        {
            DeregisterPersistenceHandlers();
            graphs.Push((INamedGraph)graph);
            if(GraphToDbId.ContainsKey((INamedGraph)graph)) // switch to graph not appearing in host graph, thus not in database -- is to be ignored (as of now: only handle graphs reachable from host graph); deregistering is idempotent
                RegisterPersistenceHandlers();
        }

        public void ReturnFromSubgraphHandler(IGraph graph) // TODO: also give new graph in event so that user can get along without building a stack on its own - giving the old subgraph is quite pointless, only for name's sake?
        {
            DeregisterPersistenceHandlers();
            IGraph oldGraph = graphs.Pop();
            Debug.Assert(graph == oldGraph);
            if(GraphToDbId.ContainsKey((INamedGraph)this.graph)) // switch back to graph not appearing in host graph, thus not in database -- is to be ignored (as of now: only handle graphs reachable from host graph); deregistering is idempotent
                RegisterPersistenceHandlers();
        }

        public void SpawnSequencesHandler(SequenceParallel parallel, ParallelExecutionBegin[] parallelExecutionBegins)
        {
            throw new Exception("The persistent graph does not support parallel sequence execution!");
        }

        public void BeginExecutionHandler(IPatternMatchingConstruct patternMatchingConstruct)
        {
        }

        public void EndExecutionHandler(IPatternMatchingConstruct patternMatchingConstruct, object result)
        {
        }

        #endregion Listen to graph processing changes with influence on persisting graph changes (from the graph processing environments)

        #region IPersistenceProviderTransactionManager

        public void Start()
        {
            if(IsActive)
                throw new Exception("Cannot start a database transaction when such one is already active");
            transaction = connection.BeginTransaction();
        }

        public void CommitAndRestart()
        {
            if(!IsActive)
                throw new Exception("Database transaction is not active");
            transaction.Commit();
            transaction = connection.BeginTransaction();
        }

        public void Commit()
        {
            if(!IsActive)
                throw new Exception("Database transaction is not active");
            transaction.Commit();
            transaction = null;
        }

        public void Rollback()
        {
            if(!IsActive)
                throw new Exception("Database transaction is not active");
            transaction.Rollback();
            transaction = null;
        }

        public bool IsActive
        {
            get { return transaction != null; }
        }

        #endregion IPersistenceProviderTransactionManager

        #region IPersistenceProviderStatistics

        public int NumNodesInDatabase
        {
            get { return DbIdToNode.Count; }
        }

        public int NumEdgesInDatabase
        {
            get { return DbIdToEdge.Count; }
        }

        public int NumObjectsInDatabase
        {
            get { return DbIdToObject.Count; }
        }

        public int NumGraphsInDatabase
        {
            get { return DbIdToGraph.Count; }
        }

        #endregion IPersistenceProviderStatistics
    }
}
