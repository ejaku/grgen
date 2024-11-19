/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.array;

import java.util.Collection;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.container.ArrayType;

public class ArrayInit extends Expression
{
	private Collection<Expression> arrayItems;
	private Entity member;
	private ArrayType arrayType;
	private boolean isConst;

	public ArrayInit(Collection<Expression> arrayItems, Entity member, ArrayType arrayType, boolean isConst)
	{
		super("array init", member != null ? member.getType() : arrayType);
		this.arrayItems = arrayItems;
		this.member = member;
		this.arrayType = arrayType;
		this.isConst = isConst;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(this);
		for(Expression arrayItem : arrayItems) {
			arrayItem.collectNeededEntities(needs);
		}
	}

	public Collection<Expression> getArrayItems()
	{
		return arrayItems;
	}

	public void setMember(Entity entity)
	{
		assert(member == null && entity != null);
		member = entity;
	}

	public Entity getMember()
	{
		return member;
	}

	public ArrayType getArrayType()
	{
		return arrayType;
	}

	public void forceNotConstant()
	{
		isConst = false;
	}

	public boolean isConstant()
	{
		return isConst;
	}

	public String getAnonymousArrayName()
	{
		return "anonymous_array_" + getId();
	}
}
