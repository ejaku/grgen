/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.Edge;

/**
 * Something, that looks like an edge.
 */
public interface EdgeCharacter {

	/**
	 * Get the IR edge for this AST edge.
	 * @return The IR edge.
	 */
	Edge getEdge();

}
