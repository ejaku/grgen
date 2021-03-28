/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.type.container.ContainerType;

public abstract class ContainerFunctionMethodInvocationBaseExpr extends BuiltinFunctionInvocationExpr
{
	protected Expression targetExpr;

	protected ContainerFunctionMethodInvocationBaseExpr(String name, Type type, Expression targetExpr)
	{
		super(name, type);
		this.targetExpr = targetExpr;
	}

	public Expression getTargetExpr()
	{
		return targetExpr;
	}
	
	public ContainerType getTargetType()
	{
		return (ContainerType)targetExpr.getType();
	}
	
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(this);
		targetExpr.collectNeededEntities(needs);
	}
}
