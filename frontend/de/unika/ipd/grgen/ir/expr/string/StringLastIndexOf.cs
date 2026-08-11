/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr.@string
{
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using BuiltinFunctionInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
using IntType = de.unika.ipd.grgen.ir.type.basic.IntType;

public class StringLastIndexOf : BuiltinFunctionInvocationExpr
{
	private Expression stringExpr;
	private Expression stringToSearchForExpr;
	private Expression startIndexExpr;

	public StringLastIndexOf(Expression stringExpr, Expression stringToSearchForExpr)
		: base("string lastIndexOf", IntType.Type)
	{
		this.stringExpr = stringExpr;
		this.stringToSearchForExpr = stringToSearchForExpr;
	}

	public StringLastIndexOf(Expression stringExpr, Expression stringToSearchForExpr, Expression startIndexExpr)
		: base("string lastIndexOf", IntType.Type)
	{
		this.stringExpr = stringExpr;
		this.stringToSearchForExpr = stringToSearchForExpr;
		this.startIndexExpr = startIndexExpr;
	}

	public virtual Expression StringExpr
	{
		get
		{
			return stringExpr;
		}
	}

	public virtual Expression StringToSearchForExpr
	{
		get
		{
			return stringToSearchForExpr;
		}
	}

	public virtual Expression StartIndexExpr
	{
		get
		{
			return startIndexExpr;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		stringExpr.CollectNeededEntities(needs);
		stringToSearchForExpr.CollectNeededEntities(needs);
		if(startIndexExpr != null)
			startIndexExpr.CollectNeededEntities(needs);
	}
}

}
