/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Created on Apr 2, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

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
}
