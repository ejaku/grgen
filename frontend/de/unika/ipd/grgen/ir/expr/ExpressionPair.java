/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;

public class ExpressionPair extends IR
{
	Expression keyExpr; // first
	Expression valueExpr; // second

	public ExpressionPair(Expression keyExpr, Expression valueExpr)
	{
		super("pair");
		this.keyExpr = keyExpr;
		this.valueExpr = valueExpr;
	}

	public Expression getKeyExpr()
	{
		return keyExpr;
	}

	public Expression getValueExpr()
	{
		return valueExpr;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		keyExpr.collectNeededEntities(needs);
		valueExpr.collectNeededEntities(needs);
	}
}
