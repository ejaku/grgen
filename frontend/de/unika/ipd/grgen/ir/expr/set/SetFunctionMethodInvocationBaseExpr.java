/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.set;

import de.unika.ipd.grgen.ir.expr.ContainerFunctionMethodInvocationBaseExpr;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.type.container.SetType;

public abstract class SetFunctionMethodInvocationBaseExpr extends ContainerFunctionMethodInvocationBaseExpr
{
	protected SetFunctionMethodInvocationBaseExpr(String name, Type type, Expression targetExpr)
	{
		super(name, type, targetExpr);
	}

	@Override
	public SetType getTargetType()
	{
		return (SetType)super.getTargetType();
	}
}
