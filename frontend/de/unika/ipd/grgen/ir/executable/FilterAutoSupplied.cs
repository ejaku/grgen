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
using IR = de.unika.ipd.grgen.ir.IR;

/// <summary>
/// An auto-supplied filter.
/// </summary>
public class FilterAutoSupplied : IR, Filter
{
	protected internal string name;

	/// <summary>
	/// The action we're a filter for </summary>
	protected internal Rule action;

	public FilterAutoSupplied(string name)
		: base(name)
	{
		this.name = name;
	}

	public virtual Rule Action
	{
		set
		{
			this.action = value;
		}
		get
		{
			return action;
		}
	}


	public virtual string FilterName
	{
		get
		{
			return name;
		}
	}
}

}
