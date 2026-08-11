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
using StringType = de.unika.ipd.grgen.ir.type.basic.StringType;

public class StringReplace : BuiltinFunctionInvocationExpr
{
	private Expression stringExpr;
	private Expression startExpr;
	private Expression lengthExpr;
	private Expression replaceStrExpr;

	public StringReplace(Expression stringExpr,
			Expression startExpr, Expression lengthExpr, Expression replaceStrExpr)
		: base("string replace", StringType.Type)
	{
		this.stringExpr = stringExpr;
		this.startExpr = startExpr;
		this.lengthExpr = lengthExpr;
		this.replaceStrExpr = replaceStrExpr;
	}

	public virtual Expression StringExpr
	{
		get
		{
			return stringExpr;
		}
	}

	public virtual Expression StartExpr
	{
		get
		{
			return startExpr;
		}
	}

	public virtual Expression LengthExpr
	{
		get
		{
			return lengthExpr;
		}
	}

	public virtual Expression ReplaceStrExpr
	{
		get
		{
			return replaceStrExpr;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		stringExpr.CollectNeededEntities(needs);
		startExpr.CollectNeededEntities(needs);
		lengthExpr.CollectNeededEntities(needs);
		replaceStrExpr.CollectNeededEntities(needs);
	}
}

}
