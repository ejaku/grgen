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
   * @see de.unika.ipd.grgen.be.C.SQLGenerator#genQuery(java.lang.StringBuffer, java.lang.String)
   */
  protected void genQuery(StringBuffer sb, String query) {
		sb.append("query(MYSQL_PARAM, " + query + ");\n");
  }

	/**
 	 * @see de.unika.ipd.grgen.be.BackendCreator#getBackend()
 	 */
	public Backend getBackend() {
		return this;
	}

}
