/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.C;

import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.BackendFactory;

/**
 * SQL Generator routines special to MySQL. 
 */
public class MySQLBackend extends SQLBackend implements BackendFactory {

  /**
   * @see de.unika.ipd.grgen.be.C.SQLGenerator#getIdType()
   */
  protected String getIdType() {
    return "INT";
  }

  /**
   * @see de.unika.ipd.grgen.be.C.SQLBackend#getBooleanType()
   */
  protected String getBooleanType() {
	  return "TINYINT";
  }

  /**
	 * @see de.unika.ipd.grgen.be.C.SQLBackend#getFalseValue()
	 */
  protected String getFalseValue() {
	  return "0";
  }

  /**
	 * @see de.unika.ipd.grgen.be.C.SQLBackend#getTrueValue()
	 */
	protected String getTrueValue() {
		return "1";
  }

  /**
   * @see de.unika.ipd.grgen.be.C.SQLBackend#getIntType()
   */
  protected String getIntType() {
	  return "INT";
  }

  /**
   * @see de.unika.ipd.grgen.be.C.SQLBackend#getStringType()
   */
  protected String getStringType() {
		//	might use VARCHAR(n) here ?
	  return "TEXT";
  }

  /**
   * @see de.unika.ipd.grgen.be.C.SQLGenerator#genQuery(java.lang.StringBuffer, java.lang.String)
   */
  protected void genQuery(StringBuffer sb, String query) {
		sb.append("query(MYSQL_PARAM, " + query + ");\n");
  }

  /* (non-Javadoc)
   * @see de.unika.ipd.grgen.be.C.SQLBackend#firstIdMarker(java.lang.String)
   */
  String firstIdMarker(String fmt, String p_fmt) {
		return nextIdMarker(fmt, p_fmt);
  }

  /* (non-Javadoc)
   * @see de.unika.ipd.grgen.be.C.SQLBackend#nextIdMarker(java.lang.String)
   */
  String nextIdMarker(String fmt, String p_fmt) {
		return fmt;
  }

	/**
 	 * @see de.unika.ipd.grgen.be.BackendCreator#getBackend()
 	 */
	public Backend getBackend() {
		return this;
	}

}
