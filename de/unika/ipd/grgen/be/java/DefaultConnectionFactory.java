/**
 * Created on Mar 15, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.Properties;


/**
 * A default JDBC connection factory.
 */
public class DefaultConnectionFactory implements ConnectionFactory {

	private String URL;
	private Properties props;
	
	public DefaultConnectionFactory(String URL, String user, String password) {
		this.URL = URL;
		props = new Properties();
		props.setProperty("user", user);
		props.setProperty("password", password);
	}
	
	public DefaultConnectionFactory(String URL, Properties props) {
		this.URL = URL;
		this.props = props;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.java.ConnectionFactory#connect()
	 */
	public Connection connect() throws SQLException {
		return DriverManager.getConnection(URL, props);
	}

}
