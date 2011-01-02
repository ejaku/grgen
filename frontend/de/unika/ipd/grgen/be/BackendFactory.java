/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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
