/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr.map
{
using ContainerFunctionMethodInvocationBaseExpr = de.unika.ipd.grgen.ir.expr.ContainerFunctionMethodInvocationBaseExpr;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Type = de.unika.ipd.grgen.ir.type.Type;
using MapType = de.unika.ipd.grgen.ir.type.container.MapType;

public abstract class MapFunctionMethodInvocationBaseExpr : ContainerFunctionMethodInvocationBaseExpr
{
	protected internal MapFunctionMethodInvocationBaseExpr(string name, Type type, Expression targetExpr)
		: base(name, type, targetExpr)
	{
	}

	public virtual MapType TargetTypeExact
	{
		get
		{
		return (MapType)TargetType;
		}
	}
}

}
