/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.C;

import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.BackendException;
import de.unika.ipd.grgen.be.BackendFactory;

/**
 * PostgreSQL Backend implementation.
 */
public class PGSQLBackend extends SQLBackend implements BackendFactory {
	
	protected int nextId;
	
	/**
	 * @see de.unika.ipd.grgen.be.C.SQLBackend#getIdType()
	 */
	protected String getIdType() {
		return "int";
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.C.SQLBackend#getBooleanType()
	 */
	protected String getBooleanType() {
		return "int"; // query can only handle "int" yet // "boolean";
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.C.SQLBackend#getFalseValue()
	 */
	protected String getFalseValue() {
		return "FALSE";
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.C.SQLBackend#getTrueValue()
	 */
	protected String getTrueValue() {
		return "TRUE";
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.C.SQLBackend#getIntType()
	 */
	protected String getIntType() {
		return "int";
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.C.SQLBackend#getStringType()
	 */
	protected String getStringType() {
		// might use varchar(n) here ?
		return "text";
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.C.SQLBackend#genQuery(java.lang.StringBuffer, java.lang.String)
	 */
	protected void genQuery(StringBuffer sb, String query) {
		sb.append("query(PGSQL_PARAM, " + query + ");\n");
	}
	
	/* (non-Javadoc)
	 * @see de.unika.ipd.grgen.be.C.SQLBackend#firstIdMarker(java.lang.String)
	 */
	String firstIdMarker(String fmt, String p_fmt) {
		nextId = 0;
		return nextIdMarker(fmt, p_fmt);
	}
	
	/* (non-Javadoc)
	 * @see de.unika.ipd.grgen.be.C.SQLBackend#nextIdMarker(java.lang.String)
	 */
	String nextIdMarker(String fmt, String p_fmt) {
		++nextId;
		return "$" + nextId + "[" + p_fmt + "]";
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.BackendFactory#getBackend()
	 */
	public Backend getBackend() throws BackendException {
		return this;
	}
	
}
