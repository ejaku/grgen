/**
 * Created on Apr 16, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.stmt;

import de.unika.ipd.grgen.util.DefaultGraphDumpable;


class DefaultDebug extends DefaultGraphDumpable {
	
	private String str;
	
	DefaultDebug(String str) {
		super(str);
		this.str = str;
	}
	
	DefaultDebug() {
		this("");
	}
	
	public String debugInfo() {
		return str;
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel()
	 */
	public String getNodeLabel() {
		return debugInfo();
	}
}