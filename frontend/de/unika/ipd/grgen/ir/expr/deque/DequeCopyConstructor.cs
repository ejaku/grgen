/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr.deque
{
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using DequeType = de.unika.ipd.grgen.ir.type.container.DequeType;

public class DequeCopyConstructor : Expression
{
	private Expression dequeToCopy;
	private DequeType dequeType;

	public DequeCopyConstructor(Expression dequeToCopy, DequeType dequeType)
		: base("deque copy constructor", dequeType)
	{
		this.dequeToCopy = dequeToCopy;
		this.dequeType = dequeType;
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		needs.Add(this);
		needs.NeedsGraph();
		dequeToCopy.CollectNeededEntities(needs);
	}

	public virtual Expression DequeToCopy
	{
		get
		{
			return dequeToCopy;
		}
	}

	public virtual DequeType DequeType
	{
		get
		{
			return dequeType;
		}
	}
}

}
