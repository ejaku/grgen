/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

public class ArcSinCosTanExpr extends Expression
{
	public enum ArcusTrigonometryFunctionType
	{
		arcsin, arccos, arctan
	}

	private ArcusTrigonometryFunctionType which;
	private Expression expr;

	public ArcSinCosTanExpr(ArcusTrigonometryFunctionType which, Expression expr)
	{
		super("arc sin cos tan expr", expr.getType());
		this.which = which;
		this.expr = expr;
	}

	public ArcusTrigonometryFunctionType getWhich()
	{
		return which;
	}

	public Expression getExpr()
	{
		return expr;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		expr.collectNeededEntities(needs);
	}
}
