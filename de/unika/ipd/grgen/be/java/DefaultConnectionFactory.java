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
