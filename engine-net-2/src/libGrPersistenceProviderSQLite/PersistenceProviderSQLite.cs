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
using System.IO;
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
    // todo: split some helper classes out of it (now that the full functionality is available)
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

        class TypesFromDatabase
        {
            public readonly Dictionary<string, AttributeTypesFromDatabase> TypesToAttributesFromDatabase;

            public TypesFromDatabase(Dictionary<string, AttributeTypesFromDatabase> typesToAttributesFromDatabase)
            {
                TypesToAttributesFromDatabase = typesToAttributesFromDatabase;
            }

            public AttributeTypesFromDatabase this[string typeName]
            {
                get { return TypesToAttributesFromDatabase[typeName]; }
            }

            public bool IsTypeKnown(InheritanceType type, TypeKind kind)
            {
                return TypesToAttributesFromDatabase.ContainsKey(type.PackagePrefixedName)
                    && TypesToAttributesFromDatabase[type.PackagePrefixedName].KindOfOwner == kind;
            }

            public bool IsTypeKnown(InheritanceType type)
            {
                return TypesToAttributesFromDatabase.ContainsKey(type.PackagePrefixedName);
            }

            public void WriteDatabaseModelToFile(StreamWriter sw)
            {
                foreach(KeyValuePair<string, AttributeTypesFromDatabase> typeToAttributes in TypesToAttributesFromDatabase)
                {
                    if(typeToAttributes.Value.KindOfOwner == TypeKind.NodeClass)
                        sw.Write("node class ");
                    else if(typeToAttributes.Value.KindOfOwner == TypeKind.EdgeClass)
                        sw.Write("edge class ");
                    else
                        sw.Write("class ");
                    sw.WriteLine(typeToAttributes.Key);
                    sw.WriteLine("{");
                    foreach(AttributeTypeFromDatabase attribute in typeToAttributes.Value.AttributeNamesToAttributeTypes.Values)
                    {
                        sw.Write("\t");
                        sw.Write(attribute.Name);
                        sw.Write(" : ");
                        sw.Write(attribute.XgrsType);
                        sw.WriteLine(";");
                    }
                    sw.WriteLine("}");
                }
            }
        }

        class AttributeTypesFromDatabase
        {
            public readonly string Name; // of type
            public readonly TypeKind KindOfOwner;
            public readonly Dictionary<string, AttributeTypeFromDatabase> AttributeNamesToAttributeTypes;

            public AttributeTypesFromDatabase(string name, TypeKind kindOfOwner)
            {
                Name = name;
                KindOfOwner = kindOfOwner;
                AttributeNamesToAttributeTypes = new Dictionary<string, AttributeTypeFromDatabase>();
            }

            public AttributeTypeFromDatabase this[string typeName]
            {
                get { return AttributeNamesToAttributeTypes[typeName]; }
            }
        }

        class AttributeTypeFromDatabase
        {
            public readonly string Name; // of attribute
            public readonly string XgrsType;
            public readonly long DbId;

            public AttributeTypeFromDatabase(string name, string xgrsType, long dbId)
            {
                Name = name;
                XgrsType = xgrsType;
                DbId = dbId;
            }
        }

        enum ContainerCommand { AssignEmptyContainer = 0, PutElement = 1, RemoveElement = 2, AssignElement = 3, AssignNull = 4 } // based on the AttributeChangeType

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

        class AttributesMarkingState
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

        SQLiteConnection connection;
        SQLiteTransaction transaction;

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
        SQLiteCommand deleteGraphCommand; // topology
        long HOST_GRAPH_ID = 0;

        // prepared statements for handling objects (assuming available object related tables)
        SQLiteCommand createObjectCommand; // topology
        SQLiteCommand[] createObjectCommands; // per-type
        SQLiteCommand[] readObjectCommands; // per-type joined with topology
        Dictionary<String, SQLiteCommand>[] readObjectContainerCommands; // per-type, per-container-attribute
        Dictionary<String, SQLiteCommand>[] updateObjectCommands; // per-type, per-non-container-attribute
        Dictionary<String, SQLiteCommand>[] updateObjectContainerCommands; // per-type, per-container-attribute (inserting container updating commands)
        SQLiteCommand deleteObjectCommand; // topology
        SQLiteCommand[] deleteObjectCommands; // per-type
        Dictionary<String, SQLiteCommand>[] deleteObjectContainerCommands; // per-type, per-container-attribute

        Dictionary<object, SetValueType> visited = new Dictionary<object, SetValueType>(); // later todo: implement visited flags in all entities, use them instead of a visited dictionary
        Stack<GcDfsStateItem> gcDfsStateItems = new Stack<GcDfsStateItem>(); // explicit stack to avoid a stack overrun from too many recursive calls during depth-first traversal

        Stack<INamedGraph> graphs; // the stack of graphs getting processed, the first entry being the host graph
        INamedGraph host; // I'd prefer to use a property accessing the stack, but this would require to create an array...
        INamedGraph graph { get { return graphs.Peek(); } } // the current graph

        IGraphProcessingEnvironment procEnv; // the graph processing environment to switch the current graph in case of to-subgraph switches

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

        readonly AttributeType IntegerAttributeType = new AttributeType(null, null, AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));

        AttributesMarkingState initializedContainers;
        AttributesMarkingState modifiedContainers;

        String connectionParameters;
        String persistentGraphParameters;


        public PersistenceProviderSQLite()
        {
        }

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

            initializedContainers = new AttributesMarkingState();
            modifiedContainers = new AttributesMarkingState();

            // likely todo: split the code into multiple classes (split dedicated tasks like reading/cleaning/schema adaptation off into own classes)
            CreateSchemaIfNotExistsOrAdaptToCompatibleChanges();
            ReadCompleteGraph();
            PrepareStatementsForGraphModifications(); // Cleanup may carry out graph modifications (even before we are notified about graph changes after registering the handlers)
            Cleanup();
            RegisterPersistenceHandlers();

            initializedContainers = null;
            modifiedContainers = null;
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
                if(IsContainerType(attributeType))
                {
                    if(IsGraphElementType(attributeType.ValueType))
                        return true;
                    if(attributeType.Kind == AttributeKind.MapAttr)
                    {
                        if(IsGraphElementType(attributeType.KeyType))
                            return true;
                    }
                }
            }
            return false;
        }

        private void CreateSchemaIfNotExistsOrAdaptToCompatibleChanges()
        {
            CreateIdentityAndTopologyTables();
            CreateTypesWithAttributeTables();

            if(ReadKnownTypes().Count == 0) // causes initial fill of the type mapping as side effect if types are available (general architecture: full table scans building memory structure (host graph with references), which is then used - thereafter, only changes are written to the database -- the types table memory structure is obtained by ReadKnownTypesWithAttributes)
                FillInitialTypes();
            else
                AdaptToTypeChanges(); // model update
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
            CreateAttributeTypesTable();
        }

        // fills types when model is empty, which only happens when database is freshly created (this also means the model mapping is empty)
        private void FillInitialTypes()
        {
            AddModelTypesToTypesTable();

            // TODO: create configuration and status table, a key-value-store, esp. including version, plus later stuff?

            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                CreateInheritanceTypeTable(nodeType, "nodeId");

                foreach(AttributeType attributeType in nodeType.AttributeTypes)
                {
                    if(!IsContainerType(attributeType))
                        continue;
                    CreateContainerTypeTable(nodeType, "nodeId", attributeType);
                }
            }

            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                CreateInheritanceTypeTable(edgeType, "edgeId");

                foreach(AttributeType attributeType in edgeType.AttributeTypes)
                {
                    if(!IsContainerType(attributeType))
                        continue;
                    CreateContainerTypeTable(edgeType, "edgeId", attributeType);
                }
            }

            foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
            {
                CreateInheritanceTypeTable(objectType, "objectId");

                foreach(AttributeType attributeType in objectType.AttributeTypes)
                {
                    if(!IsContainerType(attributeType))
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

        private void CreateAttributeTypesTable()
        {
            CreateTable("attributeTypes", "attributeTypeId",
                "typeId", "INTEGER NOT NULL",
                "attributeName", "TEXT NOT NULL",
                "xgrsType", "TEXT NOT NULL"
                );
            AddIndex("attributeTypes", "typeId");
        }

        private void CreateInheritanceTypeTable(InheritanceType type, String idColumnName)
        {
            String tableName = GetUniqueTableName(type.Package, type.Name);
            List<String> columnNamesAndTypes = new List<String>(); // ArrayBuilder
            foreach(AttributeType attributeType in type.AttributeTypes)
            {
                if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
                    continue;

                columnNamesAndTypes.Add(GetUniqueColumnName(attributeType.Name));
                columnNamesAndTypes.Add(ScalarAndReferenceTypeToSQLiteType(attributeType));
            }

            CreateTable(tableName, idColumnName, columnNamesAndTypes.ToArray()); // the id in the type table is a local copy of the global id from the corresponding topology table
        }

        private void CreateContainerTypeTable(InheritanceType type, String ownerIdColumnName, AttributeType attributeType)
        {
            String tableName = GetUniqueTableName(type.Package, type.Name, attributeType.Name);
            List<String> columnNamesAndTypes = new List<String>(); // ArrayBuilder

            columnNamesAndTypes.Add(ownerIdColumnName);
            columnNamesAndTypes.Add("INTEGER");
            columnNamesAndTypes.Add("command");
            columnNamesAndTypes.Add("INT"); // TINYINT doesn't work
            if(attributeType.Kind == AttributeKind.SetAttr) // todo: own function with type switch?
            {
                columnNamesAndTypes.Add("value");
                columnNamesAndTypes.Add(ScalarAndReferenceTypeToSQLiteType(attributeType.ValueType));
            }
            else if(attributeType.Kind == AttributeKind.MapAttr)
            {
                columnNamesAndTypes.Add("key");
                columnNamesAndTypes.Add(ScalarAndReferenceTypeToSQLiteType(attributeType.KeyType));
                columnNamesAndTypes.Add("value");
                columnNamesAndTypes.Add(ScalarAndReferenceTypeToSQLiteType(attributeType.ValueType));
            }
            else
            {
                columnNamesAndTypes.Add("value");
                columnNamesAndTypes.Add(ScalarAndReferenceTypeToSQLiteType(attributeType.ValueType));
                columnNamesAndTypes.Add("key");
                columnNamesAndTypes.Add(ScalarAndReferenceTypeToSQLiteType(IntegerAttributeType));
            }

            CreateTable(tableName, "entryId", columnNamesAndTypes.ToArray()); // the entryId denotes the row local to this table, the ownerIdColumnName is a local copy of the global id from the corresponding topology table

            AddIndex(tableName, ownerIdColumnName); // in order to delete without a full table scan (I assume small changesets in between database openings and decided for a by-default pruning on open - an alternative would be a full table replacement once in a while (would be ok in case of big changesets, as they would occur when a pruning run only occurs on explicit request, but such ones could be forgotten/missed unknowingly too easily, leading to (unexpected) slowness))
        }

        private void AddAttributeToInheritanceTypeTable(InheritanceType type, AttributeType attributeType)
        {
            String tableName = GetUniqueTableName(type.Package, type.Name);
            Debug.Assert(IsAttributeTypeMappedToDatabaseColumn(attributeType));
            String columnName = GetUniqueColumnName(attributeType.Name);
            String columnType = ScalarAndReferenceTypeToSQLiteType(attributeType);

            AddColumnToTable(tableName, columnName, columnType);
        }

        private void RemoveAttributeFromInheritanceTypeTable(string typeName, string attributeName)
        {
            String package;
            String name;
            UnpackPackagePrefixedName(typeName, out package, out name);
            String tableName = GetUniqueTableName(package, name);
            String columnName = GetUniqueColumnName(attributeName);

            DropColumnFromTable(tableName, columnName);
        }

        private void DeleteInheritanceTypeTable(string typeName)
        {
            String package;
            String name;
            UnpackPackagePrefixedName(typeName, out package, out name);
            String tableName = GetUniqueTableName(package, name);
 
            DropTable(tableName);
        }

        private void DeleteContainerTypeTable(string typeName, string attributeName)
        {
            String package;
            String name;
            UnpackPackagePrefixedName(typeName, out package, out name);
            String tableName = GetUniqueTableName(package, name, attributeName);

            DropTable(tableName);
        }

        // note that strings count as scalars
        private static bool IsScalarType(AttributeType attributeType)
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

        private static bool IsReferenceType(AttributeType attributeType)
        {
            switch(attributeType.Kind)
            {
                case AttributeKind.NodeAttr:
                case AttributeKind.EdgeAttr:
                case AttributeKind.GraphAttr:
                case AttributeKind.InternalClassObjectAttr:
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsContainerType(AttributeType attributeType)
        {
            switch(attributeType.Kind)
            {
                case AttributeKind.SetAttr:
                case AttributeKind.MapAttr:
                case AttributeKind.ArrayAttr:
                case AttributeKind.DequeAttr:
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsContainerType(String attributeType)
        {
            return TypesHelper.IsContainerType(attributeType);
        }

        private static bool IsAttributeTypeMappedToDatabaseColumn(AttributeType attributeType)
        {
            return IsScalarType(attributeType) || IsReferenceType(attributeType); // containers are not referenced by an id in a database column, but are mapped entirely to own tables
        }

        // types appearing as attributes with a complete implementation for loading/storing them from/to the database
        private static bool IsSupportedAttributeType(AttributeType attributeType)
        {
            return IsScalarType(attributeType) || IsReferenceType(attributeType) || IsContainerType(attributeType); // TODO: external/object type - also handle these.
        }

        private static bool IsGraphType(AttributeType attributeType)
        {
            return attributeType.Kind == AttributeKind.GraphAttr;
        }

        private static bool IsObjectType(AttributeType attributeType)
        {
            return attributeType.Kind == AttributeKind.InternalClassObjectAttr;
        }

        private static bool IsGraphElementType(AttributeType attributeType)
        {
            return attributeType.Kind == AttributeKind.NodeAttr || attributeType.Kind == AttributeKind.EdgeAttr;
        }

        private string ScalarAndReferenceTypeToSQLiteType(AttributeType attributeType)
        {
            //Debug.Assert(IsAttributeTypeMappedToDatabaseColumn(attributeType)); // scalar and reference types are mapped to a database column, of the type specified here; container types have a complex mapping to own tables
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
                case AttributeKind.GraphAttr: return "INTEGER"; 
                case AttributeKind.InternalClassObjectAttr: return "INTEGER";
                case AttributeKind.NodeAttr: return "INTEGER";
                case AttributeKind.EdgeAttr: return "INTEGER";
                default: throw new Exception("Attribute type must be a scalar type or reference type");
            }
        }

        private object DefaultValue(AttributeType attributeType)
        {
            switch(attributeType.Kind)
            {
                case AttributeKind.ByteAttr: return (SByte)0;
                case AttributeKind.ShortAttr: return (short)0;
                case AttributeKind.IntegerAttr: return 0;
                case AttributeKind.LongAttr: return 0L;
                case AttributeKind.BooleanAttr: return 0;
                case AttributeKind.StringAttr: return "";
                case AttributeKind.EnumAttr: IEnumerator<EnumMember> it = attributeType.EnumType.Members.GetEnumerator(); it.MoveNext(); return it.Current.Name;
                case AttributeKind.FloatAttr: return 0.0f;
                case AttributeKind.DoubleAttr: return 0.0;
                case AttributeKind.GraphAttr: return (object)DBNull.Value;
                case AttributeKind.InternalClassObjectAttr: return (object)DBNull.Value;
                case AttributeKind.NodeAttr: return (object)DBNull.Value;
                case AttributeKind.EdgeAttr: return (object)DBNull.Value;
                default: throw new Exception("Attribute type must be a scalar type or reference type");
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

            createSchemaCommand.Transaction = transaction;
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

            createIndexCommand.Transaction = transaction;
            int rowsAffected = createIndexCommand.ExecuteNonQuery();

            createIndexCommand.Dispose();
        }

        private void AddColumnToTable(String tableName, String columnName, String columnType)
        {
            StringBuilder command = new StringBuilder();
            command.Append("ALTER TABLE ");
            command.Append(tableName);
            command.Append(" ADD COLUMN ");
            command.Append(columnName);
            command.Append(" ");
            command.Append(columnType);
            SQLiteCommand addColumnCommand = new SQLiteCommand(command.ToString(), connection);

            addColumnCommand.Transaction = transaction;
            int rowsAffected = addColumnCommand.ExecuteNonQuery();

            addColumnCommand.Dispose();
        }

        private void DropColumnFromTable(String tableName, String columnName)
        {
            StringBuilder command = new StringBuilder();
            command.Append("ALTER TABLE ");
            command.Append(tableName);
            command.Append(" DROP COLUMN ");
            command.Append(columnName);
            SQLiteCommand dropColumnCommand = new SQLiteCommand(command.ToString(), connection);

            dropColumnCommand.Transaction = transaction;
            int rowsAffected = dropColumnCommand.ExecuteNonQuery();

            dropColumnCommand.Dispose();
        }

        private void DropTable(String tableName)
        {
            StringBuilder command = new StringBuilder();
            command.Append("DROP TABLE ");
            command.Append(tableName);
            SQLiteCommand createSchemaCommand = new SQLiteCommand(command.ToString(), connection);

            createSchemaCommand.Transaction = transaction;
            int rowsAffected = createSchemaCommand.ExecuteNonQuery();

            createSchemaCommand.Dispose();
        }

        #region Types table populating/handling

        private void AdaptToTypeChanges()
        {
            // the types mapping was filled from the database before, it is used during the model update, it is updated stepwise with new model, and afterwards fits to the new model
            // the types from the database are read once, used in the model update, and thrown away after the update (the old model from the database is the source, the target is the new model from the graph, defined by the model file/assembly)
            TypesFromDatabase typesFromDatabase = new TypesFromDatabase(ReadKnownTypesWithAttributes());

            Dictionary<InheritanceType, SetValueType> newModelTypes = GetNewModelTypes(typesFromDatabase);
            Dictionary<String, TypeKind> deletedModelTypes = GetDeletedModelTypes(typesFromDatabase);
            Dictionary<InheritanceType, TypeKind> typeChangedModelTypes = GetTypeChangedModelTypes(typesFromDatabase);
            bool changeMessagePrinted = false;
            DetermineAndReportChangesToUser(newModelTypes, deletedModelTypes, typeChangedModelTypes,
                typesFromDatabase, ref changeMessagePrinted);

            Dictionary<string, InheritanceType> keptModelTypes = GetKeptModelTypes(typesFromDatabase);
            List<KeyValuePair<InheritanceType, AttributeType>> addedAttributes = new List<KeyValuePair<InheritanceType, AttributeType>>();
            List<KeyValuePair<string, AttributeTypeFromDatabase>> removedAttributes = new List<KeyValuePair<string, AttributeTypeFromDatabase>>();
            List<KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>> typeChangedAttributes = new List<KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>>();
            foreach(KeyValuePair<string, InheritanceType> typeNameToType in keptModelTypes)
            {
                DetermineAndReportAttributeChangesToTheUser(typeNameToType.Value, typesFromDatabase[typeNameToType.Key],
                    ref addedAttributes, ref removedAttributes, ref typeChangedAttributes, ref changeMessagePrinted);
            }

            // no model changes
            if(!changeMessagePrinted)
            {
                InsertGraphIfMissing(HOST_GRAPH_ID, TypeNameToDbId["graph"]);
                return;
            }

            bool adaptDatabaseModelToModel = QueryUserWhetherToAdaptDatabaseToModel(removedAttributes, typeChangedAttributes);
            if(!adaptDatabaseModelToModel)
            {
                WriteDatabaseModelToFileAndReportToUser(typesFromDatabase);
                ConsoleUI.outWriter.WriteLine("Aborting database model update as requested...");
                throw new Exception("Abort database model update as requested.");
            }

            ConsoleUI.outWriter.WriteLine("Overall there are {0} new types, {1} deleted types, {2} types of a different kind, {3} added attributes, {4} removed attributes, {5} attributes of a different type in the model compared to the database.",
                newModelTypes.Count, deletedModelTypes.Count, typeChangedModelTypes.Count, addedAttributes.Count, removedAttributes.Count, typeChangedAttributes.Count);
            ConsoleUI.outWriter.WriteLine("The database is going to be updated: {0} types are going to be added, {1} types are going to be deleted, {2} attributes are going to be added, {3} attributes are going to be removed.",
                newModelTypes.Count + typeChangedModelTypes.Count, deletedModelTypes.Count + typeChangedModelTypes.Count,
                addedAttributes.Count + typeChangedAttributes.Count, removedAttributes.Count + typeChangedAttributes.Count);

            Stopwatch stopwatch = Stopwatch.StartNew();
            ConsoleUI.outWriter.WriteLine("Updating the database model to the current model...");

            // TODO: open transaction to carry out changes and commit at end when migration completed
            // TODO: check that transaction is set in new database operations called from this method on; check that using is used as needed, or dispose called
            RemoveDeletedModelTypes(deletedModelTypes, typesFromDatabase);
            RemoveAndAddTypeChangedModelTypes(typeChangedModelTypes, typesFromDatabase);
            AddNewModelTypes(newModelTypes);

            foreach(KeyValuePair<string, AttributeTypeFromDatabase> removedAttribute in removedAttributes)
            {
                RemoveDeletedModelTypeAttribute(removedAttribute.Key, removedAttribute.Value);
            }
            foreach(KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>> typeChangedAttribute in typeChangedAttributes)
            {
                RemoveDeletedModelTypeAttribute(typeChangedAttribute.Key.PackagePrefixedName, typeChangedAttribute.Value.Value);
                AddNewModelTypeAttribute(typeChangedAttribute.Key, typeChangedAttribute.Value.Key);
            }
            foreach(KeyValuePair<InheritanceType, AttributeType> addedAttribute in addedAttributes)
            {
                AddNewModelTypeAttribute(addedAttribute.Key, addedAttribute.Value);
            }

            stopwatch.Stop();
            ConsoleUI.outWriter.WriteLine("...done (database model updated in {0} ms).", stopwatch.ElapsedMilliseconds);

            InsertGraphIfMissing(HOST_GRAPH_ID, TypeNameToDbId["graph"]);
        }

        private Dictionary<InheritanceType, SetValueType> GetNewModelTypes(TypesFromDatabase typesFromDatabase)
        {
            Dictionary<InheritanceType, SetValueType> newModelTypes = new Dictionary<InheritanceType, SetValueType>();

            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                if(!typesFromDatabase.IsTypeKnown(nodeType))
                    newModelTypes.Add(nodeType, null);
            }

            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                if(!typesFromDatabase.IsTypeKnown(edgeType))
                    newModelTypes.Add(edgeType, null);
            }

            foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
            {
                if(!typesFromDatabase.IsTypeKnown(objectType))
                    newModelTypes.Add(objectType, null);
            }

            return newModelTypes;
        }

        private Dictionary<string, TypeKind> GetDeletedModelTypes(TypesFromDatabase typesFromDatabase)
        {
            Dictionary<string, TypeKind> deletedModelTypes = new Dictionary<string, TypeKind>();

            Dictionary<string, InheritanceType> allModelTypes = GetAllModelTypes();
            foreach(KeyValuePair<string, AttributeTypesFromDatabase> typeNameToKind in typesFromDatabase.TypesToAttributesFromDatabase)
            {
                if(!allModelTypes.ContainsKey(typeNameToKind.Key))
                    deletedModelTypes.Add(typeNameToKind.Key, typeNameToKind.Value.KindOfOwner);
            }

            return deletedModelTypes;
        }

        private Dictionary<string, InheritanceType> GetAllModelTypes()
        {
            Dictionary<string, InheritanceType> allModelTypes = new Dictionary<string, InheritanceType>();

            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                allModelTypes.Add(nodeType.PackagePrefixedName, nodeType);
            }

            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                allModelTypes.Add(edgeType.PackagePrefixedName, edgeType);
            }

            foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
            {
                allModelTypes.Add(objectType.PackagePrefixedName, objectType);
            }

            return allModelTypes;
        }

        Dictionary<InheritanceType, TypeKind> GetTypeChangedModelTypes(TypesFromDatabase typesFromDatabase)
        {
            Dictionary<InheritanceType, TypeKind> typeChangedModelTypes = new Dictionary<InheritanceType, TypeKind>();

            Dictionary<string, InheritanceType> allModelTypes = GetAllModelTypes();
            foreach(KeyValuePair<string, AttributeTypesFromDatabase> typeNameToKind in typesFromDatabase.TypesToAttributesFromDatabase)
            {
                if(allModelTypes.ContainsKey(typeNameToKind.Key))
                {
                    if(typeNameToKind.Value.KindOfOwner == TypeKind.NodeClass && !(allModelTypes[typeNameToKind.Key] is NodeType))
                        typeChangedModelTypes.Add(allModelTypes[typeNameToKind.Key], typeNameToKind.Value.KindOfOwner);
                    else if(typeNameToKind.Value.KindOfOwner == TypeKind.EdgeClass && !(allModelTypes[typeNameToKind.Key] is EdgeType))
                        typeChangedModelTypes.Add(allModelTypes[typeNameToKind.Key], typeNameToKind.Value.KindOfOwner);
                    else if(typeNameToKind.Value.KindOfOwner == TypeKind.ObjectClass && !(allModelTypes[typeNameToKind.Key] is ObjectType))
                        typeChangedModelTypes.Add(allModelTypes[typeNameToKind.Key], typeNameToKind.Value.KindOfOwner);
                }
            }

            return typeChangedModelTypes;
        }

        Dictionary<string, InheritanceType> GetKeptModelTypes(TypesFromDatabase typesFromDatabase)
        {
            Dictionary<string, InheritanceType> keptModelTypes = new Dictionary<string, InheritanceType>();

            Dictionary<string, InheritanceType> allModelTypes = GetAllModelTypes();
            foreach(KeyValuePair<string, AttributeTypesFromDatabase> typeNameToKind in typesFromDatabase.TypesToAttributesFromDatabase)
            {
                if(allModelTypes.ContainsKey(typeNameToKind.Key))
                {
                    if(typeNameToKind.Value.KindOfOwner == TypeKind.NodeClass && allModelTypes[typeNameToKind.Key] is NodeType)
                        keptModelTypes.Add(typeNameToKind.Key, allModelTypes[typeNameToKind.Key]);
                    else if(typeNameToKind.Value.KindOfOwner == TypeKind.EdgeClass && allModelTypes[typeNameToKind.Key] is EdgeType)
                        keptModelTypes.Add(typeNameToKind.Key, allModelTypes[typeNameToKind.Key]);
                    else if(typeNameToKind.Value.KindOfOwner == TypeKind.ObjectClass && allModelTypes[typeNameToKind.Key] is ObjectType)
                        keptModelTypes.Add(typeNameToKind.Key, allModelTypes[typeNameToKind.Key]);
                }
            }

            return keptModelTypes;
        }

        // todo: consistency -- most messages based on database first perspective (old state of model, current state of data), variable names based on model first perspective (current state of model, new state targeted for data)
        private void DetermineAndReportChangesToUser(Dictionary<InheritanceType, SetValueType> newModelTypes,
            Dictionary<String, TypeKind> deletedModelTypes, Dictionary<InheritanceType, TypeKind> typeChangedModelTypes,
            TypesFromDatabase typesFromDatabase, ref bool changeMessagePrinted)
        {
            bool deletedTypeStillHasInstances = false;
            foreach(KeyValuePair<String, TypeKind> typeNameToKind in deletedModelTypes)
            {
                string typeName = typeNameToKind.Key;
                TypeKind typeKind = typeNameToKind.Value;
                string kind = ToString(typeKind);
                string idColumnName = ToIdColumnName(typeKind);

                if(TypeHasInstances(typeName, idColumnName))
                {
                    PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                    ConsoleUI.outWriter.WriteLine("The {0} class type {1} from the database is not existing in the model.",
                        kind, typeName);
                    ConsoleUI.outWriter.WriteLine("- The {0} class type {1} has to be deleted from the database, but the database contains instances of it (aborting...).",
                        kind, typeName);
                    deletedTypeStillHasInstances = true;
                }
            }

            foreach(KeyValuePair<InheritanceType, TypeKind> typeToKind in typeChangedModelTypes)
            {
                string typeName = typeToKind.Key.PackagePrefixedName;
                TypeKind typeKind = typeToKind.Value;
                string kind = ToString(typeKind);
                string idColumnName = ToIdColumnName(typeKind);
                string typeKindModel = ToKindString(typeToKind.Key);

                if(TypeHasInstances(typeName, idColumnName))
                {
                    PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                    ConsoleUI.outWriter.WriteLine("The {0} class type {1} from the database is a {2} class type in the model.",
                        kind, typeName, typeKindModel);
                    ConsoleUI.outWriter.WriteLine("- The {0} class type {1} has to be deleted from the database, but the database contains instances of it (aborting...).",
                        kind, typeName);
                    deletedTypeStillHasInstances = true;
                }
            }

            if(deletedTypeStillHasInstances)
            {
                ConsoleUI.outWriter.WriteLine("The model used by the database contains instances of types that don't exist anymore in the model file/the model assembly.");
                ConsoleUI.outWriter.WriteLine("You must delete them first - to do so you must open the database with the old model (if you just deleted them and no dangling references exist, just opening the database with the old model may be sufficient, due to the garbage collection run in the begining).");
                ConsoleUI.outWriter.WriteLine("The database cannot be opened with the current model.");
                WriteDatabaseModelToFileAndReportToUser(typesFromDatabase);
                ConsoleUI.outWriter.WriteLine("Aborting database model update due to an incompatible model...");
                throw new Exception("Abort database model update due to an incompatible model.");
            }

            foreach(KeyValuePair<String, TypeKind> typeNameToKind in deletedModelTypes)
            {
                string typeName = typeNameToKind.Key;
                TypeKind typeKind = typeNameToKind.Value;
                string kind = ToString(typeKind);
                string idColumnName = ToIdColumnName(typeKind);

                PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                ConsoleUI.outWriter.WriteLine("The {0} class type {1} from the database is not existing in the model (anymore).",
                    kind, typeName);
                ConsoleUI.outWriter.WriteLine("- The {0} class type {1} is going to be deleted from the database, too.",
                    kind, typeName);
            }

            foreach(KeyValuePair<InheritanceType, TypeKind> typeToKind in typeChangedModelTypes)
            {
                string typeName = typeToKind.Key.PackagePrefixedName;
                TypeKind typeKind = typeToKind.Value;
                string kind = ToString(typeKind);
                string idColumnName = ToIdColumnName(typeKind);
                string typeKindModel = ToKindString(typeToKind.Key);

                PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                ConsoleUI.outWriter.WriteLine("The {0} class type {1} from the database is a {2} class type in the model.",
                    kind, typeName, typeKindModel);
                ConsoleUI.outWriter.WriteLine("- The {0} class type {1} is going to be deleted from the database.",
                    kind, typeName);
                ConsoleUI.outWriter.WriteLine("- The {0} class type {1} is going to be added to the database.",
                    typeKindModel, typeName);
            }

            foreach(InheritanceType type in newModelTypes.Keys)
            {
                string typeName = type.PackagePrefixedName;
                string typeKindModel = ToKindString(type);

                PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                ConsoleUI.outWriter.WriteLine("The database does not contain the {0} class type {1} (that exists in the model).",
                    typeKindModel, typeName);
                ConsoleUI.outWriter.WriteLine("- The {0} class type {1} is going to be added to the database, too.",
                    typeKindModel, typeName);
            }
        }

        private void PrintChangeMessageAsNeeded(ref bool changeMessagePrinted)
        {
            if(!changeMessagePrinted)
            {
                changeMessagePrinted = true;
                ConsoleUI.outWriter.WriteLine("The model used by the database differs from the model file/the model assembly.");
            }
        }

        private void DetermineAndReportAttributeChangesToTheUser(InheritanceType type, AttributeTypesFromDatabase attributes,
            ref List<KeyValuePair<InheritanceType, AttributeType>> addedAttributes,
            ref List<KeyValuePair<string, AttributeTypeFromDatabase>> removedAttributes,
            ref List<KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>> typeChangedAttributes,
            ref bool changeMessagePrinted)
        {
            foreach(KeyValuePair<string, AttributeTypeFromDatabase> attributeNameToType in attributes.AttributeNamesToAttributeTypes)
            {
                string attributeName = attributeNameToType.Key;
                AttributeTypeFromDatabase attributeType = attributeNameToType.Value;

                AttributeType attributeTypeFromModel = type.GetAttributeType(attributeNameToType.Key);
                if(attributeTypeFromModel == null)
                {
                    PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                    ConsoleUI.outWriter.WriteLine("The attribute {1}.{0} from the database is not existing in the model (anymore).",
                        attributeName, type.PackagePrefixedName);
                    ConsoleUI.outWriter.WriteLine("- The attribute {1}.{0} is going to be removed from the database, too, and all its values are going to be purged (from the instances of the type), you will loose information!",
                        attributeName, type.PackagePrefixedName);

                    removedAttributes.Add(new KeyValuePair<string, AttributeTypeFromDatabase>(type.PackagePrefixedName, attributeType));
                }
                else if(attributeType.XgrsType != TypesHelper.AttributeTypeToXgrsType(attributeTypeFromModel))
                {
                    PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                    ConsoleUI.outWriter.WriteLine("The attribute {1}.{0} of type {2} from the database is of type {3} in the model!",
                        attributeName, type.PackagePrefixedName, attributeType.XgrsType, TypesHelper.AttributeTypeToXgrsType(attributeTypeFromModel));
                    ConsoleUI.outWriter.WriteLine("- The attribute {1}.{0} is going to be removed from the database, and all its values are going to be purged (from the instances of the type), you will loose information!",
                        attributeName, type.PackagePrefixedName);
                    ConsoleUI.outWriter.WriteLine("- The attribute {1}.{0} is going to be added in the database, it is going to be initialized to the default value of the attribute type (in the instances of the type - but not the default value specified in the class).",
                        attributeTypeFromModel.Name, type.PackagePrefixedName);

                    typeChangedAttributes.Add(new KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>(type, new KeyValuePair<AttributeType, AttributeTypeFromDatabase>(attributeTypeFromModel, attributeType)));
                }
            }

            foreach(AttributeType attributeTypeFromModel in type.AttributeTypes)
            {
                if(!attributes.AttributeNamesToAttributeTypes.ContainsKey(attributeTypeFromModel.Name))
                {
                    PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                    ConsoleUI.outWriter.WriteLine("The type {0} does not contain the attribute {1} in the database (that exists in the model).",
                        type.PackagePrefixedName, attributeTypeFromModel.Name);
                    ConsoleUI.outWriter.WriteLine("- The attribute {1}.{0} is going to be added in the database, too, it is going to be initialized to the default value of the attribute type (in the instances of the type - but not the default value specified in the class).",
                        attributeTypeFromModel.Name, type.PackagePrefixedName);

                    addedAttributes.Add(new KeyValuePair<InheritanceType, AttributeType>(type, attributeTypeFromModel));
                }
            }
        }

        private bool QueryUserWhetherToAdaptDatabaseToModel(List<KeyValuePair<string, AttributeTypeFromDatabase>> removedAttributes, List<KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>> typeChangedAttributes)
        {
            List<KeyValuePair<string, AttributeTypeFromDatabase>> removedAttributesAfterFiltering = new List<KeyValuePair<string, AttributeTypeFromDatabase>>(removedAttributes);
            List<KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>> typeChangedAttributesAfterFiltering = new List<KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>>(typeChangedAttributes);

            if(persistentGraphParameters != null)
            {
                // intended general format for persistentGraphParameters: "command;command", command may be update, update/type.attr, or later key=val -- the update parameters allow for automatic migration with a script
                string[] unpackedParameters = persistentGraphParameters.Split(';');
                foreach(string parameter in unpackedParameters)
                {
                    if(parameter == "update")
                    {
                        removedAttributesAfterFiltering.Clear();
                        typeChangedAttributesAfterFiltering.Clear();
                        break;
                    }
                    else if(parameter.StartsWith("update/"))
                    {
                        String updateAttribute = parameter.Remove(0, "update/".Length);
                        String[] typeAttribute = updateAttribute.Split('.');
                        if(typeAttribute.Length != 2)
                        {
                            ConsoleUI.errorOutWriter.WriteLine("Ignoring malformed update command: " + parameter + " ('update/type.attr' expected)");
                            continue;
                        }
                        string type = typeAttribute[0];
                        string attribute = typeAttribute[1];
                        RemoveAttribute(removedAttributesAfterFiltering, typeChangedAttributesAfterFiltering, type, attribute);
                    }
                }
            }

            if(removedAttributesAfterFiltering.Count + typeChangedAttributesAfterFiltering.Count > 0)
            {
                ConsoleUI.outWriter.WriteLine("Proceed only if you already carried out all migrations you wanted to, otherwise you would loose information (when removing attributes/purging their values) - not proceeding would allow you to revert the model and save the attribute values of worth from the database.");
                ConsoleUI.outWriter.WriteLine("Do you want to proceed? (y[es]/n[o])");
                ConsoleKeyInfo key = ConsoleUI.consoleIn.ReadKeyWithControlCAsInput();
                if(key.KeyChar != 'y' && key.KeyChar != 'Y')
                    return false;

                ConsoleUI.outWriter.WriteLine("Are you sure? (y[es]/n[o])");
                key = ConsoleUI.consoleIn.ReadKeyWithControlCAsInput();
                if(key.KeyChar != 'y' && key.KeyChar != 'Y')
                    return false;
            }

            return true;
        }

        private void RemoveAttribute(List<KeyValuePair<string, AttributeTypeFromDatabase>> removedAttributesAfterFiltering,
            List<KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>> typeChangedAttributesAfterFiltering,
            string type, string attribute)
        {
            for(int i = 0; i < removedAttributesAfterFiltering.Count; ++i)
            {
                if(removedAttributesAfterFiltering[i].Key == type && removedAttributesAfterFiltering[i].Value.Name == attribute)
                {
                    removedAttributesAfterFiltering.RemoveAt(i);
                    return;
                }
            }

            for(int i = 0; i < typeChangedAttributesAfterFiltering.Count; ++i)
            {
                if(typeChangedAttributesAfterFiltering[i].Key.PackagePrefixedName == type && typeChangedAttributesAfterFiltering[i].Value.Key.Name == attribute)
                {
                    typeChangedAttributesAfterFiltering.RemoveAt(i);
                    break;
                }
            }
        }

        private void WriteDatabaseModelToFileAndReportToUser(TypesFromDatabase typesFromDatabase)
        {
            String filename = "partial_flattened_model_from_" + ExtractDataSourceName() + ".gm";
            using(StreamWriter sw = new StreamWriter(filename, false, System.Text.Encoding.UTF8))
            {
                typesFromDatabase.WriteDatabaseModelToFile(sw);
            }
            ConsoleUI.outWriter.WriteLine("Written file " + filename + " showing the partial flattened model expected by the host graph in the database.");
        }

        private string ExtractDataSourceName()
        {
            string sourceName = "dummyfilename";

            string[] parameters = connectionParameters.Split(';');
            foreach(string parameter in parameters)
            {
                if(parameter.StartsWith("source", true, System.Globalization.CultureInfo.InvariantCulture)
                    || parameter.StartsWith("datasource", true, System.Globalization.CultureInfo.InvariantCulture)
                    || parameter.StartsWith("data source", true, System.Globalization.CultureInfo.InvariantCulture))
                {
                    string[] keywordAndValue = parameter.Split('=');
                    if(keywordAndValue.Length == 2)
                    {
                        string path = keywordAndValue[1].Trim();
                        return Path.GetFileName(path);
                    }
                }
            }

            return sourceName;
        }

        private void AddNewModelTypes(Dictionary<InheritanceType, SetValueType> newModelTypes)
        {
            foreach(InheritanceType entityType in newModelTypes.Keys)
            {
                using(SQLiteCommand fillTypeCommand = GetFillTypeCommand())
                {
                    FillTypeWithAttributes(fillTypeCommand, entityType, ToKind(entityType));
                }

                String idColumnName = ToIdColumnName(entityType);

                CreateInheritanceTypeTable(entityType, idColumnName);

                foreach(AttributeType attributeType in entityType.AttributeTypes)
                {
                    if(!IsContainerType(attributeType))
                        continue;
                    CreateContainerTypeTable(entityType, idColumnName, attributeType);
                }
            }
        }

        private void RemoveDeletedModelTypes(Dictionary<String, TypeKind> deletedModelTypes, TypesFromDatabase typesFromDatabase)
        {
            // delete nodes, no instances left because of previous check, nothing in topology tables, nothing in types tables, nothing in container tables(?)
            foreach(KeyValuePair<String, TypeKind> typeNameToKind in deletedModelTypes)
            {
                using(SQLiteCommand removeTypeFromAttributeTypesCommand = PrepareTopologyDelete("attributeTypes", "typeId"))
                {
                    RemoveAttributeTypes(removeTypeFromAttributeTypesCommand, TypeNameToDbId[typeNameToKind.Key]);
                }
                using(SQLiteCommand removeTypeCommand = PrepareTopologyDelete("types", "typeId"))
                {
                    RemoveType(removeTypeCommand, TypeNameToDbId[typeNameToKind.Key]);
                }
                RemoveTypeNameFromDbIdMapping(typeNameToKind.Key);

                foreach(KeyValuePair<string, AttributeTypeFromDatabase> attributeType in typesFromDatabase[typeNameToKind.Key].AttributeNamesToAttributeTypes)
                {
                    if(IsContainerType(attributeType.Value.XgrsType))
                        DeleteContainerTypeTable(typeNameToKind.Key, attributeType.Key);
                }
                DeleteInheritanceTypeTable(typeNameToKind.Key);
            }
        }

        private void RemoveAndAddTypeChangedModelTypes(Dictionary<InheritanceType, TypeKind> typeChangedModelTypes, TypesFromDatabase typesFromDatabase)
        {
            Dictionary<string, TypeKind> deletedModelTypes = new Dictionary<string, TypeKind>();
            Dictionary<InheritanceType, SetValueType> newModelTypes = new Dictionary<InheritanceType, SetValueType>();
            foreach(KeyValuePair<InheritanceType, TypeKind> typeChangedModelTypeWithKind in typeChangedModelTypes)
            {
                InheritanceType typeChangedModelType = typeChangedModelTypeWithKind.Key;
                TypeKind kind = typeChangedModelTypeWithKind.Value;

                deletedModelTypes.Add(typeChangedModelType.PackagePrefixedName, kind);
                newModelTypes.Add(typeChangedModelType, null);
            }

            RemoveDeletedModelTypes(deletedModelTypes, typesFromDatabase);
            AddNewModelTypes(newModelTypes);
        }

        private void AddNewModelTypeAttribute(InheritanceType type, AttributeType attributeType)
        {
            // add new attribute to the attributeTypes table (corresponding type)
            using(SQLiteCommand fillAttributeTypeCommand = GetFillAttributeTypeCommand())
            {
                FillAttributeType(fillAttributeTypeCommand, attributeType, type);
            }

            // add the attribute column to the table of the according type or
            // if the type of the attribute is a container type, add a container table for the attribute
            // and write the default value of the type
            if(IsContainerType(attributeType))
            {
                string ownerIdColumnName = ToIdColumnName(type);
                CreateContainerTypeTable(type, ownerIdColumnName, attributeType);

                using(SQLiteCommand initializingInsert = PrepareContainerInitializingInsert(type, ownerIdColumnName, attributeType))
                {
                    initializingInsert.Transaction = transaction;
                    int rowsAffected = initializingInsert.ExecuteNonQuery();
                }
            }
            else
            {
                AddAttributeToInheritanceTypeTable(type, attributeType);
                using(SQLiteCommand updateCommand = PrepareUpdate(type, null, attributeType))
                {
                    updateCommand.Parameters.Clear();
                    updateCommand.Parameters.AddWithValue("@" + GetUniqueColumnName(attributeType.Name), DefaultValue(attributeType));

                    updateCommand.Transaction = transaction;
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                }
            }
        }

        private void RemoveDeletedModelTypeAttribute(string typeName, AttributeTypeFromDatabase attributeType)
        {
            // remove from the attributeTypes table
            using(SQLiteCommand removeTypeFromAttributeTypesCommand = PrepareTopologyDelete("attributeTypes", "attributeTypeId"))
            {
                RemoveAttributeType(removeTypeFromAttributeTypesCommand, attributeType.DbId);
            }

            // remove the attribute column from the table of the according type or
            // if the type of the attribute is a container type, remove the container table for the attribute
            if(IsContainerType(attributeType.XgrsType))
                DeleteContainerTypeTable(typeName, attributeType.Name);
            else
                RemoveAttributeFromInheritanceTypeTable(typeName, attributeType.Name);
        }

        private void AddModelTypesToTypesTable()
        {
            using(SQLiteCommand fillTypeCommand = GetFillTypeCommand())
            {
                FillTypeWithAttributes(fillTypeCommand, new GraphClassDummy(), TypeKind.GraphClass);

                foreach(NodeType nodeType in graph.Model.NodeModel.Types)
                {
                    FillTypeWithAttributes(fillTypeCommand, nodeType, TypeKind.NodeClass);
                }
                foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
                {
                    FillTypeWithAttributes(fillTypeCommand, edgeType, TypeKind.EdgeClass);
                }
                foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
                {
                    FillTypeWithAttributes(fillTypeCommand, objectType, TypeKind.ObjectClass);
                }
            }

            InsertGraphIfMissing(HOST_GRAPH_ID, TypeNameToDbId["graph"]);
        }

        private void FillTypeWithAttributes(SQLiteCommand fillTypeCommand, InheritanceType type, TypeKind kind)
        {
            FillType(fillTypeCommand, type, kind);
            FillAttributeTypes(type);
        }

        private void FillType(SQLiteCommand fillTypeCommand, InheritanceType type, TypeKind kind)
        {
            fillTypeCommand.Parameters.Clear();
            fillTypeCommand.Parameters.AddWithValue("@kind", (int)kind);
            fillTypeCommand.Parameters.AddWithValue("@name", type.PackagePrefixedName); // may include colons which are removed from the table name

            fillTypeCommand.Transaction = transaction;
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

        private void FillAttributeTypes(InheritanceType type)
        {
            using(SQLiteCommand fillAttributeTypeCommand = GetFillAttributeTypeCommand())
            {
                foreach(AttributeType attributeType in type.AttributeTypes)
                {
                    FillAttributeType(fillAttributeTypeCommand, attributeType, type);
                }
            }
        }

        private void RemoveAttributeTypes(SQLiteCommand removeTypeFromAttributeTypesCommand, long dbid)
        {
            removeTypeFromAttributeTypesCommand.Parameters.Clear();
            removeTypeFromAttributeTypesCommand.Parameters.AddWithValue("@typeId", dbid);

            removeTypeFromAttributeTypesCommand.Transaction = transaction;
            int rowsAffected = removeTypeFromAttributeTypesCommand.ExecuteNonQuery();
        }

        private void RemoveAttributeType(SQLiteCommand removeAttributeTypeFromAttributeTypesCommand, long dbid)
        {
            removeAttributeTypeFromAttributeTypesCommand.Parameters.Clear();
            removeAttributeTypeFromAttributeTypesCommand.Parameters.AddWithValue("@attributeTypeId", dbid);

            removeAttributeTypeFromAttributeTypesCommand.Transaction = transaction;
            int rowsAffected = removeAttributeTypeFromAttributeTypesCommand.ExecuteNonQuery();
        }

        private void RemoveType(SQLiteCommand removeTypeCommand, long dbid)
        {
            removeTypeCommand.Parameters.Clear();
            removeTypeCommand.Parameters.AddWithValue("@typeId", dbid);

            removeTypeCommand.Transaction = transaction;
            int rowsAffected = removeTypeCommand.ExecuteNonQuery();
        }

        private long FillAttributeType(SQLiteCommand fillAttributeTypeCommand, AttributeType attributeType, InheritanceType type)
        {
            fillAttributeTypeCommand.Parameters.Clear();
            fillAttributeTypeCommand.Parameters.AddWithValue("@typeId", TypeNameToDbId[type.PackagePrefixedName]);
            fillAttributeTypeCommand.Parameters.AddWithValue("@attributeName", attributeType.Name);
            fillAttributeTypeCommand.Parameters.AddWithValue("@xgrsType", TypesHelper.AttributeTypeToXgrsType(attributeType));

            fillAttributeTypeCommand.Transaction = transaction;
            int rowsAffected = fillAttributeTypeCommand.ExecuteNonQuery();

            long rowId = connection.LastInsertRowId; // attributeTypeId
            return rowId;
        }

        private SQLiteCommand GetFillAttributeTypeCommand()
        {
            String tableName = "attributeTypes";
            StringBuilder columnNames = new StringBuilder();
            StringBuilder parameterNames = new StringBuilder();
            AddInsertParameter(columnNames, parameterNames, "typeId");
            AddInsertParameter(columnNames, parameterNames, "attributeName");
            AddInsertParameter(columnNames, parameterNames, "xgrsType");

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
            // the grgen type id from the model is only unique per kind, and not stable upon insert(/delete), so we have to match by name to map to the database type id

            Dictionary<string, TypeKind> typeNameToKind = new Dictionary<string, TypeKind>();

            using(SQLiteCommand command = PrepareStatementsForReadingKnownTypes())
            {
                command.Transaction = transaction;
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    Dictionary<string, int> nameToColumnIndex = GetNameToColumnIndexMapping(reader);
                    while(reader.Read())
                    {
                        long typeId = reader.GetInt64(nameToColumnIndex["typeId"]);
                        int kind = reader.GetInt32(nameToColumnIndex["kind"]);
                        String name = reader.GetString(nameToColumnIndex["name"]);

                        typeNameToKind.Add(name, (TypeKind)kind); // note that the graph type is included here, in contrast to ReadKnownTypesWithAttributes

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

        private Dictionary<string, AttributeTypesFromDatabase> ReadKnownTypesWithAttributes()
        {
            Dictionary<string, AttributeTypesFromDatabase> typeNameToAttributeTypes = new Dictionary<string, AttributeTypesFromDatabase>();

            using(SQLiteCommand command = PrepareStatementsForReadingKnownTypesWithAttributes())
            {
                command.Transaction = transaction;
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    Dictionary<string, int> nameToColumnIndex = GetNameToColumnIndexMapping(reader);
                    while(reader.Read())
                    {
                        long typeId = reader.GetInt64(nameToColumnIndex["typeId"]);
                        int kind = reader.GetInt32(nameToColumnIndex["kind"]);
                        string name = reader.GetString(nameToColumnIndex["name"]);

                        AttributeTypeFromDatabase attributeTypeFromDatabase = null;
                        string attributeName = null;
                        if(!reader.IsDBNull(nameToColumnIndex["attributeName"]))
                        {
                            attributeName = reader.GetString(nameToColumnIndex["attributeName"]);
                            string xgrsType = reader.GetString(nameToColumnIndex["xgrsType"]);
                            long attributeTypeId = reader.GetInt64(nameToColumnIndex["attributeTypeId"]);
                            attributeTypeFromDatabase = new AttributeTypeFromDatabase(attributeName, xgrsType, attributeTypeId);
                        }

                        if((TypeKind)kind == TypeKind.GraphClass) // filter graph type because we don't add it from the model...to be removed when real graph class support is added
                            continue;

                        AttributeTypesFromDatabase attributeTypes;
                        if(!typeNameToAttributeTypes.TryGetValue(name, out attributeTypes))
                        {
                            attributeTypes = new AttributeTypesFromDatabase(name, (TypeKind)kind);
                            typeNameToAttributeTypes.Add(name, attributeTypes);
                        }

                        if(attributeName != null)
                            attributeTypes.AttributeNamesToAttributeTypes.Add(attributeName, attributeTypeFromDatabase);
                    }
                }
            }

            return typeNameToAttributeTypes;
        }

        private SQLiteCommand PrepareStatementsForReadingKnownTypesWithAttributes()
        {
            StringBuilder columnNames = new StringBuilder();
            AddQueryColumn(columnNames, "types.typeId");
            AddQueryColumn(columnNames, "kind");
            AddQueryColumn(columnNames, "name");
            AddQueryColumn(columnNames, "attributeTypeId");
            AddQueryColumn(columnNames, "attributeName");
            AddQueryColumn(columnNames, "xgrsType");

            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append("types");
            command.Append(" LEFT JOIN ");
            command.Append("attributeTypes");
            command.Append(" ON (types.typeId == attributeTypes.typeId)");

            return new SQLiteCommand(command.ToString(), connection);
        }

        // todo: IfMissing is not needed anymore, just insert; often only db id mapping needed, but dbid should be queried...
        private void InsertGraphIfMissing(long graphId, long typeId)
        {
            using(SQLiteCommand replaceHostGraphCommand = PrepareHostGraphInsert())
            {
                replaceHostGraphCommand.Parameters.Clear();
                replaceHostGraphCommand.Parameters.AddWithValue("@graphId", graphId);
                replaceHostGraphCommand.Parameters.AddWithValue("@typeId", typeId);
                replaceHostGraphCommand.Parameters.AddWithValue("@name", graph.Name);

                replaceHostGraphCommand.Transaction = transaction;
                int rowsAffected = replaceHostGraphCommand.ExecuteNonQuery();

                long rowId = connection.LastInsertRowId;
                Debug.Assert(rowId == graphId);
                AddGraphWithDbIdToDbIdMapping(graph, rowId);
            }
        }

        #endregion Types table populating/handling

        #region Initial graph reading

        private void ReadCompleteGraph()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            // prepare statements for initial graph fetching
            readGraphsCommand = PrepareStatementForReadingGraphs();

            readNodeCommands = new SQLiteCommand[graph.Model.NodeModel.Types.Length];
            readNodeContainerCommands = new Dictionary<String, SQLiteCommand>[graph.Model.NodeModel.Types.Length];
            readEdgeCommands = new SQLiteCommand[graph.Model.EdgeModel.Types.Length];
            readEdgeContainerCommands = new Dictionary<String, SQLiteCommand>[graph.Model.EdgeModel.Types.Length];

            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                readNodeCommands[nodeType.TypeID] = PrepareStatementsForReadingNodesIncludingZombieNodes(nodeType);

                readNodeContainerCommands[nodeType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in nodeType.AttributeTypes)
                {
                    if(!IsContainerType(attributeType))
                        continue;
                    readNodeContainerCommands[nodeType.TypeID].Add(attributeType.Name, PrepareStatementsForReadingContainerAttributes(nodeType, "nodeId", attributeType));
                }
            }
            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                readEdgeCommands[edgeType.TypeID] = PrepareStatementsForReadingEdgesIncludingZombieEdges(edgeType);

                readEdgeContainerCommands[edgeType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in edgeType.AttributeTypes)
                {
                    if(!IsContainerType(attributeType))
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
                    if(!IsContainerType(attributeType))
                        continue;
                    readObjectContainerCommands[objectType.TypeID].Add(attributeType.Name, PrepareStatementsForReadingContainerAttributes(objectType, "objectId", attributeType));
                }
            }

            using(transaction = connection.BeginTransaction())
            {
                try
                {
                    // pass 0 - load all graphs (without elements and without the host graph, which was already processed/inserted before) (an alternative would be to load only the host graph, and the others on-demand lazily, creating proxy objects for them - but this would be more coding effort, defy purging, and when carrying out eager loading, it's better not to load graph-by-graph but all at once; later todo when graph types are introduced: load by type)
                    ReadGraphsWithoutHostGraph();

                    // pass 1 - load all elements (first nodes then edges, dispatching them to their containing graph in case they are not zombie nodes/edges)
                    foreach(NodeType nodeType in graph.Model.NodeModel.Types)
                    {
                        ReadNodesIncludingZombieNodes(nodeType, readNodeCommands[nodeType.TypeID]);
                    }
                    foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
                    {
                        ReadEdgesIncludingZombieEdges(edgeType, readEdgeCommands[edgeType.TypeID]);
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
                            if(!IsContainerType(attributeType))
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
                            if(!IsContainerType(attributeType))
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
                            if(!IsContainerType(attributeType))
                                continue;
                            SQLiteCommand readObjectContainerCommand = readObjectContainerCommands[objectType.TypeID][attributeType.Name];
                            ReadPatchContainerAttributesInElement(objectType, "objectId", attributeType, readObjectContainerCommand);
                        }
                    }

                    transaction.Commit(); // no changes written inside transaction, but using one is recommended because of locking
                    transaction = null;
                }
                catch
                {
                    transaction.Rollback(); // no changes written inside transaction, but using one is recommended because of locking
                    transaction = null;
                    throw;
                }
            }

            stopwatch.Stop();
            ConsoleUI.outWriter.WriteLine("Read {0} nodes, {1} edges, {2} graphs, {3} internal class objects in {4} ms.", NumNodesInDatabase, NumEdgesInDatabase, NumGraphsInDatabase, NumObjectsInDatabase, stopwatch.ElapsedMilliseconds);
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
            readGraphsCommand.Transaction = transaction;
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

        private SQLiteCommand PrepareStatementsForReadingNodesIncludingZombieNodes(NodeType nodeType)
        {
            String topologyTableName = "nodes";
            String tableName = GetUniqueTableName(nodeType.Package, nodeType.Name);
            StringBuilder columnNames = new StringBuilder();
            AddQueryColumn(columnNames, "perTypeTable.nodeId");
            AddQueryColumn(columnNames, "typeId");
            AddQueryColumn(columnNames, "graphId");
            AddQueryColumn(columnNames, "name");
            foreach(AttributeType attributeType in nodeType.AttributeTypes)
            {
                if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
                    continue;
                AddQueryColumn(columnNames, GetUniqueColumnName(attributeType.Name));
            }
            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append(tableName);
            command.Append(" as perTypeTable LEFT JOIN ");
            command.Append(topologyTableName);
            command.Append(" ON (perTypeTable.nodeId == " + topologyTableName + ".nodeId)");
            return new SQLiteCommand(command.ToString(), connection);
        }

        private SQLiteCommand PrepareStatementsForReadingEdgesIncludingZombieEdges(EdgeType edgeType)
        {
            String topologyTableName = "edges";
            String tableName = GetUniqueTableName(edgeType.Package, edgeType.Name);
            StringBuilder columnNames = new StringBuilder();
            AddQueryColumn(columnNames, "perTypeTable.edgeId");
            AddQueryColumn(columnNames, "typeId");
            AddQueryColumn(columnNames, "sourceNodeId");
            AddQueryColumn(columnNames, "targetNodeId");
            AddQueryColumn(columnNames, "graphId");
            AddQueryColumn(columnNames, "name");
            foreach(AttributeType attributeType in edgeType.AttributeTypes)
            {
                if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
                    continue;
                AddQueryColumn(columnNames, GetUniqueColumnName(attributeType.Name));
            }
            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append(tableName);
            command.Append(" as perTypeTable LEFT JOIN ");
            command.Append(topologyTableName);
            command.Append(" ON (perTypeTable.edgeId == " + topologyTableName + ".edgeId)");
            return new SQLiteCommand(command.ToString(), connection);
        }

        private SQLiteCommand PrepareStatementsForReadingObjects(ObjectType objectType)
        {
            String topologyTableName = "objects";
            String tableName = GetUniqueTableName(objectType.Package, objectType.Name);
            StringBuilder columnNames = new StringBuilder();
            AddQueryColumn(columnNames, "objectId");
            AddQueryColumn(columnNames, "typeId");
            AddQueryColumn(columnNames, "name");
            foreach(AttributeType attributeType in objectType.AttributeTypes)
            {
                if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
                    continue;
                AddQueryColumn(columnNames, GetUniqueColumnName(attributeType.Name));
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
            String tableName = GetUniqueTableName(inheritanceType.Package, inheritanceType.Name, attributeType.Name);
            StringBuilder columnNames = new StringBuilder();
            AddQueryColumn(columnNames, "entryId");
            AddQueryColumn(columnNames, ownerIdColumnName);
            AddQueryColumn(columnNames, "command");
            AddQueryColumn(columnNames, "value");
            if(attributeType.Kind != AttributeKind.SetAttr)
                AddQueryColumn(columnNames, "key");
            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append(tableName);
            return new SQLiteCommand(command.ToString(), connection);
        }

        private void ReadNodesIncludingZombieNodes(NodeType nodeType, SQLiteCommand readNodeCommand)
        {
            readNodeCommand.Transaction = transaction;
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

                    long nodeId = reader.GetInt64(attributeNameToColumnIndex["nodeId"]);
                    if(!reader.IsDBNull(attributeNameToColumnIndex["graphId"]))
                    {
                        String name = reader.GetString(attributeNameToColumnIndex["name"]);
                        long graphId = reader.GetInt64(attributeNameToColumnIndex["graphId"]);
                        INamedGraph graph = DbIdToGraph[graphId];
                        graph.AddNode(node, name);
                    }
                    AddNodeWithDbIdToDbIdMapping(node, nodeId);
                }
            }
        }

        private void ReadEdgesIncludingZombieEdges(EdgeType edgeType, SQLiteCommand readEdgeCommand)
        {
            readEdgeCommand.Transaction = transaction;
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

                    long edgeId = reader.GetInt64(attributeNameToColumnIndex["edgeId"]);
                    if(!reader.IsDBNull(attributeNameToColumnIndex["graphId"]))
                    {
                        long sourceNodeId = reader.GetInt64(attributeNameToColumnIndex["sourceNodeId"]);
                        INode source = DbIdToNode[sourceNodeId];
                        long targetNodeId = reader.GetInt64(attributeNameToColumnIndex["targetNodeId"]);
                        INode target = DbIdToNode[targetNodeId];
                        edgeType.SetSourceAndTarget(edge, source, target);
                        String name = reader.GetString(attributeNameToColumnIndex["name"]);
                        long graphId = reader.GetInt64(attributeNameToColumnIndex["graphId"]);
                        INamedGraph graph = DbIdToGraph[graphId];
                        graph.AddEdge(edge, name);
                    }
                    AddEdgeWithDbIdToDbIdMapping(edge, edgeId);
                }
            }
        }

        private void ReadObjects(ObjectType objectType, SQLiteCommand readObjectCommand)
        {
            readObjectCommand.Transaction = transaction;
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
            readElementCommand.Transaction = transaction;
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

            readElementContainerAttributeCommand.Transaction = transaction;
            using(SQLiteDataReader reader = readElementContainerAttributeCommand.ExecuteReader())
            {
                Dictionary<string, int> attributeNameToColumnIndex = GetNameToColumnIndexMapping(reader);

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
                        key = GetContainerEntry(IntegerAttributeType, reader, attributeNameToColumnIndex, "key");
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
                        if(initializedContainers.IsMarked(element, attributeType.Name))
                            modifiedContainers.Mark(element, attributeType.Name);
                        else
                            initializedContainers.Mark(element, attributeType.Name);
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
                        modifiedContainers.Mark(element, attributeType.Name);
                        break;
                    }
                case ContainerCommand.AssignElement:
                    {
                        object container = element.GetAttribute(attributeType.Name);
                        ApplyContainerAssignElementCommand(container, attributeType, value, key);
                        modifiedContainers.Mark(element, attributeType.Name);
                        break;
                    }
                case ContainerCommand.AssignNull:
                    element.SetAttribute(attributeType.Name, null);
                    if(initializedContainers.IsMarked(element, attributeType.Name))
                        modifiedContainers.Mark(element, attributeType.Name);
                    else
                        initializedContainers.Mark(element, attributeType.Name);
                    break;
            }
        }

        private object GetEmptyContainer(AttributeType attributeType)
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

        private void ApplyContainerPutElementCommand(object container, AttributeType attributeType, object value, object key)
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

        private void ApplyContainerRemoveElementCommand(object container, AttributeType attributeType, object value, object key)
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

        private void ApplyContainerAssignElementCommand(object container, AttributeType attributeType, object value, object key)
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
            return GetScalarValueFromSpecifiedColumn(attributeType, GetUniqueColumnName(attributeType.Name), reader, attributeNameToColumnIndex);
        }

        private object GetScalarValueFromSpecifiedColumn(AttributeType attributeType, String columnName, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            int index = attributeNameToColumnIndex[columnName];
            return GetScalarValueFromSpecifiedColumn(attributeType, reader, index);
        }

        private object GetScalarValueOrNullFromSpecifiedColumn(AttributeType attributeType, String columnName, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            int index = attributeNameToColumnIndex[columnName];
            if(reader.IsDBNull(index))
                return null;
            return GetScalarValueFromSpecifiedColumn(attributeType, reader, index);
        }

        private object GetScalarValueFromSpecifiedColumn(AttributeType attributeType, SQLiteDataReader reader, int index)
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
            return GetGraphValueFromSpecifiedColumn(GetUniqueColumnName(attributeType.Name), reader, attributeNameToColumnIndex);
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
            return GetObjectValueFromSpecifiedColumn(GetUniqueColumnName(attributeType.Name), reader, attributeNameToColumnIndex);
        }

        private IObject GetObjectValueFromSpecifiedColumn(String columnName, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            int index = attributeNameToColumnIndex[columnName];
            if(reader.IsDBNull(index))
                return null;
            long dbid = reader.GetInt64(index);
            return DbIdToObject[dbid];
        }

        private IGraphElement GetGraphElement(IAttributeBearer owner, AttributeType attributeType, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex)
        {
            int index = attributeNameToColumnIndex[GetUniqueColumnName(attributeType.Name)];
            if(reader.IsDBNull(index))
                return null;
            long dbid = reader.GetInt64(index);
            if(attributeType.Kind == AttributeKind.NodeAttr)
                return DbIdToNode[dbid];
            else
                return DbIdToEdge[dbid];
        }

        // maybe TODO: ReadContainerEntryValue, also rename the other GetXXX to ReadXXX?
        private object GetContainerEntry(AttributeType attributeType, SQLiteDataReader reader, Dictionary<string, int> attributeNameToColumnIndex, String columnName)
        {
            Debug.Assert(columnName == "value" || columnName == "key");
            if(IsScalarType(attributeType))
                return GetScalarValueOrNullFromSpecifiedColumn(attributeType, columnName, reader, attributeNameToColumnIndex);
            else if(IsGraphType(attributeType))
                return GetGraphValueFromSpecifiedColumn(columnName, reader, attributeNameToColumnIndex);
            else if(IsObjectType(attributeType))
                return GetObjectValueFromSpecifiedColumn(columnName, reader, attributeNameToColumnIndex);
            else if(IsGraphElementType(attributeType))
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
                return DbIdToNode[dbid];
            else
                return DbIdToEdge[dbid];
        }

        private bool TypeHasInstances(String typeName, String idName)
        {
            using(SQLiteCommand command = GetTypeHasInstancesQuery(typeName, idName))
            {
                command.Transaction = transaction;
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    Dictionary<string, int> nameToColumnIndex = GetNameToColumnIndexMapping(reader);
                    while(reader.Read())
                    {
                        long result = reader.GetInt64(0);
                        return result != 0;
                    }
                }
            }
            return false;
        }

        private SQLiteCommand GetTypeHasInstancesQuery(String typeName, String idName)
        {
            String package;
            String name;
            UnpackPackagePrefixedName(typeName, out package, out name);
            String tableName = GetUniqueTableName(package, name);

            StringBuilder command = new StringBuilder();
            command.Append("SELECT EXISTS(");
            command.Append("SELECT ");
            command.Append(idName);
            command.Append(" FROM ");
            command.Append(tableName);
            command.Append(")");
            return new SQLiteCommand(command.ToString(), connection);
        }

        #endregion Initial graph reading

        #region Host graph cleaning

        private void Cleanup()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

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
            using(transaction = connection.BeginTransaction())
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

                    transaction.Commit();
                    transaction = null;
                }
                catch
                {
                    transaction.Rollback();
                    transaction = null;
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
            foreach(KeyValuePair<long, INode> dbidToNode in DbIdToNode)
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

                        if(IsContainerType(attributeType))
                            ReportReferencesIfDangling(node, attributeType, attributeValue);
                        else
                            ReportReferenceIfDangling(node, attributeType, attributeValue);
                    }
                }
            }

            foreach(KeyValuePair<long, IEdge> dbidToEdge in DbIdToEdge)
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

                        if(IsContainerType(attributeType))
                            ReportReferencesIfDangling(edge, attributeType, attributeValue);
                        else
                            ReportReferenceIfDangling(edge, attributeType, attributeValue);
                    }
                }
            }

            foreach(KeyValuePair<long, IObject> dbidToObject in DbIdToObject)
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

                        if(IsContainerType(attributeType))
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
                if(IsGraphElementType(attributeType.KeyType) && IsGraphElementType(attributeType.ValueType))
                {
                    foreach(DictionaryEntry entry in map)
                    {
                        IGraphElement graphElementKey = (IGraphElement)entry.Key;
                        IGraphElement graphElementValue = (IGraphElement)entry.Value;
                        ReportReferenceIfDangling(owner, attributeType, graphElementKey);
                        ReportReferenceIfDangling(owner, attributeType, graphElementValue);
                    }
                }
                else if(IsGraphElementType(attributeType.KeyType))
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
            if(node != null && GetContainingGraph(node) == null) // GetContainingGraph(node) == null means zombie node, i.e. a reference to it is a dangling reference
            {
                long dbid = NodeToDbId[node];
                ReportDanglingReference(owner, attributeType, dbid, true);
            }
            else if(edge != null && GetContainingGraph(edge) == null) // see comment above
            {
                long dbid = EdgeToDbId[edge];
                ReportDanglingReference(owner, attributeType, dbid, false);
            }
        }

        void ReportDanglingReference(IAttributeBearer owner, AttributeType attributeType, long dbid, bool isNode)
        {
            string graphElementReferencedKind = isNode ? "node" : "edge";
            string danglingReferencePart = " contains a dangling reference to a(n) " + graphElementReferencedKind + " (" + graphElementReferencedKind + " dbid=" + dbid + ")";
            if(owner is IGraphElement)
            {
                string ownerKind = owner is INode ? "node" : "edge";
                IGraphElement owningGraphElement = (IGraphElement)owner;
                INamedGraph containingGraph = GetContainingGraph(owningGraphElement);
                string ownerName = containingGraph != null ? containingGraph.GetElementName(owningGraphElement) : "zombie-" + ownerKind;
                string graphNamePart = containingGraph != null ? " of the graph " + containingGraph.Name : " out of graph";
                string pathPart = " of the " + ownerKind + " " + ownerName + graphNamePart;
                if(IsContainerType(attributeType))
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
                if(IsContainerType(attributeType))
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
            gcDfsStateItems.Push(new GcDfsStateItemGraph(host));
            visited.Add(host, null);

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
                                IGraph containingGraph = GetContainingGraph((IGraphElement)referencedElement);
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
        internal static IEnumerable<object> GetReferencesContainedInAttributes(IAttributeBearer element)
        {
            foreach(AttributeType attributeType in element.Type.AttributeTypes)
            {
                if(!ContainsReferences(attributeType))
                    continue;
                if(IsContainerType(attributeType))
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
        internal static IEnumerable<IGraphElement> GetGraphElements(IGraph graph)
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
            if(IsContainerType(attributeType))
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
            if(IsGraphElementType(attributeType))
                return true;
            if(IsGraphType(attributeType))
                return true;
            if(IsObjectType(attributeType))
                return true;
            return false;
        }

        private static bool ContainsGraphElementReferences(AttributeType attributeType)
        {
            if(IsGraphElementType(attributeType))
                return true;
            if(IsContainerType(attributeType))
            {
                if(attributeType.Kind == AttributeKind.MapAttr)
                {
                    if(IsGraphElementType(attributeType.KeyType))
                        return true;
                }
                if(IsGraphElementType(attributeType.ValueType))
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
            foreach(KeyValuePair<long, INamedGraph> dbIdToGraph in DbIdToGraph)
            {
                if(!visited.ContainsKey(dbIdToGraph.Value))
                {
                    graphsToBeDeleted.Add(dbIdToGraph);
                }
            }
            foreach(KeyValuePair<long, INamedGraph> dbidToGraph in graphsToBeDeleted) // maybe batch delete needed performance wise, maybe recursive delete with complex SQL statemente needed performance wise - but multiple commands within a single transaction should be also fast, and are more modular
            {
                INamedGraph graph = dbidToGraph.Value;
                long dbid = dbidToGraph.Key;

                // the RemovingXXX methods remove the topology entry, the type entry with the non-container attributes, and the containers attribute stored in extra tables
                // maybe todo: encapsulate in RemovingGraph
                foreach(IEdge edge in graph.Edges)
                {
                    RemovingEdge(edge);
                    RemoveEdge(edge);
                    ++edgesRemoved;
                }
                foreach(INode node in graph.Nodes)
                {
                    RemovingNode(node);
                    RemoveNode(node);
                    ++nodesRemoved;
                }

                SQLiteCommand deleteGraphTopologyCommand = this.deleteGraphCommand;
                deleteGraphTopologyCommand.Parameters.Clear();
                deleteGraphTopologyCommand.Parameters.AddWithValue("@graphId", dbid);
                deleteGraphTopologyCommand.Transaction = transaction;
                int rowsAffected = deleteGraphTopologyCommand.ExecuteNonQuery();

                RemoveGraphFromDbIdMapping(graph);
            }

            return graphsToBeDeleted.Count;
        }

        private int PurgeUnreachableObjects()
        {
            List<KeyValuePair<long, IObject>> objectsToBeDeleted = new List<KeyValuePair<long, IObject>>();
            foreach(KeyValuePair<long, IObject> dbIdToObject in DbIdToObject)
            {
                if(!visited.ContainsKey(dbIdToObject.Value))
                {
                    objectsToBeDeleted.Add(dbIdToObject);
                }
            }
            foreach(KeyValuePair<long, IObject> dbidToObject in objectsToBeDeleted) // maybe batch delete needed performance wise, maybe recursive delete with complex SQL statemente needed performance wise - but multiple commands within a single transaction should be also fast, and are more modular
            {
                IObject obj = dbidToObject.Value;
                long dbid = dbidToObject.Key;

                // the RemovingObject method removes the topology entry, the type entry with the non-container attributes, and the container attributes stored in extra tables
                RemovingObject(obj);
            }
            return objectsToBeDeleted.Count;
        }

        private int PurgeUnreachableZombieNodes()
        {
            List<KeyValuePair<long, INode>> nodesToBeDeleted = new List<KeyValuePair<long, INode>>();
            foreach(KeyValuePair<long, INode> dbIdToNode in DbIdToNode)
            {
                if(!visited.ContainsKey(dbIdToNode.Value))
                {
                    nodesToBeDeleted.Add(dbIdToNode);
                }
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
            foreach(KeyValuePair<long, IEdge> dbIdToEdge in DbIdToEdge)
            {
                if(!visited.ContainsKey(dbIdToEdge.Value))
                {
                    edgesToBeDeleted.Add(dbIdToEdge);
                }
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

            foreach(KeyValuePair<long, INode> dbidToNode in DbIdToNode)
            {
                long dbid = dbidToNode.Key;
                INode node = dbidToNode.Value;
                if(!visited.ContainsKey(node))
                    continue; // purged altogether later on

                foreach(AttributeType attributeType in node.Type.AttributeTypes)
                {
                    if(IsContainerType(attributeType))
                    {
                        if(!modifiedContainers.IsMarked(node, attributeType.Name))
                            continue; // container unchanged, i.e. only initialized and added to

                        object container = node.GetAttribute(attributeType.Name);
                        PurgeContainerEntries(node, attributeType);
                        WriteContainerEntries(container, attributeType, node);
                        
                        ++numContainersCompactified;
                    }
                }
            }

            foreach(KeyValuePair<long, IEdge> dbidToEdge in DbIdToEdge)
            {
                long dbid = dbidToEdge.Key;
                IEdge edge = dbidToEdge.Value;
                if(!visited.ContainsKey(edge))
                    continue; // purged altogether later on

                foreach(AttributeType attributeType in edge.Type.AttributeTypes)
                {
                    if(IsContainerType(attributeType))
                    {
                        if(!modifiedContainers.IsMarked(edge, attributeType.Name))
                            continue; // container unchanged, i.e. only initialized and added to

                        object container = edge.GetAttribute(attributeType.Name);
                        PurgeContainerEntries(edge, attributeType);
                        WriteContainerEntries(container, attributeType, edge);

                        ++numContainersCompactified;
                    }
                }
            }

            foreach(KeyValuePair<long, IObject> dbidToObject in DbIdToObject)
            {
                long dbid = dbidToObject.Key;
                IObject @object = dbidToObject.Value;
                if(!visited.ContainsKey(@object))
                    continue; // purged altogether later on

                foreach(AttributeType attributeType in @object.Type.AttributeTypes)
                {
                    if(IsContainerType(attributeType))
                    {
                        if(!modifiedContainers.IsMarked(@object, attributeType.Name))
                            continue; // container unchanged, i.e. only initialized and added to

                        object container = @object.GetAttribute(attributeType.Name);
                        PurgeContainerEntries(@object, attributeType);
                        WriteContainerEntries(container, attributeType, @object);

                        ++numContainersCompactified;
                    }
                }
            }

            return numContainersCompactified;
        }

        #endregion Host graph cleaning

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
                    if(!IsContainerType(attributeType))
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
                    if(!IsContainerType(attributeType))
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
                    if(!IsContainerType(attributeType))
                        continue;
                    updateObjectContainerCommands[objectType.TypeID][attributeType.Name] = PrepareContainerUpdatingInsert(objectType, "objectId", attributeType);
                }
            }

            deleteGraphCommand = PrepareTopologyDelete("graphs", "graphId");

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
                    if(!IsContainerType(attributeType))
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
                    if(!IsContainerType(attributeType))
                        continue;
                    deleteEdgeContainerCommands[edgeType.TypeID][attributeType.Name] = PrepareContainerDelete(edgeType, "edgeId", attributeType);
                }
            }

            deleteObjectCommand = PrepareTopologyDelete("objects", "objectId");
            deleteObjectCommands = new SQLiteCommand[graph.Model.ObjectModel.Types.Length];
            foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
            {
                deleteObjectCommands[objectType.TypeID] = PrepareDelete(objectType, "objectId");
            }
            deleteObjectContainerCommands = new Dictionary<String, SQLiteCommand>[graph.Model.ObjectModel.Types.Length];
            foreach(ObjectType objectType in graph.Model.ObjectModel.Types)
            {
                deleteObjectContainerCommands[objectType.TypeID] = new Dictionary<string, SQLiteCommand>();
                foreach(AttributeType attributeType in objectType.AttributeTypes)
                {
                    if(!IsContainerType(attributeType))
                        continue;
                    deleteObjectContainerCommands[objectType.TypeID][attributeType.Name] = PrepareContainerDelete(objectType, "objectId", attributeType);
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

        private SQLiteCommand PrepareContainerInitializingInsert(InheritanceType type, string ownerIdColumnName, AttributeType attributeType)
        {
            String tableName = GetUniqueTableName(type.Package, type.Name, attributeType.Name);
            String ownerTypeTableName = GetUniqueTableName(type.Package, type.Name);
            // the id of the container element (entryId) being the same as the primary key is defined by the dbms

            StringBuilder command = new StringBuilder();
            command.Append("INSERT INTO ");
            command.Append(tableName);
            command.Append("(");
            command.Append(ownerIdColumnName);
            command.Append(", command, value");
            if(attributeType.Kind != AttributeKind.SetAttr)
                command.Append(", key");
            command.Append(")");
            command.Append(" SELECT ");
            command.Append(ownerIdColumnName);
            command.Append(", 0, null");
            if(attributeType.Kind != AttributeKind.SetAttr)
                command.Append(", null");
            command.Append(" FROM ");
            command.Append(ownerTypeTableName);
            command.Append(" WHERE true");

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
            String tableName = GetUniqueTableName(type.Package, type.Name);
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
            String tableName = GetUniqueTableName(type.Package, type.Name, attributeType.Name);

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

        private string GetUniqueTableName(string package, string name)
        {
            return EscapeName(package, name, null);
        }

        private string GetUniqueTableName(string package, string name, string attributeName)
        {
            return EscapeName(package, name, attributeName);
        }

        private string GetUniqueColumnName(string attributeName)
        {
            return EscapeName(null, null, attributeName);
        }

        // a type/attribute name defined by the user could collide with the fixed table/column names used to model the graph,
        // the way package::typename/typename.attribute names are combined to get a valid database name could lead to name conflicts with the names chosen by the user,
        // the SQLite(/SQL standard) DBMS is case insensitive, but GrGen attributes/types are case sensitive
        // so a name disamgiguation scheme is needed, we add a hash suffix based on the domain name to the database name towards this purpose
        private string EscapeName(string package, string name, string attributeName)
        {
            string ambiguousName = PotentiallyAmbiguousDatabaseName(package, name, attributeName);
            string unambiguousName = UnambiguousDomainName(package, name, attributeName);
            string nameConflictResolutionSuffix = "_prvt_name_collisn_sufx_" + GetMd5(unambiguousName);
            return ambiguousName + nameConflictResolutionSuffix;
        }

        private string PotentiallyAmbiguousDatabaseName(string package, string name, string attributeName)
        {
            StringBuilder sb = new StringBuilder();
            if(package != null)
            {
                sb.Append(package);
                sb.Append("__");
            }
            if(name != null)
            {
                sb.Append(name);
            }
            if(attributeName != null)
            {
                if(name != null)
                    sb.Append("_");
                sb.Append(attributeName);
            }
            return sb.ToString(); // could apply ToLower so it is clearer what is semantically happening, but this would hamper readability
        }

        private string UnambiguousDomainName(string package, string name, string attributeName)
        {
            StringBuilder sb = new StringBuilder();
            if(package != null)
            {
                sb.Append(package);
                sb.Append("::");
            }
            if(name != null)
            {
                sb.Append(name);
            }
            if(attributeName != null)
            {
                if(name != null)
                    sb.Append(".");
                sb.Append(attributeName);
            }
            return sb.ToString();
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

        public void RemoveNode(INode node)
        {
            SQLiteCommand deleteNodeCommand = deleteNodeCommands[node.Type.TypeID];
            deleteNodeCommand.Parameters.Clear();
            deleteNodeCommand.Parameters.AddWithValue("@nodeId", NodeToDbId[node]);
            deleteNodeCommand.Transaction = transaction;
            int rowsAffected = deleteNodeCommand.ExecuteNonQuery();

            foreach(AttributeType attributeType in node.Type.AttributeTypes)
            {
                if(!IsContainerType(attributeType))
                    continue;
                SQLiteCommand deleteNodeContainerCommand = deleteNodeContainerCommands[node.Type.TypeID][attributeType.Name];
                deleteNodeContainerCommand.Parameters.Clear();
                deleteNodeContainerCommand.Parameters.AddWithValue("@nodeId", NodeToDbId[node]);
                deleteNodeContainerCommand.Transaction = transaction;
                rowsAffected = deleteNodeContainerCommand.ExecuteNonQuery();
            }

            RemoveNodeFromDbIdMapping(node);
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

        public void RemoveEdge(IEdge edge)
        {
            SQLiteCommand deleteEdgeCommand = deleteEdgeCommands[edge.Type.TypeID];
            deleteEdgeCommand.Parameters.Clear();
            deleteEdgeCommand.Parameters.AddWithValue("@edgeId", EdgeToDbId[edge]);
            deleteEdgeCommand.Transaction = transaction;
            int rowsAffected = deleteEdgeCommand.ExecuteNonQuery();

            foreach(AttributeType attributeType in edge.Type.AttributeTypes)
            {
                if(!IsContainerType(attributeType))
                    continue;
                SQLiteCommand deleteEdgeContainerCommand = deleteEdgeContainerCommands[edge.Type.TypeID][attributeType.Name];
                deleteEdgeContainerCommand.Parameters.Clear();
                deleteEdgeContainerCommand.Parameters.AddWithValue("@edgeId", EdgeToDbId[edge]);
                deleteEdgeContainerCommand.Transaction = transaction;
                rowsAffected = deleteEdgeContainerCommand.ExecuteNonQuery();
            }

            RemoveEdgeFromDbIdMapping(edge);
        }

        private void RemovingObject(IObject @object)
        {
            // maybe todo: introduce foreign key constraints to the database data model, with on delete cascade
            SQLiteCommand deleteObjectTopologyCommand = this.deleteObjectCommand;
            deleteObjectTopologyCommand.Parameters.Clear();
            deleteObjectTopologyCommand.Parameters.AddWithValue("@objectId", ObjectToDbId[@object]);
            deleteObjectTopologyCommand.Transaction = transaction;
            int rowsAffected = deleteObjectTopologyCommand.ExecuteNonQuery();

            SQLiteCommand deleteObjectCommand = deleteObjectCommands[@object.Type.TypeID];
            deleteObjectCommand.Parameters.Clear();
            deleteObjectCommand.Parameters.AddWithValue("@objectId", ObjectToDbId[@object]);
            deleteObjectCommand.Transaction = transaction;
            rowsAffected = deleteObjectCommand.ExecuteNonQuery();

            foreach(AttributeType attributeType in @object.Type.AttributeTypes)
            {
                if(!IsContainerType(attributeType))
                    continue;
                SQLiteCommand deleteObjectContainerCommand = deleteObjectContainerCommands[@object.Type.TypeID][attributeType.Name];
                deleteObjectContainerCommand.Parameters.Clear();
                deleteObjectContainerCommand.Parameters.AddWithValue("@objectId", ObjectToDbId[@object]);
                deleteObjectContainerCommand.Transaction = transaction;
                rowsAffected = deleteObjectContainerCommand.ExecuteNonQuery();
            }

            RemoveObjectFromDbIdMapping(@object);
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

        private static INamedGraph GetContainingGraph(IGraphElement graphElement)
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

        private void PurgeContainerEntries(IAttributeBearer owningElement, AttributeType attributeType)
        {
            // maybe todo: code de-duplication by merging nodes/edges/objects handling even further, issue: type ids are only unique per type model (but first let's introduce graph types with attributes)
            if(owningElement is INode)
            {
                INode node = (INode)owningElement;
                SQLiteCommand deleteNodeContainerCommand = deleteNodeContainerCommands[node.Type.TypeID][attributeType.Name];
                deleteNodeContainerCommand.Parameters.Clear();
                deleteNodeContainerCommand.Parameters.AddWithValue("@nodeId", NodeToDbId[node]);
                deleteNodeContainerCommand.Transaction = transaction;
                int rowsAffected = deleteNodeContainerCommand.ExecuteNonQuery();
            }
            else if(owningElement is IEdge)
            {
                IEdge edge = (IEdge)owningElement;
                SQLiteCommand deleteEdgeContainerCommand = deleteEdgeContainerCommands[edge.Type.TypeID][attributeType.Name];
                deleteEdgeContainerCommand.Parameters.Clear();
                deleteEdgeContainerCommand.Parameters.AddWithValue("@edgeId", EdgeToDbId[edge]);
                deleteEdgeContainerCommand.Transaction = transaction;
                int rowsAffected = deleteEdgeContainerCommand.ExecuteNonQuery();
            }
            else
            {
                IObject @object = (IObject)owningElement;
                SQLiteCommand deleteObjectContainerCommand = deleteObjectContainerCommands[@object.Type.TypeID][attributeType.Name];
                deleteObjectContainerCommand.Parameters.Clear();
                deleteObjectContainerCommand.Parameters.AddWithValue("@objectId", ObjectToDbId[@object]);
                deleteObjectContainerCommand.Transaction = transaction;
                int rowsAffected = deleteObjectContainerCommand.ExecuteNonQuery();
            }
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

        private void WriteContainerEntries(object container, AttributeType attributeType, IAttributeBearer owningElement)
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

        private void RemoveGraphFromDbIdMapping(INamedGraph graph)
        {
            DbIdToGraph.Remove(GraphToDbId[graph]);
            GraphToDbId.Remove(graph);
        }

        private void AddObjectWithDbIdToDbIdMapping(IObject @object, long dbid)
        {
            DbIdToObject.Add(dbid, @object);
            ObjectToDbId.Add(@object, dbid);
        }

        private void RemoveObjectFromDbIdMapping(IObject @object)
        {
            DbIdToObject.Remove(ObjectToDbId[@object]);
            ObjectToDbId.Remove(@object);
        }

        private void AddTypeNameWithDbIdToDbIdMapping(String typeName, long dbid)
        {
            DbIdToTypeName.Add(dbid, typeName);
            TypeNameToDbId.Add(typeName, dbid);
        }

        private void RemoveTypeNameFromDbIdMapping(String typeName)
        {
            DbIdToTypeName.Remove(TypeNameToDbId[typeName]);
            TypeNameToDbId.Remove(typeName);
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

        #region Simple mapping code

        string ToString(TypeKind kind)
        {
            switch(kind)
            {
                case TypeKind.NodeClass: return "node";
                case TypeKind.EdgeClass: return "edge";
                case TypeKind.ObjectClass: return "object";
                case TypeKind.GraphClass: return "graph";
                default: throw new Exception("INTERNAL ERROR");
            }
        }

        string ToIdColumnName(TypeKind kind)
        {
            return ToString(kind) + "Id";
        }

        TypeKind ToKind(InheritanceType type)
        {
            if(type is NodeType)
                return TypeKind.NodeClass;
            else if(type is EdgeType)
                return TypeKind.EdgeClass;
            else if(type is ObjectType)
                return TypeKind.ObjectClass;
            throw new Exception("INTERNAL ERROR");
        }

        string ToKindString(InheritanceType type)
        {
            if(type is NodeType)
                return "node";
            else if(type is EdgeType)
                return "edge";
            else if(type is ObjectType)
                return "object";
            throw new Exception("INTERNAL ERROR");
        }

        string ToIdColumnName(InheritanceType type)
        {
            return ToKindString(type) + "Id";
        }

        private void UnpackPackagePrefixedName(string packagePrefixedName, out string package, out string name)
        {
            if(packagePrefixedName.Contains("::"))
            {
                package = packagePrefixedName.Substring(0, packagePrefixedName.IndexOf(":"));
                name = packagePrefixedName.Substring(packagePrefixedName.LastIndexOf(":") + 1, packagePrefixedName.Length - (packagePrefixedName.LastIndexOf(":") + 1));
                Debug.Assert(packagePrefixedName.IndexOf("::") == packagePrefixedName.LastIndexOf("::")); // ensure no enum types are used here
            }
            else
            {
                package = null;
                name = packagePrefixedName;
            }
        }

        #endregion Simple mapping code

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
