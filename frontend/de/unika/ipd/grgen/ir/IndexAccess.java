/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ir.exprevals.NeededEntities;

/**
 * Base class for the different kinds of accessing an index, binding a pattern element
 */
public abstract class IndexAccess {
	public Index index = null;

	public IndexAccess(Index index) {
		this.index = index;
	}
	
	public abstract void collectNeededEntities(NeededEntities needs);
}
