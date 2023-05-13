/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.type.basic.BooleanType;

public class StringEndsWith extends BuiltinFunctionInvocationExpr
{
	private Expression stringExpr;
	private Expression stringToSearchForExpr;

	public StringEndsWith(Expression stringExpr, Expression stringToSearchForExpr)
	{
		super("string endsWith", BooleanType.getType());
		this.stringExpr = stringExpr;
		this.stringToSearchForExpr = stringToSearchForExpr;
	}

	public Expression getStringExpr()
	{
		return stringExpr;
	}

	public Expression getStringToSearchForExpr()
	{
		return stringToSearchForExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		stringExpr.collectNeededEntities(needs);
		stringToSearchForExpr.collectNeededEntities(needs);
	}
}
