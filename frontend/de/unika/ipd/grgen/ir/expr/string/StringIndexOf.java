/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.string;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.type.basic.IntType;

public class StringIndexOf extends BuiltinFunctionInvocationExpr
{
	private Expression stringExpr;
	private Expression stringToSearchForExpr;
	private Expression startIndexExpr;

	public StringIndexOf(Expression stringExpr, Expression stringToSearchForExpr)
	{
		super("string indexOf", IntType.getType());
		this.stringExpr = stringExpr;
		this.stringToSearchForExpr = stringToSearchForExpr;
	}

	public StringIndexOf(Expression stringExpr, Expression stringToSearchForExpr, Expression startIndexExpr)
	{
		super("string indexOf", IntType.getType());
		this.stringExpr = stringExpr;
		this.stringToSearchForExpr = stringToSearchForExpr;
		this.startIndexExpr = startIndexExpr;
	}

	public Expression getStringExpr()
	{
		return stringExpr;
	}

	public Expression getStringToSearchForExpr()
	{
		return stringToSearchForExpr;
	}

	public Expression getStartIndexExpr()
	{
		return startIndexExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		stringExpr.collectNeededEntities(needs);
		stringToSearchForExpr.collectNeededEntities(needs);
		if(startIndexExpr != null)
			startIndexExpr.collectNeededEntities(needs);
	}
}
