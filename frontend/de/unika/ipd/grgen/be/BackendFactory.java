/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.be;

/**
 * An interface for something that creates a backend.
 */
public interface BackendFactory {

	/**
	 * Create a new backend.
	 * @return A new backend.
	 */
	Backend getBackend();

}
