/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.C;

import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.BackendFactory;
import de.unika.ipd.grgen.be.sql.meta.DataType;
import de.unika.ipd.grgen.be.sql.meta.MarkerSource;
import de.unika.ipd.grgen.be.sql.stmt.DefaultMarkerSource;
import de.unika.ipd.grgen.util.ReadOnlyCollection;
import java.util.Collection;
import java.util.LinkedList;
import java.util.Properties;

/**
 * SQL Generator routines special to MySQL.
 */
public class MySQLBackend extends SQLBackend implements BackendFactory {
	
	private static class MyMarkerSource extends DefaultMarkerSource {
		public String nextMarkerString(DataType type) {
			return "?";
		}
	};
	
	private final Properties props = new Properties();
	
	public MySQLBackend() {
		props.put(TYPE_ID, "int");
		props.put(TYPE_INT, "int");
		props.put(TYPE_STRING, "text");
		props.put(TYPE_BOOLEAN, "tinyint");
		props.put(VALUE_TRUE, "true");
		props.put(VALUE_FALSE, "false");
		props.put(VALUE_NULL, "NULL");
	}
	
	/**
	 * Get SQL properties.
	 * This method must return a properties object which
	 * contains values for the TYPE_* and VALUE_* keys
	 * in this class.
	 * @return A properties object.
	 */
	protected Properties getSQLProperties() {
		return props;
	}
	
	public MarkerSource getMarkerSource() {
		return new MyMarkerSource();
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.BackendCreator#getBackend()
	 */
	public Backend getBackend() {
		return this;
	}
	
}
