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

	DataType getIntType();
	
	DataType getStringType();
	
	DataType getBooleanType();
	
}
