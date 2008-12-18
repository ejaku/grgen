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
 * User interface of walkers,
 * walking over structures of walkable objects (i.e. containing walkable children)
 */
public interface Walker
{
	/** reset state of walk, i.e. forget about already visited children */
	void reset();

	/** start walk on node w */
	void walk(Walkable w);
}
