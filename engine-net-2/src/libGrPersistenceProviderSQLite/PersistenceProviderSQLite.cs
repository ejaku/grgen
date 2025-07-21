/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
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
    /// </summary>
    public class PersistenceProviderSQLite : IPersistenceProvider
    {
        enum TypeKind { NodeClass = 0, EdgeClass = 1, ObjectClass = 2 };

        SQLiteConnection connection;

        // one command per node type; reading is only carried out initally, so prepared statements are only locally needed for reading
        SQLiteCommand createNodeCommand; // topology
        SQLiteCommand[] createNodeCommands; // per-type
        SQLiteCommand retypeNodeCommand; // topology
        Dictionary<String, SQLiteCommand>[] updateNodeCommands; // per-type, per-attribute
        SQLiteCommand deleteNodeCommand; // topology
        SQLiteCommand[] deleteNodeCommands; // per-type

        // one command per edge type; reading is only carried out initally, so prepared statements are only locally needed for reading
        SQLiteCommand createEdgeCommand; // topology
        SQLiteCommand[] createEdgeCommands; // per-type
        SQLiteCommand retypeEdgeCommand; // topology
        Dictionary<String, SQLiteCommand>[] updateEdgeCommands; // per-type, per-attribute
        SQLiteCommand deleteEdgeCommand; // topology
        SQLiteCommand[] deleteEdgeCommands; // per-type

        INamedGraph graph; // the host graph (todo: handling of referenced graphs / switch to subgraph processing)

        Dictionary<long, INode> DbIdToNode; // the ids are globally unique due to the topology tables, the per-type tables only reference them
        Dictionary<INode, long> NodeToDbId;
        Dictionary<long, IEdge> DbIdToEdge;
        Dictionary<IEdge, long> EdgeToDbId;
        //Dictionary<long, IObject> DbIdToObject;
        //Dictionary<IObject, long> ObjectToDbId;
        // TODO: persistent id of graphs
        Dictionary<long, string> DbIdToTypeName;
        Dictionary<string, long> TypeNameToDbId;

        int HOST_GRAPH_ID_TEMP_HACK = 0;


        public PersistenceProviderSQLite()
        {
        }

        public void Open(string connectionParameters)
        {
            connection = new SQLiteConnection(connectionParameters);
            connection.Open();
            ConsoleUI.outWriter.WriteLine("persistence provider \"libGrPersistenceProviderSQLite.dll\" connected to database with \"{0}\".", connectionParameters);
        }

        public void ReadPersistentGraphAndRegisterToListenToGraphModifications(INamedGraph namedGraph)
        {
            if(namedGraph.NumNodes != 0 || namedGraph.NumEdges != 0)
                throw new Exception("The graph must be empty!");

            graph = namedGraph;

            CreateSchemaIfNotExistsOrAdaptToCompatibleChanges();
            // TODO: CleanUnusedSubgraphAndObjectReferencesAndCompactifyContainerChangeHistory();
            ReadCompleteGraph();
            RegisterPersistenceHandlers();
            PrepareStatementsForGraphModifications();
        }

        private void CreateSchemaIfNotExistsOrAdaptToCompatibleChanges()
        {
            DbIdToNode = new Dictionary<long, INode>();
            NodeToDbId = new Dictionary<INode, long>();
            DbIdToEdge = new Dictionary<long, IEdge>();
            EdgeToDbId = new Dictionary<IEdge, long>();
            //DbIdToObject = new Dictionary<long, IObject>();
            //ObjectToDbId = new Dictionary<IObject, long>();
            DbIdToTypeName = new Dictionary<long, string>();
            TypeNameToDbId = new Dictionary<string, long>();

            CreateIdentityAndTopologyTables();
            CreateTypesWithAttributeTables();

            // TODO: adapt to compatible changes (i.e. to user changes of the model, these are reflected in the types/attributes, the basic schema will be pretty constant)
            // to be supported: extension by attributes; in case of incompatible changes: report to the user that he has to carry out the adaptation on his own with SQL commands
        }

        private void CreateIdentityAndTopologyTables()
        {
            CreateNodesTable();
            CreateEdgesTable();
            CreateGraphsTable();
            //CreateObjectsTable();
        }

        private void CreateTypesWithAttributeTables()
        {
            CreateTypesTable();
            AddUnknownModelTypesToTypesTable();

            // TODO: create configuration and status table, a key-value-store, esp. including version, plus later stuff?

            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                CreateInheritanceTypeTable(nodeType, "nodeId");
            }

            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                CreateInheritanceTypeTable(edgeType, "edgeId");
            }

            /*foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
            {
                CreateInheritanceTypeTable(objectType, "objectId");
            }*/
        }

        private void CreateNodesTable()
        {
            CreateTable("nodes", "nodeId",
                "typeId", "INTEGER NOT NULL", // type defines table where attributes are stored
                "graphId", "INTEGER NOT NULL",
                "name", "TEXT NOT NULL" // maybe TODO: name in extra table - this is for a named graph, but should be also available for unnamed graphs in the end...
                );
        }

        private void CreateEdgesTable()
        {
            CreateTable("edges", "edgeId",
                "typeId", "INTEGER NOT NULL", // type defines table where attributes are stored
                "sourceNodeId", "INTEGER NOT NULL",
                "targetNodeId", "INTEGER NOT NULL",
                "graphId", "INTEGER NOT NULL",
                "name", "TEXT NOT NULL"
                );
        }

        private void CreateGraphsTable()
        {
            // only id and name for now, but a later extension by graph attributes and then by graph types makes a lot of sense -- multiple graph tables
            CreateTable("graphs", "graphId",
                "name", "TEXT NOT NULL"
                );
        }

        /*private void CreateObjectsTable()
        {
            CreateTable("objects", "objectId"
                );
        }*/

        private void CreateTypesTable()
        {
            CreateTable("types", "typeId",
                "kind", "INT NOT NULL",
                "name", "TEXT NOT NULL"
                );
        }

        private void CreateInheritanceTypeTable(InheritanceType type, String idColumnName)
        {
            String tableName = EscapeTableName(type.PackagePrefixedName);
            List<String> columnNamesAndTypes = new List<String>(); // ArrayBuilder
            foreach(AttributeType attributeType in type.AttributeTypes)
            {
                if(!IsScalarType(attributeType)) // TODO: container type, graph type, internal object type, external/object type.
                    continue;

                columnNamesAndTypes.Add(UniquifyName(attributeType.Name));
                columnNamesAndTypes.Add(ScalarAttributeTypeToSQLiteType(attributeType));
            }

            CreateTable(tableName, idColumnName, columnNamesAndTypes.ToArray()); // the id in the type table is a local copy of the global id from the corresponding topology table
        }

        private bool IsScalarType(AttributeType attributeType)
        {
            switch(attributeType.Kind)
            {
                case AttributeKind.ByteAttr:
                case AttributeKind.ShortAttr:
                case AttributeKind.IntegerAttr:
                case AttributeKind.LongAttr:
                case AttributeKind.BooleanAttr:
                case AttributeKind.StringAttr:
                case AttributeKind.EnumAttr:
                case AttributeKind.FloatAttr:
                case AttributeKind.DoubleAttr:
                    return true;
                default:
                    return false;
            }
        }

        private string ScalarAttributeTypeToSQLiteType(AttributeType attributeType) // BasicSQLiteType()?
        {
            switch(attributeType.Kind)
            {
                case AttributeKind.ByteAttr: return "INT";
                case AttributeKind.ShortAttr: return "INT";
                case AttributeKind.IntegerAttr: return "INT";
                case AttributeKind.LongAttr: return "INT";
                case AttributeKind.BooleanAttr: return "INT";
                case AttributeKind.StringAttr: return "TEXT";
                case AttributeKind.EnumAttr: return "TEXT";
                case AttributeKind.FloatAttr: return "REAL";
                case AttributeKind.DoubleAttr: return "REAL";
                default: throw new Exception("Non-scalar attribute kind");
            }
        }

        private void CreateTable(String tableName, String idColumnName, params String[] columnNamesAndTypes)
        {
            StringBuilder columnNames = new StringBuilder();
            AddQueryColumn(columnNames, idColumnName + " INTEGER PRIMARY KEY AUTOINCREMENT"); // AUTOINCREMENT so ids cannot be reused (preventing wrong mappings after deletion)
            for(int i = 0; i < columnNamesAndTypes.Length; i += 2)
            {
                AddQueryColumn(columnNames, columnNamesAndTypes[i] + " " + columnNamesAndTypes[i + 1]);
            }

            StringBuilder command = new StringBuilder();
            command.Append("CREATE TABLE IF NOT EXISTS ");
            command.Append(tableName);
            command.Append("(");
            command.Append(columnNames.ToString());
            command.Append(") ");
            command.Append("STRICT");
            SQLiteCommand createSchemaCommand = new SQLiteCommand(command.ToString(), connection);

            int rowsAffected = createSchemaCommand.ExecuteNonQuery();

            createSchemaCommand.Dispose();
        }

        #region Types table populating

        private void AddUnknownModelTypesToTypesTable()
        {
            // the grgen type id from the model is only unique per kind, and not stable upon insert(/delete), so we have to match by name to map to the database type id
            Dictionary<string, TypeKind> typeNameToKind = ReadKnownTypes();

            using(SQLiteCommand fillTypeCommand = GetFillTypeCommand())
            {
                foreach(NodeType nodeType in graph.Model.NodeModel.Types)
                {
                    FillUnknownType(typeNameToKind, fillTypeCommand, nodeType, TypeKind.NodeClass);
                }
                foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
                {
                    FillUnknownType(typeNameToKind, fillTypeCommand, edgeType, TypeKind.EdgeClass);
                }
                /*foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
                {
                    FillUnknownType(typeNameToKind, fillTypeCommand, objectType, TypeKind.ObjectClass);
                }*/
            }
        }

        private void FillUnknownType(Dictionary<string, TypeKind> typeNameToKind, SQLiteCommand fillTypeCommand, InheritanceType type, TypeKind kind)
        {
            if(typeNameToKind.ContainsKey(type.PackagePrefixedName))
            {
                if(typeNameToKind[type.PackagePrefixedName] != kind)
                    throw new Exception("The " + kind + " " + type.PackagePrefixedName + " is of a different kind in the database (" + typeNameToKind[type.PackagePrefixedName] + ")!");
                return;
            }
            FillType(fillTypeCommand, type, kind);
        }

        private void FillType(SQLiteCommand fillTypeCommand, InheritanceType type, TypeKind kind)
        {
            fillTypeCommand.Parameters.Clear();
            fillTypeCommand.Parameters.AddWithValue("@kind", (int)kind);
            fillTypeCommand.Parameters.AddWithValue("@name", type.PackagePrefixedName); // may include colons which are removed from the table name

            int rowsAffected = fillTypeCommand.ExecuteNonQuery();

            long rowId = connection.LastInsertRowId;
            AddTypeNameWithDbIdToDbIdMapping(type.PackagePrefixedName, rowId); // the grgen type id from the model is only unique per kind, and not stable upon insert(/delete), so we use the name
        }

        private SQLiteCommand GetFillTypeCommand()
        {
            String tableName = "types";
            StringBuilder columnNames = new StringBuilder();
            StringBuilder parameterNames = new StringBuilder();
            AddInsertParameter(columnNames, parameterNames, "kind");
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

        private Dictionary<string, TypeKind> ReadKnownTypes()
        {
            Dictionary<string, TypeKind> typeNameToKind = new Dictionary<string, TypeKind>();

            using(SQLiteCommand command = PrepareStatementsForReadingKnownTypes())
            {
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    Dictionary<string, int> nameToColumnIndex = GetNameToColumnIndexMapping(reader);
                    while(reader.Read())
                    {
                        long typeId = reader.GetInt64(nameToColumnIndex["typeId"]);
                        int kind = reader.GetInt32(nameToColumnIndex["kind"]);
                        String name = reader.GetString(nameToColumnIndex["name"]);

                        typeNameToKind.Add(name, (TypeKind)kind);

                        AddTypeNameWithDbIdToDbIdMapping(name, typeId);
                    }
                }
            }

            return typeNameToKind;
        }

        private SQLiteCommand PrepareStatementsForReadingKnownTypes()
        {
            String tableName = "types";
            StringBuilder columnNames = new StringBuilder();
            AddQueryColumn(columnNames, "typeId");
            AddQueryColumn(columnNames, "kind");
            AddQueryColumn(columnNames, "name");

            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append(tableName);

            return new SQLiteCommand(command.ToString(), connection);
        }

        #endregion Types table populating

        #region Initial graph reading

        private void ReadCompleteGraph()
        {
            SQLiteCommand[] readNodeCommands = new SQLiteCommand[graph.Model.NodeModel.Types.Length];
            SQLiteCommand[] readEdgeCommands = new SQLiteCommand[graph.Model.EdgeModel.Types.Length];

            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                readNodeCommands[nodeType.TypeID] = PrepareStatementsForReadingNodes(nodeType);
            }
            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                readEdgeCommands[edgeType.TypeID] = PrepareStatementsForReadingEdges(edgeType);
            }

            // pass 1 - load all elements (first nodes then edges)
            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                ReadNodes(nodeType, readNodeCommands[nodeType.TypeID]);
            }
            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                ReadEdges(edgeType, readEdgeCommands[edgeType.TypeID]);
            }

            // pass 2 - wire their references/patch the references into the attributes - TODO
        }

        private SQLiteCommand PrepareStatementsForReadingNodes(NodeType nodeType)
        {
            // later TODO: handling of zombies in tables (out-of-graph nodes, depending on semantic model)
            String topologyTableName = "nodes";
            String tableName = EscapeTableName(nodeType.PackagePrefixedName);
            StringBuilder columnNames = new StringBuilder();
            AddQueryColumn(columnNames, "nodeId");
            AddQueryColumn(columnNames, "typeId");
            AddQueryColumn(columnNames, "graphId");
            AddQueryColumn(columnNames, "name");
            foreach(AttributeType attributeType in nodeType.AttributeTypes)
            {
                AddQueryColumn(columnNames, UniquifyName(attributeType.Name));
            }
            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append(topologyTableName);
            command.Append(" NATURAL JOIN "); // on nodeId
            command.Append(tableName);
            //command.Append("WHERE "); // TODO limit to host graph / current subgraph
            //command.Append("graphId == @graphId");
            return new SQLiteCommand(command.ToString(), connection);
        }

        private SQLiteCommand PrepareStatementsForReadingEdges(EdgeType edgeType)
        {
            // later TODO: handling of zombies in tables (out-of-graph edges, depending on semantic model)
            String topologyTableName = "edges";
            String tableName = EscapeTableName(edgeType.PackagePrefixedName);
            StringBuilder columnNames = new StringBuilder();
            AddQueryColumn(columnNames, "edgeId");
            AddQueryColumn(columnNames, "typeId");
            AddQueryColumn(columnNames, "sourceNodeId");
            AddQueryColumn(columnNames, "targetNodeId");
            AddQueryColumn(columnNames, "graphId");
            AddQueryColumn(columnNames, "name");
            foreach(AttributeType attributeType in edgeType.AttributeTypes)
            {
                AddQueryColumn(columnNames, UniquifyName(attributeType.Name));
            }
            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append(topologyTableName);
            command.Append(" NATURAL JOIN "); // on edgeId
            command.Append(tableName);
            //command.Append("WHERE "); // TODO limit to host graph / current subgraph
            //command.Append("graphId == @graphId");
            return new SQLiteCommand(command.ToString(), connection);
        }

        private void ReadNodes(NodeType nodeType, SQLiteCommand readNodeCommand)
        {
            using(SQLiteDataReader reader = readNodeCommand.ExecuteReader())
            {
                Dictionary<string, int> attributeNameToColumnIndex = GetNameToColumnIndexMapping(reader);

                while(reader.Read())
                {
                    INode node = nodeType.CreateNode();

                    foreach(AttributeType attributeType in nodeType.AttributeTypes)
                    {
                        if(!IsScalarType(attributeType))
                            continue;

                        object attributeValue = GetScalarValue(attributeType, reader, attributeNameToColumnIndex);
                        node.SetAttribute(attributeType.Name, attributeValue);
                    }

                    String name = reader.GetString(attributeNameToColumnIndex["name"]);
                    graph.AddNode(node, name);
                    //long graphId = reader.GetInt64(attributeNameToColumnIndex["graphId"]);
                    long nodeId = reader.GetInt64(attributeNameToColumnIndex["nodeId"]);
                    AddNodeWithDbIdToDbIdMapping(node, nodeId);
                }
            }
        }

        private void ReadEdges(EdgeType edgeType, SQLiteCommand readEdgeCommand)
        {
            using(SQLiteDataReader reader = readEdgeCommand.ExecuteReader())
            {
                Dictionary<string, int> attributeNameToColumnIndex = GetNameToColumnIndexMapping(reader);

                while(reader.Read())
                {
                    IEdge edge = edgeType.CreateEdge();

                    foreach(AttributeType attributeType in edgeType.AttributeTypes)
                    {
                        if(!IsScalarType(attributeType))
                            continue;

                        object attributeValue = GetScalarValue(attributeType, reader, attributeNameToColumnIndex);
                        edge.SetAttribute(attributeType.Name, attributeValue);
                    }

                    long sourceNodeId = reader.GetInt64(attributeNameToColumnIndex["sourceNodeId"]);
                    INode source = DbIdToNode[sourceNodeId];
                    long targetNodeId = reader.GetInt64(attributeNameToColumnIndex["targetNodeId"]);
                    INode target = DbIdToNode[targetNodeId];
                    edgeType.SetSourceAndTarget(edge, source, target);
                    String name = reader.GetString(attributeNameToColumnIndex["name"]);
                    graph.AddEdge(edge, name);
                    //long graphId = reader.GetInt64(attributeNameToColumnIndex["graphId"]);
                    long edgeId = reader.GetInt64(attributeNameToColumnIndex["edgeId"]);
                    AddEdgeWithDbIdToDbIdMapping(edge, edgeId);
                }
            }
        }

        private Dictionary<string, int> GetNameToColumnIndexMapping(SQLiteDataReader reader)
        {
            Dictionary<string, int> nameToColumnIndex = new Dictionary<string, int>();
            for(int i = 0; i < reader.FieldCount; ++i)
            {
                string columnName = reader.GetName(i);
                nameToColumnIndex.Add(columnName, i); // note that there are non-attribute columns existing... see also UniquifyName that prevents collisions of column names stemming from user defined attributes with pre-defined database columns
            }
            return nameToColumnIndex;
        }

        private object GetScalarValue(AttributeType attributeType, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            int index = attributeNameToColumnIndex[UniquifyName(attributeType.Name)];
            switch(attributeType.Kind)
            {
                case AttributeKind.ByteAttr: return (SByte)reader.GetInt16(index);
                case AttributeKind.ShortAttr: return reader.GetInt16(index);
                case AttributeKind.IntegerAttr: return reader.GetInt32(index);
                case AttributeKind.LongAttr: return reader.GetInt64(index);
                case AttributeKind.BooleanAttr: return reader.GetBoolean(index);
                case AttributeKind.StringAttr: return reader.GetString(index);
                case AttributeKind.EnumAttr: return Enum.Parse(attributeType.EnumType.EnumType, reader.GetString(index));
                case AttributeKind.FloatAttr: return reader.GetFloat(index);
                case AttributeKind.DoubleAttr: return reader.GetDouble(index);
                default: throw new Exception("Non-scalar attribute kind");
            }
        }

        #endregion Initial graph reading

        #region Graph modification handling preparations

        // TODO: maybe lazy initialization...
        private void PrepareStatementsForGraphModifications()
        {
            createNodeCommand = PrepareNodeInsert();
            createNodeCommands = new SQLiteCommand[graph.Model.NodeModel.Types.Length];
            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                createNodeCommands[nodeType.TypeID] = PrepareInsert(nodeType, "nodeId");
            }
            createEdgeCommand = PrepareEdgeInsert();
            createEdgeCommands = new SQLiteCommand[graph.Model.EdgeModel.Types.Length];
            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                createEdgeCommands[edgeType.TypeID] = PrepareInsert(edgeType, "edgeId");
            }

            retypeNodeCommand = PrepareRetype("nodes", "nodeId");
            retypeEdgeCommand = PrepareRetype("edges", "edgeId");

            updateNodeCommands = new Dictionary<String, SQLiteCommand>[graph.Model.NodeModel.Types.Length];
            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                updateNodeCommands[nodeType.TypeID] = new Dictionary<String, SQLiteCommand>(nodeType.NumAttributes);
                foreach(AttributeType attributeType in nodeType.AttributeTypes)
                {
                    if(!IsScalarType(attributeType))
                        continue;
                    updateNodeCommands[nodeType.TypeID][attributeType.Name] = PrepareUpdate(nodeType, "nodeId", attributeType);
                }
            }
            updateEdgeCommands = new Dictionary<String, SQLiteCommand>[graph.Model.EdgeModel.Types.Length];
            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                updateEdgeCommands[edgeType.TypeID] = new Dictionary<String, SQLiteCommand>(edgeType.NumAttributes);
                foreach(AttributeType attributeType in edgeType.AttributeTypes)
                {
                    if(!IsScalarType(attributeType))
                        continue;
                    updateEdgeCommands[edgeType.TypeID][attributeType.Name] = PrepareUpdate(edgeType, "edgeId", attributeType);
                }
            }

            deleteNodeCommand = PrepareTopologyDelete("nodes", "nodeId");
            deleteNodeCommands = new SQLiteCommand[graph.Model.NodeModel.Types.Length];
            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                deleteNodeCommands[nodeType.TypeID] = PrepareDelete(nodeType, "nodeId");
            }
            deleteEdgeCommand = PrepareTopologyDelete("edges", "edgeId");
            deleteEdgeCommands = new SQLiteCommand[graph.Model.EdgeModel.Types.Length];
            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                deleteEdgeCommands[edgeType.TypeID] = PrepareDelete(edgeType, "edgeId");
            }
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

        private SQLiteCommand PrepareInsert(InheritanceType type, string idName)
        {
            String tableName = EscapeTableName(type.PackagePrefixedName);
            StringBuilder columnNames = new StringBuilder();
            StringBuilder parameterNames = new StringBuilder();
            AddInsertParameter(columnNames, parameterNames, idName);
            foreach(AttributeType attributeType in type.AttributeTypes)
            {
                AddInsertParameter(columnNames, parameterNames, UniquifyName(attributeType.Name));
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

        private SQLiteCommand PrepareRetype(String tableName, String idName)
        {
            StringBuilder command = new StringBuilder();
            command.Append("UPDATE ");
            command.Append(tableName);
            command.Append(" SET ");
            command.Append("typeId");
            command.Append(" = ");
            command.Append("@" + "typeId");
            command.Append(" WHERE ");
            command.Append(idName);
            command.Append(" == ");
            command.Append("@" + idName);

            return new SQLiteCommand(command.ToString(), connection);
        }

        private SQLiteCommand PrepareUpdate(InheritanceType type, string idName, AttributeType attributeType)
        {
            String tableName = EscapeTableName(type.PackagePrefixedName);
            StringBuilder command = new StringBuilder();
            command.Append("UPDATE ");
            command.Append(tableName);
            command.Append(" SET ");
            command.Append(UniquifyName(attributeType.Name));
            command.Append(" = ");
            command.Append("@" + UniquifyName(attributeType.Name));
            command.Append(" WHERE ");
            command.Append(idName);
            command.Append(" == ");
            command.Append("@" + idName);

            return new SQLiteCommand(command.ToString(), connection);
        }

        private SQLiteCommand PrepareTopologyDelete(String tableName, String idName)
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

        private SQLiteCommand PrepareDelete(InheritanceType type, String idName)
        {
            String tableName = EscapeTableName(type.PackagePrefixedName);
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

        private string EscapeTableName(string name)
        {
            return UniquifyName(name.Replace(':', '_')); // TODO: could lead to issues in case user uses packages (separated by :: from the type), as well as type names containing _
        }

        // attribute name defined by user could collide with fixed column names used to model the graph,
        // type name defined by user could collide with fixed table names used to model the graph
        private string UniquifyName(string columnName)
        {
            return columnName + "_prvt_name_collisn_sufx";
        }

        private void AddInsertParameter(StringBuilder columnNames, StringBuilder parameterNames, String name)
        {
            if(columnNames.Length > 0)
                columnNames.Append(", ");
            columnNames.Append(name);
            if(parameterNames.Length > 0)
                parameterNames.Append(", ");
            parameterNames.Append("@");
            parameterNames.Append(name);
        }

        private void AddQueryColumn(StringBuilder columnNames, String name)
        {
            if(columnNames.Length > 0)
                columnNames.Append(", ");
            columnNames.Append(name);
        }

        private void RegisterPersistenceHandlers()
        {
            graph.OnNodeAdded += NodeAdded;
            graph.OnEdgeAdded += EdgeAdded;
            graph.OnObjectCreated += ObjectCreated;

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
            graph.OnObjectCreated -= ObjectCreated;

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

        #region Listen to graph changes in order to persist them

        public void NodeAdded(INode node)
        {
            SQLiteCommand addNodeTopologyCommand = createNodeCommand;
            addNodeTopologyCommand.Parameters.Clear();
            addNodeTopologyCommand.Parameters.AddWithValue("@typeId", TypeNameToDbId[node.Type.PackagePrefixedName]);
            addNodeTopologyCommand.Parameters.AddWithValue("@graphId", HOST_GRAPH_ID_TEMP_HACK);
            addNodeTopologyCommand.Parameters.AddWithValue("@name", graph.GetElementName(node));
            int rowsAffected = addNodeTopologyCommand.ExecuteNonQuery();
            long rowId = connection.LastInsertRowId;
            AddNodeWithDbIdToDbIdMapping(node, rowId);

            SQLiteCommand addNodeCommand = createNodeCommands[node.Type.TypeID];
            addNodeCommand.Parameters.Clear();
            addNodeCommand.Parameters.AddWithValue("@nodeId", rowId);
            foreach(AttributeType attrType in node.Type.AttributeTypes)
            {
                if(!IsScalarType(attrType))
                    continue;

                object value = node.GetAttribute(attrType.Name);
                addNodeCommand.Parameters.AddWithValue("@" + UniquifyName(attrType.Name), value);
            }
            rowsAffected = addNodeCommand.ExecuteNonQuery();
        }

        public void EdgeAdded(IEdge edge)
        {
            SQLiteCommand addEdgeTopologyCommand = createEdgeCommand;
            addEdgeTopologyCommand.Parameters.Clear();
            addEdgeTopologyCommand.Parameters.AddWithValue("@typeId", TypeNameToDbId[edge.Type.PackagePrefixedName]);
            addEdgeTopologyCommand.Parameters.AddWithValue("@sourceNodeId", NodeToDbId[edge.Source]);
            addEdgeTopologyCommand.Parameters.AddWithValue("@targetNodeId", NodeToDbId[edge.Target]);
            addEdgeTopologyCommand.Parameters.AddWithValue("@graphId", HOST_GRAPH_ID_TEMP_HACK);
            addEdgeTopologyCommand.Parameters.AddWithValue("@name", graph.GetElementName(edge));
            int rowsAffected = addEdgeTopologyCommand.ExecuteNonQuery();
            long rowId = connection.LastInsertRowId;
            AddEdgeWithDbIdToDbIdMapping(edge, rowId);

            SQLiteCommand addEdgeCommand = createEdgeCommands[edge.Type.TypeID];
            addEdgeCommand.Parameters.Clear();
            addEdgeCommand.Parameters.AddWithValue("@edgeId", rowId);
            foreach(AttributeType attrType in edge.Type.AttributeTypes)
            {
                if(!IsScalarType(attrType))
                    continue;

                object value = edge.GetAttribute(attrType.Name);
                addEdgeCommand.Parameters.AddWithValue("@" + UniquifyName(attrType.Name), value);
            }
            rowsAffected = addEdgeCommand.ExecuteNonQuery();
        }

        public void ObjectCreated(IObject value)
        {
        }

        public void RemovingNode(INode node)
        {
            SQLiteCommand deleteNodeTopologyCommand = this.deleteNodeCommand;
            deleteNodeTopologyCommand.Parameters.Clear();
            deleteNodeTopologyCommand.Parameters.AddWithValue("@nodeId", NodeToDbId[node]);
            int rowsAffected = deleteNodeTopologyCommand.ExecuteNonQuery();

            SQLiteCommand deleteNodeCommand = deleteNodeCommands[node.Type.TypeID];
            deleteNodeCommand.Parameters.Clear();
            deleteNodeCommand.Parameters.AddWithValue("@nodeId", NodeToDbId[node]);
            rowsAffected = deleteNodeCommand.ExecuteNonQuery();

            RemoveNodeFromDbIdMapping(node);
        }

        public void RemovingEdge(IEdge edge)
        {
            SQLiteCommand deleteEdgeTopologyCommand = this.deleteEdgeCommand;
            deleteEdgeTopologyCommand.Parameters.Clear();
            deleteEdgeTopologyCommand.Parameters.AddWithValue("@edgeId", EdgeToDbId[edge]);
            int rowsAffected = deleteEdgeTopologyCommand.ExecuteNonQuery();

            SQLiteCommand deleteEdgeCommand = deleteEdgeCommands[edge.Type.TypeID];
            deleteEdgeCommand.Parameters.Clear();
            deleteEdgeCommand.Parameters.AddWithValue("@edgeId", EdgeToDbId[edge]);
            rowsAffected = deleteEdgeCommand.ExecuteNonQuery();

            RemoveEdgeFromDbIdMapping(edge);
        }

        public void ClearingGraph(IGraph graph)
        {
            // TODO: implement optimized batch version
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
            if(!IsScalarType(attrType))
                return; // TODO: also handle these

            SQLiteCommand updateNodeCommand = updateNodeCommands[node.Type.TypeID][attrType.Name];
            updateNodeCommand.Parameters.Clear();
            updateNodeCommand.Parameters.AddWithValue("@nodeId", NodeToDbId[node]);
            updateNodeCommand.Parameters.AddWithValue("@" + UniquifyName(attrType.Name), newValue);

            int rowsAffected = updateNodeCommand.ExecuteNonQuery();
        }

        public void ChangingEdgeAttribute(IEdge edge, AttributeType attrType,
                AttributeChangeType changeType, object newValue, object keyValue)
        {
            if(!IsScalarType(attrType))
                return; // TODO: also handle these

            SQLiteCommand updateEdgeCommand = updateEdgeCommands[edge.Type.TypeID][attrType.Name];
            updateEdgeCommand.Parameters.Clear();
            updateEdgeCommand.Parameters.AddWithValue("@edgeId", EdgeToDbId[edge]);
            updateEdgeCommand.Parameters.AddWithValue("@" + UniquifyName(attrType.Name), newValue);

            int rowsAffected = updateEdgeCommand.ExecuteNonQuery();
        }

        public void ChangingObjectAttribute(IObject obj, AttributeType attrType,
                AttributeChangeType changeType, object newValue, object keyValue)
        {
        }

        public void ChangedNodeAttribute(INode node, AttributeType attrType)
        {
        }

        public void ChangedEdgeAttribute(IEdge edge, AttributeType attrType)
        {
        }

        public void RetypingNode(INode oldNode, INode newNode)
        {
            long dbid = NodeToDbId[oldNode];
            
            // only change type id
            SQLiteCommand retypeNodeTopologyCommand = retypeNodeCommand;
            retypeNodeTopologyCommand.Parameters.Clear();
            retypeNodeTopologyCommand.Parameters.AddWithValue("@typeId", TypeNameToDbId[newNode.Type.PackagePrefixedName]);
            retypeNodeTopologyCommand.Parameters.AddWithValue("@nodeId", dbid);
            int rowsAffected = retypeNodeTopologyCommand.ExecuteNonQuery();

            // delete old object from old table (must occur before as retype to own type possible)
            SQLiteCommand deleteNodeCommand = deleteNodeCommands[oldNode.Type.TypeID];
            deleteNodeCommand.Parameters.Clear();
            deleteNodeCommand.Parameters.AddWithValue("@nodeId", dbid);
            rowsAffected = deleteNodeCommand.ExecuteNonQuery();

            // insert new object -- same id, normally different table, new attributes
            SQLiteCommand addNodeCommand = createNodeCommands[newNode.Type.TypeID];
            addNodeCommand.Parameters.Clear();
            addNodeCommand.Parameters.AddWithValue("@nodeId", dbid);
            foreach(AttributeType attrType in newNode.Type.AttributeTypes)
            {
                if(!IsScalarType(attrType))
                    continue;

                object value = newNode.GetAttribute(attrType.Name);
                addNodeCommand.Parameters.AddWithValue("@" + UniquifyName(attrType.Name), value);
            }
            rowsAffected = addNodeCommand.ExecuteNonQuery();

            ReplaceNodeInDbIdMapping(oldNode, newNode, dbid);
        }

        public void RetypingEdge(IEdge oldEdge, IEdge newEdge)
        {
            long dbid = EdgeToDbId[oldEdge];

            // only change type id
            SQLiteCommand retypeEdgeTopologyCommand = retypeEdgeCommand;
            retypeEdgeTopologyCommand.Parameters.Clear();
            retypeEdgeTopologyCommand.Parameters.AddWithValue("@typeId", TypeNameToDbId[newEdge.Type.PackagePrefixedName]);
            retypeEdgeTopologyCommand.Parameters.AddWithValue("@edgeId", dbid);
            int rowsAffected = retypeEdgeTopologyCommand.ExecuteNonQuery();

            // delete old object from old table (must occur before as retype to own type possible)
            SQLiteCommand deleteEdgeCommand = deleteEdgeCommands[oldEdge.Type.TypeID];
            deleteEdgeCommand.Parameters.Clear();
            deleteEdgeCommand.Parameters.AddWithValue("@edgeId", dbid);
            rowsAffected = deleteEdgeCommand.ExecuteNonQuery();

            // insert new object -- same id, normally different table, new attributes
            SQLiteCommand addEdgeCommand = createEdgeCommands[newEdge.Type.TypeID];
            addEdgeCommand.Parameters.Clear();
            addEdgeCommand.Parameters.AddWithValue("@edgeId", dbid);
            foreach(AttributeType attrType in newEdge.Type.AttributeTypes)
            {
                if(!IsScalarType(attrType))
                    continue;

                object value = newEdge.GetAttribute(attrType.Name);
                addEdgeCommand.Parameters.AddWithValue("@" + UniquifyName(attrType.Name), value);
            }
            rowsAffected = addEdgeCommand.ExecuteNonQuery();

            ReplaceEdgeInDbIdMapping(oldEdge, newEdge, dbid);
        }

        public void RedirectingEdge(IEdge edge)
        {
        }

        #endregion Listen to graph changes in order to persist them


        #region Listen to graph processing changes with influence on persisting graph changes (from the graph processing environments)

        public void SwitchToSubgraphHandler(IGraph graph)
        {
        }

        public void ReturnFromSubgraphHandler(IGraph graph)
        {
        }

        public void BeginExecutionHandler(IPatternMatchingConstruct patternMatchingConstruct)
        {
        }

        public void EndExecutionHandler(IPatternMatchingConstruct patternMatchingConstruct, object result)
        {
        }

        #endregion Listen to graph processing changes with influence on persisting graph changes (from the graph processing environments)

        public void Close()
        {
            connection.Close();
        }

        #region Database id from/to concept mapping maintenance

        private void AddNodeWithDbIdToDbIdMapping(INode node, long dbid)
        {
            DbIdToNode.Add(dbid, node);
            NodeToDbId.Add(node, dbid);
        }

        private void RemoveNodeFromDbIdMapping(INode node)
        {
            DbIdToNode.Remove(NodeToDbId[node]);
            NodeToDbId.Remove(node);
        }

        private void ReplaceNodeInDbIdMapping(INode oldNode, INode newNode, long dbid)
        {
            Debug.Assert(NodeToDbId[oldNode] == dbid);
            DbIdToNode.Remove(dbid);
            NodeToDbId.Remove(oldNode);
            DbIdToNode.Add(dbid, newNode);
            NodeToDbId.Add(newNode, dbid);
        }

        private void AddEdgeWithDbIdToDbIdMapping(IEdge edge, long dbid)
        {
            DbIdToEdge.Add(dbid, edge);
            EdgeToDbId.Add(edge, dbid);
        }

        private void RemoveEdgeFromDbIdMapping(IEdge edge)
        {
            DbIdToEdge.Remove(EdgeToDbId[edge]);
            EdgeToDbId.Remove(edge);
        }

        private void ReplaceEdgeInDbIdMapping(IEdge oldEdge, IEdge newEdge, long dbid)
        {
            Debug.Assert(EdgeToDbId[oldEdge] == dbid);
            DbIdToEdge.Remove(dbid);
            EdgeToDbId.Remove(oldEdge);
            DbIdToEdge.Add(dbid, newEdge);
            EdgeToDbId.Add(newEdge, dbid);
        }

        private void AddTypeNameWithDbIdToDbIdMapping(String typeName, long dbid)
        {
            DbIdToTypeName.Add(dbid, typeName);
            TypeNameToDbId.Add(typeName, dbid);
        }

        #endregion Database id from/to concept mapping maintenance
    }
}
