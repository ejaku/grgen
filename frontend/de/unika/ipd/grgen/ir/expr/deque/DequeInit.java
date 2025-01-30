/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.deque;

import java.util.Collection;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.container.DequeType;

public class DequeInit extends Expression
{
	private Collection<Expression> dequeItems;
	private Entity member;
	private DequeType dequeType;
	private boolean isConst;

	public DequeInit(Collection<Expression> dequeItems, Entity member, DequeType dequeType, boolean isConst)
	{
		super("deque init", member != null ? member.getType() : dequeType);
		this.dequeItems = dequeItems;
		this.member = member;
		this.dequeType = dequeType;
		this.isConst = isConst;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(this);
		for(Expression dequeItem : dequeItems) {
			dequeItem.collectNeededEntities(needs);
		}
	}

	public Collection<Expression> getDequeItems()
	{
		return dequeItems;
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

	public DequeType getDequeType()
	{
		return dequeType;
	}

	public void forceNotConstant()
	{
		isConst = false;
	}

	public boolean isConstant()
	{
		return isConst;
	}

	public String getAnonymousDequeName()
	{
		return "anonymous_deque_" + getId();
	}
}
