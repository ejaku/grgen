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
	  return "boolean";
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

  /**
   * @see de.unika.ipd.grgen.be.BackendFactory#getBackend()
   */
  public Backend getBackend() throws BackendException {
    return this;
  }

}
