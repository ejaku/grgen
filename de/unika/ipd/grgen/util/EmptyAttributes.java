/**
 * Created on May 5, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;


/**
 * Empty attributes.
 */
public class EmptyAttributes implements Attributes {

	private static final Attributes EMPTY = new EmptyAttributes();
	
	public static Attributes get() {
		return EMPTY;
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.Attributes#containsKey(java.lang.String)
	 */
	public boolean containsKey(String key) {
		return false;
	}

	/**
	 * @see de.unika.ipd.grgen.util.Attributes#get(java.lang.String)
	 */
	public Object get(String key) {
		return null;
	}

	/**
	 * @see de.unika.ipd.grgen.util.Attributes#put(java.lang.String, java.lang.Object)
	 */
	public void put(String key, Object value) {
	}

	/**
	 * @see de.unika.ipd.grgen.util.Attributes#isInteger(java.lang.String)
	 */
	public boolean isInteger(String key) {
		return false;
	}

	/**
	 * @see de.unika.ipd.grgen.util.Attributes#isBoolean(java.lang.String)
	 */
	public boolean isBoolean(String key) {
		return false;
	}

	/**
	 * @see de.unika.ipd.grgen.util.Attributes#isString(java.lang.String)
	 */
	public boolean isString(String key) {
		return false;
	}

}
