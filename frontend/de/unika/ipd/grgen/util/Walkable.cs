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

	using System.Collections.Generic;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;

	/// <summary>
	/// Something you can walk on. This means, that there are children to visit.
	/// </summary>
	public interface Walkable
	{
		/// <summary>
		/// Get the children of this object
		/// Note: BaseNode implements Walkable </summary>
		/// <returns> The children </returns>
		ICollection<BaseNode> WalkableChildren {get;}
	}

}
