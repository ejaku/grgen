/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.type.Type;

public class TryScanExpr extends BuiltinFunctionInvocationExpr
{
	private final Expression stringExpr;
	private final Type targetType;

	public TryScanExpr(Expression stringExpr, Type targetType, Type type)
	{
		super("tryscan expression", type);
		this.stringExpr = stringExpr;
		this.targetType = targetType;
	}

	public Expression getStringExpr()
	{
		return stringExpr;
	}

	public Type getTargetType()
	{
		return targetType;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		stringExpr.collectNeededEntities(needs);
		needs.needsGraph();
	}
}
