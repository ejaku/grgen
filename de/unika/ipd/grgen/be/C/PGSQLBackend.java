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
import de.unika.ipd.grgen.be.sql.meta.StatementFactory;
import de.unika.ipd.grgen.be.sql.stmt.DefaultMarkerSource;
import de.unika.ipd.grgen.be.sql.stmt.DefaultStatementFactory;

/**
 * PostgreSQL Backend implementation.
 */
public class PGSQLBackend extends SQLBackend implements BackendFactory {
	
	private final DataType idType;
	private final DataType intType;
	private final DataType stringType;
	private final DataType booleanType;
	
	private static class PGMarkerSource extends DefaultMarkerSource {
		private int currId = 1;
		
		public String nextMarkerString(DataType type) {
			return "$" + (currId++);
		}
	}

	public MarkerSource getMarkerSource() {
		return new PGMarkerSource();
	}
	
	public PGSQLBackend() {
		super("1", "0");
		StatementFactory factory = new DefaultStatementFactory(this);
		idType = new MyDataType("int", DataType.ID, factory.constant(-1));
		intType = new MyDataType("int", DataType.INT, factory.constant(0));
		stringType = new MyDataType("text", DataType.STRING, factory.constant(""));
		booleanType = new MyDataType("int", DataType.BOOLEAN, factory.constant(0));
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.BackendFactory#getBackend()
	 */
	public Backend getBackend() throws BackendException {
		return this;
	}
	
		/**
	 * Get the id datatype.
	 * @return The id datatype.
	 */
	public DataType getIdType() {
		return idType;
	}
	
	/**
	 * Get the integer datatype.
	 * @return The integer datatype.
	 */
	public DataType getIntType() {
		return intType;
	}
	
	/**
	 * Get the string datatype.
	 * @return The string datatype.
	 */
	public DataType getStringType() {
		return stringType;
	}
	
	/**
	 * Get the boolean datatype.
	 * @return The boolean datatype.
	 */
	public DataType getBooleanType() {
		return booleanType;
	}

	
}
