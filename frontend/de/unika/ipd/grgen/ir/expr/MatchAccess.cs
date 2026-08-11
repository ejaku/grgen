/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr
{
using de.unika.ipd.grgen.ir;

public class MatchAccess : Expression
{
	internal Expression expression;
	internal Entity entity; // member

	public MatchAccess(Expression expression, Entity entity)
		: base("match access", entity.Type)
	{
		this.expression = expression;
		this.entity = entity;
	}

	public virtual Expression Expr
	{
		get
		{
			return expression;
		}
	}

	public virtual Entity Entity
	{
		get
		{
			return entity;
		}
	}
}

}
