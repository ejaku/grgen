/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
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
