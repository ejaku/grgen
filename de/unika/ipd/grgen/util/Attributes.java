/**
 * Created on Apr 2, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;



/**
 * A collection of attributes.
 */
public interface Attributes {

	boolean containsKey(String key);
	
	Object get(String key);
	
	public boolean isInteger(String key);

	public boolean isBoolean(String key);

	public boolean isString(String key);
	
}
