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
    /// A component carrying out the initial reading-in of the complete host graph.
    /// </summary>
    internal class HostGraphReader
    {
        // prepared statements for handling nodes (assuming available node related tables)
        SQLiteCommand[] readNodeCommands; // per-type joined with topology
        Dictionary<String, SQLiteCommand>[] readNodeContainerCommands; // per-type, per-container-attribute

        // prepared statements for handling edges (assuming available edge related tables)
        SQLiteCommand[] readEdgeCommands; // per-type joined with topology
        Dictionary<String, SQLiteCommand>[] readEdgeContainerCommands; // per-type, per-container-attribute

        // prepared statements for handling graphs
        SQLiteCommand readGraphsCommand;

        // prepared statements for handling objects (assuming available object related tables)
        SQLiteCommand[] readObjectCommands; // per-type joined with topology
        Dictionary<String, SQLiteCommand>[] readObjectContainerCommands; // per-type, per-container-attribute

        PersistenceProviderSQLite persistenceProvider;


        internal HostGraphReader(PersistenceProviderSQLite persistenceProvider)
        {
            this.persistenceProvider = persistenceProvider;
        }

        internal void ReadCompleteGraph()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            // prepare statements for initial graph fetching
            readGraphsCommand = PrepareStatementForReadingGraphs();

            readNodeCommands = new SQLiteCommand[persistenceProvider.graph.Model.NodeModel.Types.Length];
            readNodeContainerCommands = new Dictionary<String, SQLiteCommand>[persistenceProvider.graph.Model.NodeModel.Types.Length];
            readEdgeCommands = new SQLiteCommand[persistenceProvider.graph.Model.EdgeModel.Types.Length];
            readEdgeContainerCommands = new Dictionary<String, SQLiteCommand>[persistenceProvider.graph.Model.EdgeModel.Types.Length];

            foreach(NodeType nodeType in persistenceProvider.graph.Model.NodeModel.Types)
            {
                readNodeCommands[nodeType.TypeID] = PrepareStatementsForReadingNodesIncludingZombieNodes(nodeType);

                readNodeContainerCommands[nodeType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in nodeType.AttributeTypes)
                {
                    if(!PersistenceProviderSQLite.IsContainerType(attributeType))
                        continue;
                    readNodeContainerCommands[nodeType.TypeID].Add(attributeType.Name, PrepareStatementsForReadingContainerAttributes(nodeType, "nodeId", attributeType));
                }
            }
            foreach(EdgeType edgeType in persistenceProvider.graph.Model.EdgeModel.Types)
            {
                readEdgeCommands[edgeType.TypeID] = PrepareStatementsForReadingEdgesIncludingZombieEdges(edgeType);

                readEdgeContainerCommands[edgeType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in edgeType.AttributeTypes)
                {
                    if(!PersistenceProviderSQLite.IsContainerType(attributeType))
                        continue;
                    readEdgeContainerCommands[edgeType.TypeID].Add(attributeType.Name, PrepareStatementsForReadingContainerAttributes(edgeType, "edgeId", attributeType));
                }
            }

            readObjectCommands = new SQLiteCommand[persistenceProvider.graph.Model.ObjectModel.Types.Length];
            readObjectContainerCommands = new Dictionary<String, SQLiteCommand>[persistenceProvider.graph.Model.ObjectModel.Types.Length];

            foreach(ObjectType objectType in persistenceProvider.graph.Model.ObjectModel.Types)
            {
                readObjectCommands[objectType.TypeID] = PrepareStatementsForReadingObjects(objectType);

                readObjectContainerCommands[objectType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in objectType.AttributeTypes)
                {
                    if(!PersistenceProviderSQLite.IsContainerType(attributeType))
                        continue;
                    readObjectContainerCommands[objectType.TypeID].Add(attributeType.Name, PrepareStatementsForReadingContainerAttributes(objectType, "objectId", attributeType));
                }
            }

            using(persistenceProvider.transaction = persistenceProvider.connection.BeginTransaction())
            {
                try
                {
                    // pass 0 - load all graphs (without elements and without the host graph, which was already processed/inserted before) (an alternative would be to load only the host graph, and the others on-demand lazily, creating proxy objects for them - but this would be more coding effort, defy purging, and when carrying out eager loading, it's better not to load graph-by-graph but all at once; later todo when graph types are introduced: load by type)
                    ReadGraphsWithoutHostGraph();

                    // pass 1 - load all elements (first nodes then edges, dispatching them to their containing graph in case they are not zombie nodes/edges)
                    foreach(NodeType nodeType in persistenceProvider.graph.Model.NodeModel.Types)
                    {
                        ReadNodesIncludingZombieNodes(nodeType, readNodeCommands[nodeType.TypeID]);
                    }
                    foreach(EdgeType edgeType in persistenceProvider.graph.Model.EdgeModel.Types)
                    {
                        ReadEdgesIncludingZombieEdges(edgeType, readEdgeCommands[edgeType.TypeID]);
                    }

                    // pass 2 - load all objects
                    foreach(ObjectType objectType in persistenceProvider.graph.Model.ObjectModel.Types)
                    {
                        ReadObjects(objectType, readObjectCommands[objectType.TypeID]);
                    }

                    // pass I - wire references/patch the references into the attributes (TODO - split this into helper methods like PatchReferencesFromDatabase)
                    // 2nd full table scan, type table by type table, patching node/edge/object references (graphs were handled before) in the nodes/edges/objects (in-memory), accessed by their database-id to type mapping
                    foreach(NodeType nodeType in persistenceProvider.graph.Model.NodeModel.Types)
                    {
                        PatchAttributesInElement(nodeType, "nodeId", readNodeCommands[nodeType.TypeID]);

                        foreach(AttributeType attributeType in nodeType.AttributeTypes)
                        {
                            if(!PersistenceProviderSQLite.IsContainerType(attributeType))
                                continue;
                            SQLiteCommand readNodeContainerCommand = readNodeContainerCommands[nodeType.TypeID][attributeType.Name];
                            ReadPatchContainerAttributesInElement(nodeType, "nodeId", attributeType, readNodeContainerCommand);
                        }
                    }
                    foreach(EdgeType edgeType in persistenceProvider.graph.Model.EdgeModel.Types)
                    {
                        PatchAttributesInElement(edgeType, "edgeId", readEdgeCommands[edgeType.TypeID]);

                        foreach(AttributeType attributeType in edgeType.AttributeTypes)
                        {
                            if(!PersistenceProviderSQLite.IsContainerType(attributeType))
                                continue;
                            SQLiteCommand readEdgeContainerCommand = readEdgeContainerCommands[edgeType.TypeID][attributeType.Name];
                            ReadPatchContainerAttributesInElement(edgeType, "edgeId", attributeType, readEdgeContainerCommand);
                        }
                    }
                    foreach(ObjectType objectType in persistenceProvider.graph.Model.ObjectModel.Types)
                    {
                        PatchAttributesInElement(objectType, "objectId", readObjectCommands[objectType.TypeID]);

                        foreach(AttributeType attributeType in objectType.AttributeTypes)
                        {
                            if(!PersistenceProviderSQLite.IsContainerType(attributeType))
                                continue;
                            SQLiteCommand readObjectContainerCommand = readObjectContainerCommands[objectType.TypeID][attributeType.Name];
                            ReadPatchContainerAttributesInElement(objectType, "objectId", attributeType, readObjectContainerCommand);
                        }
                    }

                    persistenceProvider.transaction.Commit(); // no changes written inside transaction, but using one is recommended because of locking
                    persistenceProvider.transaction = null;
                }
                catch
                {
                    persistenceProvider.transaction.Rollback(); // no changes written inside transaction, but using one is recommended because of locking
                    persistenceProvider.transaction = null;
                    throw;
                }
            }

            stopwatch.Stop();
            ConsoleUI.outWriter.WriteLine("Read {0} nodes, {1} edges, {2} graphs, {3} internal class objects in {4} ms.",
                persistenceProvider.NumNodesInDatabase, persistenceProvider.NumEdgesInDatabase, persistenceProvider.NumGraphsInDatabase, persistenceProvider.NumObjectsInDatabase, stopwatch.ElapsedMilliseconds);
        }

        private SQLiteCommand PrepareStatementForReadingGraphs()
        {
            String topologyTableName = "graphs";
            StringBuilder columnNames = new StringBuilder();
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "graphId");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "typeId");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "name");

            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append(topologyTableName);
            return new SQLiteCommand(command.ToString(), persistenceProvider.connection);
        }

        private void ReadGraphsWithoutHostGraph()
        {
            readGraphsCommand.Transaction = persistenceProvider.transaction;
            using(SQLiteDataReader reader = readGraphsCommand.ExecuteReader())
            {
                Dictionary<string, int> attributeNameToColumnIndex = PersistenceProviderSQLite.GetNameToColumnIndexMapping(reader);

                while(reader.Read())
                {
                    long graphId = reader.GetInt64(attributeNameToColumnIndex["graphId"]);
                    if(graphId == PersistenceProviderSQLite.HOST_GRAPH_ID)
                        continue; // skip host graph - the host graph is contained in the graphs table with id HOST_GRAPH_ID and corresponds to the bottom element of the graphs stack, which coincides with graph at this point, being the top element of the graphs stack 
                    long typeId = reader.GetInt64(attributeNameToColumnIndex["typeId"]);
                    String name = reader.GetString(attributeNameToColumnIndex["name"]);
                    INamedGraph graph = (INamedGraph)persistenceProvider.graph.CreateEmptyEquivalent(name); // somewhen later: create based on typeId
                    persistenceProvider.AddGraphWithDbIdToDbIdMapping(graph, graphId);
                }
            }
        }

        private SQLiteCommand PrepareStatementsForReadingNodesIncludingZombieNodes(NodeType nodeType)
        {
            String topologyTableName = "nodes";
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(nodeType.Package, nodeType.Name);
            StringBuilder columnNames = new StringBuilder();
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "perTypeTable.nodeId");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "typeId");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "graphId");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "name");
            foreach(AttributeType attributeType in nodeType.AttributeTypes)
            {
                if(!PersistenceProviderSQLite.IsAttributeTypeMappedToDatabaseColumn(attributeType))
                    continue;
                PersistenceProviderSQLite.AddQueryColumn(columnNames, PersistenceProviderSQLite.GetUniqueColumnName(attributeType.Name));
            }
            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append(tableName);
            command.Append(" as perTypeTable LEFT JOIN ");
            command.Append(topologyTableName);
            command.Append(" ON (perTypeTable.nodeId == " + topologyTableName + ".nodeId)");
            return new SQLiteCommand(command.ToString(), persistenceProvider.connection);
        }

        private SQLiteCommand PrepareStatementsForReadingEdgesIncludingZombieEdges(EdgeType edgeType)
        {
            String topologyTableName = "edges";
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(edgeType.Package, edgeType.Name);
            StringBuilder columnNames = new StringBuilder();
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "perTypeTable.edgeId");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "typeId");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "sourceNodeId");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "targetNodeId");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "graphId");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "name");
            foreach(AttributeType attributeType in edgeType.AttributeTypes)
            {
                if(!PersistenceProviderSQLite.IsAttributeTypeMappedToDatabaseColumn(attributeType))
                    continue;
                PersistenceProviderSQLite.AddQueryColumn(columnNames, PersistenceProviderSQLite.GetUniqueColumnName(attributeType.Name));
            }
            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append(tableName);
            command.Append(" as perTypeTable LEFT JOIN ");
            command.Append(topologyTableName);
            command.Append(" ON (perTypeTable.edgeId == " + topologyTableName + ".edgeId)");
            return new SQLiteCommand(command.ToString(), persistenceProvider.connection);
        }

        private SQLiteCommand PrepareStatementsForReadingObjects(ObjectType objectType)
        {
            String topologyTableName = "objects";
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(objectType.Package, objectType.Name);
            StringBuilder columnNames = new StringBuilder();
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "objectId");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "typeId");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "name");
            foreach(AttributeType attributeType in objectType.AttributeTypes)
            {
                if(!PersistenceProviderSQLite.IsAttributeTypeMappedToDatabaseColumn(attributeType))
                    continue;
                PersistenceProviderSQLite.AddQueryColumn(columnNames, PersistenceProviderSQLite.GetUniqueColumnName(attributeType.Name));
            }
            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append(topologyTableName);
            command.Append(" NATURAL JOIN "); // on objectId
            command.Append(tableName);
            return new SQLiteCommand(command.ToString(), persistenceProvider.connection);
        }

        // attributes of container type don't appear as a column in the type table of its owner type, they come with an entire table on their own (per container attribute of their owner type)
        private SQLiteCommand PrepareStatementsForReadingContainerAttributes(InheritanceType inheritanceType, String ownerIdColumnName, AttributeType attributeType)
        {
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(inheritanceType.Package, inheritanceType.Name, attributeType.Name);
            StringBuilder columnNames = new StringBuilder();
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "entryId");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, ownerIdColumnName);
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "command");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "value");
            if(attributeType.Kind != AttributeKind.SetAttr)
                PersistenceProviderSQLite.AddQueryColumn(columnNames, "key");
            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append(tableName);
            return new SQLiteCommand(command.ToString(), persistenceProvider.connection);
        }

        private void ReadNodesIncludingZombieNodes(NodeType nodeType, SQLiteCommand readNodeCommand)
        {
            readNodeCommand.Transaction = persistenceProvider.transaction;
            using(SQLiteDataReader reader = readNodeCommand.ExecuteReader())
            {
                Dictionary<string, int> attributeNameToColumnIndex = PersistenceProviderSQLite.GetNameToColumnIndexMapping(reader);

                while(reader.Read())
                {
                    INode node = nodeType.CreateNode();

                    foreach(AttributeType attributeType in nodeType.AttributeTypes)
                    {
                        object attributeValue;
                        if(PersistenceProviderSQLite.IsScalarType(attributeType))
                            attributeValue = GetScalarValue(attributeType, reader, attributeNameToColumnIndex);
                        else if(PersistenceProviderSQLite.IsGraphType(attributeType))
                            attributeValue = GetGraphValue(attributeType, reader, attributeNameToColumnIndex);
                        else
                            continue;
                        node.SetAttribute(attributeType.Name, attributeValue);
                    }

                    long nodeId = reader.GetInt64(attributeNameToColumnIndex["nodeId"]);
                    if(!reader.IsDBNull(attributeNameToColumnIndex["graphId"]))
                    {
                        String name = reader.GetString(attributeNameToColumnIndex["name"]);
                        long graphId = reader.GetInt64(attributeNameToColumnIndex["graphId"]);
                        INamedGraph graph = persistenceProvider.DbIdToGraph[graphId];
                        graph.AddNode(node, name);
                    }
                    persistenceProvider.AddNodeWithDbIdToDbIdMapping(node, nodeId);
                }
            }
        }

        private void ReadEdgesIncludingZombieEdges(EdgeType edgeType, SQLiteCommand readEdgeCommand)
        {
            readEdgeCommand.Transaction = persistenceProvider.transaction;
            using(SQLiteDataReader reader = readEdgeCommand.ExecuteReader())
            {
                Dictionary<string, int> attributeNameToColumnIndex = PersistenceProviderSQLite.GetNameToColumnIndexMapping(reader);

                while(reader.Read())
                {
                    IEdge edge = edgeType.CreateEdge();

                    foreach(AttributeType attributeType in edgeType.AttributeTypes)
                    {
                        object attributeValue;
                        if(PersistenceProviderSQLite.IsScalarType(attributeType))
                            attributeValue = GetScalarValue(attributeType, reader, attributeNameToColumnIndex);
                        else if(PersistenceProviderSQLite.IsGraphType(attributeType))
                            attributeValue = GetGraphValue(attributeType, reader, attributeNameToColumnIndex);
                        else
                            continue;
                        edge.SetAttribute(attributeType.Name, attributeValue);
                    }

                    long edgeId = reader.GetInt64(attributeNameToColumnIndex["edgeId"]);
                    if(!reader.IsDBNull(attributeNameToColumnIndex["graphId"]))
                    {
                        long sourceNodeId = reader.GetInt64(attributeNameToColumnIndex["sourceNodeId"]);
                        INode source = persistenceProvider.DbIdToNode[sourceNodeId];
                        long targetNodeId = reader.GetInt64(attributeNameToColumnIndex["targetNodeId"]);
                        INode target = persistenceProvider.DbIdToNode[targetNodeId];
                        edgeType.SetSourceAndTarget(edge, source, target);
                        String name = reader.GetString(attributeNameToColumnIndex["name"]);
                        long graphId = reader.GetInt64(attributeNameToColumnIndex["graphId"]);
                        INamedGraph graph = persistenceProvider.DbIdToGraph[graphId];
                        graph.AddEdge(edge, name);
                    }
                    persistenceProvider.AddEdgeWithDbIdToDbIdMapping(edge, edgeId);
                }
            }
        }

        private void ReadObjects(ObjectType objectType, SQLiteCommand readObjectCommand)
        {
            readObjectCommand.Transaction = persistenceProvider.transaction;
            using(SQLiteDataReader reader = readObjectCommand.ExecuteReader())
            {
                Dictionary<string, int> attributeNameToColumnIndex = PersistenceProviderSQLite.GetNameToColumnIndexMapping(reader);

                while(reader.Read())
                {
                    String name = reader.GetString(attributeNameToColumnIndex["name"]);
                    IObject classObject = objectType.CreateObject(persistenceProvider.host, persistenceProvider.graph.Model.ObjectUniquenessIsEnsured ? name : null);

                    foreach(AttributeType attributeType in objectType.AttributeTypes)
                    {
                        object attributeValue;
                        if(PersistenceProviderSQLite.IsScalarType(attributeType))
                            attributeValue = GetScalarValue(attributeType, reader, attributeNameToColumnIndex);
                        else if(PersistenceProviderSQLite.IsGraphType(attributeType))
                            attributeValue = GetGraphValue(attributeType, reader, attributeNameToColumnIndex);
                        else
                            continue;
                        classObject.SetAttribute(attributeType.Name, attributeValue);
                    }

                    long objectId = reader.GetInt64(attributeNameToColumnIndex["objectId"]);
                    persistenceProvider.AddObjectWithDbIdToDbIdMapping(classObject, objectId);
                }
            }
        }

        private void PatchAttributesInElement(InheritanceType inheritanceType, String elementIdColumnName, SQLiteCommand readElementCommand)
        {
            readElementCommand.Transaction = persistenceProvider.transaction;
            using(SQLiteDataReader reader = readElementCommand.ExecuteReader())
            {
                Dictionary<string, int> attributeNameToColumnIndex = PersistenceProviderSQLite.GetNameToColumnIndexMapping(reader);

                while(reader.Read())
                {
                    long elementId = reader.GetInt64(attributeNameToColumnIndex[elementIdColumnName]);
                    IAttributeBearer element = GetElement(inheritanceType, elementId);

                    foreach(AttributeType attributeType in inheritanceType.AttributeTypes)
                    {
                        object attributeValue;
                        if(PersistenceProviderSQLite.IsObjectType(attributeType))
                            attributeValue = GetObjectValue(attributeType, reader, attributeNameToColumnIndex);
                        else if(PersistenceProviderSQLite.IsGraphElementType(attributeType))
                            attributeValue = GetGraphElement(element, attributeType, reader, attributeNameToColumnIndex);
                        else
                            continue;
                        element.SetAttribute(attributeType.Name, attributeValue);
                    }
                }
            }
        }

        private void ReadPatchContainerAttributesInElement(InheritanceType inheritanceType, String owningElementIdColumnName, AttributeType attributeType, SQLiteCommand readElementContainerAttributeCommand)
        {
            // all graphs/nodes/edges/objects must be available (but not their attributes), now in the patching pass, build the containers and fill them from the container tables
            // container TODO: write back to database a new sequence of adds (not needed when only adds, could be optimized away, extra map would be needed storing this information)

            readElementContainerAttributeCommand.Transaction = persistenceProvider.transaction;
            using(SQLiteDataReader reader = readElementContainerAttributeCommand.ExecuteReader())
            {
                Dictionary<string, int> attributeNameToColumnIndex = PersistenceProviderSQLite.GetNameToColumnIndexMapping(reader);

                while(reader.Read())
                {
                    long entryId = reader.GetInt64(attributeNameToColumnIndex["entryId"]);
                    long ownerId = reader.GetInt64(attributeNameToColumnIndex[owningElementIdColumnName]);
                    byte command = reader.GetByte(attributeNameToColumnIndex["command"]);

                    object value = null;
                    object key = null;
                    if(attributeType.Kind == AttributeKind.SetAttr)
                    {
                        value = GetContainerEntry(attributeType.ValueType, reader, attributeNameToColumnIndex, "value");
                    }
                    else if(attributeType.Kind == AttributeKind.ArrayAttr || attributeType.Kind == AttributeKind.DequeAttr)
                    {
                        value = GetContainerEntry(attributeType.ValueType, reader, attributeNameToColumnIndex, "value");
                        key = GetContainerEntry(persistenceProvider.IntegerAttributeType, reader, attributeNameToColumnIndex, "key");
                    }
                    else if(attributeType.Kind == AttributeKind.MapAttr)
                    {
                        key = GetContainerEntry(attributeType.KeyType, reader, attributeNameToColumnIndex, "key");
                        value = GetContainerEntry(attributeType.ValueType, reader, attributeNameToColumnIndex, "value");
                    }

                    IAttributeBearer element = GetElement(inheritanceType, ownerId);
                    ApplyContainerCommandToElement(element, attributeType, (ContainerCommand)command, value, key);
                }
            }
        }

        private IAttributeBearer GetElement(InheritanceType inheritanceType, long ownerId)
        {
            if(inheritanceType is NodeType)
                return persistenceProvider.DbIdToNode[ownerId];
            else if(inheritanceType is EdgeType)
                return persistenceProvider.DbIdToEdge[ownerId];
            else if(inheritanceType is ObjectType)
                return persistenceProvider.DbIdToObject[ownerId];
            throw new Exception("Unsupported inheritance type");
        }

        private void ApplyContainerCommandToElement(IAttributeBearer element, AttributeType attributeType, ContainerCommand command, object value, object key)
        {
            // Es gibt immer ein Assign-empty oder -null pro owning Element id als ersten Eintrag in der Eintragsfolge, es kann mehrere geben, der letzte dieser Einträge in der Eintragsfolge ist der aktuelle Container (der Rest Geschichte)
            switch(command)
            {
                case ContainerCommand.AssignEmptyContainer:
                    {
                        object container = GetEmptyContainer(attributeType);
                        element.SetAttribute(attributeType.Name, container);
                        if(persistenceProvider.initializedContainers.IsMarked(element, attributeType.Name))
                            persistenceProvider.modifiedContainers.Mark(element, attributeType.Name);
                        else
                            persistenceProvider.initializedContainers.Mark(element, attributeType.Name);
                        break;
                    }
                case ContainerCommand.PutElement:
                    {
                        object container = element.GetAttribute(attributeType.Name);
                        ApplyContainerPutElementCommand(container, attributeType, value, key);
                        break;
                    }
                case ContainerCommand.RemoveElement:
                    {
                        object container = element.GetAttribute(attributeType.Name);
                        ApplyContainerRemoveElementCommand(container, attributeType, value, key);
                        persistenceProvider.modifiedContainers.Mark(element, attributeType.Name);
                        break;
                    }
                case ContainerCommand.AssignElement:
                    {
                        object container = element.GetAttribute(attributeType.Name);
                        ApplyContainerAssignElementCommand(container, attributeType, value, key);
                        persistenceProvider.modifiedContainers.Mark(element, attributeType.Name);
                        break;
                    }
                case ContainerCommand.AssignNull:
                    element.SetAttribute(attributeType.Name, null);
                    if(persistenceProvider.initializedContainers.IsMarked(element, attributeType.Name))
                        persistenceProvider.modifiedContainers.Mark(element, attributeType.Name);
                    else
                        persistenceProvider.initializedContainers.Mark(element, attributeType.Name);
                    break;
            }
        }

        private static object GetEmptyContainer(AttributeType attributeType)
        {
            switch(attributeType.Kind)
            {
                case AttributeKind.SetAttr:
                {
                    Type srcType = attributeType.ValueType.Type;
                    Type dstType = typeof(SetValueType);
                    IDictionary set = ContainerHelper.NewDictionary(srcType, dstType);
                    return set;
                }
                case AttributeKind.MapAttr:
                {
                    Type srcType = attributeType.KeyType.Type;
                    Type dstType = attributeType.ValueType.Type; 
                    IDictionary map = ContainerHelper.NewDictionary(srcType, dstType);
                    return map;
                }
                case AttributeKind.ArrayAttr:
                {
                    Type valueType = attributeType.ValueType.Type;
                    IList array = ContainerHelper.NewList(valueType);
                    return array;
                }
                case AttributeKind.DequeAttr:
                {
                    Type valueType = attributeType.ValueType.Type;
                    IDeque deque = ContainerHelper.NewDeque(valueType);
                    return deque;
                }
                default:
                    throw new Exception("Unsupported container type");
            }
        }

        private static void ApplyContainerPutElementCommand(object container, AttributeType attributeType, object value, object key)
        {
            switch(attributeType.Kind)
            {
                case AttributeKind.SetAttr:
                    IDictionary set = (IDictionary)container;
                    set[value] = null;
                    break;
                case AttributeKind.MapAttr:
                    IDictionary map = (IDictionary)container;
                    map[key] = value;
                    break;
                case AttributeKind.ArrayAttr:
                    IList array = (IList)container;
                    if(key == null)
                        array.Add(value);
                    else
                        array.Insert((int)key, value);
                    break;
                case AttributeKind.DequeAttr:
                    IDeque deque = (IDeque)container;
                    if(key == null)
                        deque.Add(value);
                    else
                        deque.EnqueueAt((int)key, value);
                    break;
                default:
                    throw new Exception("Unsupported container type");
            }
        }

        private static void ApplyContainerRemoveElementCommand(object container, AttributeType attributeType, object value, object key)
        {
            switch(attributeType.Kind)
            {
                case AttributeKind.SetAttr:
                    IDictionary set = (IDictionary)container;
                    set.Remove(value);
                    break;
                case AttributeKind.MapAttr:
                    IDictionary map = (IDictionary)container;
                    map.Remove(key);
                    break;
                case AttributeKind.ArrayAttr:
                    IList array = (IList)container;
                    if(key == null)
                        array.RemoveAt(array.Count - 1);
                    else
                        array.RemoveAt((int)key);
                    break;
                case AttributeKind.DequeAttr:
                    IDeque deque = (IDeque)container;
                    if(key == null)
                        deque.Dequeue();
                    else
                        deque.DequeueAt((int)key);
                    break;
                default:
                    throw new Exception("Unsupported container type");
            }
        }

        private static void ApplyContainerAssignElementCommand(object container, AttributeType attributeType, object value, object key)
        {
            switch(attributeType.Kind)
            {
                case AttributeKind.MapAttr:
                    IDictionary map = (IDictionary)container;
                    map[key] = value;
                    break;
                case AttributeKind.ArrayAttr:
                    IList array = (IList)container;
                    array[(int)key] = value;
                    break;
                case AttributeKind.DequeAttr:
                    IDeque deque = (IDeque)container;
                    deque[(int)key] = value;
                    break;
                default:
                    throw new Exception("Unsupported container type");
            }
        }

        private static object GetScalarValue(AttributeType attributeType, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            return GetScalarValueFromSpecifiedColumn(attributeType, PersistenceProviderSQLite.GetUniqueColumnName(attributeType.Name), reader, attributeNameToColumnIndex);
        }

        private static object GetScalarValueFromSpecifiedColumn(AttributeType attributeType, String columnName, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            int index = attributeNameToColumnIndex[columnName];
            return GetScalarValueFromSpecifiedColumn(attributeType, reader, index);
        }

        private static object GetScalarValueOrNullFromSpecifiedColumn(AttributeType attributeType, String columnName, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            int index = attributeNameToColumnIndex[columnName];
            if(reader.IsDBNull(index))
                return null;
            return GetScalarValueFromSpecifiedColumn(attributeType, reader, index);
        }

        private static object GetScalarValueFromSpecifiedColumn(AttributeType attributeType, SQLiteDataReader reader, int index)
        {
            switch(attributeType.Kind)
            {
                case AttributeKind.ByteAttr: return (SByte)reader.GetInt16(index);
                case AttributeKind.ShortAttr: return reader.GetInt16(index);
                case AttributeKind.IntegerAttr: return reader.GetInt32(index);
                case AttributeKind.LongAttr: return reader.GetInt64(index);
                case AttributeKind.BooleanAttr: return reader.GetBoolean(index);
                case AttributeKind.StringAttr: return reader.IsDBNull(index) ? null : reader.GetString(index);
                case AttributeKind.EnumAttr: return Enum.Parse(attributeType.EnumType.EnumType, reader.GetString(index));
                case AttributeKind.FloatAttr: return reader.GetFloat(index);
                case AttributeKind.DoubleAttr: return reader.GetDouble(index);
                default: throw new Exception("Non-scalar attribute kind");
            }
        }

        private IGraph GetGraphValue(AttributeType attributeType, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            return GetGraphValueFromSpecifiedColumn(PersistenceProviderSQLite.GetUniqueColumnName(attributeType.Name), reader, attributeNameToColumnIndex);
        }

        private IGraph GetGraphValueFromSpecifiedColumn(String columnName, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            int index = attributeNameToColumnIndex[columnName];
            if(reader.IsDBNull(index))
                return null;
            long dbid = reader.GetInt64(index);
            return persistenceProvider.DbIdToGraph[dbid];
        }

        private IObject GetObjectValue(AttributeType attributeType, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            return GetObjectValueFromSpecifiedColumn(PersistenceProviderSQLite.GetUniqueColumnName(attributeType.Name), reader, attributeNameToColumnIndex);
        }

        private IObject GetObjectValueFromSpecifiedColumn(String columnName, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            int index = attributeNameToColumnIndex[columnName];
            if(reader.IsDBNull(index))
                return null;
            long dbid = reader.GetInt64(index);
            return persistenceProvider.DbIdToObject[dbid];
        }

        private IGraphElement GetGraphElement(IAttributeBearer owner, AttributeType attributeType, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            int index = attributeNameToColumnIndex[PersistenceProviderSQLite.GetUniqueColumnName(attributeType.Name)];
            if(reader.IsDBNull(index))
                return null;
            long dbid = reader.GetInt64(index);
            if(attributeType.Kind == AttributeKind.NodeAttr)
                return persistenceProvider.DbIdToNode[dbid];
            else
                return persistenceProvider.DbIdToEdge[dbid];
        }

        // maybe TODO: ReadContainerEntryValue, also rename the other GetXXX to ReadXXX?
        private object GetContainerEntry(AttributeType attributeType, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex, String columnName)
        {
            Debug.Assert(columnName == "value" || columnName == "key");
            if(PersistenceProviderSQLite.IsScalarType(attributeType))
                return GetScalarValueOrNullFromSpecifiedColumn(attributeType, columnName, reader, attributeNameToColumnIndex);
            else if(PersistenceProviderSQLite.IsGraphType(attributeType))
                return GetGraphValueFromSpecifiedColumn(columnName, reader, attributeNameToColumnIndex);
            else if(PersistenceProviderSQLite.IsObjectType(attributeType))
                return GetObjectValueFromSpecifiedColumn(columnName, reader, attributeNameToColumnIndex);
            else if(PersistenceProviderSQLite.IsGraphElementType(attributeType))
                return GetGraphElementFromSpecifiedColumn(attributeType, columnName, reader, attributeNameToColumnIndex);
            throw new Exception("Unsupported container type");
        }

        private IGraphElement GetGraphElementFromSpecifiedColumn(AttributeType attributeType, String columnName, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            int index = attributeNameToColumnIndex[columnName];
            if(reader.IsDBNull(index))
                return null;
            long dbid = reader.GetInt64(index);
            if(attributeType.Kind == AttributeKind.NodeAttr)
                return persistenceProvider.DbIdToNode[dbid];
            else
                return persistenceProvider.DbIdToEdge[dbid];
        }
    }
}
