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

	/** Type constants for the classify function. */
	int ID = 0;
	int INT = 1;
	int STRING = 2;
	int BOOLEAN = 3;
	int OTHER = 4;
	
	/**
	 * Get the SQL representation of the datatype.
	 * @return The SQL representation of the datatype.
	 */
	String getText();
	
	/**
	 * Return an integer constant (defined above) to classify this
	 * datatype.
	 * @return An integer type to classify this datatype.
	 */
	int classify();
	
	/**
	 * Give an expression that is a default initializer
	 * for this type.
	 * @return An expression that represents the default initializer
	 * for an item of this type.
	 */
	Term initValue();
	
}
