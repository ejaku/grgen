/**
 * Created on Apr 18, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.meta;


/**
 * Something that is declared.
 * 
 */
public interface Declared {

	/**
	 * The name with which the object was declared with.
	 * @return The declaration name.
	 */
	String getDeclName();
	
	/**
	 * Dump the declaration. 
	 * @param sb The string buffer to dump to.
	 * @return The string buffer.
	 */
	StringBuffer dumpDecl(StringBuffer sb);
	
	
}
