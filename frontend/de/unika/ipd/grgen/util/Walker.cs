/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.util
{
/// <summary>
/// User interface of walkers,
/// walking over structures of walkable objects (i.e. containing walkable children)
/// </summary>
public interface Walker
{
	/// <summary>
	/// reset state of walk, i.e. forget about already visited children </summary>
	void Reset();

	/// <summary>
	/// start walk on node w </summary>
	void Walk(Walkable w);
}

}
