/**
 * Created on Mar 10, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

import de.unika.ipd.grgen.be.sql.SQLParameters;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.report.ErrorReporter;


/**
 * A database context.
 */
class DatabaseContext extends Base implements Queries {

	/** Database parameters. */
	private SQLParameters parameters;
	
	/** The database connection. */
	private Connection conn;

	/** The statement strings. */
	private final String[] stmtStrings;
	
	/** The error reporter. */
	private final ErrorReporter reporter;
	
	private final String edgesTableName;
	
	private final String nodesTableName;
	
	private PreparedStatement[] stmts = new PreparedStatement[COUNT];

	/** Initialize the statement strings. */
	private String[] initStatements() {
		SQLParameters p = parameters;
		String[] res = new String[COUNT];
		
		res[ADD_NODE] = "INSERT INTO " +  nodesTableName + " ("
			+ p.getColNodesId() + "," + p.getColNodesTypeId() + ") VALUES (?,?)";

		res[ADD_EDGE] = "INSERT INTO " +  edgesTableName + " ("
			+ p.getColEdgesId() + "," + p.getColEdgesTypeId() + ") VALUES (?,?)";
		
		res[GET_ALL_NODES] = "SELECT " + p.getColNodesId() + " FROM " + nodesTableName;
		
		res[REMOVE_EDGE] = "DELETE FROM " + edgesTableName + " WHERE "
			+ p.getColEdgesId() + " = ?";
		
		res[REMOVE_NODE] = "DELETE FROM " + nodesTableName + " WHERE "
			+ p.getColNodesId() + " = ?";

		res[EDGE_SOURCE] = "SELECT " + p.getColEdgesSrcId() + " FROM " + edgesTableName
			+ " WHERE " + p.getColEdgesId() + " = ?";
		
		res[EDGE_TARGET] = "SELECT " + p.getColEdgesTgtId() + " FROM " + edgesTableName
			+ " WHERE " + p.getColEdgesId() + " = ?";

		res[NODE_INCOMING] = "SELECT " + p.getColEdgesId() + " FROM " + edgesTableName
			+ " WHERE " + p.getColEdgesTgtId() + " = ?";
		
		res[NODE_OUTGOING] = "SELECT " + p.getColEdgesId() + " FROM " + edgesTableName
			+ " WHERE " + p.getColEdgesSrcId() + " = ?";
		
		res[CHANGE_NODE_TYPE] = "UPDATE " + nodesTableName + " SET "
	  	+ " SET " + p.getColNodesTypeId() + " = ?"
	    + " WHERE " + p.getColNodesId() + " = ?";
		
		res[NODE_GET_TYPE] = "SELECT " + p.getColNodesTypeId() + " FROM "
			+ nodesTableName + " WHERE " + p.getColNodesId() + " = ?";
 
		res[EDGE_GET_TYPE] = "SELECT " + p.getColEdgesTypeId() + " FROM "
			+ edgesTableName + " WHERE " + p.getColEdgesId() + " = ?";

		res[CREATE_NODES_TABLE] = "CREATE TABLE " + nodesTableName + " ("
			+ p.getColNodesId() + " " + p.getIdType() + " NOT NULL PRIMARY KEY, "
			+ p.getColNodesTypeId() + " " + p.getIdType() + " NOT NULL)";
		
		res[CREATE_EDGES_TABLE] = "CREATE TABLE " + edgesTableName + " ("
			+ p.getColEdgesId() + " " + p.getIdType() + " NOT NULL PRIMARY KEY, "
			+ p.getColEdgesTypeId() + " " + p.getIdType() + " NOT NULL, "
			+ p.getColEdgesSrcId() + " " + p.getIdType() + " NOT NULL, "
			+ p.getColEdgesTgtId() + " " + p.getIdType() + " NOT NULL)";

		res[DELETE_NODES_TABLE] = "DROP TABLE " + nodesTableName;

		res[DELETE_EDGES_TABLE] = "DROP TABLE " + edgesTableName;
		
		return res;
	}
	
	private PreparedStatement getStmt(int id) {
		assert id >= 0 && id < COUNT;
		return stmts[id];
	}

	DatabaseContext(String prefix, SQLParameters parameters,
									Connection connection, ErrorReporter reporter) {
		this.conn = connection;
		this.parameters = parameters;
		this.reporter = reporter;
		
		this.edgesTableName = prefix + "_" + parameters.getTableEdges();
		this.nodesTableName = prefix + "_" + parameters.getTableNodes();
		
		stmtStrings = initStatements();
		
		for(int i = 0; i < COUNT; i++) {
			try {
				debug.report(NOTE, "preparing statement: " + stmtStrings[i]);
				stmts[i] = conn.prepareStatement(stmtStrings[i]);
			} catch(SQLException e) {
				reporter.error("could not prepare statement: " + stmtStrings[i]);
			}
		}
	}
	
	public final Connection getConnection() {
		return conn;
	}
	
	private PreparedStatement prepareStatementForCall(int id, int[] params) {
		PreparedStatement stmt = getStmt(id);
		
		debug.report(NOTE, "calling statement(" + id + "): " + stmtStrings[id]);
		
		try {
			assert params.length == stmt.getParameterMetaData().getParameterCount();
		} catch(SQLException e) {
			reporter.error(e.toString());
		}

		for(int i = 0; i < params.length; i++) {
			try {
				stmt.setInt(i, params[i]);
			} catch(SQLException e) {
				reporter.error("could not prepare parameter " + i + ": " + e.toString());
			}
		}
		
		return stmt;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.java.Queries#exec(int, int[])
	 */
	public ResultSet exec(int stmtId, int[] params) {
		ResultSet set = null;
		PreparedStatement stmt = prepareStatementForCall(stmtId, params);

		try {
			set = stmt.executeQuery();
		} catch(SQLException e) {
			reporter.error(e.toString());
		}
		
		return set;
	}

	/**
	 * @see de.unika.ipd.grgen.be.java.Queries#execUpdate(int, int[])
	 */
	public int execUpdate(int stmtId, int[] params) {
		int res = 0;
		PreparedStatement stmt = prepareStatementForCall(stmtId, params);

		try {
			res = stmt.executeUpdate();
		} catch(SQLException e) {
			reporter.error(e.toString());
		}
		return res;
	}
}
