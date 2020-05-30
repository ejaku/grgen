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

public class ArrayIndexOfByExpr extends ArrayFunctionMethodInvocationBaseExpr
{
	private Entity member;
	private Expression valueExpr;
	private Expression startIndexExpr;

	public ArrayIndexOfByExpr(Expression targetExpr, Entity member, Expression valueExpr)
	{
		super("array indexOfBy expr", IntType.getType(), targetExpr);
		this.member = member;
		this.valueExpr = valueExpr;
	}

	public ArrayIndexOfByExpr(Expression targetExpr, Entity member, Expression valueExpr, Expression startIndexExpr)
	{
		super("array indexOfBy expr", IntType.getType(), targetExpr);
		this.member = member;
		this.valueExpr = valueExpr;
		this.startIndexExpr = startIndexExpr;
	}

	public Entity getMember()
	{
		return member;
	}

	public Expression getValueExpr()
	{
		return valueExpr;
	}

	public Expression getStartIndexExpr()
	{
		return startIndexExpr;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(this);
		targetExpr.collectNeededEntities(needs);
		valueExpr.collectNeededEntities(needs);
		if(startIndexExpr != null)
			startIndexExpr.collectNeededEntities(needs);
	}
}
