/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.type.basic.StringType;

public class StringReplace extends BuiltinFunctionInvocationExpr
{
	private Expression stringExpr;
	private Expression startExpr;
	private Expression lengthExpr;
	private Expression replaceStrExpr;

	public StringReplace(Expression stringExpr,
			Expression startExpr, Expression lengthExpr, Expression replaceStrExpr)
	{
		super("string replace", StringType.getType());
		this.stringExpr = stringExpr;
		this.startExpr = startExpr;
		this.lengthExpr = lengthExpr;
		this.replaceStrExpr = replaceStrExpr;
	}

	public Expression getStringExpr()
	{
		return stringExpr;
	}

	public Expression getStartExpr()
	{
		return startExpr;
	}

	public Expression getLengthExpr()
	{
		return lengthExpr;
	}

	public Expression getReplaceStrExpr()
	{
		return replaceStrExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		stringExpr.collectNeededEntities(needs);
		startExpr.collectNeededEntities(needs);
		lengthExpr.collectNeededEntities(needs);
		replaceStrExpr.collectNeededEntities(needs);
	}
}
