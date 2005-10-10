/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/


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
	int GET_ALL_NODES = 11;
	
	int CREATE_NODES_TABLE = 12;
	int CREATE_EDGES_TABLE = 13;
	int DELETE_NODES_TABLE = 14;
	int DELETE_EDGES_TABLE = 15;
	
	int COUNT = DELETE_EDGES_TABLE + 1;
	
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
	 * Execute a query.
	 * @see #exec(int, int[]);
	 */
	ResultSet exec(int stmt, int param);
	
	/**
	 * Execute a query which has no results.
	 * These are mostly (INSERT, DELETE or UPDATE) queries.
	 * @param stmt The statement ID.
	 * @param params The parameters.
	 * @return The amount of manipulated rows.
	 */
	int execUpdate(int stmt, int[] params);
	
	
	
}
