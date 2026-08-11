/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.expr.graph
{
using de.unika.ipd.grgen.ir;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Type = de.unika.ipd.grgen.ir.type.Type;

public class Nameof : Expression
{
	/// <summary>
	/// The entity whose name we want to know. </summary>
	private readonly Expression namedEntity;

	public Nameof(Expression entity, Type type)
		: base("nameof", type)
	{
		this.namedEntity = entity;
	}

	public virtual Expression NamedEntity
	{
		get
		{
			return namedEntity;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
	public override void CollectNeededEntities(NeededEntities needs)
	{
		needs.NeedsGraph();

		if(namedEntity != null)
			namedEntity.CollectNeededEntities(needs);
	}
}

}
