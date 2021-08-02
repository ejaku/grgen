/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.1
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.string;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.type.Type;

public class StringAsArray extends BuiltinFunctionInvocationExpr
{
	private Expression stringExpr;
	private Expression stringToSplitAtExpr;

	public StringAsArray(Expression stringExpr, Expression stringToSplitAtExpr, Type targetType)
	{
		super("string asArray", targetType);
		this.stringExpr = stringExpr;
		this.stringToSplitAtExpr = stringToSplitAtExpr;
	}

	public Expression getStringExpr()
	{
		return stringExpr;
	}

	public Expression getStringToSplitAtExpr()
	{
		return stringToSplitAtExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		stringExpr.collectNeededEntities(needs);
		stringToSplitAtExpr.collectNeededEntities(needs);
	}
}
