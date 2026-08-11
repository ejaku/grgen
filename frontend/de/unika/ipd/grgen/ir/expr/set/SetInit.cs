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

using System.Collections.Generic;
using System.Diagnostics;

using de.unika.ipd.grgen.ir;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using SetType = de.unika.ipd.grgen.ir.type.container.SetType;

public class SetInit : Expression
{
	private ICollection<Expression> setItems;
	private Entity member;
	private SetType setType;
	private bool isConst;

	public SetInit(ICollection<Expression> setItems, Entity member, SetType setType, bool isConst)
		: base("set init", member != null ? member.Type : setType)
	{
		this.setItems = setItems;
		this.member = member;
		this.setType = setType;
		this.isConst = isConst;
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		needs.Add(this);
		foreach(Expression setItem in setItems)
			setItem.CollectNeededEntities(needs);
	}

	public virtual ICollection<Expression> SetItems
	{
		get
		{
			return setItems;
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


	public virtual SetType SetType
	{
		get
		{
			return setType;
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

	public virtual string AnonymousSetName
	{
		get
		{
			return "anonymous_set_" + Id;
		}
	}
}

}
