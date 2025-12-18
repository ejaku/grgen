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
    /// A class used in adapting the stored database model to the model of the host graph, which includes initialization of an empty database model.
    /// </summary>
    internal class ModelInitializerAndUpdater
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
            // contains entity types and enum types
            public readonly Dictionary<string, AttributeTypesFromDatabase> TypesToAttributesFromDatabase;

            public TypesFromDatabase(Dictionary<string, AttributeTypesFromDatabase> typesToAttributesFromDatabase)
            {
                TypesToAttributesFromDatabase = typesToAttributesFromDatabase;
            }

            public AttributeTypesFromDatabase this[string typeName]
            {
                get { return TypesToAttributesFromDatabase[typeName]; }
            }

            public bool IsTypeOfSameKindKnown(InheritanceType type, TypeKind kind)
            {
                return TypesToAttributesFromDatabase.ContainsKey(type.PackagePrefixedName)
                    && TypesToAttributesFromDatabase[type.PackagePrefixedName].KindOfOwner == kind;
            }

            public bool IsTypeOfSameKindKnown(EnumAttributeType type)
            {
                return TypesToAttributesFromDatabase.ContainsKey(type.PackagePrefixedName)
                    && TypesToAttributesFromDatabase[type.PackagePrefixedName].KindOfOwner == TypeKind.Enum;
            }

            public bool IsTypeKnown(InheritanceType type)
            {
                return TypesToAttributesFromDatabase.ContainsKey(type.PackagePrefixedName);
            }

            public bool IsTypeKnown(EnumAttributeType type)
            {
                return TypesToAttributesFromDatabase.ContainsKey(type.PackagePrefixedName);
            }

            public bool SomeTypeHasAnAttributeOfType(String typeName)
            {
                foreach(KeyValuePair<string, AttributeTypesFromDatabase> typeToAttributeTypes in TypesToAttributesFromDatabase)
                {
                    AttributeTypesFromDatabase attributeTypes = typeToAttributeTypes.Value;

                    foreach(KeyValuePair<string, AttributeTypeFromDatabase> attributeNameToAttributeType in attributeTypes.AttributeNamesToAttributeTypes)
                    {
                        AttributeTypeFromDatabase attributeType = attributeNameToAttributeType.Value;

                        if(attributeType.XgrsType == typeName)
                            return true;
                    }
                }

                return false;
            }

            public void WriteDatabaseModelToFile(StreamWriter sw)
            {
                foreach(KeyValuePair<string, AttributeTypesFromDatabase> typeToAttributes in TypesToAttributesFromDatabase)
                {
                    string typeName = typeToAttributes.Key;
                    AttributeTypesFromDatabase attributeTypesFromDatabase = typeToAttributes.Value;

                    if(attributeTypesFromDatabase.KindOfOwner == TypeKind.NodeClass
                        || attributeTypesFromDatabase.KindOfOwner == TypeKind.EdgeClass
                        || attributeTypesFromDatabase.KindOfOwner == TypeKind.ObjectClass)
                    {
                        if(typeName == "Node" || typeName == "AEdge" || typeName == "Edge" || typeName == "UEdge" || typeName == "Object")
                        {
                            sw.Write("// " + ToClassPrefixString(attributeTypesFromDatabase.KindOfOwner) + "class ");
                            sw.WriteLine(typeName + ";");
                            continue;
                        }

                        sw.Write(ToClassPrefixString(attributeTypesFromDatabase.KindOfOwner) + "class ");
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
                    else if(attributeTypesFromDatabase.KindOfOwner == TypeKind.Enum)
                    {
                        sw.Write("enum ");
                        sw.Write(typeName);
                        sw.Write(" { ");
                        bool first = true;
                        foreach(AttributeTypeFromDatabase enumCase in attributeTypesFromDatabase.AttributeNamesToAttributeTypes.Values)
                        {
                            if(first)
                                first = false;
                            else
                                sw.Write(", ");
                            sw.Write(enumCase.Name + " = " + enumCase.XgrsType);
                        }
                        sw.WriteLine(" } // cases are matched by name only when persisting");
                    }
                }
            }

            private static string ToClassPrefixString(TypeKind kind)
            {
                switch(kind)
                {
                    case TypeKind.NodeClass: return "node ";
                    case TypeKind.EdgeClass: return "edge ";
                    case TypeKind.ObjectClass: return "";
                    default: throw new Exception("INTERNAL ERROR");
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
            public readonly string Name; // of attribute or enum case
            public readonly string XgrsType; // type of attribute or value of enum case
            public readonly long DbId;

            public AttributeTypeFromDatabase(string name, string xgrsType, long dbId)
            {
                Name = name;
                XgrsType = xgrsType;
                DbId = dbId;
            }
        }


        PersistenceProviderSQLite persistenceProvider;


        internal ModelInitializerAndUpdater(PersistenceProviderSQLite persistenceProvider)
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

        #region Database initialization (including types table)

        private void CreateIdentityAndTopologyTables()
        {
            CreateNodesTable();
            CreateEdgesTable();
            CreateGraphsTable();
            CreateObjectsTable();
        }

        private void CreateNodesTable()
        {
            persistenceProvider.CreateTable("nodes", "nodeId",
                "typeId", "INTEGER NOT NULL", // type defines table where attributes are stored
                "graphId", "INTEGER NOT NULL",
                "name", "TEXT NOT NULL" // maybe TODO: name in extra table - this is for a named graph, but should be also available for unnamed graphs in the end...
                );
            // AddIndex("nodes", "graphId"); maybe add later again for quick graph purging 
        }

        private void CreateEdgesTable()
        {
            persistenceProvider.CreateTable("edges", "edgeId",
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
            persistenceProvider.CreateTable("graphs", "graphId",
                "typeId", "INTEGER NOT NULL",
                "name", "TEXT NOT NULL"
                );
        }

        private void CreateObjectsTable()
        {
            persistenceProvider.CreateTable("objects", "objectId",
                "typeId", "INTEGER NOT NULL",
                "name", "TEXT NOT NULL"
                );
        }

        private void CreateTypesWithAttributeTables()
        {
            CreateTypesTable();
            CreateAttributeTypesTable();
        }

        private void CreateTypesTable()
        {
            persistenceProvider.CreateTable("types", "typeId",
                "kind", "INT NOT NULL",
                "name", "TEXT NOT NULL"
                );
        }

        private void CreateAttributeTypesTable()
        {
            persistenceProvider.CreateTable("attributeTypes", "attributeTypeId",
                "typeId", "INTEGER NOT NULL",
                "attributeName", "TEXT NOT NULL",
                "xgrsType", "TEXT NOT NULL"
                );
            persistenceProvider.AddIndex("attributeTypes", "typeId");
        }

        // fills types when model is empty, which only happens when database is freshly created (this also means the model mapping is empty)
        private void FillInitialTypes()
        {
            FillModelTypesToTypesTable();

            // TODO: create configuration and status table, a key-value-store, esp. including version, plus later stuff?

            IGraphModel model = persistenceProvider.graph.Model;

            foreach(NodeType nodeType in model.NodeModel.Types)
            {
                CreateInheritanceTypeTable(nodeType, "nodeId");

                foreach(AttributeType attributeType in nodeType.AttributeTypes)
                {
                    if(!PersistenceProviderSQLite.IsContainerType(attributeType))
                        continue;
                    CreateContainerTypeTable(nodeType, "nodeId", attributeType);
                }
            }

            foreach(EdgeType edgeType in model.EdgeModel.Types)
            {
                CreateInheritanceTypeTable(edgeType, "edgeId");

                foreach(AttributeType attributeType in edgeType.AttributeTypes)
                {
                    if(!PersistenceProviderSQLite.IsContainerType(attributeType))
                        continue;
                    CreateContainerTypeTable(edgeType, "edgeId", attributeType);
                }
            }

            foreach(ObjectType objectType in model.ObjectModel.Types)
            {
                CreateInheritanceTypeTable(objectType, "objectId");

                foreach(AttributeType attributeType in objectType.AttributeTypes)
                {
                    if(!PersistenceProviderSQLite.IsContainerType(attributeType))
                        continue;
                    CreateContainerTypeTable(objectType, "objectId", attributeType);
                }
            }
        }

        private void FillModelTypesToTypesTable()
        {
            using(SQLiteCommand fillTypeCommand = PrepareFillTypeCommand())
            {
                FillTypeWithAttributes(fillTypeCommand, new GraphClassDummy(), TypeKind.GraphClass);

                IGraphModel model = persistenceProvider.graph.Model;

                foreach(EnumAttributeType enumType in model.EnumAttributeTypes)
                {
                    FillEnumWithCases(fillTypeCommand, enumType);
                }

                foreach(NodeType nodeType in model.NodeModel.Types)
                {
                    FillTypeWithAttributes(fillTypeCommand, nodeType, TypeKind.NodeClass);
                }
                foreach(EdgeType edgeType in model.EdgeModel.Types)
                {
                    FillTypeWithAttributes(fillTypeCommand, edgeType, TypeKind.EdgeClass);
                }
                foreach(ObjectType objectType in model.ObjectModel.Types)
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

        private void FillAttributeTypes(InheritanceType type)
        {
            using(SQLiteCommand fillAttributeTypeCommand = PrepareFillAttributeTypeCommand())
            {
                foreach(AttributeType attributeType in type.AttributeTypes)
                {
                    FillAttributeType(fillAttributeTypeCommand, attributeType, type);
                }
            }
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

        private void FillEnumWithCases(SQLiteCommand fillEnumTypeCommand, EnumAttributeType type)
        {
            FillEnumType(fillEnumTypeCommand, type);
            FillEnumCases(type);
        }

        private void FillEnumType(SQLiteCommand fillTypeCommand, EnumAttributeType type)
        {
            fillTypeCommand.Parameters.Clear();
            fillTypeCommand.Parameters.AddWithValue("@kind", (int)TypeKind.Enum);
            fillTypeCommand.Parameters.AddWithValue("@name", type.PackagePrefixedName); // may include colons which are removed from the table name

            fillTypeCommand.Transaction = persistenceProvider.transaction;
            int rowsAffected = fillTypeCommand.ExecuteNonQuery();

            long rowId = persistenceProvider.connection.LastInsertRowId;
            AddTypeNameWithDbIdToDbIdMapping(type.PackagePrefixedName, rowId); // won't appear in the normal tables, only used in the attributeTypes table
        }

        private void FillEnumCases(EnumAttributeType type)
        {
            using(SQLiteCommand fillEnumCasesCommand = PrepareFillAttributeTypeCommand())
            {
                foreach(EnumMember enumCase in type.Members)
                {
                    FillEnumCase(fillEnumCasesCommand, enumCase.Name, enumCase.Value.ToString(), type);
                }
            }
        }

        private long FillEnumCase(SQLiteCommand fillAttributeTypeCommand, String enumCase, String value, EnumAttributeType type)
        {
            fillAttributeTypeCommand.Parameters.Clear();
            fillAttributeTypeCommand.Parameters.AddWithValue("@typeId", persistenceProvider.TypeNameToDbId[type.PackagePrefixedName]);
            fillAttributeTypeCommand.Parameters.AddWithValue("@attributeName", enumCase);
            fillAttributeTypeCommand.Parameters.AddWithValue("@xgrsType", value);

            fillAttributeTypeCommand.Transaction = persistenceProvider.transaction;
            int rowsAffected = fillAttributeTypeCommand.ExecuteNonQuery();

            long rowId = persistenceProvider.connection.LastInsertRowId; // attributeTypeId
            return rowId;
        }

        private void CreateInheritanceTypeTable(InheritanceType type, String idColumnName)
        {
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(type.Package, type.Name);
            List<String> columnNamesAndTypes = new List<String>(); // ArrayBuilder
            foreach(AttributeType attributeType in type.AttributeTypes)
            {
                if(!PersistenceProviderSQLite.IsAttributeTypeMappedToDatabaseColumn(attributeType))
                    continue;

                columnNamesAndTypes.Add(PersistenceProviderSQLite.GetUniqueColumnName(attributeType.Name));
                columnNamesAndTypes.Add(PersistenceProviderSQLite.ScalarAndReferenceTypeToSQLiteType(attributeType));
            }

            persistenceProvider.CreateTable(tableName, idColumnName, columnNamesAndTypes.ToArray()); // the id in the type table is a local copy of the global id from the corresponding topology table
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
                columnNamesAndTypes.Add(PersistenceProviderSQLite.ScalarAndReferenceTypeToSQLiteType(attributeType.ValueType));
            }
            else if(attributeType.Kind == AttributeKind.MapAttr)
            {
                columnNamesAndTypes.Add("key");
                columnNamesAndTypes.Add(PersistenceProviderSQLite.ScalarAndReferenceTypeToSQLiteType(attributeType.KeyType));
                columnNamesAndTypes.Add("value");
                columnNamesAndTypes.Add(PersistenceProviderSQLite.ScalarAndReferenceTypeToSQLiteType(attributeType.ValueType));
            }
            else
            {
                columnNamesAndTypes.Add("value");
                columnNamesAndTypes.Add(PersistenceProviderSQLite.ScalarAndReferenceTypeToSQLiteType(attributeType.ValueType));
                columnNamesAndTypes.Add("key");
                columnNamesAndTypes.Add(PersistenceProviderSQLite.ScalarAndReferenceTypeToSQLiteType(persistenceProvider.IntegerAttributeType));
            }

            persistenceProvider.CreateTable(tableName, "entryId", columnNamesAndTypes.ToArray()); // the entryId denotes the row local to this table, the ownerIdColumnName is a local copy of the global id from the corresponding topology table

            persistenceProvider.AddIndex(tableName, ownerIdColumnName); // in order to delete without a full table scan (I assume small changesets in between database openings and decided for a by-default pruning on open - an alternative would be a full table replacement once in a while (would be ok in case of big changesets, as they would occur when a pruning run only occurs on explicit request, but such ones could be forgotten/missed unknowingly too easily, leading to (unexpected) slowness))
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

        #endregion Database initialization (including types table)

        #region Database adaptation to type/attribute changes from the model

        private void AdaptDatabaseTypesToModelTypes()
        {
            // the types mapping was filled from the database before, it is used during the model update, it is updated stepwise with new model, and afterwards fits to the new model
            // the types from the database are read once, used in the model update, and thrown away after the update (the old model from the database is the source, the target is the new model from the graph, defined by the model file/assembly)
            TypesFromDatabase typesFromDatabase = new TypesFromDatabase(ReadKnownTypesWithAttributes());

            Dictionary<EnumAttributeType, SetValueType> newEnumTypes = GetNewEnumTypes(typesFromDatabase); // types from the model not in the database (maybe todo: adapt names to reflect this)
            Dictionary<String, TypeKind> deletedEnumTypes = GetDeletedEnumTypes(typesFromDatabase); // types from the database not in the model (maybe todo: adapt names to reflect this)
            Dictionary<InheritanceType, SetValueType> typeChangedEnumTypes = GetTypeChangedEnumTypes(typesFromDatabase); // enum in database, entity type in model
            bool changeMessagePrinted = false;
            ReportEnumTypeChangesToTheUserAbortingIfAttributesOfRemovedOrRetypedEnumsExist(newEnumTypes, deletedEnumTypes, typeChangedEnumTypes,
                typesFromDatabase, ref changeMessagePrinted);

            Dictionary<string, EnumAttributeType> keptEnumTypes = GetKeptEnumTypes(typesFromDatabase); // enum case changes are only computed for types in the model _and_ in the database
            List<KeyValuePair<EnumAttributeType, string>> addedEnumCases = new List<KeyValuePair<EnumAttributeType, string>>();
            List<KeyValuePair<string, AttributeTypeFromDatabase>> removedEnumCases = new List<KeyValuePair<string, AttributeTypeFromDatabase>>();
            foreach(KeyValuePair<string, EnumAttributeType> typeNameToType in keptEnumTypes)
            {
                string typeName = typeNameToType.Key;
                EnumAttributeType type = typeNameToType.Value;

                DetermineAndReportEnumCaseChangesToTheUser(type, typesFromDatabase[typeName],
                    ref addedEnumCases, ref removedEnumCases, ref changeMessagePrinted);
            }
            foreach(EnumAttributeType keptEnumType in keptEnumTypes.Values)
            {
                UpdateEnumCaseValues(keptEnumType, typesFromDatabase); // for development/debugging by the user only, refresh the integer value/set it to the value from the model
            }

            Dictionary<InheritanceType, SetValueType> newEntityTypes = GetNewEntityTypes(typesFromDatabase); // types from the model not in the database (maybe todo: adapt names to reflect this)
            Dictionary<String, TypeKind> deletedEntityTypes = GetDeletedEntityTypes(typesFromDatabase); // types from the database not in the model (maybe todo: adapt names to reflect this)
            Dictionary<INamed, TypeKind> typeChangedEntityTypes = GetTypeChangedEntityTypes(typesFromDatabase); // same name but changed type kind (may be enum in model, cannot be enum in database)
            ReportEntityTypeChangesToTheUserAbortingIfInstancesOfRemovedOrRetypedTypesExist(newEntityTypes, deletedEntityTypes, typeChangedEntityTypes,
                typesFromDatabase, ref changeMessagePrinted);

            Dictionary<string, InheritanceType> keptEntityTypes = GetKeptEntityTypes(typesFromDatabase); // attribute changes are only computed for types in the model _and_ in the database
            List<KeyValuePair<InheritanceType, AttributeType>> addedAttributes = new List<KeyValuePair<InheritanceType, AttributeType>>();
            List<KeyValuePair<string, AttributeTypeFromDatabase>> removedAttributes = new List<KeyValuePair<string, AttributeTypeFromDatabase>>();
            List<KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>> typeChangedAttributes = new List<KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>>(); // type differs in between model and database
            foreach(KeyValuePair<string, InheritanceType> typeNameToType in keptEntityTypes)
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

            bool adaptDatabaseModelToModel = QueryUserWhetherToAdaptDatabaseToModel(
                removedAttributes, typeChangedAttributes, removedEnumCases,
                persistenceProvider.persistentGraphParameters);
            if(!adaptDatabaseModelToModel)
            {
                WriteDatabaseModelToFileAndReportToUser(typesFromDatabase, persistenceProvider.connectionParameters);
                ConsoleUI.outWriter.WriteLine("Aborting database model update as requested...");
                throw new Exception("Abort database model update as requested.");
            }

            ConsoleUI.outWriter.WriteLine("Overall there are "
                + "{0} new enum types, {1} deleted enum types, {2} enum types kind changed to an entity type, "
                + "{3} added enum cases, {4} removed enum cases, "
                + "{5} new entity types, {6} deleted entity types, {7} entity types of a different kind (maybe even enum), "
                + "{8} added attributes, {9} removed attributes, {10} attributes of a different type, when comparing the database to the model.",
                newEnumTypes.Count, deletedEnumTypes.Count, typeChangedEnumTypes.Count,
                addedEnumCases.Count, removedEnumCases.Count,
                newEntityTypes.Count, deletedEntityTypes.Count, typeChangedEntityTypes.Count,
                addedAttributes.Count, removedAttributes.Count, typeChangedAttributes.Count);
            ConsoleUI.outWriter.WriteLine("The database is going to be updated: "
                + "{0} types are going to be introduced, {1} types are going to be deleted, "
                + "{2} attributes are going to be added, {3} attributes are going to be removed, "
                + "{4} enum cases are going to be added, {5} enum cases are going to be removed.",
                newEntityTypes.Count + newEnumTypes.Count + typeChangedEntityTypes.Count + typeChangedEnumTypes.Count,
                deletedEntityTypes.Count + deletedEnumTypes.Count + typeChangedEntityTypes.Count + typeChangedEnumTypes.Count,
                addedAttributes.Count + typeChangedAttributes.Count, removedAttributes.Count + typeChangedAttributes.Count,
                addedEnumCases.Count, removedEnumCases.Count);

            Stopwatch stopwatch = Stopwatch.StartNew();
            ConsoleUI.outWriter.WriteLine("Updating the database model to the current model...");

            using(persistenceProvider.transaction = persistenceProvider.connection.BeginTransaction())
            {
                try
                {
                    RemoveDeletedEnums(deletedEnumTypes);
                    RemoveTypeChangedEnumsAndAddEntityTypes(typeChangedEnumTypes);
                    AddNewEnums(newEnumTypes);

                    foreach(KeyValuePair<string, AttributeTypeFromDatabase> removedEnumCase in removedEnumCases)
                    {
                        AttributeTypeFromDatabase enumCaseAttributeType = removedEnumCase.Value;

                        RemoveDeletedEnumCase(enumCaseAttributeType);
                    }
                    foreach(KeyValuePair<EnumAttributeType, string> addedEnumCase in addedEnumCases)
                    {
                        EnumAttributeType enumType = addedEnumCase.Key;
                        string enumCaseName = addedEnumCase.Value;
                        EnumMember member = GetEnumCase(enumType, enumCaseName);

                        AddNewEnumCase(enumType, enumCaseName, member.Value.ToString());
                    }

                    RemoveDeletedEntityTypes(deletedEntityTypes, typesFromDatabase); // first remove than add to prevent duplicate name collisions
                    RemoveAndAddTypeChangedEntityTypes(typeChangedEntityTypes, typesFromDatabase);
                    AddNewEntityTypes(newEntityTypes);

                    ConsoleUI.outWriter.WriteLine("...done with the types, continuing with the attributes..."); // potential todo: more detailed progress, reporting statistics about database concepts

                    foreach(KeyValuePair<string, AttributeTypeFromDatabase> removedAttribute in removedAttributes)
                    {
                        string typeName = removedAttribute.Key;
                        AttributeTypeFromDatabase attributeTypeFromDatabase = removedAttribute.Value;

                        RemoveDeletedEntityTypeAttribute(typeName, attributeTypeFromDatabase);
                    }
                    foreach(KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>> typeChangedAttribute in typeChangedAttributes)
                    {
                        InheritanceType type = typeChangedAttribute.Key;
                        AttributeType attributeType = typeChangedAttribute.Value.Key;
                        AttributeTypeFromDatabase attributeTypeFromDatabase = typeChangedAttribute.Value.Value;

                        RemoveDeletedEntityTypeAttribute(type.PackagePrefixedName, attributeTypeFromDatabase);
                        AddNewEntityTypeAttribute(type, attributeType);
                    }
                    foreach(KeyValuePair<InheritanceType, AttributeType> addedAttribute in addedAttributes)
                    {
                        InheritanceType type = addedAttribute.Key;
                        AttributeType attributeType = addedAttribute.Value;

                        AddNewEntityTypeAttribute(type, attributeType);
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

        private Dictionary<InheritanceType, SetValueType> GetNewEntityTypes(TypesFromDatabase typesFromDatabase)
        {
            Dictionary<InheritanceType, SetValueType> newEntityTypes = new Dictionary<InheritanceType, SetValueType>();

            IGraphModel model = persistenceProvider.graph.Model;

            foreach(NodeType nodeType in model.NodeModel.Types)
            {
                if(!typesFromDatabase.IsTypeKnown(nodeType))
                    newEntityTypes.Add(nodeType, null);
            }

            foreach(EdgeType edgeType in model.EdgeModel.Types)
            {
                if(!typesFromDatabase.IsTypeKnown(edgeType))
                    newEntityTypes.Add(edgeType, null);
            }

            foreach(ObjectType objectType in model.ObjectModel.Types)
            {
                if(!typesFromDatabase.IsTypeKnown(objectType))
                    newEntityTypes.Add(objectType, null);
            }

            return newEntityTypes;
        }

        private Dictionary<string, TypeKind> GetDeletedEntityTypes(TypesFromDatabase typesFromDatabase)
        {
            Dictionary<string, TypeKind> deletedEntityTypes = new Dictionary<string, TypeKind>();

            Dictionary<string, InheritanceType> entityModelTypes = GetEntityTypesFromModel();
            Dictionary<string, EnumAttributeType> enumModelTypes = GetEnumTypesFromModel();
            foreach(KeyValuePair<string, AttributeTypesFromDatabase> typeNameToAttributeTypes in typesFromDatabase.TypesToAttributesFromDatabase)
            {
                string typeName = typeNameToAttributeTypes.Key;
                AttributeTypesFromDatabase attributeTypesFromDatabase = typeNameToAttributeTypes.Value;

                TypeKind typeKind = attributeTypesFromDatabase.KindOfOwner; // filter to only entity types
                if(typeKind == TypeKind.GraphClass)
                    continue;
                if(typeKind == TypeKind.Enum)
                    continue;

                if(!entityModelTypes.ContainsKey(typeName) && !enumModelTypes.ContainsKey(typeName))
                    deletedEntityTypes.Add(typeName, attributeTypesFromDatabase.KindOfOwner);
            }

            return deletedEntityTypes;
        }

        private Dictionary<INamed, TypeKind> GetTypeChangedEntityTypes(TypesFromDatabase typesFromDatabase)
        {
            Dictionary<INamed, TypeKind> typeChangedEntityTypes = new Dictionary<INamed, TypeKind>();

            Dictionary<string, InheritanceType> entityModelTypes = GetEntityTypesFromModel();
            Dictionary<string, EnumAttributeType> enumModelTypes = GetEnumTypesFromModel();
            foreach(KeyValuePair<string, AttributeTypesFromDatabase> typeNameToAttributeTypes in typesFromDatabase.TypesToAttributesFromDatabase)
            {
                String typeName = typeNameToAttributeTypes.Key;
                AttributeTypesFromDatabase attributeTypesFromDatabase = typeNameToAttributeTypes.Value;

                TypeKind typeKind = attributeTypesFromDatabase.KindOfOwner;
                if(typeKind == TypeKind.GraphClass)
                    continue;
                if(typeKind == TypeKind.Enum)
                    continue; // type kind changes from enum type to an entity type were already handled with enums

                if(entityModelTypes.ContainsKey(typeName))
                {
                    if(!IsOfSameKind(typeKind, entityModelTypes[typeName]))
                        typeChangedEntityTypes.Add(entityModelTypes[typeName], typeKind);
                }
                else if(enumModelTypes.ContainsKey(typeName))
                {
                    typeChangedEntityTypes.Add(enumModelTypes[typeName], typeKind);
                }
            }

            return typeChangedEntityTypes;
        }

        private Dictionary<string, InheritanceType> GetKeptEntityTypes(TypesFromDatabase typesFromDatabase)
        {
            Dictionary<string, InheritanceType> keptEntityTypes = new Dictionary<string, InheritanceType>();

            Dictionary<string, InheritanceType> entityModelTypes = GetEntityTypesFromModel();
            foreach(KeyValuePair<string, AttributeTypesFromDatabase> typeNameToAttributeTypes in typesFromDatabase.TypesToAttributesFromDatabase)
            {
                String typeName = typeNameToAttributeTypes.Key;
                AttributeTypesFromDatabase attributeTypesFromDatabase = typeNameToAttributeTypes.Value;

                TypeKind typeKind = attributeTypesFromDatabase.KindOfOwner; // filter to only entity types
                if(typeKind == TypeKind.GraphClass)
                    continue;
                if(typeKind == TypeKind.Enum)
                    continue;

                if(entityModelTypes.ContainsKey(typeName))
                {
                    if(IsOfSameKind(typeKind, entityModelTypes[typeName]))
                        keptEntityTypes.Add(typeName, entityModelTypes[typeName]);
                }
            }

            return keptEntityTypes;
        }

        private Dictionary<string, InheritanceType> GetEntityTypesFromModel()
        {
            Dictionary<string, InheritanceType> entityModelTypes = new Dictionary<string, InheritanceType>();

            IGraphModel model = persistenceProvider.graph.Model;

            foreach(NodeType nodeType in model.NodeModel.Types)
            {
                entityModelTypes.Add(nodeType.PackagePrefixedName, nodeType);
            }

            foreach(EdgeType edgeType in model.EdgeModel.Types)
            {
                entityModelTypes.Add(edgeType.PackagePrefixedName, edgeType);
            }

            foreach(ObjectType objectType in model.ObjectModel.Types)
            {
                entityModelTypes.Add(objectType.PackagePrefixedName, objectType);
            }

            return entityModelTypes;
        }

        private void ReportEntityTypeChangesToTheUserAbortingIfInstancesOfRemovedOrRetypedTypesExist(Dictionary<InheritanceType, SetValueType> newEntityTypes,
            Dictionary<String, TypeKind> deletedEntityTypes, Dictionary<INamed, TypeKind> typeChangedEntityTypes,
            TypesFromDatabase typesFromDatabase, ref bool changeMessagePrinted)
        {
            bool deletedTypeStillHasInstances = false;
            foreach(KeyValuePair<String, TypeKind> typeNameToKind in deletedEntityTypes)
            {
                string typeName = typeNameToKind.Key;
                TypeKind typeKind = typeNameToKind.Value;

                string kindName = ToString(typeKind);
                if(TypeHasInstances(typeName, typeKind))
                {
                    PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                    ConsoleUI.outWriter.WriteLine("The {0} class type {1} from the database is not existing in the model.",
                        kindName, typeName);
                    ConsoleUI.outWriter.WriteLine("- The {0} class type {1} has to be deleted from the database, but the database contains instances of it (ABORTING...).",
                        kindName, typeName);
                    deletedTypeStillHasInstances = true;
                }
            }

            foreach(KeyValuePair<INamed, TypeKind> typeToKind in typeChangedEntityTypes)
            {
                INamed type = typeToKind.Key;
                TypeKind kindInDatabase = typeToKind.Value;

                string typeName = type.PackagePrefixedName;
                TypeKind kindInModel = ToKind(typeToKind.Key);
                string kindInModelTypeString = ToTypeString(kindInModel);
                string kindInDatabaseTypeString = ToTypeString(kindInDatabase);

                if(TypeHasInstances(typeName, kindInDatabase))
                {
                    PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                    ConsoleUI.outWriter.WriteLine("The {0} type {1} from the database is a {2} type in the model.",
                        kindInDatabaseTypeString, typeName, kindInModelTypeString);
                    ConsoleUI.outWriter.WriteLine("- The {0} type {1} has to be deleted from the database, but the database contains instances of it (ABORTING...).",
                        kindInDatabaseTypeString, typeName);
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

            foreach(KeyValuePair<String, TypeKind> typeNameToKind in deletedEntityTypes)
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

            foreach(KeyValuePair<INamed, TypeKind> typeToKind in typeChangedEntityTypes)
            {
                INamed type = typeToKind.Key;
                TypeKind kindInDatabase = typeToKind.Value;

                string typeName = type.PackagePrefixedName;
                TypeKind kindInModel = ToKind(type);
                string kindInModelTypeString = ToTypeString(kindInModel);
                string kindInDatabaseTypeString = ToTypeString(kindInDatabase);

                PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                ConsoleUI.outWriter.WriteLine("The {0} type {1} from the database is a {2} type in the model.",
                    kindInDatabaseTypeString, typeName, kindInModelTypeString);
                ConsoleUI.outWriter.WriteLine("- The {0} type {1} is going to be deleted from the database.",
                    kindInDatabaseTypeString, typeName);
                ConsoleUI.outWriter.WriteLine("- The {0} type {1} is going to be added to the database.",
                    kindInModelTypeString, typeName);
            }

            foreach(InheritanceType type in newEntityTypes.Keys)
            {
                string typeName = type.PackagePrefixedName;
                string kindName = ToKindString(type);
                PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                ConsoleUI.outWriter.WriteLine("The database does not contain the {0} class type {1} (that exists in the model).",
                    kindName, typeName);
                ConsoleUI.outWriter.WriteLine("- The {0} class type {1} is going to be added to the database, too.",
                    kindName, typeName);
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
                    ConsoleUI.outWriter.WriteLine("- The attribute {0}.{1} is going to be removed from the database, too, and all its values are going to be purged (from the instances of the type), YOU MAY LOOSE DATA!",
                        type.PackagePrefixedName, attributeNameFromDatabase);

                    removedAttributes.Add(new KeyValuePair<string, AttributeTypeFromDatabase>(type.PackagePrefixedName, attributeTypeFromDatabase));
                }
                else if(attributeTypeFromDatabase.XgrsType != TypesHelper.AttributeTypeToXgrsType(attributeTypeFromModel))
                {
                    PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                    ConsoleUI.outWriter.WriteLine("The attribute {0}.{1} of type {2} from the database is of type {3} in the model!",
                        type.PackagePrefixedName, attributeNameFromDatabase, attributeTypeFromDatabase.XgrsType, TypesHelper.AttributeTypeToXgrsType(attributeTypeFromModel));
                    ConsoleUI.outWriter.WriteLine("- The attribute {0}.{1} is going to be removed from the database, and all its values are going to be purged (from the instances of the type), YOU MAY LOOSE DATA!",
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

        private static bool QueryUserWhetherToAdaptDatabaseToModel(
            List<KeyValuePair<string, AttributeTypeFromDatabase>> removedAttributes,
            List<KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>> typeChangedAttributes,
            List<KeyValuePair<string, AttributeTypeFromDatabase>> removedEnumCases,
            string persistentGraphParameters)
        {
            List<KeyValuePair<string, AttributeTypeFromDatabase>> removedAttributesAfterFiltering = new List<KeyValuePair<string, AttributeTypeFromDatabase>>(removedAttributes);
            List<KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>> typeChangedAttributesAfterFiltering = new List<KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>>(typeChangedAttributes);
            List<KeyValuePair<string, AttributeTypeFromDatabase>> removedEnumCasesAfterFiltering = new List<KeyValuePair<string, AttributeTypeFromDatabase>>(removedEnumCases);

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
                        removedEnumCasesAfterFiltering.Clear();
                        break;
                    }
                    else if(parameter.StartsWith("update/"))
                    {
                        String updateAttribute = parameter.Remove(0, "update/".Length);
                        String[] typeAttribute = updateAttribute.Split('.');
                        if(typeAttribute.Length == 2)
                        {
                            string type = typeAttribute[0];
                            string attribute = typeAttribute[1];
                            FilterOutAttributeOrEnumCaseFromUpdateListThatRequiresUserConfirmation(removedAttributesAfterFiltering,
                                typeChangedAttributesAfterFiltering, removedEnumCasesAfterFiltering, type, attribute);
                        }
                        else if(typeAttribute.Length == 1)
                        {
                            string type = typeAttribute[0];
                            string attribute = null;
                            FilterOutAttributeOrEnumCaseFromUpdateListThatRequiresUserConfirmation(removedAttributesAfterFiltering,
                                typeChangedAttributesAfterFiltering, removedEnumCasesAfterFiltering, type, attribute);
                        }
                        else
                        {
                            ConsoleUI.errorOutWriter.WriteLine("Ignoring malformed update command: " + parameter + " ('update/type.attr' or 'update/enumtype' expected)");
                            continue;
                        }
                    }
                }
            }

            if(removedAttributesAfterFiltering.Count + typeChangedAttributesAfterFiltering.Count + removedEnumCasesAfterFiltering.Count > 0)
            {
                ConsoleUI.outWriter.WriteLine("Proceed only if you already carried out all migrations you wanted to, otherwise you COULD LOOSE DATA (when the values of removed attributes are purged), or LOADING MAY FAIL thereafter (when removed enum cases are still in use) - not proceeding would allow you to revert the model and save the attribute values of worth from the database or filter out the removed enum cases from the attributes.");
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

        private static void FilterOutAttributeOrEnumCaseFromUpdateListThatRequiresUserConfirmation(
            List<KeyValuePair<string, AttributeTypeFromDatabase>> removedAttributes,
            List<KeyValuePair<InheritanceType, KeyValuePair<AttributeType, AttributeTypeFromDatabase>>> typeChangedAttributes,
            List<KeyValuePair<string, AttributeTypeFromDatabase>> removedEnumCases,
            string typeToFilterOut, string attributeToFilterOut)
        {
            if(attributeToFilterOut != null)
            {
                for(int i = 0; i < removedAttributes.Count; ++i)
                {
                    if(removedAttributes[i].Key == typeToFilterOut && removedAttributes[i].Value.Name == attributeToFilterOut)
                    {
                        removedAttributes.RemoveAt(i);
                        return;
                    }
                }

                for(int i = 0; i < typeChangedAttributes.Count; ++i)
                {
                    if(typeChangedAttributes[i].Key.PackagePrefixedName == typeToFilterOut && typeChangedAttributes[i].Value.Key.Name == attributeToFilterOut)
                    {
                        typeChangedAttributes.RemoveAt(i);
                        return;
                    }
                }
            }
            else
            {
                for(int i = removedEnumCases.Count - 1; i >= 0 ; --i)
                {
                    if(removedEnumCases[i].Key == typeToFilterOut)
                    {
                        removedEnumCases.RemoveAt(i);
                    }
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

        private void AddNewEntityTypes(Dictionary<InheritanceType, SetValueType> newEntityTypes)
        {
            foreach(InheritanceType entityType in newEntityTypes.Keys)
            {
                using(SQLiteCommand fillTypeCommand = PrepareFillTypeCommand())
                {
                    FillTypeWithAttributes(fillTypeCommand, entityType, ToKind(entityType));
                }

                String idColumnName = ToIdColumnName(entityType);

                CreateInheritanceTypeTable(entityType, idColumnName);

                foreach(AttributeType attributeType in entityType.AttributeTypes)
                {
                    if(!PersistenceProviderSQLite.IsContainerType(attributeType))
                        continue;
                    CreateContainerTypeTable(entityType, idColumnName, attributeType);
                }
            }
        }

        private void RemoveDeletedEntityTypes(Dictionary<String, TypeKind> deletedEntityTypes, TypesFromDatabase typesFromDatabase)
        {
            // delete nodes, no instances left because of previous check, nothing in topology tables, nothing in types tables, nothing in container tables(?)
            foreach(KeyValuePair<String, TypeKind> typeNameToKind in deletedEntityTypes)
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

                    if(PersistenceProviderSQLite.IsContainerType(attributeTypeFromDatabase.XgrsType))
                        DeleteContainerTypeTable(typeName, attributeName);
                }
                DeleteInheritanceTypeTable(typeName);
            }
        }

        private void RemoveAndAddTypeChangedEntityTypes(Dictionary<INamed, TypeKind> typeChangedEntityTypes, TypesFromDatabase typesFromDatabase)
        {
            Dictionary<string, TypeKind> deletedEntityTypes = new Dictionary<string, TypeKind>();
            Dictionary<InheritanceType, SetValueType> newEntityTypes = new Dictionary<InheritanceType, SetValueType>();
            Dictionary<EnumAttributeType, SetValueType> newEnumTypes = new Dictionary<EnumAttributeType, SetValueType>();
            foreach(KeyValuePair<INamed, TypeKind> typeChangedEntityTypeToKind in typeChangedEntityTypes)
            {
                INamed typeChangedEntityType = typeChangedEntityTypeToKind.Key;
                TypeKind typeKind = typeChangedEntityTypeToKind.Value;

                deletedEntityTypes.Add(typeChangedEntityType.PackagePrefixedName, typeKind);
                if(typeChangedEntityType is InheritanceType)
                    newEntityTypes.Add(typeChangedEntityType as InheritanceType, null);
                else
                    newEnumTypes.Add(typeChangedEntityType as EnumAttributeType, null);
            }

            RemoveDeletedEntityTypes(deletedEntityTypes, typesFromDatabase);
            AddNewEntityTypes(newEntityTypes);
            AddNewEnums(newEnumTypes);
        }

        private void AddNewEntityTypeAttribute(InheritanceType type, AttributeType attributeType)
        {
            // add new attribute to the attributeTypes table (corresponding type)
            using(SQLiteCommand fillAttributeTypeCommand = PrepareFillAttributeTypeCommand())
            {
                FillAttributeType(fillAttributeTypeCommand, attributeType, type);
            }

            // add the attribute column to the table of the according type or
            // if the type of the attribute is a container type, add a container table for the attribute
            // and write the default value of the type
            if(PersistenceProviderSQLite.IsContainerType(attributeType))
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

        private void RemoveDeletedEntityTypeAttribute(string typeName, AttributeTypeFromDatabase attributeType)
        {
            // remove from the attributeTypes table
            using(SQLiteCommand removeTypeFromAttributeTypesCommand = persistenceProvider.PrepareTopologyDelete("attributeTypes", "attributeTypeId"))
            {
                RemoveAttributeType(removeTypeFromAttributeTypesCommand, attributeType.DbId);
            }

            // remove the attribute column from the table of the according type or
            // if the type of the attribute is a container type, remove the container table for the attribute
            if(PersistenceProviderSQLite.IsContainerType(attributeType.XgrsType))
                DeleteContainerTypeTable(typeName, attributeType.Name);
            else
                RemoveAttributeFromInheritanceTypeTable(typeName, attributeType.Name);
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

        private void AddAttributeToInheritanceTypeTable(InheritanceType type, AttributeType attributeType)
        {
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(type.Package, type.Name);
            Debug.Assert(PersistenceProviderSQLite.IsAttributeTypeMappedToDatabaseColumn(attributeType));
            String columnName = PersistenceProviderSQLite.GetUniqueColumnName(attributeType.Name);
            String columnType = PersistenceProviderSQLite.ScalarAndReferenceTypeToSQLiteType(attributeType);

            persistenceProvider.AddColumnToTable(tableName, columnName, columnType);
        }

        private void RemoveAttributeFromInheritanceTypeTable(string typeName, string attributeName)
        {
            String package;
            String name;
            UnpackPackagePrefixedName(typeName, out package, out name);
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(package, name);
            String columnName = PersistenceProviderSQLite.GetUniqueColumnName(attributeName);

            persistenceProvider.DropColumnFromTable(tableName, columnName);
        }

        private void DeleteInheritanceTypeTable(string typeName)
        {
            String package;
            String name;
            UnpackPackagePrefixedName(typeName, out package, out name);
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(package, name);

            persistenceProvider.DropTable(tableName);
        }

        private void DeleteContainerTypeTable(string typeName, string attributeName)
        {
            String package;
            String name;
            UnpackPackagePrefixedName(typeName, out package, out name);
            String tableName = PersistenceProviderSQLite.GetUniqueTableName(package, name, attributeName);

            persistenceProvider.DropTable(tableName);
        }

        private bool TypeHasInstances(String typeName, TypeKind typeKind)
        {
            string idColumnName = ToIdColumnName(typeKind);
            using(SQLiteCommand command = PrepareTypeHasInstancesQuery(typeName, idColumnName))
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

        #endregion Database adaptation to type/attribute changes from the model

        #region Database adaptation to enum type/case changes from the model

        private Dictionary<EnumAttributeType, SetValueType> GetNewEnumTypes(TypesFromDatabase typesFromDatabase)
        {
            Dictionary<EnumAttributeType, SetValueType> newEnumTypes = new Dictionary<EnumAttributeType, SetValueType>();

            IGraphModel model = persistenceProvider.graph.Model;

            foreach(EnumAttributeType enumType in model.EnumAttributeTypes)
            {
                if(!typesFromDatabase.IsTypeKnown(enumType))
                    newEnumTypes.Add(enumType, null);
            }

            return newEnumTypes;
        }

        private Dictionary<String, TypeKind> GetDeletedEnumTypes(TypesFromDatabase typesFromDatabase)
        {
            Dictionary<string, TypeKind> deletedEnumTypes = new Dictionary<string, TypeKind>();

            Dictionary<string, EnumAttributeType> enumModelTypes = GetEnumTypesFromModel();
            Dictionary<string, InheritanceType> entityModelTypes = GetEntityTypesFromModel();
            foreach(KeyValuePair<string, AttributeTypesFromDatabase> typeNameToAttributeTypes in typesFromDatabase.TypesToAttributesFromDatabase)
            {
                string typeName = typeNameToAttributeTypes.Key;
                AttributeTypesFromDatabase attributeTypesFromDatabase = typeNameToAttributeTypes.Value;

                if(attributeTypesFromDatabase.KindOfOwner != TypeKind.Enum)
                    continue;

                if(!enumModelTypes.ContainsKey(typeName) && !entityModelTypes.ContainsKey(typeName))
                    deletedEnumTypes.Add(typeName, attributeTypesFromDatabase.KindOfOwner);
            }

            return deletedEnumTypes;
        }

        private Dictionary<InheritanceType, SetValueType> GetTypeChangedEnumTypes(TypesFromDatabase typesFromDatabase)
        {
            Dictionary<InheritanceType, SetValueType> typeChangedEnumTypes = new Dictionary<InheritanceType, SetValueType>();

            Dictionary<string, InheritanceType> entityModelTypes = GetEntityTypesFromModel();
            foreach(KeyValuePair<string, AttributeTypesFromDatabase> typeNameToAttributeTypes in typesFromDatabase.TypesToAttributesFromDatabase)
            {
                string typeName = typeNameToAttributeTypes.Key;
                AttributeTypesFromDatabase attributeTypesFromDatabase = typeNameToAttributeTypes.Value;

                if(attributeTypesFromDatabase.KindOfOwner != TypeKind.Enum) // we only handle enum to entity here, entity to enum is handled by entities 
                    continue;

                if(entityModelTypes.ContainsKey(typeName))
                    typeChangedEnumTypes.Add(entityModelTypes[typeName], null);
            }

            return typeChangedEnumTypes;
        }

        private Dictionary<string, EnumAttributeType> GetKeptEnumTypes(TypesFromDatabase typesFromDatabase)
        {
            Dictionary<string, EnumAttributeType> keptEnumTypes = new Dictionary<string, EnumAttributeType>();

            Dictionary<string, EnumAttributeType> enumModelTypes = GetEnumTypesFromModel();
            foreach(KeyValuePair<string, AttributeTypesFromDatabase> typeNameToAttributeTypes in typesFromDatabase.TypesToAttributesFromDatabase)
            {
                String typeName = typeNameToAttributeTypes.Key;
                AttributeTypesFromDatabase attributeTypesFromDatabase = typeNameToAttributeTypes.Value;

                if(attributeTypesFromDatabase.KindOfOwner != TypeKind.Enum)
                    continue;

                if(enumModelTypes.ContainsKey(typeName))
                    keptEnumTypes.Add(typeName, enumModelTypes[typeName]);
            }

            return keptEnumTypes;
        }

        private void UpdateEnumCaseValues(EnumAttributeType type, TypesFromDatabase typesFromDatabase)
        {
            using(SQLiteCommand updateEnumCasesCommand = PrepareUpdateAttributeTypeXgrsTypeCommand())
            {
                foreach(EnumMember enumCase in type.Members)
                {
                    AttributeTypesFromDatabase attributeTypesFromDatabase = typesFromDatabase.TypesToAttributesFromDatabase[type.PackagePrefixedName];
                    if(attributeTypesFromDatabase.AttributeNamesToAttributeTypes.ContainsKey(enumCase.Name))
                    {
                        AttributeTypeFromDatabase attributeTypeFromDatabase = attributeTypesFromDatabase.AttributeNamesToAttributeTypes[enumCase.Name];
                        UpdateAttributeTypeXgrsType(updateEnumCasesCommand, attributeTypeFromDatabase.DbId, enumCase.Value.ToString());
                    }
                }
            }
        }

        private void UpdateAttributeTypeXgrsType(SQLiteCommand updateAttributeTypeXgrsTypeCommand, long attributeTypeId, string xgrsType)
        {
            updateAttributeTypeXgrsTypeCommand.Parameters.Clear();
            updateAttributeTypeXgrsTypeCommand.Parameters.AddWithValue("@attributeTypeId", attributeTypeId);
            updateAttributeTypeXgrsTypeCommand.Parameters.AddWithValue("@xgrsType", xgrsType);
            updateAttributeTypeXgrsTypeCommand.Transaction = persistenceProvider.transaction;
            int rowsAffected = updateAttributeTypeXgrsTypeCommand.ExecuteNonQuery();
        }

        private Dictionary<string, EnumAttributeType> GetEnumTypesFromModel()
        {
            Dictionary<string, EnumAttributeType> enumModelTypes = new Dictionary<string, EnumAttributeType>();

            IGraphModel model = persistenceProvider.graph.Model;
            foreach(EnumAttributeType enumAttributeType in model.EnumAttributeTypes)
            {
                enumModelTypes.Add(enumAttributeType.PackagePrefixedName, enumAttributeType);
            }

            return enumModelTypes;
        }

        private void ReportEnumTypeChangesToTheUserAbortingIfAttributesOfRemovedOrRetypedEnumsExist(
            Dictionary<EnumAttributeType, SetValueType> newEnumTypes, Dictionary<String, TypeKind> deletedEnumTypes,
            Dictionary<InheritanceType, SetValueType> typeChangedEnumTypes,
            TypesFromDatabase typesFromDatabase, ref bool changeMessagePrinted)
        {
            // maybe todo: some code unification of enum processing with entity type processing could be possible; this function could be maybe merged with ReportTypeChangesToTheUserAbortingIfInstancesOfRemovedOrRetypedTypesExist; maybe use GrGenType as base of EnumAttributeType
            // maybe todo: names and types, database first perspective vs. model first perspective (of diff database to model (current model, old database)), model/database part, added has type from model, removed has type from database

            bool deletedTypeStillHasAttributes = false;
            foreach(KeyValuePair<String, TypeKind> typeNameToKind in deletedEnumTypes)
            {
                string typeName = typeNameToKind.Key;
                TypeKind typeKind = typeNameToKind.Value;

                string kindName = ToString(typeKind);
                if(typesFromDatabase.SomeTypeHasAnAttributeOfType(typeName))
                {
                    PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                    ConsoleUI.outWriter.WriteLine("The enum type {0} from the database is not existing in the model.",
                        typeName);
                    ConsoleUI.outWriter.WriteLine("- The enum type {0} has to be deleted from the database, but the database contains a type that has an attributes of this type (ABORTING...).",
                        typeName);
                    deletedTypeStillHasAttributes = true;
                }
            }

            foreach(InheritanceType type in typeChangedEnumTypes.Keys)
            {
                string typeName = type.PackagePrefixedName;
                string kindName = ToKindString(type);
                if(typesFromDatabase.SomeTypeHasAnAttributeOfType(typeName))
                {
                    PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                    ConsoleUI.outWriter.WriteLine("The enum type {0} from the database is a {1} type in the model.",
                        typeName, kindName);
                    ConsoleUI.outWriter.WriteLine("- The enum type {0} has to be deleted from the database, but the database contains instances of it (ABORTING...).",
                        typeName);
                    deletedTypeStillHasAttributes = true;
                }
            }

            if(deletedTypeStillHasAttributes)
            {
                ConsoleUI.outWriter.WriteLine("The model used by the database contains attributes of enums that don't exist anymore in the model file/the model assembly.");
                ConsoleUI.outWriter.WriteLine("You must delete them first - to do so you must open the database with the old model.");
                ConsoleUI.outWriter.WriteLine("The database cannot be opened with the current model.");
                WriteDatabaseModelToFileAndReportToUser(typesFromDatabase, persistenceProvider.connectionParameters);
                ConsoleUI.outWriter.WriteLine("Aborting database model update due to an incompatible model...");
                throw new Exception("Abort database model update due to an incompatible model.");
            }

            foreach(KeyValuePair<String, TypeKind> typeNameToKind in deletedEnumTypes)
            {
                string typeName = typeNameToKind.Key;

                PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                ConsoleUI.outWriter.WriteLine("The enum type {0} from the database is not existing in the model (anymore).",
                    typeName);
                ConsoleUI.outWriter.WriteLine("- The enum type {0} is going to be deleted from the database, too.",
                    typeName);
            }

            foreach(InheritanceType type in typeChangedEnumTypes.Keys)
            {
                string typeName = type.PackagePrefixedName;
                string kindName = ToKindString(type);

                PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                ConsoleUI.outWriter.WriteLine("The enum type {0} from the database is a {1} type in the model.",
                    typeName, kindName);
                ConsoleUI.outWriter.WriteLine("- The enum type {0} is going to be deleted from the database, too.",
                    typeName);
                ConsoleUI.outWriter.WriteLine("- The {0} type {1} is going to be added to the database, too.",
                    kindName, typeName);
            }

            foreach(EnumAttributeType type in newEnumTypes.Keys)
            {
                string typeName = type.PackagePrefixedName;

                PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                ConsoleUI.outWriter.WriteLine("The database does not contain the enum type {0} (that exists in the model).",
                    typeName);
                ConsoleUI.outWriter.WriteLine("- The enum type {0} is going to be added to the database, too.",
                    typeName);
            }
        }

        private static void DetermineAndReportEnumCaseChangesToTheUser(EnumAttributeType type, AttributeTypesFromDatabase attributeTypesFromDatabase,
            ref List<KeyValuePair<EnumAttributeType, string>> addedEnumCases,
            ref List<KeyValuePair<string, AttributeTypeFromDatabase>> removedEnumCases,
            ref bool changeMessagePrinted)
        {
            foreach(KeyValuePair<string, AttributeTypeFromDatabase> attributeNameToAttributeTypeFromDatabase in attributeTypesFromDatabase.AttributeNamesToAttributeTypes)
            {
                string attributeNameFromDatabase = attributeNameToAttributeTypeFromDatabase.Key;
                AttributeTypeFromDatabase attributeTypeFromDatabase = attributeNameToAttributeTypeFromDatabase.Value;

                String enumCaseFromDatabase = attributeNameFromDatabase;
                EnumMember enumCaseFromModel = GetEnumCase(type, enumCaseFromDatabase);
                if(enumCaseFromModel == null)
                {
                    PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                    ConsoleUI.outWriter.WriteLine("The enum case {0}.{1} from the database is not existing in the model (anymore).",
                        type.PackagePrefixedName, attributeNameFromDatabase);
                    ConsoleUI.outWriter.WriteLine("- The enum case {0}.{1} is going to be removed from the database, too, CONTINUING MAY RESULT IN AN ENDURING DATABASE LOADING ERROR (enum parse error) when the enum case is still used in the old data!",
                        type.PackagePrefixedName, attributeNameFromDatabase);

                    removedEnumCases.Add(new KeyValuePair<string, AttributeTypeFromDatabase>(type.PackagePrefixedName, attributeTypeFromDatabase));
                }
            }

            foreach(EnumMember enumCaseFromModel in type.Members)
            {
                if(!attributeTypesFromDatabase.AttributeNamesToAttributeTypes.ContainsKey(enumCaseFromModel.Name))
                {
                    PrintChangeMessageAsNeeded(ref changeMessagePrinted);
                    ConsoleUI.outWriter.WriteLine("The enum type {0} does not contain the case {1} in the database (that exists in the model).",
                        type.PackagePrefixedName, enumCaseFromModel.Name);
                    ConsoleUI.outWriter.WriteLine("- The enum case {0}.{1} is going to be added in the database, too.",
                        type.PackagePrefixedName, enumCaseFromModel.Name);

                    addedEnumCases.Add(new KeyValuePair<EnumAttributeType, string>(type, enumCaseFromModel.Name));
                }
            }
        }

        private static EnumMember GetEnumCase(EnumAttributeType type, string enumCaseFromDatabase)
        {
            foreach(EnumMember enumCase in type.Members)
            {
                if(enumCase.Name == enumCaseFromDatabase)
                    return enumCase;
            }
            return null;
        }

        private void AddNewEnums(Dictionary<EnumAttributeType, SetValueType> newEnumTypes)
        {
            foreach(EnumAttributeType enumType in newEnumTypes.Keys)
            {
                using(SQLiteCommand fillTypeCommand = PrepareFillTypeCommand())
                {
                    FillEnumWithCases(fillTypeCommand, enumType);
                }
            }
        }

        private void RemoveDeletedEnums(Dictionary<String, TypeKind> deletedEnumTypes)
        {
            foreach(KeyValuePair<String, TypeKind> typeNameToKind in deletedEnumTypes)
            {
                string typeName = typeNameToKind.Key;

                using(SQLiteCommand removeTypeFromAttributeTypesCommand = persistenceProvider.PrepareTopologyDelete("attributeTypes", "typeId"))
                {
                    RemoveAttributeTypes(removeTypeFromAttributeTypesCommand, persistenceProvider.TypeNameToDbId[typeName]);
                }
                using(SQLiteCommand removeTypeCommand = persistenceProvider.PrepareTopologyDelete("types", "typeId"))
                {
                    RemoveType(removeTypeCommand, persistenceProvider.TypeNameToDbId[typeName]);
                }
                RemoveTypeNameFromDbIdMapping(typeName);
            }
        }

        private void RemoveTypeChangedEnumsAndAddEntityTypes(Dictionary<InheritanceType, SetValueType> typeChangedEnumTypes)
        {
            Dictionary<String, TypeKind> deletedEnumTypes = new Dictionary<String, TypeKind>();
            Dictionary<InheritanceType, SetValueType> newEntityTypes = new Dictionary<InheritanceType, SetValueType>();
            foreach(InheritanceType typeChangedEnumType in typeChangedEnumTypes.Keys)
            {
                deletedEnumTypes.Add(typeChangedEnumType.PackagePrefixedName, TypeKind.Enum);
                newEntityTypes.Add(typeChangedEnumType, null);
            }

            RemoveDeletedEnums(deletedEnumTypes);
            AddNewEntityTypes(newEntityTypes);
        }

        private void AddNewEnumCase(EnumAttributeType type, string enumCase, string value)
        {
            using(SQLiteCommand fillAttributeTypeCommand = PrepareFillAttributeTypeCommand())
            {
                FillEnumCase(fillAttributeTypeCommand, enumCase, value, type);
            }
        }

        private void RemoveDeletedEnumCase(AttributeTypeFromDatabase enumCase)
        {
            using(SQLiteCommand removeTypeFromAttributeTypesCommand = persistenceProvider.PrepareTopologyDelete("attributeTypes", "attributeTypeId"))
            {
                RemoveAttributeType(removeTypeFromAttributeTypesCommand, enumCase.DbId);
            }
        }

        #endregion Database adaptation to enum type/case changes from the model


        #region Database-to-Model updating handling preparations / command/statement generation

        private SQLiteCommand PrepareFillTypeCommand()
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

        private SQLiteCommand PrepareFillAttributeTypeCommand()
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

        private SQLiteCommand PrepareUpdateAttributeTypeXgrsTypeCommand()
        {
            StringBuilder command = new StringBuilder();
            command.Append("UPDATE ");
            command.Append("attributeTypes");
            command.Append(" SET ");
            command.Append("xgrsType");
            command.Append(" = ");
            command.Append("@xgrsType");
            command.Append(" WHERE ");
            command.Append("attributeTypeId");
            command.Append(" == ");
            command.Append("@attributeTypeId");

            return new SQLiteCommand(command.ToString(), persistenceProvider.connection);
        }

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

        private SQLiteCommand PrepareTypeHasInstancesQuery(String typeName, String idColumnName)
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

        #endregion Database-to-Model updating handling preparations / command/statement generation

        #region Helper code, esp. simple mapping code

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

        private static string ToString(TypeKind kind)
        {
            switch(kind)
            {
                case TypeKind.NodeClass: return "node";
                case TypeKind.EdgeClass: return "edge";
                case TypeKind.ObjectClass: return "object";
                case TypeKind.GraphClass: return "graph";
                case TypeKind.Enum: return "enum";
                default: throw new Exception("INTERNAL ERROR");
            }
        }

        private static string ToIdColumnName(TypeKind kind)
        {
            return ToString(kind) + "Id";
        }

        private static string ToTypeString(TypeKind kind)
        {
            switch(kind)
            {
                case TypeKind.NodeClass: return "node class";
                case TypeKind.EdgeClass: return "edge class";
                case TypeKind.ObjectClass: return "object class";
                case TypeKind.GraphClass: return "graph";
                case TypeKind.Enum: return "enum";
                default: throw new Exception("INTERNAL ERROR");
            }
        }

        private static TypeKind ToKind(INamed type)
        {
            if(type is EnumAttributeType)
                return TypeKind.Enum;
            else
                return ToKind((InheritanceType)type);
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

        #endregion Helper code, esp. simple mapping code

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
