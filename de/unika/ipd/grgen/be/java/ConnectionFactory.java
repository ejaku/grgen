/**
 * Created on Mar 15, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import java.sql.Connection;
import java.sql.SQLException;


/**
 * Something that can open database connections.
 */
public interface ConnectionFactory {

	/**
	 * Make a new database connection.
	 * @return A new database connection.
	 */
	Connection connect() throws SQLException;
	
}
