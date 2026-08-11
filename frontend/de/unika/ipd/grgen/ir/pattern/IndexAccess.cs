/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.pattern
{
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Index = de.unika.ipd.grgen.ir.model.Index;

/// <summary>
/// Base class for the different kinds of accessing an index, binding a pattern element
/// </summary>
public abstract class IndexAccess
{
	public Index index = null;

	public IndexAccess(Index index)
	{
		this.index = index;
	}

	public abstract void CollectNeededEntities(NeededEntities needs);
}

}
