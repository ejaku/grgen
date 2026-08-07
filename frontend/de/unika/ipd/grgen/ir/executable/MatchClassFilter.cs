/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.executable
{
using DefinedMatchType = de.unika.ipd.grgen.ir.type.DefinedMatchType;

/// <summary>
/// A match class filter (base type for auto-generated match class filters and match class filter functions).
/// </summary>
public interface MatchClassFilter
{
	DefinedMatchType MatchClass {get;}
}

}
