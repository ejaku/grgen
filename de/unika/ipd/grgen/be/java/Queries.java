/**
 * Created on Mar 6, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import java.sql.Connection;
import java.sql.PreparedStatement;


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
	
	int NODE_GET_TYPE = 8;
	int EDGE_GET_TYPE = 9;
	
	int COUNT = EDGE_GET_TYPE + 1;
	
	/**
	 * Get the prepared query for a query ID. 
	 * @param query The query ID.
	 * @return The jdbc prepared query.
	 */
	PreparedStatement getStatement(int query);
	
	/**
	 * Get the number of parameters a query takes.
	 * @param query The query ID.
	 * @return The number of parameters the query takes.
	 */
	int getParameters(int query);
	
	/**
	 * Get the connection to the database.
	 * @return The database connection.
	 */
	Connection getConnection();
}
