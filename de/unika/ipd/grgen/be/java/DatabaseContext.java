/**
 * Created on Mar 10, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import java.sql.Connection;
import java.sql.ResultSet;

import de.unika.ipd.grgen.be.sql.SQLParameters;


/**
 * A database context.
 */
class DatabaseContext implements Queries {

	private SQLParameters parameters;
	
	/** The database connection. */
	private Connection conn;

	/** The statement strings. */
	private static final String[] stmtStrings = initStatements();

	/** Initialize the statement strings. */
	private static String[] initStatements() {
		String[] res = new String[COUNT];
		
		return res;
	}

	DatabaseContext(SQLParameters parameters) {
		this.parameters = parameters;
	}
	
	public final Connection getConnection() {
		return conn;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.java.Queries#exec(int, int[])
	 */
	public ResultSet exec(int stmt, int[] params) {
		return null;
	}

	/**
	 * @see de.unika.ipd.grgen.be.java.Queries#execUpdate(int, int[])
	 */
	public int execUpdate(int stmt, int[] params) {
		// TODO Auto-generated method stub
		return 0;
	}
}
