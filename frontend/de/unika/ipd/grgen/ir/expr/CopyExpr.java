/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.1
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.type.Type;

public class CopyExpr extends BuiltinFunctionInvocationExpr
{
	private final Expression sourceExpr;
	private final boolean deep;

	public CopyExpr(Expression sourceExpr, Type type, boolean deep)
	{
		super("copy expression", type);
		this.sourceExpr = sourceExpr;
		this.deep = deep;
	}

	public Expression getSourceExpr()
	{
		return sourceExpr;
	}

	public boolean getDeep()
	{
		return deep;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		sourceExpr.collectNeededEntities(needs);
		needs.needsGraph();
	}
}
