/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ir
{
/// <summary>
/// A bad IR element.
/// This used in case of an error.
/// </summary>
public class Bad : IR
{
	private static readonly IR bad = new Bad();

	private Bad()
		: base("bad")
	{
	}

	/// <returns> A bad ir object. </returns>
	public static IR BadObject
	{
		get
		{
			return bad;
		}
	}

	public override bool IsBad()
	{
		return true;
	}
}

}
