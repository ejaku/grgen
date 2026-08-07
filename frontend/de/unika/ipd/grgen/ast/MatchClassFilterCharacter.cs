/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast
{
using DefinedMatchTypeNode = de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;

/// <summary>
/// AST interface representing match class filters
/// </summary>
public interface MatchClassFilterCharacter
{
	// returns the name of the filter (plain name without entity in case of an auto-generated filter)
	string FilterName {get;}

	// returns the match class the filter applies to
	DefinedMatchTypeNode MatchType {get;}
}

}
