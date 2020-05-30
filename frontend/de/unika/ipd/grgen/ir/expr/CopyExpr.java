/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

	public CopyExpr(Expression sourceExpr, Type type)
	{
		super("copy expression", type);
		this.sourceExpr = sourceExpr;
	}

	public Expression getSourceExpr()
	{
		return sourceExpr;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs)
	{
		sourceExpr.collectNeededEntities(needs);
		needs.needsGraph();
	}
}
