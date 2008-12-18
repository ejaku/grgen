/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

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
 * Default annotations implementation.
 */
public class DefaultAnnotations implements Annotations {

	private final Map<String, Object> annots = new HashMap<String, Object>();

	/**
	 * @see de.unika.ipd.grgen.util.Annotations#containsKey(java.lang.String)
	 */
	public boolean containsKey(String key) {
		return annots.containsKey(key);
	}

	/**
	 * @see de.unika.ipd.grgen.util.Annotations#get(java.lang.String)
	 */
	public Object get(String key) {
		return annots.get(key);
	}

	/**
	 * @see de.unika.ipd.grgen.util.Annotations#isBoolean(java.lang.String)
	 */
	public boolean isBoolean(String key) {
		return containsKey(key) && get(key) instanceof Boolean;
	}

	/**
	 * @see de.unika.ipd.grgen.util.Annotations#isInteger(java.lang.String)
	 */
	public boolean isInteger(String key) {
		return containsKey(key) && get(key) instanceof Integer;
	}

	/**
	 * @see de.unika.ipd.grgen.util.Annotations#isString(java.lang.String)
	 */
	public boolean isString(String key) {
		return containsKey(key) && get(key) instanceof String;
	}

	public boolean isFlagSet(String key) {
		if(!containsKey(key)) return false;
		Object val = get(key);
		return val instanceof Boolean && ((Boolean) val).booleanValue();
	}

	/**
	 * @see de.unika.ipd.grgen.util.Annotations#put(java.lang.String, java.lang.Object)
	 */
	public void put(String key, Object value) {
		annots.put(key, value);
	}
}
