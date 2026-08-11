/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr.set
{
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using SetType = de.unika.ipd.grgen.ir.type.container.SetType;

public class SetCopyConstructor : Expression
{
	private Expression setToCopy;
	private SetType setType;

	public SetCopyConstructor(Expression setToCopy, SetType setType)
		: base("set copy constructor", setType)
	{
		this.setToCopy = setToCopy;
		this.setType = setType;
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		needs.Add(this);
		needs.NeedsGraph();
		setToCopy.CollectNeededEntities(needs);
	}

	public virtual Expression SetToCopy
	{
		get
		{
			return setToCopy;
		}
	}

	public virtual SetType SetType
	{
		get
		{
			return setType;
		}
	}
}

}
