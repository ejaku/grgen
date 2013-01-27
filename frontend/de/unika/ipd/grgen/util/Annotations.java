/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;

import java.util.Set;

/**
 * A collection of annotations.
 */
public interface Annotations {

	boolean containsKey(String key);

	Object get(String key);

	boolean isInteger(String key);

	boolean isBoolean(String key);

	boolean isString(String key);

	boolean isFlagSet(String key);

	void put(String key, Object value);

	Set<String> keySet();
}
