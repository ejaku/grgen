/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.C;

import java.util.Collection;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

/**
 * SQL Generator routines special to MySQL. 
 */
public class MySQLBackend extends SQLBackend {

  /**
   * @see de.unika.ipd.grgen.be.C.SQLGenerator#getIdType()
   */
  protected String getIdType() {
    return "INT";
  }

  /**
   * @see de.unika.ipd.grgen.be.C.SQLGenerator#genColumn(java.lang.String, java.lang.String, boolean, boolean, boolean, boolean)
   */
  protected String genColumn(
    String name,
    String type,
    boolean notNull,
    boolean unique,
    boolean auto_inc,
    boolean primary) {

		return name + " " + type 
			+ (notNull ? " NOT NULL" : " NULL")
			+ (unique && !primary ? " UNIQUE" : "")
			+ (auto_inc ? " AUTO_INCREMENT" : "")
			+ (primary ? " PRIMARY KEY" : "");
  }

  /**
   * @see de.unika.ipd.grgen.be.C.SQLGenerator#genTable(java.lang.String, java.util.Collection)
   */
  protected String genTable(String name, Collection cols) {
    String res = "CREATE TABLE " + name + " (";
    int i = 0;
    Iterator it;
    
    for(it = cols.iterator(), i = 0; it.hasNext(); i++) 
    	res += (i > 0 ? ", " : "") + (String) it.next();
    
    res += ") TYPE=HEAP";
    return res;
  }

  /**
   * @see de.unika.ipd.grgen.be.C.SQLGenerator#genFunction(java.lang.String)
   */
  protected String genFunction(String name) {
    return null;
  }

  /**
   * @see de.unika.ipd.grgen.be.C.SQLGenerator#genIndex()
   */
  protected void genIndex() {
  }

  /**
   * @see de.unika.ipd.grgen.be.C.SQLGenerator#genDbCreate()
   */
  public Collection genDbCreate(String db) {
    Collection res = new LinkedList();
    res.add("DROP DATABASE IF EXISTS " + db);
    res.add("CREATE DATABASE " + db);
    res.add("USE " + db);
    return res;
  }

  /**
   * @see de.unika.ipd.grgen.be.C.SQLGenerator#genQuery(java.lang.StringBuffer, java.lang.String)
   */
  protected void genQuery(StringBuffer sb, String query) {
    sb.append("query(MYSQL_PARAM, " + query + ");\n");
  }

  /**
   * @see de.unika.ipd.grgen.be.C.SQLBackend#genMakeMatchRes(java.lang.StringBuffer, java.util.List, java.util.List)
   */
  protected void genMakeMatchRes(StringBuffer sb, List nodes, List edges) {
    sb.append("match_get_res(MYSQL_PARAM, match " + nodes.size() 
			+ ", " + ");\n");
  }

}
