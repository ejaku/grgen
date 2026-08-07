/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt.procenv
{
using System.Diagnostics;

using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using BuiltinProcedureInvocationBase = de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;
using Type = de.unika.ipd.grgen.ir.type.Type;

public class GetEquivalentOrAddProc : BuiltinProcedureInvocationBase
{
	internal Type returnType;

	private readonly Expression subgraphExpr;
	private readonly Expression arrayExpr;
	private readonly bool includingAttributes;

	public GetEquivalentOrAddProc(Type returnType,
			Expression subgraphExpr, Expression arrayExpr, bool includingAttributes)
		: base("get equivalent or add procedure")
	{
		this.returnType = returnType;
		this.subgraphExpr = subgraphExpr;
		this.arrayExpr = arrayExpr;
		this.includingAttributes = includingAttributes;
	}

	public virtual Expression SubgraphExpr
	{
		get
		{
		return subgraphExpr;
		}
	}

	public virtual Expression ArrayExpr
	{
		get
		{
		return arrayExpr;
		}
	}

	public virtual bool IncludingAttributes
	{
		get
		{
		return includingAttributes;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		subgraphExpr.CollectNeededEntities(needs);
		arrayExpr.CollectNeededEntities(needs);
	}

	public override int ReturnArity()
	{
		return 1;
	}

	public override Type GetReturnType(int index)
	{
		Debug.Assert((index == 0));
		return returnType;
	}
}

}
