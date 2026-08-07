/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ir.expr.array
{
using Entity = de.unika.ipd.grgen.ir.Entity;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using ArrayType = de.unika.ipd.grgen.ir.type.container.ArrayType;

public class ArrayOrderDescendingBy : ArrayFunctionMethodInvocationBaseExpr
{
	private Entity member;

	public ArrayOrderDescendingBy(Expression targetExpr, Entity member)
		: base("array order descending by expr", (ArrayType)targetExpr.Type, targetExpr)
	{
		this.member = member;
	}

	public virtual Entity Member
	{
		get
		{
		return member;
		}
	}
}

}
