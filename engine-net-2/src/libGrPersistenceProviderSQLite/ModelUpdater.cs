/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Text;
using de.unika.ipd.grGen.libConsoleAndOS;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.libGrPersistenceProviderSQLite
{
    /// <summary>
    /// A component used in adapting the stored database model to the model of the host graph.
    /// </summary>
    internal class ModelUpdater
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
                    string typeName = typeToAttributes.Key;
                    AttributeTypesFromDatabase attributeTypesFromDatabase = typeToAttributes.Value;

                    if(attributeTypesFromDatabase.KindOfOwner == TypeKind.NodeClass)
                        sw.Write("node class ");
                    else if(attributeTypesFromDatabase.KindOfOwner == TypeKind.EdgeClass)
                        sw.Write("edge class ");
                    else
                        sw.Write("class ");
                    sw.WriteLine(typeName);
                    sw.WriteLine("{");
                    foreach(AttributeTypeFromDatabase attribute in attributeTypesFromDatabase.AttributeNamesToAttributeTypes.Values)
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

        PersistenceProviderSQLite persistenceProvider;

        internal ModelUpdater(PersistenceProviderSQLite persistenceProvider)
        {
            this.persistenceProvider = persistenceProvider;
        }

        internal void CreateSchemaIfNotExistsOrAdaptToCompatibleChanges()
        {
            CreateIdentityAndTopologyTables();
            CreateTypesWithAttributeTables();

            if(ReadKnownTypes().Count == 0) // causes initial fill of the type mapping as side effect if types are available (general architecture: full table scans building memory structure (host graph with references), which is then used - thereafter, only changes are written to the database -- the types table memory structure is obtained by ReadKnownTypesWithAttributes)
                FillInitialTypes();
            else
                AdaptDatabaseTypesToModelTypes(); // model update
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

            foreach(NodeType nodeType in persistenceProvider.graph.Model.NodeModel.Types)
            {
                CreateInheritanceTypeTable(nodeType, "nodeId");

                foreach(AttributeType attributeType in nodeType.AttributeTypes)
                {
                    if(!IsContainerType(attributeType))
                        continue;
                    CreateContainerTypeTable(nodeType, "nodeId", attributeType);
                }
            }

            foreach(EdgeType edgeType in persistenceProvider.graph.Model.EdgeModel.Types)
            {
                CreateInheritanceTypeTable(edgeType, "edgeId");

                foreach(AttributeType attributeType in edgeType.AttributeTypes)
                {
                    if(!IsContainerType(attributeType))
                        continue;
                    CreateContainerTypeTable(edgeType, "edgeId", attributeType);
                }
            }

            foreach(ObjectType objectType in persistenceProvider.graph.Model.ObjectModel.Types)
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
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(type.Package, type.Name);
            List<String> columnNamesAndTypes = new List<String>(); // ArrayBuilder
            foreach(AttributeType attributeType in type.AttributeTypes)
            {
                if(!IsAttributeTypeMappedToDatabaseColumn(attributeType))
                    continue;

                columnNamesAndTypes.Add(PersistenceProviderSQLite.GetUniqueColumnName(attributeType.Name));
                columnNamesAndTypes.Add(ScalarAndReferenceTypeToSQLiteType(attributeType));
            }

            CreateTable(tableName, idColumnName, columnNamesAndTypes.ToArray()); // the id in the type table is a local copy of the global id from the corresponding topology table
        }

        private void CreateContainerTypeTable(InheritanceType type, String ownerIdColumnName, AttributeType attributeType)
        {
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(type.Package, type.Name, attributeType.Name);
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
                columnNamesAndTypes.Add(ScalarAndReferenceTypeToSQLiteType(persistenceProvider.IntegerAttributeType));
            }

            CreateTable(tableName, "entryId", columnNamesAndTypes.ToArray()); // the entryId denotes the row local to this table, the ownerIdColumnName is a local copy of the global id from the corresponding topology table

            AddIndex(tableName, ownerIdColumnName); // in order to delete without a full table scan (I assume small changesets in between database openings and decided for a by-default pruning on open - an alternative would be a full table replacement once in a while (would be ok in case of big changesets, as they would occur when a pruning run only occurs on explicit request, but such ones could be forgotten/missed unknowingly too easily, leading to (unexpected) slowness))
        }

        private void AddAttributeToInheritanceTypeTable(InheritanceType type, AttributeType attributeType)
        {
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(type.Package, type.Name);
            Debug.Assert(IsAttributeTypeMappedToDatabaseColumn(attributeType));
            String columnName = PersistenceProviderSQLite.GetUniqueColumnName(attributeType.Name);
            String columnType = ScalarAndReferenceTypeToSQLiteType(attributeType);

            AddColumnToTable(tableName, columnName, columnType);
        }

        private void RemoveAttributeFromInheritanceTypeTable(string typeName, string attributeName)
        {
            String package;
            String name;
            UnpackPackagePrefixedName(typeName, out package, out name);
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(package, name);
            String columnName = PersistenceProviderSQLite.GetUniqueColumnName(attributeName);

            DropColumnFromTable(tableName, columnName);
        }

        private void DeleteInheritanceTypeTable(string typeName)
        {
            String package;
            String name;
            UnpackPackagePrefixedName(typeName, out package, out name);
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(package, name);
 
            DropTable(tableName);
        }

        private void DeleteContainerTypeTable(string typeName, string attributeName)
        {
            String package;
            String name;
            UnpackPackagePrefixedName(typeName, out package, out name);
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(package, name, attributeName);

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

        private static string ScalarAndReferenceTypeToSQLiteType(AttributeType attributeType)
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

        private static object DefaultValue(AttributeType attributeType)
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
            PersistenceProviderSQLite.AddQueryColumn(columnNames, idColumnName + " INTEGER PRIMARY KEY AUTOINCREMENT"); // AUTOINCREMENT so ids cannot be reused (preventing wrong mappings after deletion)
            for(int i = 0; i < columnNamesAndTypes.Length; i += 2)
            {
                PersistenceProviderSQLite.AddQueryColumn(columnNames, columnNamesAndTypes[i] + " " + columnNamesAndTypes[i + 1]);
            }

            StringBuilder command = new StringBuilder();
            command.Append("CREATE TABLE IF NOT EXISTS ");
            command.Append(tableName);
            command.Append("(");
            command.Append(columnNames.ToString());
            command.Append(") ");
            command.Append("STRICT");
            using(SQLiteCommand createSchemaCommand = new SQLiteCommand(command.ToString(), persistenceProvider.connection))
            {
                createSchemaCommand.Transaction = persistenceProvider.transaction;
                int rowsAffected = createSchemaCommand.ExecuteNonQuery();
            }
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
            using(SQLiteCommand createIndexCommand = new SQLiteCommand(command.ToString(), persistenceProvider.connection))
            {
                createIndexCommand.Transaction = persistenceProvider.transaction;
                int rowsAffected = createIndexCommand.ExecuteNonQuery();
            }
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
            using(SQLiteCommand addColumnCommand = new SQLiteCommand(command.ToString(), persistenceProvider.connection))
            {
                addColumnCommand.Transaction = persistenceProvider.transaction;
                int rowsAffected = addColumnCommand.ExecuteNonQuery();
            }
        }

        private void DropColumnFromTable(String tableName, String columnName)
        {
            StringBuilder command = new StringBuilder();
            command.Append("ALTER TABLE ");
            command.Append(tableName);
            command.Append(" DROP COLUMN ");
            command.Append(columnName);
            using(SQLiteCommand dropColumnCommand = new SQLiteCommand(command.ToString(), persistenceProvider.connection))
            {
                dropColumnCommand.Transaction = persistenceProvider.transaction;
                int rowsAffected = dropColumnCommand.ExecuteNonQuery();
            }
        }

        private void DropTable(String tableName)
        {
            StringBuilder command = new StringBuilder();
            command.Append("DROP TABLE ");
            command.Append(tableName);
            using(SQLiteCommand dropTableCommand = new SQLiteCommand(command.ToString(), persistenceProvider.connection))
            {
                dropTableCommand.Transaction = persistenceProvider.transaction;
                int rowsAffected = dropTableCommand.ExecuteNonQuery();
            }
        }

        #region Types table populating/handling

        private void AdaptDatabaseTypesToModelTypes()
        {
            // the types mapping was filled from the database before, it is used during the model update, it is updated stepwise with new model, and afterwards fits to the new model
            // the types from the database are read once, used in the model update, and thrown away after the update (the old model from the database is the source, the target is the new model from the graph, defined by the model file/assembly)
            TypesFromDatabase typesFromDatabase = new TypesFromDatabase(ReadKnownTypesWithAttributes());

            Dictionary<InheritanceType, SetValueType> newModelTypes = GetNewModelTypes(typesFromDatabase); // types from the model not in the database
            Dictionary<String, TypeKind> deletedModelTypes = GetDeletedModelTypes(typesFromDatabase); // types from the database not in the model
            Dictionary<InheritanceType, TypeKind> typeChangedModelTypes = GetTypeChangedModelTypes(typesFromDatabase); // same name but changed type kind
            bool changeMessagePrinted = false;
            DetermineAndReportTypeChangesToTheUser(newModelTypes, deletedModelTypes, typeChangedModelTypes,
                typesFromDatabase, ref changeMessagePrinted);

            Dictionary<string, InheritanceType> keptModelTypes = GetKeptModelTypes(typesFromDatabase); // attribute changes are only computed for types in the model _and_ in the database
            List<KeyValuePair<InheritanceType, AttributeType>> addedAttributes = new List<KeyValuePair<InheritanceType, AttributeType>>();
            List<KeyValuePair<string, AttributeTypeFromDatabase>> removedAttributes = new List<KeyValuePair<string, AttributeTypeFromDatabase>>();
            List<KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>> typeChangedAttributes = new List<KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>>(); // type differs in between model and database
            foreach(KeyValuePair<string, InheritanceType> typeNameToType in keptModelTypes)
            {
                string typeName = typeNameToType.Key;
                InheritanceType type = typeNameToType.Value;

                DetermineAndReportAttributeChangesToTheUser(type, typesFromDatabase[typeName],
                    ref addedAttributes, ref removedAttributes, ref typeChangedAttributes, ref changeMessagePrinted);
            }

            // no model changes
            if(!changeMessagePrinted)
            {
                InsertGraphIfMissing(PersistenceProviderSQLite.HOST_GRAPH_ID, persistenceProvider.TypeNameToDbId["graph"]);
                return;
            }

            bool adaptDatabaseModelToModel = QueryUserWhetherToAdaptDatabaseToModel(removedAttributes, typeChangedAttributes, persistenceProvider.persistentGraphParameters);
            if(!adaptDatabaseModelToModel)
            {
                WriteDatabaseModelToFileAndReportToUser(typesFromDatabase, persistenceProvider.connectionParameters);
                ConsoleUI.outWriter.WriteLine("Aborting database model update as requested...");
                throw new Exception("Abort database model update as requested.");
            }

            ConsoleUI.outWriter.WriteLine("Overall there are {0} new types, {1} deleted types, {2} types of a different kind, {3} added attributes, {4} removed attributes, {5} attributes of a different type in the model compared to the database.",
                newModelTypes.Count, deletedModelTypes.Count, typeChangedModelTypes.Count, addedAttributes.Count, removedAttributes.Count, typeChangedAttributes.Count);
            ConsoleUI.outWriter.WriteLine("The database is going to be updated: {0} types are going to be introduced, {1} types are going to be deleted, {2} attributes are going to be added, {3} attributes are going to be removed.",
                newModelTypes.Count + typeChangedModelTypes.Count, deletedModelTypes.Count + typeChangedModelTypes.Count,
                addedAttributes.Count + typeChangedAttributes.Count, removedAttributes.Count + typeChangedAttributes.Count);

            Stopwatch stopwatch = Stopwatch.StartNew();
            ConsoleUI.outWriter.WriteLine("Updating the database model to the current model...");

            using(persistenceProvider.transaction = persistenceProvider.connection.BeginTransaction())
            {
                try
                {
                    RemoveDeletedModelTypes(deletedModelTypes, typesFromDatabase); // first remove than delete to prevent duplicate name collisions
                    RemoveAndAddTypeChangedModelTypes(typeChangedModelTypes, typesFromDatabase);
                    AddNewModelTypes(newModelTypes);

                    ConsoleUI.outWriter.WriteLine("...done with the types, continuing with the attributes..."); // potential todo: more detailed progress, reporting statistics about database concepts

                    foreach(KeyValuePair<string, AttributeTypeFromDatabase> removedAttribute in removedAttributes)
                    {
                        string typeName = removedAttribute.Key;
                        AttributeTypeFromDatabase attributeTypeFromDatabase = removedAttribute.Value;

                        RemoveDeletedModelTypeAttribute(typeName, attributeTypeFromDatabase);
                    }
                    foreach(KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>> typeChangedAttribute in typeChangedAttributes)
                    {
                        InheritanceType type = typeChangedAttribute.Key;
                        AttributeType attributeType = typeChangedAttribute.Value.Key;
                        AttributeTypeFromDatabase attributeTypeFromDatabase = typeChangedAttribute.Value.Value;

                        RemoveDeletedModelTypeAttribute(type.PackagePrefixedName, attributeTypeFromDatabase);
                        AddNewModelTypeAttribute(type, attributeType);
                    }
                    foreach(KeyValuePair<InheritanceType, AttributeType> addedAttribute in addedAttributes)
                    {
                        InheritanceType type = addedAttribute.Key;
                        AttributeType attributeType = addedAttribute.Value;

                        AddNewModelTypeAttribute(type, attributeType);
                    }

                    persistenceProvider.transaction.Commit();
                    persistenceProvider.transaction = null;
                }
                catch
                {
                    persistenceProvider.transaction.Rollback();
                    persistenceProvider.transaction = null;
                    stopwatch.Stop();
                    ConsoleUI.outWriter.WriteLine("...undone due to failure (completed after {0} ms).", stopwatch.ElapsedMilliseconds);
                    throw;
                }
            }

            stopwatch.Stop();
            ConsoleUI.outWriter.WriteLine("...done (database model updated in {0} ms).", stopwatch.ElapsedMilliseconds);

            InsertGraphIfMissing(PersistenceProviderSQLite.HOST_GRAPH_ID, persistenceProvider.TypeNameToDbId["graph"]);
        }

        private Dictionary<InheritanceType, SetValueType> GetNewModelTypes(TypesFromDatabase typesFromDatabase)
        {
            Dictionary<InheritanceType, SetValueType> newModelTypes = new Dictionary<InheritanceType, SetValueType>();

            foreach(NodeType nodeType in persistenceProvider.graph.Model.NodeModel.Types)
            {
                if(!typesFromDatabase.IsTypeKnown(nodeType))
                    newModelTypes.Add(nodeType, null);
            }

            foreach(EdgeType edgeType in persistenceProvider.graph.Model.EdgeModel.Types)
            {
                if(!typesFromDatabase.IsTypeKnown(edgeType))
                    newModelTypes.Add(edgeType, null);
            }

            foreach(ObjectType objectType in persistenceProvider.graph.Model.ObjectModel.Types)
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
            foreach(KeyValuePair<string, AttributeTypesFromDatabase> typeNameToAttributeTypes in typesFromDatabase.TypesToAttributesFromDatabase)
            {
                string typeName = typeNameToAttributeTypes.Key;
                AttributeTypesFromDatabase attributeTypesFromDatabase = typeNameToAttributeTypes.Value;

                if(!allModelTypes.ContainsKey(typeName))
                    deletedModelTypes.Add(typeName, attributeTypesFromDatabase.KindOfOwner);
            }

            return deletedModelTypes;
        }

        private Dictionary<string, InheritanceType> GetAllModelTypes()
        {
            Dictionary<string, InheritanceType> allModelTypes = new Dictionary<string, InheritanceType>();

            foreach(NodeType nodeType in persistenceProvider.graph.Model.NodeModel.Types)
            {
                allModelTypes.Add(nodeType.PackagePrefixedName, nodeType);
            }

            foreach(EdgeType edgeType in persistenceProvider.graph.Model.EdgeModel.Types)
            {
                allModelTypes.Add(edgeType.PackagePrefixedName, edgeType);
            }

            foreach(ObjectType objectType in persistenceProvider.graph.Model.ObjectModel.Types)
            {
                allModelTypes.Add(objectType.PackagePrefixedName, objectType);
            }

            return allModelTypes;
        }

        private Dictionary<InheritanceType, TypeKind> GetTypeChangedModelTypes(TypesFromDatabase typesFromDatabase)
        {
            Dictionary<InheritanceType, TypeKind> typeChangedModelTypes = new Dictionary<InheritanceType, TypeKind>();

            Dictionary<string, InheritanceType> allModelTypes = GetAllModelTypes();
            foreach(KeyValuePair<string, AttributeTypesFromDatabase> typeNameToAttributeTypes in typesFromDatabase.TypesToAttributesFromDatabase)
            {
                String typeName = typeNameToAttributeTypes.Key;
                TypeKind typeKind = typeNameToAttributeTypes.Value.KindOfOwner;

                if(allModelTypes.ContainsKey(typeName) && typeKind != TypeKind.GraphClass)
                {
                    if(!IsOfSameKind(typeKind, allModelTypes[typeName]))
                        typeChangedModelTypes.Add(allModelTypes[typeName], typeKind);
                }
            }

            return typeChangedModelTypes;
        }

        private Dictionary<string, InheritanceType> GetKeptModelTypes(TypesFromDatabase typesFromDatabase)
        {
            Dictionary<string, InheritanceType> keptModelTypes = new Dictionary<string, InheritanceType>();

            Dictionary<string, InheritanceType> allModelTypes = GetAllModelTypes();
            foreach(KeyValuePair<string, AttributeTypesFromDatabase> typeNameToAttributeTypes in typesFromDatabase.TypesToAttributesFromDatabase)
            {
                String typeName = typeNameToAttributeTypes.Key;
                TypeKind typeKind = typeNameToAttributeTypes.Value.KindOfOwner;

                if(allModelTypes.ContainsKey(typeName) && typeKind != TypeKind.GraphClass)
                {
                    if(IsOfSameKind(typeKind, allModelTypes[typeName]))
                        keptModelTypes.Add(typeName, allModelTypes[typeName]);
                }
            }

            return keptModelTypes;
        }

        // todo: consistency -- most messages based on database first perspective (old state of model, current state of data), variable names based on model first perspective (current state of model, new state targeted for data)
        private void DetermineAndReportTypeChangesToTheUser(Dictionary<InheritanceType, SetValueType> newModelTypes,
            Dictionary<String, TypeKind> deletedModelTypes, Dictionary<InheritanceType, TypeKind> typeChangedModelTypes,
            TypesFromDatabase typesFromDatabase, ref bool changeMessagePrinted)
        {
            bool deletedTypeStillHasInstances = false;
            foreach(KeyValuePair<String, TypeKind> typeNameToKind in deletedModelTypes)
            {
                string typeName = typeNameToKind.Key;
                TypeKind typeKind = typeNameToKind.Value;

                string kindName = ToString(typeKind);
                if(TypeHasInstances(typeName, typeKind))
                {
                    PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                    ConsoleUI.outWriter.WriteLine("The {0} class type {1} from the database is not existing in the model.",
                        kindName, typeName);
                    ConsoleUI.outWriter.WriteLine("- The {0} class type {1} has to be deleted from the database, but the database contains instances of it (aborting...).",
                        kindName, typeName);
                    deletedTypeStillHasInstances = true;
                }
            }

            foreach(KeyValuePair<InheritanceType, TypeKind> typeToKind in typeChangedModelTypes)
            {
                string typeName = typeToKind.Key.PackagePrefixedName;
                TypeKind typeKind = typeToKind.Value;

                string kindName = ToString(typeKind);
                string kindNameModel = ToKindString(typeToKind.Key);
                if(TypeHasInstances(typeName, typeKind))
                {
                    PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                    ConsoleUI.outWriter.WriteLine("The {0} class type {1} from the database is a {2} class type in the model.",
                        kindName, typeName, kindNameModel);
                    ConsoleUI.outWriter.WriteLine("- The {0} class type {1} has to be deleted from the database, but the database contains instances of it (aborting...).",
                        kindName, typeName);
                    deletedTypeStillHasInstances = true;
                }
            }

            if(deletedTypeStillHasInstances)
            {
                ConsoleUI.outWriter.WriteLine("The model used by the database contains instances of types that don't exist anymore in the model file/the model assembly.");
                ConsoleUI.outWriter.WriteLine("You must delete them first - to do so you must open the database with the old model (if you just deleted them and no dangling references exist, just opening the database with the old model may be sufficient, due to the garbage collection run in the begining).");
                ConsoleUI.outWriter.WriteLine("The database cannot be opened with the current model.");
                WriteDatabaseModelToFileAndReportToUser(typesFromDatabase, persistenceProvider.connectionParameters);
                ConsoleUI.outWriter.WriteLine("Aborting database model update due to an incompatible model...");
                throw new Exception("Abort database model update due to an incompatible model.");
            }

            foreach(KeyValuePair<String, TypeKind> typeNameToKind in deletedModelTypes)
            {
                string typeName = typeNameToKind.Key;
                TypeKind typeKind = typeNameToKind.Value;

                string kindName = ToString(typeKind);
                PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                ConsoleUI.outWriter.WriteLine("The {0} class type {1} from the database is not existing in the model (anymore).",
                    kindName, typeName);
                ConsoleUI.outWriter.WriteLine("- The {0} class type {1} is going to be deleted from the database, too.",
                    kindName, typeName);
            }

            foreach(KeyValuePair<InheritanceType, TypeKind> typeToKind in typeChangedModelTypes)
            {
                string typeName = typeToKind.Key.PackagePrefixedName;
                TypeKind typeKind = typeToKind.Value;

                string kindName = ToString(typeKind);
                string kindModel = ToKindString(typeToKind.Key);
                PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                ConsoleUI.outWriter.WriteLine("The {0} class type {1} from the database is a {2} class type in the model.",
                    kindName, typeName, kindModel);
                ConsoleUI.outWriter.WriteLine("- The {0} class type {1} is going to be deleted from the database.",
                    kindName, typeName);
                ConsoleUI.outWriter.WriteLine("- The {0} class type {1} is going to be added to the database.",
                    kindModel, typeName);
            }

            foreach(InheritanceType type in newModelTypes.Keys)
            {
                string typeName = type.PackagePrefixedName;
                string kindNameModel = ToKindString(type);
                PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                ConsoleUI.outWriter.WriteLine("The database does not contain the {0} class type {1} (that exists in the model).",
                    kindNameModel, typeName);
                ConsoleUI.outWriter.WriteLine("- The {0} class type {1} is going to be added to the database, too.",
                    kindNameModel, typeName);
            }
        }

        private static void PrintChangeMessageAsNeeded(ref bool changeMessagePrinted)
        {
            if(!changeMessagePrinted)
            {
                changeMessagePrinted = true;
                ConsoleUI.outWriter.WriteLine("The model used by the database differs from the model file/the model assembly.");
            }
        }

        private static void DetermineAndReportAttributeChangesToTheUser(InheritanceType type, AttributeTypesFromDatabase attributeTypesFromDatabase,
            ref List<KeyValuePair<InheritanceType, AttributeType>> addedAttributes,
            ref List<KeyValuePair<string, AttributeTypeFromDatabase>> removedAttributes,
            ref List<KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>> typeChangedAttributes,
            ref bool changeMessagePrinted)
        {
            foreach(KeyValuePair<string, AttributeTypeFromDatabase> attributeNameToAttributeTypeFromDatabase in attributeTypesFromDatabase.AttributeNamesToAttributeTypes)
            {
                string attributeNameFromDatabase = attributeNameToAttributeTypeFromDatabase.Key;
                AttributeTypeFromDatabase attributeTypeFromDatabase = attributeNameToAttributeTypeFromDatabase.Value;

                AttributeType attributeTypeFromModel = type.GetAttributeType(attributeNameFromDatabase);
                if(attributeTypeFromModel == null)
                {
                    PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                    ConsoleUI.outWriter.WriteLine("The attribute {0}.{1} from the database is not existing in the model (anymore).",
                        type.PackagePrefixedName, attributeNameFromDatabase);
                    ConsoleUI.outWriter.WriteLine("- The attribute {0}.{1} is going to be removed from the database, too, and all its values are going to be purged (from the instances of the type), you will loose information!",
                        type.PackagePrefixedName, attributeNameFromDatabase);

                    removedAttributes.Add(new KeyValuePair<string, AttributeTypeFromDatabase>(type.PackagePrefixedName, attributeTypeFromDatabase));
                }
                else if(attributeTypeFromDatabase.XgrsType != TypesHelper.AttributeTypeToXgrsType(attributeTypeFromModel))
                {
                    PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                    ConsoleUI.outWriter.WriteLine("The attribute {0}.{1} of type {2} from the database is of type {3} in the model!",
                        type.PackagePrefixedName, attributeNameFromDatabase, attributeTypeFromDatabase.XgrsType, TypesHelper.AttributeTypeToXgrsType(attributeTypeFromModel));
                    ConsoleUI.outWriter.WriteLine("- The attribute {0}.{1} is going to be removed from the database, and all its values are going to be purged (from the instances of the type), you will loose information!",
                        type.PackagePrefixedName, attributeNameFromDatabase);
                    ConsoleUI.outWriter.WriteLine("- The attribute {0}.{1} is going to be added in the database, it is going to be initialized to the default value of the attribute type (in the instances of the type - but not the default value specified in the class).",
                        type.PackagePrefixedName, attributeTypeFromModel.Name);

                    typeChangedAttributes.Add(new KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>(type, new KeyValuePair<AttributeType, AttributeTypeFromDatabase>(attributeTypeFromModel, attributeTypeFromDatabase)));
                }
            }

            foreach(AttributeType attributeTypeFromModel in type.AttributeTypes)
            {
                if(!attributeTypesFromDatabase.AttributeNamesToAttributeTypes.ContainsKey(attributeTypeFromModel.Name))
                {
                    PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                    ConsoleUI.outWriter.WriteLine("The type {0} does not contain the attribute {1} in the database (that exists in the model).",
                        type.PackagePrefixedName, attributeTypeFromModel.Name);
                    ConsoleUI.outWriter.WriteLine("- The attribute {0}.{1} is going to be added in the database, too, it is going to be initialized to the default value of the attribute type (in the instances of the type - but not the default value specified in the class).",
                        type.PackagePrefixedName, attributeTypeFromModel.Name);

                    addedAttributes.Add(new KeyValuePair<InheritanceType, AttributeType>(type, attributeTypeFromModel));
                }
            }
        }

        private static bool QueryUserWhetherToAdaptDatabaseToModel(List<KeyValuePair<string, AttributeTypeFromDatabase>> removedAttributes,
            List<KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>> typeChangedAttributes,
            string persistentGraphParameters)
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

        private static void RemoveAttribute(List<KeyValuePair<string, AttributeTypeFromDatabase>> removedAttributesAfterFiltering,
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

        private static void WriteDatabaseModelToFileAndReportToUser(TypesFromDatabase typesFromDatabase, string connectionParameters)
        {
            String filename = "partial_flattened_model_from_" + ExtractDataSourceName(connectionParameters) + ".gm";
            using(StreamWriter sw = new StreamWriter(filename, false, System.Text.Encoding.UTF8))
            {
                typesFromDatabase.WriteDatabaseModelToFile(sw);
            }
            ConsoleUI.outWriter.WriteLine("Written file " + filename + " showing the partial flattened model expected by the host graph in the database.");
        }

        private static string ExtractDataSourceName(string connectionParameters)
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
                string typeName = typeNameToKind.Key;
                TypeKind typeKind = typeNameToKind.Value;

                using(SQLiteCommand removeTypeFromAttributeTypesCommand = persistenceProvider.PrepareTopologyDelete("attributeTypes", "typeId"))
                {
                    RemoveAttributeTypes(removeTypeFromAttributeTypesCommand, persistenceProvider.TypeNameToDbId[typeName]);
                }
                using(SQLiteCommand removeTypeCommand = persistenceProvider.PrepareTopologyDelete("types", "typeId"))
                {
                    RemoveType(removeTypeCommand, persistenceProvider.TypeNameToDbId[typeName]);
                }
                RemoveTypeNameFromDbIdMapping(typeName);

                foreach(KeyValuePair<string, AttributeTypeFromDatabase> attributeNameToAttributeType in typesFromDatabase[typeName].AttributeNamesToAttributeTypes)
                {
                    string attributeName = attributeNameToAttributeType.Key;
                    AttributeTypeFromDatabase attributeTypeFromDatabase = attributeNameToAttributeType.Value;

                    if(IsContainerType(attributeTypeFromDatabase.XgrsType))
                        DeleteContainerTypeTable(typeName, attributeName);
                }
                DeleteInheritanceTypeTable(typeName);
            }
        }

        private void RemoveAndAddTypeChangedModelTypes(Dictionary<InheritanceType, TypeKind> typeChangedModelTypes, TypesFromDatabase typesFromDatabase)
        {
            Dictionary<string, TypeKind> deletedModelTypes = new Dictionary<string, TypeKind>();
            Dictionary<InheritanceType, SetValueType> newModelTypes = new Dictionary<InheritanceType, SetValueType>();
            foreach(KeyValuePair<InheritanceType, TypeKind> typeChangedModelTypeToKind in typeChangedModelTypes)
            {
                InheritanceType typeChangedModelType = typeChangedModelTypeToKind.Key;
                TypeKind typeKind = typeChangedModelTypeToKind.Value;

                deletedModelTypes.Add(typeChangedModelType.PackagePrefixedName, typeKind);
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
                    initializingInsert.Transaction = persistenceProvider.transaction;
                    int rowsAffected = initializingInsert.ExecuteNonQuery();
                }
            }
            else
            {
                AddAttributeToInheritanceTypeTable(type, attributeType);
                using(SQLiteCommand updateCommand = persistenceProvider.PrepareUpdate(type, null, attributeType))
                {
                    updateCommand.Parameters.Clear();
                    updateCommand.Parameters.AddWithValue("@" + PersistenceProviderSQLite.GetUniqueColumnName(attributeType.Name), DefaultValue(attributeType));

                    updateCommand.Transaction = persistenceProvider.transaction;
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                }
            }
        }

        private void RemoveDeletedModelTypeAttribute(string typeName, AttributeTypeFromDatabase attributeType)
        {
            // remove from the attributeTypes table
            using(SQLiteCommand removeTypeFromAttributeTypesCommand = persistenceProvider.PrepareTopologyDelete("attributeTypes", "attributeTypeId"))
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

                foreach(NodeType nodeType in persistenceProvider.graph.Model.NodeModel.Types)
                {
                    FillTypeWithAttributes(fillTypeCommand, nodeType, TypeKind.NodeClass);
                }
                foreach(EdgeType edgeType in persistenceProvider.graph.Model.EdgeModel.Types)
                {
                    FillTypeWithAttributes(fillTypeCommand, edgeType, TypeKind.EdgeClass);
                }
                foreach(ObjectType objectType in persistenceProvider.graph.Model.ObjectModel.Types)
                {
                    FillTypeWithAttributes(fillTypeCommand, objectType, TypeKind.ObjectClass);
                }
            }

            InsertGraphIfMissing(PersistenceProviderSQLite.HOST_GRAPH_ID, persistenceProvider.TypeNameToDbId["graph"]);
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

            fillTypeCommand.Transaction = persistenceProvider.transaction;
            int rowsAffected = fillTypeCommand.ExecuteNonQuery();

            long rowId = persistenceProvider.connection.LastInsertRowId;
            AddTypeNameWithDbIdToDbIdMapping(type.PackagePrefixedName, rowId); // the grgen type id from the model is only unique per kind, and not stable upon insert(/delete), so we use the name
        }

        private SQLiteCommand GetFillTypeCommand()
        {
            String tableName = "types";
            StringBuilder columnNames = new StringBuilder();
            StringBuilder parameterNames = new StringBuilder();
            PersistenceProviderSQLite.AddInsertParameter(columnNames, parameterNames, "kind");
            PersistenceProviderSQLite.AddInsertParameter(columnNames, parameterNames, "name");

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

            return new SQLiteCommand(command.ToString(), persistenceProvider.connection);
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

            removeTypeFromAttributeTypesCommand.Transaction = persistenceProvider.transaction;
            int rowsAffected = removeTypeFromAttributeTypesCommand.ExecuteNonQuery();
        }

        private void RemoveAttributeType(SQLiteCommand removeAttributeTypeFromAttributeTypesCommand, long dbid)
        {
            removeAttributeTypeFromAttributeTypesCommand.Parameters.Clear();
            removeAttributeTypeFromAttributeTypesCommand.Parameters.AddWithValue("@attributeTypeId", dbid);

            removeAttributeTypeFromAttributeTypesCommand.Transaction = persistenceProvider.transaction;
            int rowsAffected = removeAttributeTypeFromAttributeTypesCommand.ExecuteNonQuery();
        }

        private void RemoveType(SQLiteCommand removeTypeCommand, long dbid)
        {
            removeTypeCommand.Parameters.Clear();
            removeTypeCommand.Parameters.AddWithValue("@typeId", dbid);

            removeTypeCommand.Transaction = persistenceProvider.transaction;
            int rowsAffected = removeTypeCommand.ExecuteNonQuery();
        }

        private long FillAttributeType(SQLiteCommand fillAttributeTypeCommand, AttributeType attributeType, InheritanceType type)
        {
            fillAttributeTypeCommand.Parameters.Clear();
            fillAttributeTypeCommand.Parameters.AddWithValue("@typeId", persistenceProvider.TypeNameToDbId[type.PackagePrefixedName]);
            fillAttributeTypeCommand.Parameters.AddWithValue("@attributeName", attributeType.Name);
            fillAttributeTypeCommand.Parameters.AddWithValue("@xgrsType", TypesHelper.AttributeTypeToXgrsType(attributeType));

            fillAttributeTypeCommand.Transaction = persistenceProvider.transaction;
            int rowsAffected = fillAttributeTypeCommand.ExecuteNonQuery();

            long rowId = persistenceProvider.connection.LastInsertRowId; // attributeTypeId
            return rowId;
        }

        private SQLiteCommand GetFillAttributeTypeCommand()
        {
            String tableName = "attributeTypes";
            StringBuilder columnNames = new StringBuilder();
            StringBuilder parameterNames = new StringBuilder();
            PersistenceProviderSQLite.AddInsertParameter(columnNames, parameterNames, "typeId");
            PersistenceProviderSQLite.AddInsertParameter(columnNames, parameterNames, "attributeName");
            PersistenceProviderSQLite.AddInsertParameter(columnNames, parameterNames, "xgrsType");

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

            return new SQLiteCommand(command.ToString(), persistenceProvider.connection);
        }

        private Dictionary<string, TypeKind> ReadKnownTypes()
        {
            // the grgen type id from the model is only unique per kind, and not stable upon insert(/delete), so we have to match by name to map to the database type id

            Dictionary<string, TypeKind> typeNameToKind = new Dictionary<string, TypeKind>();

            using(SQLiteCommand command = PrepareStatementsForReadingKnownTypes())
            {
                command.Transaction = persistenceProvider.transaction;
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    Dictionary<string, int> nameToColumnIndex = PersistenceProviderSQLite.GetNameToColumnIndexMapping(reader);
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
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "typeId");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "kind");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "name");

            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append(tableName);

            return new SQLiteCommand(command.ToString(), persistenceProvider.connection);
        }

        private Dictionary<string, AttributeTypesFromDatabase> ReadKnownTypesWithAttributes()
        {
            Dictionary<string, AttributeTypesFromDatabase> typeNameToAttributeTypes = new Dictionary<string, AttributeTypesFromDatabase>();

            using(SQLiteCommand command = PrepareStatementsForReadingKnownTypesWithAttributes())
            {
                command.Transaction = persistenceProvider.transaction;
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    Dictionary<string, int> nameToColumnIndex = PersistenceProviderSQLite.GetNameToColumnIndexMapping(reader);
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
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "types.typeId");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "kind");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "name");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "attributeTypeId");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "attributeName");
            PersistenceProviderSQLite.AddQueryColumn(columnNames, "xgrsType");

            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append("types");
            command.Append(" LEFT JOIN ");
            command.Append("attributeTypes");
            command.Append(" ON (types.typeId == attributeTypes.typeId)");

            return new SQLiteCommand(command.ToString(), persistenceProvider.connection);
        }

        // todo: IfMissing is not needed anymore, just insert; often only db id mapping needed, but dbid should be queried...
        private void InsertGraphIfMissing(long graphId, long typeId)
        {
            using(SQLiteCommand replaceHostGraphCommand = PrepareHostGraphInsert())
            {
                replaceHostGraphCommand.Parameters.Clear();
                replaceHostGraphCommand.Parameters.AddWithValue("@graphId", graphId);
                replaceHostGraphCommand.Parameters.AddWithValue("@typeId", typeId);
                replaceHostGraphCommand.Parameters.AddWithValue("@name", persistenceProvider.graph.Name);

                replaceHostGraphCommand.Transaction = persistenceProvider.transaction;
                int rowsAffected = replaceHostGraphCommand.ExecuteNonQuery();

                long rowId = persistenceProvider.connection.LastInsertRowId;
                Debug.Assert(rowId == graphId);
                persistenceProvider.AddGraphWithDbIdToDbIdMapping(persistenceProvider.graph, rowId);
            }
        }

        private bool TypeHasInstances(String typeName, TypeKind typeKind)
        {
            string idColumnName = ToIdColumnName(typeKind);
            using(SQLiteCommand command = GetTypeHasInstancesQuery(typeName, idColumnName))
            {
                command.Transaction = persistenceProvider.transaction;
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    Dictionary<string, int> nameToColumnIndex = PersistenceProviderSQLite.GetNameToColumnIndexMapping(reader);
                    while(reader.Read())
                    {
                        long result = reader.GetInt64(0);
                        return result != 0;
                    }
                }
            }
            return false;
        }

        private SQLiteCommand GetTypeHasInstancesQuery(String typeName, String idColumnName)
        {
            String package;
            String name;
            UnpackPackagePrefixedName(typeName, out package, out name);
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(package, name);

            StringBuilder command = new StringBuilder();
            command.Append("SELECT EXISTS(");
            command.Append("SELECT ");
            command.Append(idColumnName);
            command.Append(" FROM ");
            command.Append(tableName);
            command.Append(")");
            return new SQLiteCommand(command.ToString(), persistenceProvider.connection);
        }

        #endregion Types table populating/handling

        private SQLiteCommand PrepareHostGraphInsert()
        {
            String tableName = "graphs";
            StringBuilder columnNames = new StringBuilder();
            StringBuilder parameterNames = new StringBuilder();
            PersistenceProviderSQLite.AddInsertParameter(columnNames, parameterNames, "graphId");
            PersistenceProviderSQLite.AddInsertParameter(columnNames, parameterNames, "typeId");
            PersistenceProviderSQLite.AddInsertParameter(columnNames, parameterNames, "name");
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

            return new SQLiteCommand(command.ToString(), persistenceProvider.connection);
        }

        private SQLiteCommand PrepareContainerInitializingInsert(InheritanceType type, string ownerIdColumnName, AttributeType attributeType)
        {
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(type.Package, type.Name, attributeType.Name);
            String ownerTypeTableName = PersistenceProviderSQLite.GetUniqueTableName(type.Package, type.Name);
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
            command.Append(" WHERE true"); // needed by the SQLite parser

            return new SQLiteCommand(command.ToString(), persistenceProvider.connection);
        }

        #region Simple mapping code

        private static string ToString(TypeKind kind)
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

        private static string ToIdColumnName(TypeKind kind)
        {
            return ToString(kind) + "Id";
        }

        private static TypeKind ToKind(InheritanceType type)
        {
            if(type is NodeType)
                return TypeKind.NodeClass;
            else if(type is EdgeType)
                return TypeKind.EdgeClass;
            else if(type is ObjectType)
                return TypeKind.ObjectClass;
            throw new Exception("INTERNAL ERROR");
        }

        private static string ToKindString(InheritanceType type)
        {
            if(type is NodeType)
                return "node";
            else if(type is EdgeType)
                return "edge";
            else if(type is ObjectType)
                return "object";
            throw new Exception("INTERNAL ERROR");
        }

        private static string ToIdColumnName(InheritanceType type)
        {
            return ToKindString(type) + "Id";
        }

        private static void UnpackPackagePrefixedName(string packagePrefixedName, out string package, out string name)
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

        private static bool IsOfSameKind(TypeKind typeKindFromDatabase, InheritanceType typeFromModel)
        {
            if(typeKindFromDatabase == TypeKind.NodeClass && typeFromModel is NodeType)
                return true;
            else if(typeKindFromDatabase == TypeKind.EdgeClass && typeFromModel is EdgeType)
                return true;
            else if(typeKindFromDatabase == TypeKind.ObjectClass && typeFromModel is ObjectType)
                return true;
            else if(typeKindFromDatabase == TypeKind.GraphClass)
                throw new Exception("Unexpected kind - internal error!");
            return false;
        }

        #endregion Simple mapping code

        #region Database id from/to concept mapping maintenance

        private void AddTypeNameWithDbIdToDbIdMapping(String typeName, long dbid)
        {
            persistenceProvider.DbIdToTypeName.Add(dbid, typeName);
            persistenceProvider.TypeNameToDbId.Add(typeName, dbid);
        }

        private void RemoveTypeNameFromDbIdMapping(String typeName)
        {
            persistenceProvider.DbIdToTypeName.Remove(persistenceProvider.TypeNameToDbId[typeName]);
            persistenceProvider.TypeNameToDbId.Remove(typeName);
        }
        
        #endregion Database id from/to concept mapping maintenance
    }
}
