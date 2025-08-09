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
    /// (Design idea: everything reachable from the host graph is stored in the database, in contrast to all graphs/nodes/edges/objects created at runtime, with the elements visible due to graph processing events lying in between)
    /// </summary>
    public class PersistenceProviderSQLite : IPersistenceProvider
    {
        private class GraphClassDummy : InheritanceType
        {
            private static int DUMMY_GRAPH_TYPE_ID = 0;
            public GraphClassDummy() : base(DUMMY_GRAPH_TYPE_ID)
            {
            }

            public override bool IsAbstract
            {
                get { return false; }
            }

            public override bool IsConst
            {
                get { return false; }
            }

            public override int NumAttributes
            {
                get { return 0; }
            }

            public override IEnumerable<AttributeType> AttributeTypes
            {
                get { yield break; }
            }

            public override int NumFunctionMethods
            {
                get { return 0; }
            }

            public override IEnumerable<IFunctionDefinition> FunctionMethods
            {
                get { yield break; }
            }

            public override int NumProcedureMethods
            {
                get { return 0; }
            }

            public override IEnumerable<IProcedureDefinition> ProcedureMethods
            {
                get { yield break; }
            }

            public override string Name
            {
                get { return "graph"; }
            }

            public override string Package
            {
                get { return null; }
            }

            public override string PackagePrefixedName
            {
                get { return Package != null ? Package + "::" + Name : Name; }
            }

            public override AttributeType GetAttributeType(string name)
            {
                return null;
            }

            public override IFunctionDefinition GetFunctionMethod(string name)
            {
                return null;
            }

            public override IProcedureDefinition GetProcedureMethod(string name)
            {
                return null;
            }

            public override bool IsA(GrGenType other)
            {
                if(other == this)
                    return true;
                else
                    return false;
            }
        }

        enum TypeKind { NodeClass = 0, EdgeClass = 1, GraphClass = 2, ObjectClass = 3 };

        SQLiteConnection connection;

        // prepared statements for handling nodes
        SQLiteCommand createNodeCommand; // topology
        SQLiteCommand[] createNodeCommands; // per-type
        SQLiteCommand[] readNodeCommands; // per-type joined with topology
        SQLiteCommand retypeNodeCommand; // topology
        Dictionary<String, SQLiteCommand>[] updateNodeCommands; // per-type, per-attribute
        SQLiteCommand deleteNodeCommand; // topology
        SQLiteCommand[] deleteNodeCommands; // per-type

        // prepared statements for handling edges
        SQLiteCommand createEdgeCommand; // topology
        SQLiteCommand[] createEdgeCommands; // per-type
        SQLiteCommand[] readEdgeCommands; // per-type joined with topology
        SQLiteCommand retypeEdgeCommand; // topology
        Dictionary<String, SQLiteCommand>[] updateEdgeCommands; // per-type, per-attribute
        SQLiteCommand deleteEdgeCommand; // topology
        SQLiteCommand[] deleteEdgeCommands; // per-type

        // prepared statements for handling graphs
        SQLiteCommand createGraphCommand;
        SQLiteCommand readGraphsCommand;
        long HOST_GRAPH_ID = 0;

        // prepared statements for handling objects
        SQLiteCommand createObjectCommand; // topology
        SQLiteCommand[] createObjectCommands; // per-type
        SQLiteCommand[] readObjectCommands; // per-type joined with topology
        Dictionary<String, SQLiteCommand>[] updateObjectCommands; // per-type, per-attribute

        Stack<INamedGraph> graphs; // the stack of graphs getting processed, the first entry being the host graph
        INamedGraph host; // I'd prefer to use a property accessing the stack, but this would require to create an array...
        INamedGraph graph { get { return graphs.Peek(); } } // the current graph

        IGraphProcessingEnvironment procEnv; // the graph processing environment to switch the current graph in case of to-subgraph switches (todo: and for db-transaction handling)

        // database id to concept mappings, and vice versa
        Dictionary<long, INode> DbIdToNode; // the ids in node/edge mappings are globally unique due to the topology tables, the per-type tables only reference them
        Dictionary<INode, long> NodeToDbId;
        Dictionary<long, IEdge> DbIdToEdge;
        Dictionary<IEdge, long> EdgeToDbId;
        Dictionary<long, INamedGraph> DbIdToGraph;
        Dictionary<INamedGraph, long> GraphToDbId;
        Dictionary<long, IObject> DbIdToObject;
        Dictionary<IObject, long> ObjectToDbId;
        Dictionary<long, string> DbIdToTypeName;
        Dictionary<string, long> TypeNameToDbId;


        public PersistenceProviderSQLite()
        {
        }

        public void Open(string connectionParameters)
        {
            connection = new SQLiteConnection(connectionParameters);
            connection.Open();
            ConsoleUI.outWriter.WriteLine("persistence provider \"libGrPersistenceProviderSQLite.dll\" connected to database with \"{0}\".", connectionParameters);
        }

        public void ReadPersistentGraphAndRegisterToListenToGraphModifications(INamedGraph hostGraph)
        {
            if(hostGraph.NumNodes != 0 || hostGraph.NumEdges != 0)
                throw new Exception("The graph must be empty!");

            graphs = new Stack<INamedGraph>();
            graphs.Push(hostGraph);
            host = hostGraph;

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
            DbIdToGraph = new Dictionary<long, INamedGraph>();
            GraphToDbId = new Dictionary<INamedGraph, long>();
            DbIdToObject = new Dictionary<long, IObject>();
            ObjectToDbId = new Dictionary<IObject, long>();
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
            CreateObjectsTable();
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

            foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
            {
                CreateInheritanceTypeTable(objectType, "objectId");
            }
        }

        private void CreateNodesTable()
        {
            CreateTable("nodes", "nodeId",
                "typeId", "INTEGER NOT NULL", // type defines table where attributes are stored
                "graphId", "INTEGER NOT NULL",
                "name", "TEXT NOT NULL" // maybe TODO: name in extra table - this is for a named graph, but should be also available for unnamed graphs in the end...
                );
            // AddIndex("nodes", "graphId"); maybe add later again for quick graph purging 
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
            // AddIndex("edges", "graphId"); maybe add later again for quick graph purging 
        }

        private void CreateGraphsTable()
        {
            // only id and name for now, plus a by-now constant typeId, so a later extension by graph attributes and then by graph types is easy (multiple graph tables)
            CreateTable("graphs", "graphId",
                "typeId", "INTEGER NOT NULL",
                "name", "TEXT NOT NULL"
                );
        }

        private void CreateObjectsTable()
        {
            CreateTable("objects", "objectId",
                "typeId", "INTEGER NOT NULL",
                "name", "TEXT NOT NULL"
                );
        }

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
                if(!IsScalarType(attributeType) && !IsGraphType(attributeType) && !IsObjectType(attributeType)) // TODO: container type, external/object type.
                    continue;

                columnNamesAndTypes.Add(UniquifyName(attributeType.Name));
                columnNamesAndTypes.Add(AttributeTypeToSQLiteType(attributeType));
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

        private bool IsGraphType(AttributeType attributeType)
        {
            return attributeType.Kind == AttributeKind.GraphAttr;
        }

        private bool IsObjectType(AttributeType attributeType)
        {
            return attributeType.Kind == AttributeKind.InternalClassObjectAttr;
        }

        private string AttributeTypeToSQLiteType(AttributeType attributeType) // TODO: Basic/ScalarSQLiteType()? separate by attribute type?
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
                case AttributeKind.GraphAttr: return "INTEGER"; // non-scalar type, but reference type with simple mapping (container types have a complex mapping)
                case AttributeKind.InternalClassObjectAttr: return "INTEGER"; // non-scalar type, but reference type with simple mapping (container types have a complex mapping)
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

        private void AddIndex(String tableName, String indexColumnName)
        {
            StringBuilder command = new StringBuilder();
            command.Append("CREATE INDEX IF NOT EXISTS ");
            command.Append("idx_" + indexColumnName);
            command.Append(" ON ");
            command.Append(tableName);
            command.Append("(");
            command.Append(indexColumnName);
            command.Append(")");
            SQLiteCommand createIndexCommand = new SQLiteCommand(command.ToString(), connection);

            int rowsAffected = createIndexCommand.ExecuteNonQuery();

            createIndexCommand.Dispose();
        }

        #region Types table populating

        private void AddUnknownModelTypesToTypesTable()
        {
            // the grgen type id from the model is only unique per kind, and not stable upon insert(/delete), so we have to match by name to map to the database type id
            Dictionary<string, TypeKind> typeNameToKind = ReadKnownTypes();

            using(SQLiteCommand fillTypeCommand = GetFillTypeCommand())
            {
                FillUnknownType(typeNameToKind, fillTypeCommand, new GraphClassDummy(), TypeKind.GraphClass);

                foreach(NodeType nodeType in graph.Model.NodeModel.Types)
                {
                    FillUnknownType(typeNameToKind, fillTypeCommand, nodeType, TypeKind.NodeClass);
                }
                foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
                {
                    FillUnknownType(typeNameToKind, fillTypeCommand, edgeType, TypeKind.EdgeClass);
                }
                foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
                {
                    FillUnknownType(typeNameToKind, fillTypeCommand, objectType, TypeKind.ObjectClass);
                }
            }

            InsertGraphIfMissing(HOST_GRAPH_ID, TypeNameToDbId["graph"]);
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

        private void InsertGraphIfMissing(long graphId, long typeId)
        {
            using(SQLiteCommand replaceHostGraphCommand = PrepareHostGraphInsert())
            {
                replaceHostGraphCommand.Parameters.Clear();
                replaceHostGraphCommand.Parameters.AddWithValue("@graphId", graphId);
                replaceHostGraphCommand.Parameters.AddWithValue("@typeId", typeId);
                replaceHostGraphCommand.Parameters.AddWithValue("@name", graph.Name);

                int rowsAffected = replaceHostGraphCommand.ExecuteNonQuery();

                long rowId = connection.LastInsertRowId;
                Debug.Assert(rowId == graphId);
                AddGraphWithDbIdToDbIdMapping(graph, rowId);
            }
        }

        #endregion Types table populating

        #region Initial graph reading

        private void ReadCompleteGraph()
        {
            // prepare statements for initial graph fetching
            readGraphsCommand = PrepareStatementForReadingGraphs();

            readNodeCommands = new SQLiteCommand[graph.Model.NodeModel.Types.Length];
            readEdgeCommands = new SQLiteCommand[graph.Model.EdgeModel.Types.Length];

            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                readNodeCommands[nodeType.TypeID] = PrepareStatementsForReadingNodes(nodeType);
            }
            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                readEdgeCommands[edgeType.TypeID] = PrepareStatementsForReadingEdges(edgeType);
            }

            readObjectCommands = new SQLiteCommand[graph.Model.ObjectModel.Types.Length];

            foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
            {
                readObjectCommands[objectType.TypeID] = PrepareStatementsForReadingObjects(objectType);
            }

            // pass 0 - load all graphs (without elements and without the host graph, which was already processed/inserted before) (an alternative would be to load only the host graph, and the others on-demand lazily, creating proxy objects for them - but this would be more coding effort, defy purging, and when carrying out eager loading, it's better not to load graph-by-graph but all at once; later todo when graph types are introduced: load by type)
            ReadGraphsWithoutHostGraph();

            // pass 1 - load all elements (first nodes then edges, dispatching them to their containing graph)
            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                ReadNodes(nodeType, readNodeCommands[nodeType.TypeID]);
            }
            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                ReadEdges(edgeType, readEdgeCommands[edgeType.TypeID]);
            }

            // pass 2 - load all objects
            foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
            {
                ReadObjects(objectType, readObjectCommands[objectType.TypeID]);
            }

            // pass I - wire references/patch the references into the attributes (TODO - split this into helper methods like PatchReferencesFromDatabase)
            // 2nd full table scan, type table by type table, patching (TODO: node/edge) object references (graphs were handled before) in the nodes/edges/objects (in-memory), accessed by their database-id to type mapping
            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                PatchNodes(nodeType, readNodeCommands[nodeType.TypeID]);
            }
            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                PatchEdges(edgeType, readEdgeCommands[edgeType.TypeID]);
            }
            foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
            {
                PatchObjects(objectType, readObjectCommands[objectType.TypeID]);
            }

            // references to nodes/edges from different graphs are not supported as of now in import/export, because persistent names only hold in the current graph, keep this in db persistence handling
            // but the user could assign references to elements from another graph to an attribute, so todo: maybe relax that constraint, requiring a model supporting graphof, and a global post-patching step after everything was read locally, until a check that the user doesn't assign references to elements from another graph is implemented

            // cleaning pass (TODO) -- the ones from database that are not reachable in-memory from the host graph on
            PurgeUnreachableGraphs(); // after all references to the graphs are known, we can purge the ones not in use; node/edge references are not a prerequisite for this (it only must be ensured all of them were instantiated before), but containers which may also contain graph references
            PurgeUnreachableObjects(); // after all references to the objects are known, we can purge the ones not in use (similar to a full garbage collection run in .NET)
        }

        private SQLiteCommand PrepareStatementForReadingGraphs()
        {
            String topologyTableName = "graphs";
            StringBuilder columnNames = new StringBuilder();
            AddQueryColumn(columnNames, "graphId");
            AddQueryColumn(columnNames, "typeId");
            AddQueryColumn(columnNames, "name");

            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append(topologyTableName);
            return new SQLiteCommand(command.ToString(), connection);
        }

        private void ReadGraphsWithoutHostGraph()
        {
            using(SQLiteDataReader reader = readGraphsCommand.ExecuteReader())
            {
                Dictionary<string, int> attributeNameToColumnIndex = GetNameToColumnIndexMapping(reader);

                while(reader.Read())
                {
                    long graphId = reader.GetInt64(attributeNameToColumnIndex["graphId"]);
                    if(graphId == HOST_GRAPH_ID)
                        continue; // skip host graph - the host graph is contained in the graphs table with id HOST_GRAPH_ID and corresponds to the bottom element of the graphs stack, which coincides with graph at this point, being the top element of the graphs stack 
                    long typeId = reader.GetInt64(attributeNameToColumnIndex["typeId"]);
                    String name = reader.GetString(attributeNameToColumnIndex["name"]);
                    INamedGraph graph = (INamedGraph)this.graph.CreateEmptyEquivalent(name); // somewhen later: create based on typeId
                    AddGraphWithDbIdToDbIdMapping(graph, graphId);
                }
            }
        }

        private SQLiteCommand PrepareStatementsForReadingNodes(NodeType nodeType)
        {
            // later TODO: handling of zombies in tables (out-of-graph nodes, depending on semantic model, in current semantic model not possible)
            String topologyTableName = "nodes";
            String tableName = EscapeTableName(nodeType.PackagePrefixedName);
            StringBuilder columnNames = new StringBuilder();
            AddQueryColumn(columnNames, "nodeId");
            AddQueryColumn(columnNames, "typeId");
            AddQueryColumn(columnNames, "graphId");
            AddQueryColumn(columnNames, "name");
            foreach(AttributeType attributeType in nodeType.AttributeTypes)
            {
                if(!IsScalarType(attributeType) && !IsGraphType(attributeType) && !IsObjectType(attributeType))
                    continue;
                AddQueryColumn(columnNames, UniquifyName(attributeType.Name));
            }
            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append(topologyTableName);
            command.Append(" NATURAL JOIN "); // on nodeId
            command.Append(tableName);
            return new SQLiteCommand(command.ToString(), connection);
        }

        private SQLiteCommand PrepareStatementsForReadingEdges(EdgeType edgeType)
        {
            // later TODO: handling of zombies in tables (out-of-graph edges, depending on semantic model, in current semantic model not possible)
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
                if(!IsScalarType(attributeType) && !IsGraphType(attributeType) && !IsObjectType(attributeType))
                    continue;
                AddQueryColumn(columnNames, UniquifyName(attributeType.Name));
            }
            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append(topologyTableName);
            command.Append(" NATURAL JOIN "); // on edgeId
            command.Append(tableName);
            return new SQLiteCommand(command.ToString(), connection);
        }

        private SQLiteCommand PrepareStatementsForReadingObjects(ObjectType objectType)
        {
            String topologyTableName = "objects";
            String tableName = EscapeTableName(objectType.PackagePrefixedName);
            StringBuilder columnNames = new StringBuilder();
            AddQueryColumn(columnNames, "objectId");
            AddQueryColumn(columnNames, "typeId");
            AddQueryColumn(columnNames, "name");
            foreach(AttributeType attributeType in objectType.AttributeTypes)
            {
                if(!IsScalarType(attributeType) && !IsGraphType(attributeType) && !IsObjectType(attributeType))
                    continue;
                AddQueryColumn(columnNames, UniquifyName(attributeType.Name));
            }
            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append(topologyTableName);
            command.Append(" NATURAL JOIN "); // on objectId
            command.Append(tableName);
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
                        object attributeValue;
                        if(IsScalarType(attributeType))
                            attributeValue = GetScalarValue(attributeType, reader, attributeNameToColumnIndex);
                        else if(IsGraphType(attributeType))
                            attributeValue = GetGraphValue(attributeType, reader, attributeNameToColumnIndex);
                        else
                            continue;
                        node.SetAttribute(attributeType.Name, attributeValue);
                    }

                    String name = reader.GetString(attributeNameToColumnIndex["name"]);
                    long graphId = reader.GetInt64(attributeNameToColumnIndex["graphId"]);
                    INamedGraph graph = DbIdToGraph[graphId];
                    graph.AddNode(node, name);
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
                        object attributeValue;
                        if(IsScalarType(attributeType))
                            attributeValue = GetScalarValue(attributeType, reader, attributeNameToColumnIndex);
                        else if(IsGraphType(attributeType))
                            attributeValue = GetGraphValue(attributeType, reader, attributeNameToColumnIndex);
                        else
                            continue;
                        edge.SetAttribute(attributeType.Name, attributeValue);
                    }

                    long sourceNodeId = reader.GetInt64(attributeNameToColumnIndex["sourceNodeId"]);
                    INode source = DbIdToNode[sourceNodeId];
                    long targetNodeId = reader.GetInt64(attributeNameToColumnIndex["targetNodeId"]);
                    INode target = DbIdToNode[targetNodeId];
                    edgeType.SetSourceAndTarget(edge, source, target);
                    String name = reader.GetString(attributeNameToColumnIndex["name"]);
                    long graphId = reader.GetInt64(attributeNameToColumnIndex["graphId"]);
                    INamedGraph graph = DbIdToGraph[graphId];
                    graph.AddEdge(edge, name);
                    long edgeId = reader.GetInt64(attributeNameToColumnIndex["edgeId"]);
                    AddEdgeWithDbIdToDbIdMapping(edge, edgeId);
                }
            }
        }

        private void ReadObjects(ObjectType objectType, SQLiteCommand readObjectCommand)
        {
            using(SQLiteDataReader reader = readObjectCommand.ExecuteReader())
            {
                Dictionary<string, int> attributeNameToColumnIndex = GetNameToColumnIndexMapping(reader);

                while(reader.Read())
                {
                    String name = reader.GetString(attributeNameToColumnIndex["name"]);
                    IObject classObject = objectType.CreateObject(host, graph.Model.ObjectUniquenessIsEnsured ? name : null);

                    foreach(AttributeType attributeType in objectType.AttributeTypes)
                    {
                        object attributeValue;
                        if(IsScalarType(attributeType))
                            attributeValue = GetScalarValue(attributeType, reader, attributeNameToColumnIndex);
                        else if(IsGraphType(attributeType))
                            attributeValue = GetGraphValue(attributeType, reader, attributeNameToColumnIndex);
                        else
                            continue;
                        classObject.SetAttribute(attributeType.Name, attributeValue);
                    }

                    long objectId = reader.GetInt64(attributeNameToColumnIndex["objectId"]);
                    AddObjectWithDbIdToDbIdMapping(classObject, objectId);
                }
            }
        }

        private void PatchNodes(NodeType nodeType, SQLiteCommand readNodeCommand)
        {
            using(SQLiteDataReader reader = readNodeCommand.ExecuteReader())
            {
                Dictionary<string, int> attributeNameToColumnIndex = GetNameToColumnIndexMapping(reader);

                while(reader.Read())
                {
                    long nodeId = reader.GetInt64(attributeNameToColumnIndex["nodeId"]);
                    INode node = DbIdToNode[nodeId];

                    foreach(AttributeType attributeType in nodeType.AttributeTypes)
                    {
                        object attributeValue;
                        if(IsObjectType(attributeType))
                            attributeValue = GetObjectValue(attributeType, reader, attributeNameToColumnIndex);
                        else
                            continue;
                        node.SetAttribute(attributeType.Name, attributeValue);
                    }
                }
            }
        }

        private void PatchEdges(EdgeType edgeType, SQLiteCommand readEdgeCommand)
        {
            using(SQLiteDataReader reader = readEdgeCommand.ExecuteReader())
            {
                Dictionary<string, int> attributeNameToColumnIndex = GetNameToColumnIndexMapping(reader);

                while(reader.Read())
                {
                    long edgeId = reader.GetInt64(attributeNameToColumnIndex["edgeId"]);
                    IEdge edge = DbIdToEdge[edgeId];

                    foreach(AttributeType attributeType in edgeType.AttributeTypes)
                    {
                        object attributeValue;
                        if(IsObjectType(attributeType))
                            attributeValue = GetObjectValue(attributeType, reader, attributeNameToColumnIndex);
                        else
                            continue;
                        edge.SetAttribute(attributeType.Name, attributeValue);
                    }
                }
            }
        }

        private void PatchObjects(ObjectType objectType, SQLiteCommand readObjectCommand)
        {
            using(SQLiteDataReader reader = readObjectCommand.ExecuteReader())
            {
                Dictionary<string, int> attributeNameToColumnIndex = GetNameToColumnIndexMapping(reader);

                while(reader.Read())
                {
                    long objectId = reader.GetInt64(attributeNameToColumnIndex["objectId"]);
                    IObject classObject = DbIdToObject[objectId];

                    foreach(AttributeType attributeType in objectType.AttributeTypes)
                    {
                        object attributeValue;
                        if(IsObjectType(attributeType))
                            attributeValue = GetObjectValue(attributeType, reader, attributeNameToColumnIndex);
                        else
                            continue;
                        classObject.SetAttribute(attributeType.Name, attributeValue);
                    }
                }
            }
        }

        private void PurgeUnreachableGraphs()
        {
            // TODO: implement -- collect all graphs reachable from the host graph, purge the ones in the database that are not reachable
        }

        private void PurgeUnreachableObjects()
        {
            // TODO: implement -- collect all objects reachable from the host graph, purge the ones in the database that are not reachable
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

        private IGraph GetGraphValue(AttributeType attributeType, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            int index = attributeNameToColumnIndex[UniquifyName(attributeType.Name)];
            if(reader.IsDBNull(index))
                return null;
            long dbid = reader.GetInt64(index);
            return DbIdToGraph[dbid];
        }

        private IObject GetObjectValue(AttributeType attributeType, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            int index = attributeNameToColumnIndex[UniquifyName(attributeType.Name)];
            if(reader.IsDBNull(index))
                return null;
            long dbid = reader.GetInt64(index);
            return DbIdToObject[dbid];
        }

        #endregion Initial graph reading

        #region Graph modification handling preparations

        // TODO: maybe lazy initialization...
        private void PrepareStatementsForGraphModifications()
        {
            createGraphCommand = PrepareGraphInsert();

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

            createObjectCommand = PrepareObjectInsert();
            createObjectCommands = new SQLiteCommand[graph.Model.ObjectModel.Types.Length];
            foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
            {
                createObjectCommands[objectType.TypeID] = PrepareInsert(objectType, "objectId");
            }

            retypeNodeCommand = PrepareRetype("nodes", "nodeId");
            retypeEdgeCommand = PrepareRetype("edges", "edgeId");

            updateNodeCommands = new Dictionary<String, SQLiteCommand>[graph.Model.NodeModel.Types.Length];
            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                updateNodeCommands[nodeType.TypeID] = new Dictionary<String, SQLiteCommand>(nodeType.NumAttributes);
                foreach(AttributeType attributeType in nodeType.AttributeTypes)
                {
                    if(!IsScalarType(attributeType) && !IsGraphType(attributeType) && !IsObjectType(attributeType))
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
                    if(!IsScalarType(attributeType) && !IsGraphType(attributeType) && !IsObjectType(attributeType))
                        continue;
                    updateEdgeCommands[edgeType.TypeID][attributeType.Name] = PrepareUpdate(edgeType, "edgeId", attributeType);
                }
            }

            updateObjectCommands = new Dictionary<String, SQLiteCommand>[graph.Model.ObjectModel.Types.Length];
            foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
            {
                updateObjectCommands[objectType.TypeID] = new Dictionary<String, SQLiteCommand>(objectType.NumAttributes);
                foreach(AttributeType attributeType in objectType.AttributeTypes)
                {
                    if(!IsScalarType(attributeType) && !IsGraphType(attributeType) && !IsObjectType(attributeType))
                        continue;
                    updateObjectCommands[objectType.TypeID][attributeType.Name] = PrepareUpdate(objectType, "objectId", attributeType);
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

        private SQLiteCommand PrepareHostGraphInsert()
        {
            String tableName = "graphs";
            StringBuilder columnNames = new StringBuilder();
            StringBuilder parameterNames = new StringBuilder();
            AddInsertParameter(columnNames, parameterNames, "graphId");
            AddInsertParameter(columnNames, parameterNames, "typeId");
            AddInsertParameter(columnNames, parameterNames, "name");
            StringBuilder command = new StringBuilder();
            command.Append("REPLACE INTO ");
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
            String tableName = EscapeTableName(type.PackagePrefixedName);
            StringBuilder columnNames = new StringBuilder();
            StringBuilder parameterNames = new StringBuilder();
            AddInsertParameter(columnNames, parameterNames, idName);
            foreach(AttributeType attributeType in type.AttributeTypes)
            {
                if(!IsScalarType(attributeType) && !IsGraphType(attributeType) && !IsObjectType(attributeType))
                    continue;
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

        #region Listen to graph changes in order to persist them

        public void NodeAdded(INode node)
        {
            NodeAdded(node, graph);
        }

        public void NodeAdded(INode node, INamedGraph graph)
        {
            SQLiteCommand addNodeTopologyCommand = createNodeCommand;
            addNodeTopologyCommand.Parameters.Clear();
            addNodeTopologyCommand.Parameters.AddWithValue("@typeId", TypeNameToDbId[node.Type.PackagePrefixedName]);
            addNodeTopologyCommand.Parameters.AddWithValue("@graphId", GraphToDbId[graph]);
            addNodeTopologyCommand.Parameters.AddWithValue("@name", graph.GetElementName(node));
            int rowsAffected = addNodeTopologyCommand.ExecuteNonQuery();
            long rowId = connection.LastInsertRowId;
            AddNodeWithDbIdToDbIdMapping(node, rowId);

            // pre-run adding graphs and objects if needed, otherwise during command builing below the command would be filled and executed (indirectly) another time, wreaking havoc on the parameters of the command
            AddReferencesToDatabase(node);

            SQLiteCommand addNodeCommand = createNodeCommands[node.Type.TypeID];
            addNodeCommand.Parameters.Clear();
            addNodeCommand.Parameters.AddWithValue("@nodeId", rowId);
            foreach(AttributeType attributeType in node.Type.AttributeTypes)
            {
                if(!IsScalarType(attributeType) && !IsGraphType(attributeType) && !IsObjectType(attributeType))
                    continue;
                object value = node.GetAttribute(attributeType.Name);
                addNodeCommand.Parameters.AddWithValue("@" + UniquifyName(attributeType.Name), ValueOrIdOfReferencedElement(value, attributeType));
            }
            rowsAffected = addNodeCommand.ExecuteNonQuery();
        }

        public void EdgeAdded(IEdge edge)
        {
            EdgeAdded(edge, graph);
        }

        public void EdgeAdded(IEdge edge, INamedGraph graph)
        {
            SQLiteCommand addEdgeTopologyCommand = createEdgeCommand;
            addEdgeTopologyCommand.Parameters.Clear();
            addEdgeTopologyCommand.Parameters.AddWithValue("@typeId", TypeNameToDbId[edge.Type.PackagePrefixedName]);
            addEdgeTopologyCommand.Parameters.AddWithValue("@sourceNodeId", NodeToDbId[edge.Source]);
            addEdgeTopologyCommand.Parameters.AddWithValue("@targetNodeId", NodeToDbId[edge.Target]);
            addEdgeTopologyCommand.Parameters.AddWithValue("@graphId", GraphToDbId[graph]);
            addEdgeTopologyCommand.Parameters.AddWithValue("@name", graph.GetElementName(edge));
            int rowsAffected = addEdgeTopologyCommand.ExecuteNonQuery();
            long rowId = connection.LastInsertRowId;
            AddEdgeWithDbIdToDbIdMapping(edge, rowId);

            // pre-run adding graphs and objects if needed, otherwise during command builing below the command would be filled and executed (indirectly) another time, wreaking havoc on the parameters of the command
            AddReferencesToDatabase(edge);

            SQLiteCommand addEdgeCommand = createEdgeCommands[edge.Type.TypeID];
            addEdgeCommand.Parameters.Clear();
            addEdgeCommand.Parameters.AddWithValue("@edgeId", rowId);
            foreach(AttributeType attributeType in edge.Type.AttributeTypes)
            {
                if(!IsScalarType(attributeType) && !IsGraphType(attributeType) && !IsObjectType(attributeType))
                    continue;
                object value = edge.GetAttribute(attributeType.Name);
                addEdgeCommand.Parameters.AddWithValue("@" + UniquifyName(attributeType.Name), ValueOrIdOfReferencedElement(value, attributeType));
            }
            rowsAffected = addEdgeCommand.ExecuteNonQuery();
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
            if(!IsScalarType(attrType) && !IsGraphType(attrType) && !IsObjectType(attrType))
                return; // TODO: also handle these

            if(IsGraphType(attrType))
                AddGraphAsNeeded((INamedGraph)newValue);
            else if(IsObjectType(attrType))
                AddObjectAsNeeded((IObject)newValue);

            SQLiteCommand updateNodeCommand = updateNodeCommands[node.Type.TypeID][attrType.Name];
            updateNodeCommand.Parameters.Clear();
            updateNodeCommand.Parameters.AddWithValue("@nodeId", NodeToDbId[node]);
            updateNodeCommand.Parameters.AddWithValue("@" + UniquifyName(attrType.Name), ValueOrIdOfReferencedElement(newValue, attrType));

            int rowsAffected = updateNodeCommand.ExecuteNonQuery();
        }

        public void ChangingEdgeAttribute(IEdge edge, AttributeType attrType,
                AttributeChangeType changeType, object newValue, object keyValue)
        {
            if(!IsScalarType(attrType) && !IsGraphType(attrType) && !IsObjectType(attrType))
                return; // TODO: also handle these

            if(IsGraphType(attrType))
                AddGraphAsNeeded((INamedGraph)newValue);
            else if(IsObjectType(attrType))
                AddObjectAsNeeded((IObject)newValue);

            SQLiteCommand updateEdgeCommand = updateEdgeCommands[edge.Type.TypeID][attrType.Name];
            updateEdgeCommand.Parameters.Clear();
            updateEdgeCommand.Parameters.AddWithValue("@edgeId", EdgeToDbId[edge]);
            updateEdgeCommand.Parameters.AddWithValue("@" + UniquifyName(attrType.Name), ValueOrIdOfReferencedElement(newValue, attrType));

            int rowsAffected = updateEdgeCommand.ExecuteNonQuery();
        }

        public void ChangingObjectAttribute(IObject obj, AttributeType attrType,
                AttributeChangeType changeType, object newValue, object keyValue)
        {
            if(!ObjectToDbId.ContainsKey(obj))
                return; // object not known to the graph means we receive an update notification for an object that is not reachable (yet) from the graph -- to be ignored, when the object becomes known, the by-then current attribute will be written

            if(!IsScalarType(attrType) && !IsGraphType(attrType) && !IsObjectType(attrType))
                return; // TODO: also handle these

            if(IsGraphType(attrType))
                AddGraphAsNeeded((INamedGraph)newValue);
            else if(IsObjectType(attrType))
                AddObjectAsNeeded((IObject)newValue);

            SQLiteCommand updateObjectCommand = updateObjectCommands[obj.Type.TypeID][attrType.Name];
            updateObjectCommand.Parameters.Clear();
            updateObjectCommand.Parameters.AddWithValue("@objectId", ObjectToDbId[obj]);
            updateObjectCommand.Parameters.AddWithValue("@" + UniquifyName(attrType.Name), ValueOrIdOfReferencedElement(newValue, attrType));

            int rowsAffected = updateObjectCommand.ExecuteNonQuery();
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
            foreach(AttributeType attributeType in newNode.Type.AttributeTypes)
            {
                if(!IsScalarType(attributeType) && !IsGraphType(attributeType) && !IsObjectType(attributeType))
                    continue;
                object value = newNode.GetAttribute(attributeType.Name);
                addNodeCommand.Parameters.AddWithValue("@" + UniquifyName(attributeType.Name), ValueOrIdOfReferencedElement(value, attributeType));
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
            foreach(AttributeType attributeType in newEdge.Type.AttributeTypes)
            {
                if(!IsScalarType(attributeType) && !IsGraphType(attributeType) && !IsObjectType(attributeType))
                    continue;
                object value = newEdge.GetAttribute(attributeType.Name);
                addEdgeCommand.Parameters.AddWithValue("@" + UniquifyName(attributeType.Name), ValueOrIdOfReferencedElement(value, attributeType));
            }
            rowsAffected = addEdgeCommand.ExecuteNonQuery();

            ReplaceEdgeInDbIdMapping(oldEdge, newEdge, dbid);
        }

        public void RedirectingEdge(IEdge edge)
        {
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
            int rowsAffected = addGraphTopologyCommand.ExecuteNonQuery();
            long rowId = connection.LastInsertRowId;
            AddGraphWithDbIdToDbIdMapping(graph, rowId);

            foreach(INode node in graph.Nodes)
            {
                NodeAdded(node, graph);
            }
            foreach(IEdge edge in graph.Edges)
            {
                EdgeAdded(edge, graph);
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

            foreach(AttributeType attributeType in root.Type.AttributeTypes)
            {
                if(IsGraphType(attributeType))
                {
                    object val = root.GetAttribute(attributeType.Name);
                    AddGraphAsNeeded((INamedGraph)val);
                }
                else if(IsObjectType(attributeType))
                {
                    object val = root.GetAttribute(attributeType.Name);
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
            }

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

                foreach(AttributeType attributeType in current.Type.AttributeTypes)
                {
                    if(IsGraphType(attributeType))
                    {
                        object val = current.GetAttribute(attributeType.Name);
                        AddGraphAsNeeded((INamedGraph)val);
                    }
                    else if(IsObjectType(attributeType))
                    {
                        object val = current.GetAttribute(attributeType.Name);
                        IObject obj = (IObject)val;
                        if(obj != null)
                        {
                            if(!ObjectToDbId.ContainsKey(obj))
                                todos.Push(obj);
                        }
                    }
                }

                if(objectWritten)
                    CompleteObjectEntry((IObject)current);
            }
        }

        private void WriteObjectBaseEntry(IObject obj)
        {
            SQLiteCommand addObjectTopologyCommand = createObjectCommand;
            addObjectTopologyCommand.Parameters.Clear();
            addObjectTopologyCommand.Parameters.AddWithValue("@typeId", TypeNameToDbId[obj.Type.PackagePrefixedName]);
            addObjectTopologyCommand.Parameters.AddWithValue("@name", obj.GetObjectName());
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
                if(!IsScalarType(attributeType) && !IsGraphType(attributeType) && !IsObjectType(attributeType))
                    continue;
                object val = obj.GetAttribute(attributeType.Name);
                addObjectCommand.Parameters.AddWithValue("@" + UniquifyName(attributeType.Name), ValueOrIdOfReferencedElement(val, attributeType));
            }
            int rowsAffected = addObjectCommand.ExecuteNonQuery();
        }

        #endregion Listen to graph changes in order to persist them

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

        public void BeginExecutionHandler(IPatternMatchingConstruct patternMatchingConstruct)
        {
        }

        public void EndExecutionHandler(IPatternMatchingConstruct patternMatchingConstruct, object result)
        {
        }

        #endregion Listen to graph processing changes with influence on persisting graph changes (from the graph processing environments)

        // TODO: a deregister would make sense so event handlers can be removed when the action environment is changed (but in this case the persistent graph will be released, so no urgent action needed)
        public void RegisterToListenToProcessingEnvironmentEvents(IGraphProcessingEnvironment procEnv)
        {
            this.procEnv = procEnv; // TODO: db transaction processing; not supported: parallel graph changes; not supported: changes to referenced graphs outside of event control; potentially possible but not wanted: listen to changes to all graphs

            procEnv.OnSwitchingToSubgraph += SwitchToSubgraphHandler;
            procEnv.OnReturnedFromSubgraph += ReturnFromSubgraphHandler;
        }

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

        private void AddGraphWithDbIdToDbIdMapping(INamedGraph graph, long dbid)
        {
            DbIdToGraph.Add(dbid, graph);
            GraphToDbId.Add(graph, dbid);
        }

        private void AddObjectWithDbIdToDbIdMapping(IObject obj, long dbid)
        {
            DbIdToObject.Add(dbid, obj);
            ObjectToDbId.Add(obj, dbid);
        }

        private void AddTypeNameWithDbIdToDbIdMapping(String typeName, long dbid)
        {
            DbIdToTypeName.Add(dbid, typeName);
            TypeNameToDbId.Add(typeName, dbid);
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

            return newValue;
        }

        #endregion Database id from/to concept mapping maintenance
    }
}
