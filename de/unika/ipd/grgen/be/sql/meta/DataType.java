/**
 * Created on Apr 16, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.meta;


/**
 * A SQL basic data type.
 */
public interface DataType extends MetaBase {

	/**
	 * Get the SQL representation of the datatype. 
	 * @return The SQL representation of the datatype.
	 */
	String getText();
	
}
