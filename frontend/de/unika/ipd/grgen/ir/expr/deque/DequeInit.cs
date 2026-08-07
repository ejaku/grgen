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

using System.Collections.Generic;
using System.Diagnostics;

using de.unika.ipd.grgen.ir;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using DequeType = de.unika.ipd.grgen.ir.type.container.DequeType;

public class DequeInit : Expression
{
	private ICollection<Expression> dequeItems;
	private Entity member;
	private DequeType dequeType;
	private bool isConst;

	public DequeInit(ICollection<Expression> dequeItems, Entity member, DequeType dequeType, bool isConst)
		: base("deque init", member != null ? member.Type : dequeType)
	{
		this.dequeItems = dequeItems;
		this.member = member;
		this.dequeType = dequeType;
		this.isConst = isConst;
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		needs.Add(this);
		foreach(Expression dequeItem in dequeItems)
			dequeItem.CollectNeededEntities(needs);
	}

	public virtual ICollection<Expression> DequeItems
	{
		get
		{
		return dequeItems;
		}
	}

	public virtual Entity Member
	{
		set
		{
		Debug.Assert((member == null && value != null));
		member = value;
		}
		get
		{
		return member;
		}
	}


	public virtual DequeType DequeType
	{
		get
		{
		return dequeType;
		}
	}

	public virtual void ForceNotConstant()
	{
		isConst = false;
	}

	public virtual bool IsConstant()
	{
		return isConst;
	}

	public virtual string AnonymousDequeName
	{
		get
		{
		return "anonymous_deque_" + Id;
		}
	}
}

}
