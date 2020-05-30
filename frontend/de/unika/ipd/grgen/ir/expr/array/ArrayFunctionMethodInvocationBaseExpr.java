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

import de.unika.ipd.grgen.ir.expr.ContainerFunctionMethodInvocationBaseExpr;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.type.container.ArrayType;

public abstract class ArrayFunctionMethodInvocationBaseExpr extends ContainerFunctionMethodInvocationBaseExpr
{
	protected ArrayFunctionMethodInvocationBaseExpr(String name, Type type, Expression targetExpr)
	{
		super(name, type, targetExpr);
	}
	
	@Override
	public ArrayType getTargetType()
	{
		return (ArrayType)super.getTargetType();
	}
}
