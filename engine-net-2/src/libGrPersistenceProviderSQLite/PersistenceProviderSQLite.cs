/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Security.Cryptography;
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

        enum ContainerCommand { AssignEmptyContainer = 0, PutElement = 1, RemoveElement = 2, AssignElement = 3, AssignNull = 4 } // based on the AttributeChangeType

        SQLiteConnection connection;

        // prepared statements for handling nodes (assuming available node related tables)
        SQLiteCommand createNodeCommand; // topology
        SQLiteCommand[] createNodeCommands; // per-type
        SQLiteCommand[] readNodeCommands; // per-type joined with topology
        Dictionary<String, SQLiteCommand>[] readNodeContainerCommands; // per-type, per-container-attribute
        Dictionary<String, SQLiteCommand>[] updateNodeCommands; // per-type, per-non-container-attribute
        Dictionary<String, SQLiteCommand>[] updateNodeContainerCommands; // per-type, per-container-attribute (inserting container updating commands)
        SQLiteCommand deleteNodeCommand; // topology
        SQLiteCommand[] deleteNodeCommands; // per-type
        Dictionary<String, SQLiteCommand>[] deleteNodeContainerCommands; // per-type, per-container-attribute

        // prepared statements for handling edges (assuming available edge related tables)
        SQLiteCommand createEdgeCommand; // topology
        SQLiteCommand[] createEdgeCommands; // per-type
        SQLiteCommand[] readEdgeCommands; // per-type joined with topology
        Dictionary<String, SQLiteCommand>[] readEdgeContainerCommands; // per-type, per-container-attribute
        Dictionary<String, SQLiteCommand>[] updateEdgeCommands; // per-type, per-non-container-attribute
        Dictionary<String, SQLiteCommand>[] updateEdgeContainerCommands; // per-type, per-container-attribute (inserting container updating commands)
        SQLiteCommand deleteEdgeCommand; // topology
        SQLiteCommand[] deleteEdgeCommands; // per-type
        Dictionary<String, SQLiteCommand>[] deleteEdgeContainerCommands; // per-type, per-container-attribute

        // database edge redirections, due to a node retype requiring an adaptation to the new node id, or a domain object/application layer edge redirect
        SQLiteCommand updateEdgeSourceCommand; // topology
        SQLiteCommand updateEdgeTargetCommand; // topology
        SQLiteCommand redirectEdgeCommand; // topology

        // prepared statements for handling graphs
        SQLiteCommand createGraphCommand;
        SQLiteCommand readGraphsCommand;
        long HOST_GRAPH_ID = 0;

        // prepared statements for handling objects (assuming available object related tables)
        SQLiteCommand createObjectCommand; // topology
        SQLiteCommand[] createObjectCommands; // per-type
        SQLiteCommand[] readObjectCommands; // per-type joined with topology
        Dictionary<String, SQLiteCommand>[] readObjectContainerCommands; // per-type, per-container-attribute
        Dictionary<String, SQLiteCommand>[] updateObjectCommands; // per-type, per-non-container-attribute
        Dictionary<String, SQLiteCommand>[] updateObjectContainerCommands; // per-type, per-container-attribute (inserting container updating commands)

        Stack<INamedGraph> graphs; // the stack of graphs getting processed, the first entry being the host graph
        INamedGraph host; // I'd prefer to use a property accessing the stack, but this would require to create an array...
        INamedGraph graph { get { return graphs.Peek(); } } // the current graph

        IGraphProcessingEnvironment procEnv; // the graph processing environment to switch the current graph in case of to-subgraph switches (todo: and for db-transaction handling)

        IEdge edgeGettingRedirected;

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

        // database id to zombie concept mappings, and vice versa - nodes/edges created for node/edge ids that don't exist anymore, they appear when a container history log is replayed for a storage
        Dictionary<long, INode> DbIdToZombieNode;
        Dictionary<INode, long> ZombieNodeToDbId;
        Dictionary<long, IEdge> DbIdToZombieEdge;
        Dictionary<IEdge, long> ZombieEdgeToDbId;


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

            // the following constraint is not required in grs import/export, but that one is flawed, based on an assumption that often holds but does not hold in the general case:
            // a graph element reference (ugly duckling) belongs to the current graph (this breaks when the user assigns a node/edge from another graph to a node/edge valued attribute in a node/edge of the current graph, or to an internal class object reachable from multiple graphs) (or when this happens automatically, e.g. with inducedSubgraph/definedSubgraph)
            // only since graphof is a general handling possible (yet to be implemented in the grs import/export)
            // here in the persistent graph we enfore the general handling (ironically, this is less needed here due to global node/edge ids in the database, but the entire runtime graph containing the node/edge may not be known, gets only known by the assignment of a graph element to a graph element attribute)
            if(ModelContainsGraphElementReferences(hostGraph.Model) && !hostGraph.Model.GraphElementsReferenceContainingGraph)
                throw new Exception("When the nodes/edges/objects in the graph model contain node or edge references (i.e. node/edge typed attributes), the graph elements must reference the graph they are contained in (add a node edge graph; declaration to the model)!");

            graphs = new Stack<INamedGraph>();
            graphs.Push(hostGraph);
            host = hostGraph;

            CreateSchemaIfNotExistsOrAdaptToCompatibleChanges();
            // TODO: CleanUnusedSubgraphAndObjectReferencesAndCompactifyContainerChangeHistory(); also todo: vacuum the underlying database once in while, to be detected when exactly, maybe only on user request
            ReadCompleteGraph();
            RegisterPersistenceHandlers();
            PrepareStatementsForGraphModifications();
        }

        private bool ModelContainsGraphElementReferences(IGraphModel model)
        {
            bool result = false;
            foreach(NodeType nodeType in model.NodeModel.Types)
            {
                result |= TypeContainsGraphElementReferences(nodeType);
            }
            foreach(EdgeType edgeType in model.EdgeModel.Types)
            {
                result |= TypeContainsGraphElementReferences(edgeType);
            }
            foreach(ObjectType objectType in model.ObjectModel.Types)
            {
                result |= TypeContainsGraphElementReferences(objectType);
            }
            return result;
        }

        private bool TypeContainsGraphElementReferences(InheritanceType type)
        {
            foreach(AttributeType attributeType in type.AttributeTypes)
            {
                if(IsGraphElementType(attributeType))
                    return true;
                if(IsSupportedContainerType(attributeType))
                {
                    if(IsGraphElementType(attributeType.ValueType)) // container todo: also KeyType in case of a map
                        return true;
                }
            }
            return false;
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

            DbIdToZombieNode = new Dictionary<long, INode>();
            ZombieNodeToDbId = new Dictionary<INode, long>();
            DbIdToZombieEdge = new Dictionary<long, IEdge>();
            ZombieEdgeToDbId = new Dictionary<IEdge, long>();

            CreateIdentityAndTopologyTables();
            CreateTypesWithAttributeTables();

            // TODO: adapt to compatible changes (i.e. to user changes of the model, these are reflected in the types/attributes, the basic schema will be pretty constant)
            // to be supported: extension by attributes; in case of incompatible changes: report to the user that he has to carry out the adaptation on his own with SQL commands (also TODO: be as robust as possible when model flags change)
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

                foreach(AttributeType attributeType in nodeType.AttributeTypes)
                {
                    if(!IsSupportedContainerType(attributeType))
                        continue;
                    CreateContainerTypeTable(nodeType, "nodeId", attributeType);
                }
            }

            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                CreateInheritanceTypeTable(edgeType, "edgeId");

                foreach(AttributeType attributeType in edgeType.AttributeTypes)
                {
                    if(!IsSupportedContainerType(attributeType))
                        continue;
                    CreateContainerTypeTable(edgeType, "edgeId", attributeType);
                }
            }

            foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
            {
                CreateInheritanceTypeTable(objectType, "objectId");

                foreach(AttributeType attributeType in objectType.AttributeTypes)
                {
                    if(!IsSupportedContainerType(attributeType))
                        continue;
                    CreateContainerTypeTable(objectType, "objectId", attributeType);
                }
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
                if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
                    continue;

                columnNamesAndTypes.Add(UniquifyName(attributeType.Name));
                columnNamesAndTypes.Add(AttributeTypeToSQLiteType(attributeType));
            }

            CreateTable(tableName, idColumnName, columnNamesAndTypes.ToArray()); // the id in the type table is a local copy of the global id from the corresponding topology table
        }

        private void CreateContainerTypeTable(InheritanceType type, String ownerIdColumnName, AttributeType attributeType)
        {
            String tableName = EscapeTableName(type.PackagePrefixedName + "_" + attributeType.Name); // container todo: encode container type into name (or maybe entry in types table) ... to allow database <-> model synch
            List<String> columnNamesAndTypes = new List<String>(); // ArrayBuilder

            columnNamesAndTypes.Add(ownerIdColumnName);
            columnNamesAndTypes.Add("INTEGER");
            columnNamesAndTypes.Add("command");
            columnNamesAndTypes.Add("INT"); // TINYINT doesn't work
            columnNamesAndTypes.Add("value");
            columnNamesAndTypes.Add(AttributeTypeToSQLiteType(attributeType.ValueType));
            //columnNamesAndTypes.Add("key");
            //columnNamesAndTypes.Add(AttributeTypeToSQLiteType(attributeType.KeyType));

            CreateTable(tableName, "entryId", columnNamesAndTypes.ToArray()); // the entryId denotes the row local to this table, the ownerIdColumnName is a local copy of the global id from the corresponding topology table

            AddIndex(tableName, ownerIdColumnName); // in order to delete without a full table scan (I assume small changesets in between database openings and decided for a by-default pruning on open - an alternative would be a full table replacement once in a while (would be ok in case of big changesets, as they would occur when a pruning run only occurs on explicit request, but such ones could be forgotten/missed unknowingly too easily, leading to (unexpected) slowness))
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

        private bool IsAttributeTypeMappedToDatabaseColumn(AttributeType attributeType)
        {
            return IsScalarType(attributeType) || IsGraphType(attributeType) || IsObjectType(attributeType) || IsGraphElementType(attributeType); // containers are not referenced by an id in a database column, but are mapped entirely to own tables
        }

        // types appearing as attributes with a complete implementation for loading/storing them from/to the database
        private bool IsSupportedAttributeType(AttributeType attributeType)
        {
            return IsScalarType(attributeType) || IsGraphType(attributeType) || IsObjectType(attributeType) || IsGraphElementType(attributeType) || IsSupportedContainerType(attributeType); // TODO: external/object type - also handle these.
        }

        private bool IsGraphType(AttributeType attributeType)
        {
            return attributeType.Kind == AttributeKind.GraphAttr;
        }

        private bool IsObjectType(AttributeType attributeType)
        {
            return attributeType.Kind == AttributeKind.InternalClassObjectAttr;
        }

        private bool IsGraphElementType(AttributeType attributeType)
        {
            return attributeType.Kind == AttributeKind.NodeAttr || attributeType.Kind == AttributeKind.EdgeAttr;
        }

        private bool IsSupportedContainerType(AttributeType attributeType)
        {
            return attributeType.Kind == AttributeKind.SetAttr; // container TODO: array, map, deque
        }

        // TODO: separate references from scalars (from containers)
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
                case AttributeKind.NodeAttr: return "INTEGER"; // non-scalar type, but reference type with simple mapping (container types have a complex mapping)
                case AttributeKind.EdgeAttr: return "INTEGER"; // non-scalar type, but reference type with simple mapping (container types have a complex mapping)
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
            readNodeContainerCommands = new Dictionary<String, SQLiteCommand>[graph.Model.NodeModel.Types.Length];
            readEdgeCommands = new SQLiteCommand[graph.Model.EdgeModel.Types.Length];
            readEdgeContainerCommands = new Dictionary<String, SQLiteCommand>[graph.Model.EdgeModel.Types.Length];

            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                readNodeCommands[nodeType.TypeID] = PrepareStatementsForReadingNodes(nodeType);

                readNodeContainerCommands[nodeType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in nodeType.AttributeTypes)
                {
                    if(!IsSupportedContainerType(attributeType))
                        continue;
                    readNodeContainerCommands[nodeType.TypeID].Add(attributeType.Name, PrepareStatementsForReadingContainerAttributes(nodeType, "nodeId", attributeType));
                }
            }
            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                readEdgeCommands[edgeType.TypeID] = PrepareStatementsForReadingEdges(edgeType);

                readEdgeContainerCommands[edgeType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in edgeType.AttributeTypes)
                {
                    if(!IsSupportedContainerType(attributeType))
                        continue;
                    readEdgeContainerCommands[edgeType.TypeID].Add(attributeType.Name, PrepareStatementsForReadingContainerAttributes(edgeType, "edgeId", attributeType));
                }
            }

            readObjectCommands = new SQLiteCommand[graph.Model.ObjectModel.Types.Length];
            readObjectContainerCommands = new Dictionary<String, SQLiteCommand>[graph.Model.ObjectModel.Types.Length];

            foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
            {
                readObjectCommands[objectType.TypeID] = PrepareStatementsForReadingObjects(objectType);

                readObjectContainerCommands[objectType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in objectType.AttributeTypes)
                {
                    if(!IsSupportedContainerType(attributeType))
                        continue;
                    readObjectContainerCommands[objectType.TypeID].Add(attributeType.Name, PrepareStatementsForReadingContainerAttributes(objectType, "objectId", attributeType));
                }
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
            // 2nd full table scan, type table by type table, patching node/edge/object references (graphs were handled before) in the nodes/edges/objects (in-memory), accessed by their database-id to type mapping
            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                PatchAttributesInElement(nodeType, "nodeId", readNodeCommands[nodeType.TypeID]);

                foreach(AttributeType attributeType in nodeType.AttributeTypes)
                {
                    if(!IsSupportedContainerType(attributeType))
                        continue;
                    SQLiteCommand readNodeContainerCommand = readNodeContainerCommands[nodeType.TypeID][attributeType.Name];
                    ReadPatchContainerAttributesInElement(nodeType, "nodeId", attributeType, readNodeContainerCommand);
                }
            }
            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                PatchAttributesInElement(edgeType, "edgeId", readEdgeCommands[edgeType.TypeID]);

                foreach(AttributeType attributeType in edgeType.AttributeTypes)
                {
                    if(!IsSupportedContainerType(attributeType))
                        continue;
                    SQLiteCommand readEdgeContainerCommand = readEdgeContainerCommands[edgeType.TypeID][attributeType.Name];
                    ReadPatchContainerAttributesInElement(edgeType, "edgeId", attributeType, readEdgeContainerCommand);
                }
            }
            foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
            {
                PatchAttributesInElement(objectType, "objectId", readObjectCommands[objectType.TypeID]);

                foreach(AttributeType attributeType in objectType.AttributeTypes)
                {
                    if(!IsSupportedContainerType(attributeType))
                        continue;
                    SQLiteCommand readObjectContainerCommand = readObjectContainerCommands[objectType.TypeID][attributeType.Name];
                    ReadPatchContainerAttributesInElement(objectType, "objectId", attributeType, readObjectContainerCommand);
                }
            }

            ClearZombies(); // nodes/edges not existing anymore in the current state of the graph, that appeared during container history log replaying (inserted into and maybe deleted again from a container)

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
                if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
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
                if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
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
                if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
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

        // attributes of container type don't appear as a column in the type table of its owner type, they come with an entire table on their own (per container attribute of their owner type)
        private SQLiteCommand PrepareStatementsForReadingContainerAttributes(InheritanceType inheritanceType, String ownerIdColumnName, AttributeType attributeType)
        {
            String tableName = EscapeTableName(inheritanceType.PackagePrefixedName + "_" + attributeType.Name);
            StringBuilder columnNames = new StringBuilder();
            AddQueryColumn(columnNames, "entryId");
            AddQueryColumn(columnNames, ownerIdColumnName);
            AddQueryColumn(columnNames, "command");
            AddQueryColumn(columnNames, "value");
            //AddQueryColumn(columnNames, "key");
            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
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

        private void PatchAttributesInElement(InheritanceType inheritanceType, String elementIdColumnName, SQLiteCommand readElementCommand)
        {
            using(SQLiteDataReader reader = readElementCommand.ExecuteReader())
            {
                Dictionary<string, int> attributeNameToColumnIndex = GetNameToColumnIndexMapping(reader);

                while(reader.Read())
                {
                    long elementId = reader.GetInt64(attributeNameToColumnIndex[elementIdColumnName]);
                    IAttributeBearer element = GetElement(inheritanceType, elementId);

                    foreach(AttributeType attributeType in inheritanceType.AttributeTypes)
                    {
                        object attributeValue;
                        if(IsObjectType(attributeType))
                            attributeValue = GetObjectValue(attributeType, reader, attributeNameToColumnIndex);
                        else if(IsGraphElementType(attributeType))
                            attributeValue = GetGraphElement(attributeType, reader, attributeNameToColumnIndex);
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

            using(SQLiteDataReader reader = readElementContainerAttributeCommand.ExecuteReader())
            {
                Dictionary<string, int> attributeNameToColumnIndex = GetNameToColumnIndexMapping(reader);

                while(reader.Read())
                {
                    long entryId = reader.GetInt64(attributeNameToColumnIndex["entryId"]);
                    long ownerId = reader.GetInt64(attributeNameToColumnIndex[owningElementIdColumnName]);
                    byte command = reader.GetByte(attributeNameToColumnIndex["command"]);
                    object value = null;
                    if(command != (byte)ContainerCommand.AssignEmptyContainer && command != (byte)ContainerCommand.AssignNull)
                        value = GetContainerEntryValue(attributeType.ValueType, reader, attributeNameToColumnIndex);
                    //object key = ReadContainerEntryKey(attributeType.KeyType, reader, attributeNameToColumnIndex);

                    IAttributeBearer element = GetElement(inheritanceType, ownerId);
                    ApplyContainerCommandToElement(element, attributeType, (ContainerCommand)command, value, null);
                }
            }

            ReportZombies();
        }

        private IAttributeBearer GetElement(InheritanceType inheritanceType, long ownerId)
        {
            if(inheritanceType is NodeType)
                return DbIdToNode[ownerId];
            else if(inheritanceType is EdgeType)
                return DbIdToEdge[ownerId];
            else if(inheritanceType is ObjectType)
                return DbIdToObject[ownerId];
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
                        break;
                    }
                case ContainerCommand.AssignElement:
                    // container TODO
                    break;
                case ContainerCommand.AssignNull:
                    element.SetAttribute(attributeType.Name, null);
                    break;
            }
        }

        private object GetEmptyContainer(AttributeType attributeType)
        {
            switch(attributeType.Kind)
            {
                case AttributeKind.SetAttr:
                    Type srcType = attributeType.ValueType.Type;
                    Type dstType = typeof(SetValueType);
                    IDictionary set = ContainerHelper.NewDictionary(srcType, dstType);
                    return set;
                default:
                    throw new Exception("Unknown container type");
            }
        }

        private void ApplyContainerPutElementCommand(object container, AttributeType attributeType, object value, object key)
        {
            switch(attributeType.Kind)
            {
                case AttributeKind.SetAttr:
                    IDictionary set = (IDictionary)container;
                    set[value] = null;
                    break;
                default:
                    throw new Exception("Unknown container type");
            }
        }

        private void ApplyContainerRemoveElementCommand(object container, AttributeType attributeType, object value, object key)
        {
            switch(attributeType.Kind)
            {
                case AttributeKind.SetAttr:
                    IDictionary set = (IDictionary)container;
                    set.Remove(value);
                    break;
                default:
                    throw new Exception("Unknown container type");
            }
        }

        private void ReportZombies()
        {
            // container TODO - remember all owner.containers seen, after log replay completed, check for and report zombies
        }

        private void ClearZombies()
        {
            DbIdToZombieNode.Clear();
            ZombieNodeToDbId.Clear();
            DbIdToZombieEdge.Clear();
            ZombieEdgeToDbId.Clear();
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
            return GetScalarValueFromSpecifiedColumn(attributeType, UniquifyName(attributeType.Name), reader, attributeNameToColumnIndex);
        }

        private object GetScalarValueFromSpecifiedColumn(AttributeType attributeType, String columnName, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            int index = attributeNameToColumnIndex[columnName];
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
            return GetGraphValueFromSpecifiedColumn(UniquifyName(attributeType.Name), reader, attributeNameToColumnIndex);
        }

        private IGraph GetGraphValueFromSpecifiedColumn(String columnName, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            int index = attributeNameToColumnIndex[columnName];
            if(reader.IsDBNull(index))
                return null;
            long dbid = reader.GetInt64(index);
            return DbIdToGraph[dbid];
        }

        private IObject GetObjectValue(AttributeType attributeType, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            return GetObjectValueFromSpecifiedColumn(UniquifyName(attributeType.Name), reader, attributeNameToColumnIndex);
        }

        private IObject GetObjectValueFromSpecifiedColumn(String columnName, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            int index = attributeNameToColumnIndex[columnName];
            if(reader.IsDBNull(index))
                return null;
            long dbid = reader.GetInt64(index);
            return DbIdToObject[dbid];
        }

        private IGraphElement GetGraphElement(AttributeType attributeType, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            int index = attributeNameToColumnIndex[UniquifyName(attributeType.Name)];
            if(reader.IsDBNull(index))
                return null;
            long dbid = reader.GetInt64(index);
            if(attributeType.Kind == AttributeKind.NodeAttr)
                return DbIdToNode[dbid];
            else
                return DbIdToEdge[dbid];
        }

        // maybe TODO: ReadContainerEntryValue, also rename the other GetXXX to ReadXXX?
        private object GetContainerEntryValue(AttributeType attributeType, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            if(IsScalarType(attributeType))
                return GetScalarValueFromSpecifiedColumn(attributeType, "value", reader, attributeNameToColumnIndex);
            else if(IsGraphType(attributeType))
                return GetGraphValueFromSpecifiedColumn("value", reader, attributeNameToColumnIndex);
            else if(IsObjectType(attributeType))
                return GetObjectValueFromSpecifiedColumn("value", reader, attributeNameToColumnIndex);
            else if(IsGraphElementType(attributeType))
                return GetGraphElementFromSpecifiedColumnOrZombieIfNotExisting(attributeType, "value", reader, attributeNameToColumnIndex); // zombies can only appear for nodes and edges, graphs and objects are garbage collected (after the read process), so zombies can not appear (neither can they for value types)
            throw new Exception("Unsupported container type");
        }

        private IGraphElement GetGraphElementFromSpecifiedColumnOrZombieIfNotExisting(AttributeType attributeType, String columnName, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            int index = attributeNameToColumnIndex[columnName];
            if(reader.IsDBNull(index))
                return null;
            long dbid = reader.GetInt64(index);
            if(attributeType.Kind == AttributeKind.NodeAttr)
            {
                INode node;
                if(DbIdToNode.TryGetValue(dbid, out node))
                    return node;
                else
                    return GetOrCreateZombieNode(dbid);
            }
            else
            {
                IEdge edge;
                if(DbIdToEdge.TryGetValue(dbid, out edge))
                    return edge;
                else
                    return GetOrCreateZombieEdge(dbid);
            }
        }

        private INode GetOrCreateZombieNode(long dbid)
        {
            INode node;
            if(DbIdToZombieNode.TryGetValue(dbid, out node))
                return node;
            node = graph.Model.NodeModel.RootType.CreateNode();
            AddZombieNodeWithDbIdToDbIdMapping(node, dbid);
            return node;
        }

        private IEdge GetOrCreateZombieEdge(long dbid)
        {
            IEdge edge;
            if(DbIdToZombieEdge.TryGetValue(dbid, out edge))
                return edge;
            edge = graph.Model.EdgeModel.RootType.CreateEdge();
            AddZombieEdgeWithDbIdToDbIdMapping(edge, dbid);
            return edge;
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

            updateEdgeSourceCommand = PrepareUpdateEdgeSource();
            updateEdgeTargetCommand = PrepareUpdateEdgeTarget();
            redirectEdgeCommand = PrepareRedirectEdge();

            updateNodeCommands = new Dictionary<String, SQLiteCommand>[graph.Model.NodeModel.Types.Length];
            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                updateNodeCommands[nodeType.TypeID] = new Dictionary<String, SQLiteCommand>(nodeType.NumAttributes);
                foreach(AttributeType attributeType in nodeType.AttributeTypes)
                {
                    if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
                        continue;
                    updateNodeCommands[nodeType.TypeID][attributeType.Name] = PrepareUpdate(nodeType, "nodeId", attributeType);
                }
            }
            updateNodeContainerCommands = new Dictionary<String, SQLiteCommand>[graph.Model.NodeModel.Types.Length];
            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                updateNodeContainerCommands[nodeType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in nodeType.AttributeTypes)
                {
                    if(!IsSupportedContainerType(attributeType))
                        continue;
                    updateNodeContainerCommands[nodeType.TypeID][attributeType.Name] = PrepareContainerUpdatingInsert(nodeType, "nodeId", attributeType);
                }
            }
            updateEdgeCommands = new Dictionary<String, SQLiteCommand>[graph.Model.EdgeModel.Types.Length];
            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                updateEdgeCommands[edgeType.TypeID] = new Dictionary<String, SQLiteCommand>(edgeType.NumAttributes);
                foreach(AttributeType attributeType in edgeType.AttributeTypes)
                {
                    if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
                        continue;
                    updateEdgeCommands[edgeType.TypeID][attributeType.Name] = PrepareUpdate(edgeType, "edgeId", attributeType);
                }
            }
            updateEdgeContainerCommands = new Dictionary<String, SQLiteCommand>[graph.Model.EdgeModel.Types.Length];
            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                updateEdgeContainerCommands[edgeType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in edgeType.AttributeTypes)
                {
                    if(!IsSupportedContainerType(attributeType))
                        continue;
                    updateEdgeContainerCommands[edgeType.TypeID][attributeType.Name] = PrepareContainerUpdatingInsert(edgeType, "edgeId", attributeType);
                }
            }

            updateObjectCommands = new Dictionary<String, SQLiteCommand>[graph.Model.ObjectModel.Types.Length];
            foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
            {
                updateObjectCommands[objectType.TypeID] = new Dictionary<String, SQLiteCommand>(objectType.NumAttributes);
                foreach(AttributeType attributeType in objectType.AttributeTypes)
                {
                    if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
                        continue;
                    updateObjectCommands[objectType.TypeID][attributeType.Name] = PrepareUpdate(objectType, "objectId", attributeType);
                }
            }
            updateObjectContainerCommands = new Dictionary<String, SQLiteCommand>[graph.Model.ObjectModel.Types.Length];
            foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
            {
                updateObjectContainerCommands[objectType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in objectType.AttributeTypes)
                {
                    if(!IsSupportedContainerType(attributeType))
                        continue;
                    updateObjectContainerCommands[objectType.TypeID][attributeType.Name] = PrepareContainerUpdatingInsert(objectType, "objectId", attributeType);
                }
            }

            deleteNodeCommand = PrepareTopologyDelete("nodes", "nodeId");
            deleteNodeCommands = new SQLiteCommand[graph.Model.NodeModel.Types.Length];
            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                deleteNodeCommands[nodeType.TypeID] = PrepareDelete(nodeType, "nodeId");
            }
            deleteNodeContainerCommands = new Dictionary<String, SQLiteCommand>[graph.Model.NodeModel.Types.Length];
            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                deleteNodeContainerCommands[nodeType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in nodeType.AttributeTypes)
                {
                    if(!IsSupportedContainerType(attributeType))
                        continue;
                    deleteNodeContainerCommands[nodeType.TypeID][attributeType.Name] = PrepareContainerDelete(nodeType, "nodeId", attributeType);
                }
            }
            deleteEdgeCommand = PrepareTopologyDelete("edges", "edgeId");
            deleteEdgeCommands = new SQLiteCommand[graph.Model.EdgeModel.Types.Length];
            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                deleteEdgeCommands[edgeType.TypeID] = PrepareDelete(edgeType, "edgeId");
            }
            deleteEdgeContainerCommands = new Dictionary<String, SQLiteCommand>[graph.Model.EdgeModel.Types.Length];
            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                deleteEdgeContainerCommands[edgeType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in edgeType.AttributeTypes)
                {
                    if(!IsSupportedContainerType(attributeType))
                        continue;
                    deleteEdgeContainerCommands[edgeType.TypeID][attributeType.Name] = PrepareContainerDelete(edgeType, "edgeId", attributeType);
                }
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
                if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
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

        private SQLiteCommand PrepareContainerUpdatingInsert(InheritanceType type, string ownerIdColumnName, AttributeType attributeType)
        {
            String tableName = EscapeTableName(type.PackagePrefixedName + "_" + attributeType.Name); // container todo: could yield a name already in use; general todo: SQLite/SQL is case insensitive
            StringBuilder columnNames = new StringBuilder();
            StringBuilder parameterNames = new StringBuilder();
            // the id of the container element (entryId) being the same as the primary key is defined by the dbms
            AddInsertParameter(columnNames, parameterNames, ownerIdColumnName);
            AddInsertParameter(columnNames, parameterNames, "command");
            AddInsertParameter(columnNames, parameterNames, "value");

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

        private SQLiteCommand PrepareContainerDelete(InheritanceType type, string ownerIdColumnName, AttributeType attributeType)
        {
            String tableName = EscapeTableName(type.PackagePrefixedName + "_" + attributeType.Name); // container todo: could yield a name already in use; general todo: SQLite/SQL is case insensitive

            StringBuilder command = new StringBuilder();
            command.Append("DELETE FROM ");
            command.Append(tableName);
            command.Append(" WHERE ");
            command.Append(ownerIdColumnName);
            command.Append("==");
            command.Append("@" + ownerIdColumnName);

            return new SQLiteCommand(command.ToString(), connection);
        }

        #endregion Graph modification handling preparations

        private string EscapeTableName(string name)
        {
            return UniquifyName(name.Replace(':', '_')); // TODO: could lead to issues in case user uses packages (separated by :: from the type), as well as type names containing _
        }

        // attribute name defined by user could collide with fixed column names used to model the graph,
        // type name defined by user could collide with fixed table names used to model the graph
        // SQLite(/SQL standard) is case insensitive, but GrGen attributes/types are case sensitive
        private string UniquifyName(string columnName)
        {
            return columnName + "_prvt_name_collisn_sufx_" + GetMd5(columnName);
        }

        private string GetMd5(string input)
        {
            using(MD5 md5 = MD5.Create()) // todo in case of performance issues: name mapping could be cached in a loopkup table, always same names used due to fixed model
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return HexString(hashBytes);
            }
        }

        private string HexString(byte[] input)
        {
            StringBuilder sb = new StringBuilder(input.Length * 2);
            for(int i = 0; i < input.Length; ++i)
            {
                sb.AppendFormat("{0:X2}", input[i]);
            }
            return sb.ToString();
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
                addNodeCommand.Parameters.AddWithValue("@" + UniquifyName(attributeType.Name), ValueOrIdOfReferencedElement(value, attributeType));
            }
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
                addEdgeCommand.Parameters.AddWithValue("@" + UniquifyName(attributeType.Name), ValueOrIdOfReferencedElement(value, attributeType));
            }
            int rowsAffected = addEdgeCommand.ExecuteNonQuery();

            WriteContainerEntries(edge);
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

            foreach(AttributeType attributeType in node.Type.AttributeTypes)
            {
                if(!IsSupportedContainerType(attributeType))
                    continue;
                SQLiteCommand deleteNodeContainerCommand = deleteNodeContainerCommands[node.Type.TypeID][attributeType.Name];
                deleteNodeContainerCommand.Parameters.Clear();
                deleteNodeContainerCommand.Parameters.AddWithValue("@nodeId", NodeToDbId[node]);
                rowsAffected = deleteNodeContainerCommand.ExecuteNonQuery();
            }

            RemoveNodeFromDbIdMapping(node);
        }

        public void RemovingEdge(IEdge edge)
        {
            if(edge == edgeGettingRedirected)
                return;

            SQLiteCommand deleteEdgeTopologyCommand = this.deleteEdgeCommand;
            deleteEdgeTopologyCommand.Parameters.Clear();
            deleteEdgeTopologyCommand.Parameters.AddWithValue("@edgeId", EdgeToDbId[edge]);
            int rowsAffected = deleteEdgeTopologyCommand.ExecuteNonQuery();

            SQLiteCommand deleteEdgeCommand = deleteEdgeCommands[edge.Type.TypeID];
            deleteEdgeCommand.Parameters.Clear();
            deleteEdgeCommand.Parameters.AddWithValue("@edgeId", EdgeToDbId[edge]);
            rowsAffected = deleteEdgeCommand.ExecuteNonQuery();

            foreach(AttributeType attributeType in edge.Type.AttributeTypes)
            {
                if(!IsSupportedContainerType(attributeType))
                    continue;
                SQLiteCommand deleteEdgeContainerCommand = deleteEdgeContainerCommands[edge.Type.TypeID][attributeType.Name];
                deleteEdgeContainerCommand.Parameters.Clear();
                deleteEdgeContainerCommand.Parameters.AddWithValue("@edgeId", EdgeToDbId[edge]);
                rowsAffected = deleteEdgeContainerCommand.ExecuteNonQuery();
            }

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
            if(!IsSupportedAttributeType(attrType))
                return;

            if(IsGraphType(attrType))
                AddGraphAsNeeded((INamedGraph)newValue);
            else if(IsObjectType(attrType))
                AddObjectAsNeeded((IObject)newValue);
            else if(IsGraphElementType(attrType))
                AddGraphAsNeeded((INamedGraph)((IContained)newValue).GetContainingGraph());

            if(IsSupportedContainerType(attrType))
                WriteContainerChange(node, attrType, changeType, newValue, keyValue);
            else
            {
                SQLiteCommand updateNodeCommand = updateNodeCommands[node.Type.TypeID][attrType.Name];
                updateNodeCommand.Parameters.Clear();
                updateNodeCommand.Parameters.AddWithValue("@nodeId", NodeToDbId[node]);
                updateNodeCommand.Parameters.AddWithValue("@" + UniquifyName(attrType.Name), ValueOrIdOfReferencedElement(newValue, attrType));

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
                AddGraphAsNeeded((INamedGraph)((IContained)newValue).GetContainingGraph());

            if(IsSupportedContainerType(attrType))
                WriteContainerChange(edge, attrType, changeType, newValue, keyValue);
            else
            {
                SQLiteCommand updateEdgeCommand = updateEdgeCommands[edge.Type.TypeID][attrType.Name];
                updateEdgeCommand.Parameters.Clear();
                updateEdgeCommand.Parameters.AddWithValue("@edgeId", EdgeToDbId[edge]);
                updateEdgeCommand.Parameters.AddWithValue("@" + UniquifyName(attrType.Name), ValueOrIdOfReferencedElement(newValue, attrType));

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
                AddGraphAsNeeded((INamedGraph)((IContained)newValue).GetContainingGraph());

            if(IsSupportedContainerType(attrType))
                WriteContainerChange(obj, attrType, changeType, newValue, keyValue);
            else
            {
                SQLiteCommand updateObjectCommand = updateObjectCommands[obj.Type.TypeID][attrType.Name];
                updateObjectCommand.Parameters.Clear();
                updateObjectCommand.Parameters.AddWithValue("@objectId", ObjectToDbId[obj]);
                updateObjectCommand.Parameters.AddWithValue("@" + UniquifyName(attrType.Name), ValueOrIdOfReferencedElement(newValue, attrType));

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
                int rowsAffected = updateEdgeSourceCommand.ExecuteNonQuery();
            }
            foreach(IEdge incomingEdge in oldNode.Incoming)
            {
                SQLiteCommand updateEdgeTargetCommand = this.updateEdgeTargetCommand;
                updateEdgeTargetCommand.Parameters.Clear();
                updateEdgeTargetCommand.Parameters.AddWithValue("@edgeId", EdgeToDbId[incomingEdge]);
                updateEdgeTargetCommand.Parameters.AddWithValue("@targetNodeId", NodeToDbId[newNode]);
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
                if(IsSupportedContainerType(attributeType))
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
                    AddGraphAsNeeded((INamedGraph)((IContained)val).GetContainingGraph());
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
                if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
                    continue;
                object val = obj.GetAttribute(attributeType.Name);
                addObjectCommand.Parameters.AddWithValue("@" + UniquifyName(attributeType.Name), ValueOrIdOfReferencedElement(val, attributeType));
            }
            int rowsAffected = addObjectCommand.ExecuteNonQuery();

            WriteContainerEntries(obj);
        }

        private void WriteContainerChange(IAttributeBearer owningElement, AttributeType attributeType,
                AttributeChangeType changeType, object newValue, object keyValue)
        {
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
                        long entryId = ExecuteUpdatingInsert(updatingInsert, attributeType.ValueType, owningElementId, owningElementIdColumnName, ContainerCommand.PutElement, newValue, null);
                        break;
                    }
                case AttributeChangeType.RemoveElement:
                    {
                        SQLiteCommand updatingInsert = GetUpdateContainerCommand(owningElement, attributeType);
                        string owningElementIdColumnName;
                        long owningElementId = GetDbIdAndColumnName(owningElement, out owningElementIdColumnName);
                        long entryId = ExecuteUpdatingInsert(updatingInsert, attributeType.ValueType, owningElementId, owningElementIdColumnName, ContainerCommand.RemoveElement, newValue, null);
                        break;
                    }
                case AttributeChangeType.AssignElement:
                    // container TODO
                    break;
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
                if(!IsSupportedContainerType(attributeType))
                    continue;

                WriteContainerEntries(owningElement.GetAttribute(attributeType.Name), attributeType, owningElement);
            }
        }

        private void WriteContainerEntries(object container, AttributeType attributeType, IAttributeBearer owningElement)
        {
            if(attributeType.Kind == AttributeKind.SetAttr)
                WriteSetEntries((IDictionary)container, attributeType, owningElement);
            // container TODO: other container types
        }

        private void WriteSetEntries(IDictionary set, AttributeType attributeType, IAttributeBearer owningElement)
        {
            string owningElementIdColumnName;
            long owningElementId = GetDbIdAndColumnName(owningElement, out owningElementIdColumnName);

            // new entire container
            SQLiteCommand updatingInsert = GetUpdateContainerCommand(owningElement, attributeType);
            if(set == null)
            {
                long entryId = ExecuteUpdatingInsert(updatingInsert, attributeType, owningElementId, owningElementIdColumnName, ContainerCommand.AssignNull, null, null); ;
            }
            else
            {
                long entryId = ExecuteUpdatingInsert(updatingInsert, attributeType, owningElementId, owningElementIdColumnName, ContainerCommand.AssignEmptyContainer, null, null);

                // add all container entries - explode complete container into series of adds, i.e. put-elements
                foreach(DictionaryEntry entry in set)
                {
                    if(IsGraphType(attributeType.ValueType))
                        AddGraphAsNeeded((INamedGraph)entry.Key);
                    else if(IsObjectType(attributeType.ValueType))
                        AddObjectAsNeeded((IObject)entry.Key);
                    else if(IsGraphElementType(attributeType.ValueType))
                        AddGraphAsNeeded((INamedGraph)((IContained)entry.Key).GetContainingGraph());

                    entryId = ExecuteUpdatingInsert(updatingInsert, attributeType.ValueType, owningElementId, owningElementIdColumnName, ContainerCommand.PutElement, entry.Key, null);
                }
            }
        }

        private long ExecuteUpdatingInsert(SQLiteCommand updatingInsert, AttributeType attributeType, long owningElementId, string owningElementIdColumnName, ContainerCommand command, object value, object key)
        {
            updatingInsert.Parameters.Clear();
            updatingInsert.Parameters.AddWithValue("@" + owningElementIdColumnName, owningElementId);
            updatingInsert.Parameters.AddWithValue("@command", command);
            object valueOrId = ValueOrIdOfReferencedElement(value, attributeType);
            updatingInsert.Parameters.AddWithValue("@value", value != null ? valueOrId : DBNull.Value);
            int rowsAffected = updatingInsert.ExecuteNonQuery();
            return connection.LastInsertRowId;
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

        private void AddZombieNodeWithDbIdToDbIdMapping(INode node, long dbid)
        {
            DbIdToZombieNode.Add(dbid, node);
            ZombieNodeToDbId.Add(node, dbid);
        }

        private void AddZombieEdgeWithDbIdToDbIdMapping(IEdge edge, long dbid)
        {
            DbIdToZombieEdge.Add(dbid, edge);
            ZombieEdgeToDbId.Add(edge, dbid);
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

        #endregion Database id from/to concept mapping maintenance
    }
}
