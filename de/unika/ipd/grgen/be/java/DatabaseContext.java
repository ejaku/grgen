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
	
	private PreparedStatement[] stmts = new PreparedStatement[COUNT];

	/** Initialize the statement strings. */
	private String[] initStatements() {
		SQLParameters p = parameters;
		String[] res = new String[COUNT];
		
		res[ADD_NODE] = "INSERT INTO " +  p.getTableNodes() + " ("
			+ p.getColNodesId() + "," + p.getColNodesTypeId() + ") VALUES (?,?)";

		res[ADD_EDGE] = "INSERT INTO " +  p.getTableEdges() + " ("
			+ p.getColEdgesId() + "," + p.getColEdgesTypeId() + ") VALUES (?,?)";
		
		res[GET_ALL_NODES] = "SELECT " + p.getColNodesId() + " FROM " + p.getTableNodes();
		
		res[REMOVE_EDGE] = "DELETE FROM " + p.getTableEdges() + " WHERE " 
			+ p.getColEdgesId() + " = ?";
		
		res[REMOVE_NODE] = "DELETE FROM " + p.getTableNodes() + " WHERE " 
			+ p.getColNodesId() + " = ?";

		res[EDGE_SOURCE] = "SELECT " + p.getColEdgesSrcId() + " FROM " + p.getTableEdges()
			+ " WHERE " + p.getColEdgesId() + " = ?";
		
		res[EDGE_TARGET] = "SELECT " + p.getColEdgesTgtId() + " FROM " + p.getTableEdges()
			+ " WHERE " + p.getColEdgesId() + " = ?";

		res[NODE_INCOMING] = "SELECT " + p.getColEdgesId() + " FROM " + p.getTableEdges()
			+ " WHERE " + p.getColEdgesTgtId() + " = ?";
		
		res[NODE_OUTGOING] = "SELECT " + p.getColEdgesId() + " FROM " + p.getTableEdges()
			+ " WHERE " + p.getColEdgesSrcId() + " = ?";
		
		res[CHANGE_NODE_TYPE] = "UPDATE " + p.getTableNodes() + " SET "
	  	+ " SET " + p.getColNodesTypeId() + " = ?"
	    + " WHERE " + p.getColNodesId() + " = ?";
		
		res[NODE_GET_TYPE] = "SELECT " + p.getColNodesTypeId() + " FROM " 
			+ p.getTableNodes() + " WHERE " + p.getColNodesId() + " = ?";
 
		res[EDGE_GET_TYPE] = "SELECT " + p.getColEdgesTypeId() + " FROM " 
			+ p.getTableEdges() + " WHERE " + p.getColEdgesId() + " = ?";

		return res;
	}
	
	private PreparedStatement getStmt(int id) {
		assert id >= 0 && id < COUNT;
		return stmts[id];
	}

	DatabaseContext(SQLParameters parameters, Connection connection, ErrorReporter reporter) {
		this.conn = connection;
		this.parameters = parameters;
		this.reporter = reporter;
		
		debug.entering();
		stmtStrings = initStatements();
		
		for(int i = 0; i < COUNT; i++) {
			try {
				debug.report(NOTE, "preparing statement: " + stmtStrings[i]);
				stmts[i] = conn.prepareStatement(stmtStrings[i]);
			} catch(SQLException e) {
				reporter.error("could not prepare statement: " + stmtStrings[i]);
			}
		}
		debug.leaving();
	}
	
	public final Connection getConnection() {
		return conn;
	}
	
	private PreparedStatement prepareStatementForCall(int id, int[] params) {
		PreparedStatement stmt = getStmt(id);
		
		try {
			assert params.length == stmt.getParameterMetaData().getParameterCount();
		} catch(SQLException e) {
			// TODO Error handling.
		}

		for(int i = 0; i < params.length; i++) {
			try {
				stmt.setInt(i, params[i]);
			} catch(SQLException e) {
				// TODO Error handling.
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
			// TODO error handling.
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
			// TODO error handling.
		}
		return res;
	}
}
