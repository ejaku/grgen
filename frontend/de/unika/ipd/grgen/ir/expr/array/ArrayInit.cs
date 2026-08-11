/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr.array
{

using System.Collections.Generic;
using System.Diagnostics;

using de.unika.ipd.grgen.ir;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using ArrayType = de.unika.ipd.grgen.ir.type.container.ArrayType;

public class ArrayInit : Expression
{
	private ICollection<Expression> arrayItems;
	private Entity member;
	private ArrayType arrayType;
	private bool isConst;

	public ArrayInit(ICollection<Expression> arrayItems, Entity member, ArrayType arrayType, bool isConst)
		: base("array init", member != null ? member.Type : arrayType)
	{
		this.arrayItems = arrayItems;
		this.member = member;
		this.arrayType = arrayType;
		this.isConst = isConst;
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		needs.Add(this);
		foreach(Expression arrayItem in arrayItems)
			arrayItem.CollectNeededEntities(needs);
	}

	public virtual ICollection<Expression> ArrayItems
	{
		get
		{
		return arrayItems;
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


	public virtual ArrayType ArrayType
	{
		get
		{
		return arrayType;
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

	public virtual string AnonymousArrayName
	{
		get
		{
		return "anonymous_array_" + Id;
		}
	}
}

}
