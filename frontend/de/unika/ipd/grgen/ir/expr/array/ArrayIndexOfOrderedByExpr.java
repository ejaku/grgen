/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.expr.array;

import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.basic.IntType;

public class ArrayIndexOfOrderedByExpr extends ArrayFunctionMethodInvocationBaseExpr
{
	private Entity member;
	private Expression valueExpr;

	public ArrayIndexOfOrderedByExpr(Expression targetExpr, Entity member, Expression valueExpr)
	{
		super("array indexOfOrderedBy expr", IntType.getType(), targetExpr);
		this.member = member;
		this.valueExpr = valueExpr;
	}

	public Entity getMember()
	{
		return member;
	}

	public Expression getValueExpr()
	{
		return valueExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(this);
		targetExpr.collectNeededEntities(needs);
		valueExpr.collectNeededEntities(needs);
	}
}
