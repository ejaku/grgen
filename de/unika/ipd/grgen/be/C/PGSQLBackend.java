/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.C;
import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.BackendException;
import de.unika.ipd.grgen.be.BackendFactory;
import de.unika.ipd.grgen.be.sql.meta.DataType;
import de.unika.ipd.grgen.be.sql.meta.MarkerSource;
import de.unika.ipd.grgen.be.sql.stmt.DefaultMarkerSource;
import de.unika.ipd.grgen.util.ReadOnlyCollection;
import java.util.Collection;
import java.util.LinkedList;
import java.util.Properties;

/**
 * PostgreSQL Backend implementation.
 */
public class PGSQLBackend extends SQLBackend implements BackendFactory {
	
	private static class PGMarkerSource extends DefaultMarkerSource {
		private int currId = 1;
		
		public String nextMarkerString(DataType type) {
			return "$" + (currId++) + '[' + type.getText() + ']';
		}
	}

	public MarkerSource getMarkerSource() {
		return new PGMarkerSource();
	}
	
	private final Properties props = new Properties();
	
	protected Properties getSQLProperties() {
		return props;
	}
	
	public PGSQLBackend() {
		props.put(TYPE_ID, "int");
		props.put(TYPE_INT, "int");
		props.put(TYPE_STRING, "text");
		props.put(TYPE_BOOLEAN, "int");
		props.put(VALUE_TRUE, "true");
		props.put(VALUE_FALSE, "false");
		props.put(VALUE_NULL, "NULL");
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.BackendFactory#getBackend()
	 */
	public Backend getBackend() throws BackendException {
		return this;
	}
	
}
