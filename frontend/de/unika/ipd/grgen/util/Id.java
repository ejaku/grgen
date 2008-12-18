/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

/**
 * An interface for something that has an id unique in
 * space and life time of the program.
 */
public interface Id {

	/**
	 * Get the id.
	 * An implementation must ensure, that for all objects that are instance of Id
	 * the two strings (returned be the <code>getId()</code> methods
	 * respectively) differ.
	 * @return A new id.
	 */
	String getId();
}
