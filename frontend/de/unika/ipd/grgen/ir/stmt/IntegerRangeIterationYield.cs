/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt
{
using de.unika.ipd.grgen.ir;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

/// <summary>
/// Represents an accumulation yielding of a container variable in the IR.
/// </summary>
public class IntegerRangeIterationYield : BlockNestingStatement
{
	private Variable iterationVar;
	private Expression leftExpr;
	private Expression rightExpr;

	public IntegerRangeIterationYield(Variable iterationVar, Expression left, Expression right)
		: base("integer range iteration yield")
	{
		this.iterationVar = iterationVar;
		this.leftExpr = left;
		this.rightExpr = right;
	}

	public virtual Variable IterationVar
	{
		get
		{
		return iterationVar;
		}
	}

	public virtual Expression LeftExpr
	{
		get
		{
		return leftExpr;
		}
	}

	public virtual Expression RightExpr
	{
		get
		{
		return rightExpr;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		leftExpr.CollectNeededEntities(needs);
		rightExpr.CollectNeededEntities(needs);
		foreach(EvalStatement accumulationStatement in statements)
			accumulationStatement.CollectNeededEntities(needs);
		if(needs.variables != null)
			needs.variables.Remove(iterationVar);
	}
}

}
