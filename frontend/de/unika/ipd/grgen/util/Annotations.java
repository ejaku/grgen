/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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
}
