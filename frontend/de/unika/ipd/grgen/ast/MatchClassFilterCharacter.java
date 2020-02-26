/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

/**
 * AST interface representing match class filters
 */
public interface MatchClassFilterCharacter {
	// returns the name of the filter (plain name without entity in case of an auto-generated filter)
	String getFilterName();

	// returns the match class the filter applies to
	DefinedMatchTypeNode getMatchTypeNode();
}