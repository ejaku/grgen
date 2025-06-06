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
using System.Text;
using de.unika.ipd.grGen.libConsoleAndOS;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.libGrPersistenceProviderSQLite
{
    /// <summary>
    /// An implementation of the IPersistenceProvider that allows to persist changes to a named graph to an SQLite database
    /// (utilizing a package reference to the official SQLite ADO.NET driver).
    /// </summary>
    public class PersistenceProviderSQLite : IPersistenceProvider
    {
        SQLiteConnection connection;

        // one command per node type; reading is only carried out initally, so prepared statements are only locally needed
        SQLiteCommand[] createNodeCommands;
        SQLiteCommand[] updateNodeCommands;
        SQLiteCommand[] deleteNodeCommands;

        INamedGraph graph;

        Dictionary<long, INode>[] DbIdToNodeByType; // id only unique per type/table
        Dictionary<INode, long> NodeToDbId;

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
            DbIdToNodeByType = new Dictionary<long, INode>[graph.Model.NodeModel.Types.Length];
            for(int i = 0; i < graph.Model.NodeModel.Types.Length; ++i)
            {
                DbIdToNodeByType[i] = new Dictionary<long, INode>();
            }
            NodeToDbId = new Dictionary<INode, long>();

            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                CreateNodeTable(nodeType);
            }

            // TODO: adapt to compatible changes
        }

        private void CreateNodeTable(NodeType nodeType)
        {
            String tableName = EscapeTableName(nodeType.PackagePrefixedName);
            StringBuilder columnNames = new StringBuilder();
            AddQueryColumn(columnNames, UniquifyName("dbid") + " INTEGER PRIMARY KEY"); // no AUTOINCREMENT so gaps can be closed...
            AddQueryColumn(columnNames, UniquifyName("graph") + " INTEGER NOT NULL");
            AddQueryColumn(columnNames, UniquifyName("name") + " TEXT NOT NULL");
            foreach(AttributeType attributeType in nodeType.AttributeTypes)
            {
                if(!IsScalarType(attributeType)) // TODO: container type, graph type, internal object type, external/object type.
                    continue;

                AddQueryColumn(columnNames, attributeType.Name + " " + ScalarAttributeTypeToSQLiteType(attributeType));
            }
            StringBuilder command = new StringBuilder();
            command.Append("CREATE TABLE IF NOT EXISTS ");
            command.Append(tableName);
            command.Append("(");
            command.Append(columnNames.ToString());
            command.Append(") ");
            command.Append("STRICT");
            SQLiteCommand createNodeSchemaCommand = new SQLiteCommand(command.ToString(), connection);

            int rowsAffected = createNodeSchemaCommand.ExecuteNonQuery();

            createNodeSchemaCommand.Dispose();
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
                case AttributeKind.EnumAttr: return "INT";
                case AttributeKind.FloatAttr: return "REAL";
                case AttributeKind.DoubleAttr: return "REAL";
                default: throw new Exception("Non-scalar attribute kind");
            }
        }

        private void ReadCompleteGraph()
        {
            SQLiteCommand[] readNodeCommands = new SQLiteCommand[graph.Model.NodeModel.Types.Length];

            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                readNodeCommands[nodeType.TypeID] = PrepareStatementsForReadingNodes(nodeType);
            }

            // pass 1 - load all elements (first nodes then edges)
            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                ReadNodes(nodeType, readNodeCommands[nodeType.TypeID]);
            }

            // pass 2 - wire their references/patch the references into the attributes - TODO
        }

        private SQLiteCommand PrepareStatementsForReadingNodes(NodeType nodeType)
        {
            String tableName = EscapeTableName(nodeType.PackagePrefixedName);
            StringBuilder columnNames = new StringBuilder();
            AddQueryColumn(columnNames, UniquifyName("dbid"));
            AddQueryColumn(columnNames, UniquifyName("graph"));
            AddQueryColumn(columnNames, UniquifyName("name"));
            foreach(AttributeType attributeType in nodeType.AttributeTypes)
            {
                AddQueryColumn(columnNames, attributeType.Name);
            }
            StringBuilder command = new StringBuilder();
            command.Append("SELECT ");
            command.Append(columnNames.ToString());
            command.Append(" FROM ");
            command.Append(tableName);
            //command.Append("WHERE "); // TODO limit to host graph / current subgraph
            //command.Append("graph == @graph");
            return new SQLiteCommand(command.ToString(), connection);
        }

        private void ReadNodes(NodeType nodeType, SQLiteCommand readNodeCommand)
        {
            using(SQLiteDataReader reader = readNodeCommand.ExecuteReader())
            {
                Dictionary<string, int> attributeNameToColumnIndex = new Dictionary<string, int>();
                for(int i = 0; i < reader.FieldCount; ++i)
                {
                    string columnName = reader.GetName(i);
                    attributeNameToColumnIndex.Add(columnName, i); // note that there are non-attribute columns existing...
                }

                while(reader.Read())
                {
                    INode node = nodeType.CreateNode();

                    foreach(AttributeType attributeType in nodeType.AttributeTypes)
                    {
                        if(!IsScalarType(attributeType))
                            continue;

                        object attributeValue = GetScalarValue(attributeType, reader, attributeNameToColumnIndex[attributeType.Name]);
                        node.SetAttribute(attributeType.Name, attributeValue);
                    }

                    String name = reader.GetString(attributeNameToColumnIndex[UniquifyName("name")]);
                    graph.AddNode(node, name);
                    //long graphId = reader.GetInt64(attributeNameToColumnIndex[UniquifyName("graph")]);
                    long dbid = reader.GetInt64(attributeNameToColumnIndex[UniquifyName("dbid")]);
                    AddNodeWithDbIdToDbIdMapping(node, dbid);
                }
            }
        }

        private object GetScalarValue(AttributeType attributeType, SQLiteDataReader reader, int index)
        {
            switch(attributeType.Kind)
            {
                case AttributeKind.ByteAttr: return reader.GetByte(index);
                case AttributeKind.ShortAttr: return reader.GetInt16(index);
                case AttributeKind.IntegerAttr: return reader.GetInt32(index);
                case AttributeKind.LongAttr: return reader.GetInt64(index);
                case AttributeKind.BooleanAttr: return reader.GetBoolean(index);
                case AttributeKind.StringAttr: return reader.GetString(index);
                case AttributeKind.EnumAttr: return attributeType.EnumType[reader.GetInt32(index)]; // TODO: EnumMember result needed? TODO: multiple names mapping to int?
                case AttributeKind.FloatAttr: return reader.GetFloat(index);
                case AttributeKind.DoubleAttr: return reader.GetDouble(index);
                default: throw new Exception("Non-scalar attribute kind");
            }
        }

        private void PrepareStatementsForGraphModifications()
        {
            createNodeCommands = new SQLiteCommand[graph.Model.NodeModel.Types.Length];
            updateNodeCommands = new SQLiteCommand[graph.Model.NodeModel.Types.Length];
            deleteNodeCommands = new SQLiteCommand[graph.Model.NodeModel.Types.Length];

            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                String tableName = EscapeTableName(nodeType.PackagePrefixedName);
                StringBuilder columnNames = new StringBuilder();
                StringBuilder parameterNames = new StringBuilder();
                AddInsertParameter(columnNames, parameterNames, UniquifyName("graph"));
                AddInsertParameter(columnNames, parameterNames, UniquifyName("name"));
                foreach(AttributeType attributeType in nodeType.AttributeTypes)
                {
                    AddInsertParameter(columnNames, parameterNames, attributeType.Name);
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

                createNodeCommands[nodeType.TypeID] = new SQLiteCommand(command.ToString(), connection);
            }

            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                // updateNodeCommands[nodeType.TypeID] = new SQLiteCommand(command.ToString(), connection); TODO
            }

            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                String tableName = EscapeTableName(nodeType.PackagePrefixedName);
                StringBuilder command = new StringBuilder();
                command.Append("DELETE FROM ");
                command.Append(tableName);
                command.Append(" WHERE ");
                command.Append(UniquifyName("dbid"));
                command.Append("==");
                command.Append(UniquifyName("@dbid"));

                deleteNodeCommands[nodeType.TypeID] = new SQLiteCommand(command.ToString(), connection);
            }
        }

        private string EscapeTableName(string name)
        {
            return name.Replace(':', '_'); // TODO: could lead to issues in case user uses packages as well as types with _
        }

        private string UniquifyName(string columnName)
        {
            return columnName + "_prevent_attribute_name_collisions_suffix";
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
            graph.OnRemovingEdges += RemovingEdges;

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
            graph.OnRemovingEdges -= RemovingEdges;

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
            SQLiteCommand addNodeCommand = createNodeCommands[node.Type.TypeID];
            addNodeCommand.Parameters.Clear();
            addNodeCommand.Parameters.AddWithValue(UniquifyName("@graph"), HOST_GRAPH_ID_TEMP_HACK);
            addNodeCommand.Parameters.AddWithValue(UniquifyName("@name"), graph.GetElementName(node));

            foreach(AttributeType attrType in node.Type.AttributeTypes)
            {
                if(!IsScalarType(attrType))
                    continue;

                object value = node.GetAttribute(attrType.Name);
                addNodeCommand.Parameters.AddWithValue("@" + attrType.Name, value);
            }

            int rowsAffected = addNodeCommand.ExecuteNonQuery();
            long rowId = connection.LastInsertRowId;

            AddNodeWithDbIdToDbIdMapping(node, rowId);
        }

        public void EdgeAdded(IEdge edge)
        {
        }

        public void ObjectCreated(IObject value)
        {
        }

        public void RemovingNode(INode node)
        {
            SQLiteCommand deleteNodeCommand = deleteNodeCommands[node.Type.TypeID];
            deleteNodeCommand.Parameters.Clear();
            deleteNodeCommand.Parameters.AddWithValue(UniquifyName("@dbid"), NodeToDbId[node]);

            int rowsAffected = deleteNodeCommand.ExecuteNonQuery();

            RemoveNodeFromDbIdMapping(node);
        }

        public void RemovingEdge(IEdge edge)
        {
        }

        public void RemovingEdges(INode node)
        {
        }

        public void ClearingGraph(IGraph graph)
        {
        }

        public void ChangingNodeAttribute(INode node, AttributeType attrType,
                AttributeChangeType changeType, object newValue, object keyValue)
        {
            //updateNodeCommands;
        }

        public void ChangingEdgeAttribute(IEdge edge, AttributeType attrType,
                AttributeChangeType changeType, object newValue, object keyValue)
        {
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
            //createNodeCommands;
            //deleteNodeCommands;
        }

        public void RetypingEdge(IEdge oldEdge, IEdge newEdge)
        {
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

        private void AddNodeWithDbIdToDbIdMapping(INode node, long dbid)
        {
            DbIdToNodeByType[node.Type.TypeID].Add(dbid, node);
            NodeToDbId.Add(node, dbid);
        }

        private void RemoveNodeFromDbIdMapping(INode node)
        {
            DbIdToNodeByType[node.Type.TypeID].Remove(NodeToDbId[node]);
            NodeToDbId.Remove(node);
        }
    }
}
