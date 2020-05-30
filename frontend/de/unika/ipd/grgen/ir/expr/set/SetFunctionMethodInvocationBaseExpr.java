/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.set;

import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.type.container.SetType;

public abstract class SetFunctionMethodInvocationBaseExpr extends Expression
{
	protected Expression targetExpr;

	protected SetFunctionMethodInvocationBaseExpr(String name, Type type, Expression targetExpr)
	{
		super(name, type);
		this.targetExpr = targetExpr;
	}

	public Expression getTargetExpr()
	{
		return targetExpr;
	}
	
	public SetType getTargetType()
	{
		return (SetType)type;
	}
}
