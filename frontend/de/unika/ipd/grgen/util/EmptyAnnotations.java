/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;

import java.util.HashSet;
import java.util.Set;


/**
 * Empty annotations.
 */
public class EmptyAnnotations implements Annotations {

	private static final Annotations EMPTY = new EmptyAnnotations();

	public static Annotations get() {
		return EMPTY;
	}

	/** @see de.unika.ipd.grgen.util.Annotations#containsKey(java.lang.String) */
	public boolean containsKey(String key) {
		return false;
	}

	/** @see de.unika.ipd.grgen.util.Annotations#get(java.lang.String) */
	public Object get(String key) {
		return null;
	}

	/** @see de.unika.ipd.grgen.util.Annotations#put(java.lang.String, java.lang.Object) */
	public void put(String key, Object value) {
	}

	/** @see de.unika.ipd.grgen.util.Annotations#isInteger(java.lang.String) */
	public boolean isInteger(String key) {
		return false;
	}

	/** @see de.unika.ipd.grgen.util.Annotations#isBoolean(java.lang.String) */
	public boolean isBoolean(String key) {
		return false;
	}

	/** @see de.unika.ipd.grgen.util.Annotations#isString(java.lang.String) */
	public boolean isString(String key) {
		return false;
	}

	public boolean isFlagSet(String key) {
		return false;
	}

	public Set<String> keySet() {
		return new HashSet<String>();
	}
}
