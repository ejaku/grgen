/**
 * Created on Mar 10, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import java.sql.Connection;


/**
 * A database context.
 */
public abstract class DatabaseContext implements Queries {

	private Connection conn;
	
	public DatabaseContext(Connection conn) {
		this.conn = conn;
	}
	
	public final Connection getConnection() {
		return conn;
	}
	
}
