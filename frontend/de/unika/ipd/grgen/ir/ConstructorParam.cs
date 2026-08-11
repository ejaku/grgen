/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll
/// </summary>

namespace de.unika.ipd.grgen.ir
{
using Expression = de.unika.ipd.grgen.ir.expr.Expression;

public class ConstructorParam : IR
{
	private Entity entity;
	private Expression defValue;

	public ConstructorParam(Entity entity, Expression defValue)
		: base("constructor param")
	{
		this.entity = entity;
		this.defValue = defValue;
	}

	public virtual Entity Entity
	{
		get
		{
			return entity;
		}
	}

	public virtual Expression DefValue
	{
		get
		{
			return defValue;
		}
	}
}

}
