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
