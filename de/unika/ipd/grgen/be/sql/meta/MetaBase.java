/**
 * Created on Apr 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.meta;



/**
 * Base for each SQL meta construct.
 */
public interface MetaBase { 

	/**
	 * Append it to a string buffer.
	 * @param sb The string buffer.
	 */
	StringBuffer dump(StringBuffer sb);
	
	/**
	 * Get some special debug info for this object.
	 * This is mostly verbose stuff for dumping.
	 * @return Debug info.
	 */
	String debugInfo();
	
}
