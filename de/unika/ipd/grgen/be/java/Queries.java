/**
 * Created on Mar 6, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import java.sql.Connection;
import java.sql.ResultSet;


/**
 * Constants for the Statement IDs.
 */
public interface Queries {
	
	int ADD_NODE = 0;
	int ADD_EDGE = 1;
	int REMOVE_NODE = 2;
	int REMOVE_EDGE = 3;
	
	int EDGE_SOURCE = 4;
	int EDGE_TARGET = 5;
	int NODE_INCOMING = 6;
	int NODE_OUTGOING = 7;
	
	int CHANGE_NODE_TYPE = 8;
	
	int NODE_GET_TYPE = 9;
	int EDGE_GET_TYPE = 10;
	
	int COUNT = EDGE_GET_TYPE + 1;
	
	/**
	 * Get the connection to the database.
	 * @return The database connection.
	 */
	Connection getConnection();
	
	/**
	 * Execute a query.
	 * @param stmt The query ID (one of the declared IDs above).
	 * @param params The parameters (mostly IDs).
	 * @return The result set of the query.
	 */
	ResultSet exec(int stmt, int[] params);
	
	/**
	 * Execute a query which has no results.
	 * These are mostly (INSERT, DELETE or UPDATE) queries.
	 * @param stmt The statement ID. 
	 * @param params The parameters.
	 * @return The amount of manipulated rows.
	 */
	int execUpdate(int stmt, int[] params);
	
	
	
}
