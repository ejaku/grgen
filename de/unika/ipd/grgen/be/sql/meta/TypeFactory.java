/**
 * Created on Apr 16, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.meta;


/**
 * A factory which produces SQL types.
 */
public interface TypeFactory {

	/**
	 * Get the id datatype.
	 * @return The id datatype.
	 */
	DataType getIdType();
	
	/**
	 * Get the integer datatype.
	 * @return The integer datatype.
	 */
	DataType getIntType();
	
	/**
	 * Get the string datatype.
	 * @return The string datatype.
	 */
	DataType getStringType();
	
	/**
	 * Get the boolean datatype.
	 * @return The boolean datatype.
	 */
	DataType getBooleanType();
	
}
