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
using System.Security.Cryptography;
using System.Text;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.libGrPersistenceProviderSQLite
{
    internal enum TypeKind { NodeClass = 0, EdgeClass = 1, GraphClass = 2, ObjectClass = 3, Enum = 4 };

    internal enum ContainerCommand { AssignEmptyContainer = 0, PutElement = 1, RemoveElement = 2, AssignElement = 3, AssignNull = 4 } // based on the AttributeChangeType

    /// <summary>
    /// A base class for the PersistenceProviderSQLite that contains some infrastructure and shared code.
    /// </summary>
    public class PersistenceProviderSQLiteBase
    {
        internal SQLiteConnection connection;
        internal SQLiteTransaction transaction;

        internal String connectionParameters;
        internal String persistentGraphParameters;

        // database id to concept mappings, and vice versa
        internal Dictionary<long, INode> DbIdToNode; // the ids in node/edge mappings are globally unique due to the topology tables, the per-type tables only reference them
        internal Dictionary<INode, long> NodeToDbId;
        internal Dictionary<long, IEdge> DbIdToEdge;
        internal Dictionary<IEdge, long> EdgeToDbId;
        internal Dictionary<long, INamedGraph> DbIdToGraph;
        internal Dictionary<INamedGraph, long> GraphToDbId;
        internal Dictionary<long, IObject> DbIdToObject;
        internal Dictionary<IObject, long> ObjectToDbId;
        internal Dictionary<long, string> DbIdToTypeName;
        internal Dictionary<string, long> TypeNameToDbId;


        protected PersistenceProviderSQLiteBase()
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
        }

        #region Common code related to types / attribute types

        internal static bool ModelContainsGraphElementReferences(IGraphModel model)
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

        private static bool TypeContainsGraphElementReferences(InheritanceType type)
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

        // note that strings count as scalars
        internal static bool IsScalarType(AttributeType attributeType)
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

        internal static bool IsContainerType(AttributeType attributeType)
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

        internal static bool IsContainerType(String attributeType)
        {
            return TypesHelper.IsContainerType(attributeType);
        }

        internal static bool IsAttributeTypeMappedToDatabaseColumn(AttributeType attributeType)
        {
            return IsScalarType(attributeType) || IsReferenceType(attributeType); // containers are not referenced by an id in a database column, but are mapped entirely to own tables
        }

        // types appearing as attributes with a complete implementation for loading/storing them from/to the database
        internal static bool IsSupportedAttributeType(AttributeType attributeType)
        {
            return IsScalarType(attributeType) || IsReferenceType(attributeType) || IsContainerType(attributeType); // TODO: external/object type - also handle these.
        }

        internal static bool IsGraphType(AttributeType attributeType)
        {
            return attributeType.Kind == AttributeKind.GraphAttr;
        }

        internal static bool IsObjectType(AttributeType attributeType)
        {
            return attributeType.Kind == AttributeKind.InternalClassObjectAttr;
        }

        internal static bool IsGraphElementType(AttributeType attributeType)
        {
            return attributeType.Kind == AttributeKind.NodeAttr || attributeType.Kind == AttributeKind.EdgeAttr;
        }

        internal static string ScalarAndReferenceTypeToSQLiteType(AttributeType attributeType)
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

        #endregion Common code related to types / attribute types

        #region Database infrastructure code

        internal void CreateTable(String tableName, String idColumnName, params String[] columnNamesAndTypes)
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
            using(SQLiteCommand createSchemaCommand = new SQLiteCommand(command.ToString(), connection))
            {
                createSchemaCommand.Transaction = transaction;
                int rowsAffected = createSchemaCommand.ExecuteNonQuery();
            }
        }

        internal void AddIndex(String tableName, String indexColumnName)
        {
            StringBuilder command = new StringBuilder();
            command.Append("CREATE INDEX IF NOT EXISTS ");
            command.Append("idx_" + indexColumnName);
            command.Append(" ON ");
            command.Append(tableName);
            command.Append("(");
            command.Append(indexColumnName);
            command.Append(")");
            using(SQLiteCommand createIndexCommand = new SQLiteCommand(command.ToString(), connection))
            {
                createIndexCommand.Transaction = transaction;
                int rowsAffected = createIndexCommand.ExecuteNonQuery();
            }
        }

        internal void AddColumnToTable(String tableName, String columnName, String columnType)
        {
            StringBuilder command = new StringBuilder();
            command.Append("ALTER TABLE ");
            command.Append(tableName);
            command.Append(" ADD COLUMN ");
            command.Append(columnName);
            command.Append(" ");
            command.Append(columnType);
            using(SQLiteCommand addColumnCommand = new SQLiteCommand(command.ToString(), connection))
            {
                addColumnCommand.Transaction = transaction;
                int rowsAffected = addColumnCommand.ExecuteNonQuery();
            }
        }

        internal void DropColumnFromTable(String tableName, String columnName)
        {
            StringBuilder command = new StringBuilder();
            command.Append("ALTER TABLE ");
            command.Append(tableName);
            command.Append(" DROP COLUMN ");
            command.Append(columnName);
            using(SQLiteCommand dropColumnCommand = new SQLiteCommand(command.ToString(), connection))
            {
                dropColumnCommand.Transaction = transaction;
                int rowsAffected = dropColumnCommand.ExecuteNonQuery();
            }
        }

        internal void DropTable(String tableName)
        {
            StringBuilder command = new StringBuilder();
            command.Append("DROP TABLE ");
            command.Append(tableName);
            using(SQLiteCommand dropTableCommand = new SQLiteCommand(command.ToString(), connection))
            {
                dropTableCommand.Transaction = transaction;
                int rowsAffected = dropTableCommand.ExecuteNonQuery();
            }
        }

        internal static Dictionary<string, int> GetNameToColumnIndexMapping(SQLiteDataReader reader)
        {
            Dictionary<string, int> nameToColumnIndex = new Dictionary<string, int>();
            for(int i = 0; i < reader.FieldCount; ++i)
            {
                string columnName = reader.GetName(i);
                nameToColumnIndex.Add(columnName, i); // note that there are non-attribute columns existing... see also UniquifyName that prevents collisions of column names stemming from user defined attributes with pre-defined database columns
            }
            return nameToColumnIndex;
        }

        internal static string GetUniqueTableName(string package, string name)
        {
            return EscapeName(package, name, null);
        }

        internal static string GetUniqueTableName(string package, string name, string attributeName)
        {
            return EscapeName(package, name, attributeName);
        }

        internal static string GetUniqueColumnName(string attributeName)
        {
            return EscapeName(null, null, attributeName);
        }

        // a type/attribute name defined by the user could collide with the fixed table/column names used to model the graph,
        // the way package::typename/typename.attribute names are combined to get a valid database name could lead to name conflicts with the names chosen by the user,
        // the SQLite(/SQL standard) DBMS is case insensitive, but GrGen attributes/types are case sensitive
        // so a name disamgiguation scheme is needed, we add a hash suffix based on the domain name to the database name towards this purpose
        private static string EscapeName(string package, string name, string attributeName)
        {
            string ambiguousName = PotentiallyAmbiguousDatabaseName(package, name, attributeName);
            string unambiguousName = UnambiguousDomainName(package, name, attributeName);
            string nameConflictResolutionSuffix = "_prvt_name_collisn_sufx_" + GetMd5(unambiguousName);
            return ambiguousName + nameConflictResolutionSuffix;
        }

        private static string PotentiallyAmbiguousDatabaseName(string package, string name, string attributeName)
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

        private static string UnambiguousDomainName(string package, string name, string attributeName)
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

        private static string GetMd5(string input)
        {
            using(MD5 md5 = MD5.Create()) // todo in case of performance issues: name mapping could be cached in a loopkup table, always same names used due to fixed model
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return HexString(hashBytes);
            }
        }

        private static string HexString(byte[] input)
        {
            StringBuilder sb = new StringBuilder(input.Length * 2);
            for(int i = 0; i < input.Length; ++i)
            {
                sb.AppendFormat("{0:X2}", input[i]);
            }
            return sb.ToString();
        }

        internal static void AddInsertParameter(StringBuilder columnNames, StringBuilder parameterNames, String name)
        {
            if(columnNames.Length > 0)
                columnNames.Append(", ");
            columnNames.Append(name);
            if(parameterNames.Length > 0)
                parameterNames.Append(", ");
            parameterNames.Append("@");
            parameterNames.Append(name);
        }

        internal static void AddQueryColumn(StringBuilder columnNames, String name)
        {
            if(columnNames.Length > 0)
                columnNames.Append(", ");
            columnNames.Append(name);
        }

        #endregion Database infrastructure code

        #region Database id from/to concept mapping maintenance

        internal void AddNodeWithDbIdToDbIdMapping(INode node, long dbid)
        {
            DbIdToNode.Add(dbid, node);
            NodeToDbId.Add(node, dbid);
        }

        internal void RemoveNodeFromDbIdMapping(INode node)
        {
            DbIdToNode.Remove(NodeToDbId[node]);
            NodeToDbId.Remove(node);
        }

        internal void AddEdgeWithDbIdToDbIdMapping(IEdge edge, long dbid)
        {
            DbIdToEdge.Add(dbid, edge);
            EdgeToDbId.Add(edge, dbid);
        }

        internal void RemoveEdgeFromDbIdMapping(IEdge edge)
        {
            DbIdToEdge.Remove(EdgeToDbId[edge]);
            EdgeToDbId.Remove(edge);
        }

        internal void AddGraphWithDbIdToDbIdMapping(INamedGraph graph, long dbid)
        {
            DbIdToGraph.Add(dbid, graph);
            GraphToDbId.Add(graph, dbid);
        }

        internal void RemoveGraphFromDbIdMapping(INamedGraph graph)
        {
            DbIdToGraph.Remove(GraphToDbId[graph]);
            GraphToDbId.Remove(graph);
        }

        internal void AddObjectWithDbIdToDbIdMapping(IObject @object, long dbid)
        {
            DbIdToObject.Add(dbid, @object);
            ObjectToDbId.Add(@object, dbid);
        }

        internal void RemoveObjectFromDbIdMapping(IObject @object)
        {
            DbIdToObject.Remove(ObjectToDbId[@object]);
            ObjectToDbId.Remove(@object);
        }

        #endregion Database id from/to concept mapping maintenance
    }
}
