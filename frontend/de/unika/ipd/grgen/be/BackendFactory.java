/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
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
