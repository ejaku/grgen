/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.util; // potential TODO: create common package, move there (things shared != utility code)

/** Direction of an edge (as requested by neighborhood query) */
public enum Direction
{
	INCIDENT,
	INCOMING,
	OUTGOING,
	INVALID
}
