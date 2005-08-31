/**
 * Created on Apr 5, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

import java.util.HashMap;
import java.util.Map;


/**
 * Default attribute implementation.
 */
public class DefaultAttributes implements Attributes {

	private final Map<String, Object> attrs = new HashMap<String, Object>();
	
	/**
	 * @see de.unika.ipd.grgen.util.Attributes#containsKey(java.lang.String)
	 */
	public boolean containsKey(String key) {
		return attrs.containsKey(key);
	}

	/**
	 * @see de.unika.ipd.grgen.util.Attributes#get(java.lang.String)
	 */
	public Object get(String key) {
		return attrs.get(key);
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.Attributes#isBoolean(java.lang.String)
	 */
	public boolean isBoolean(String key) {
		return containsKey(key) && get(key) instanceof Boolean;
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.Attributes#isInteger(java.lang.String)
	 */
	public boolean isInteger(String key) {
		return containsKey(key) && get(key) instanceof Integer;
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.Attributes#isString(java.lang.String)
	 */
	public boolean isString(String key) {
		return containsKey(key) && get(key) instanceof String;
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.Attributes#put(java.lang.String, java.lang.Object)
	 */
	public void put(String key, Object value) {
		attrs.put(key, value);
	}
}
