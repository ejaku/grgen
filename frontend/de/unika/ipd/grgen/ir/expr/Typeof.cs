/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.expr
{
using de.unika.ipd.grgen.ir;
using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

public class Typeof : Expression
{
	/// <summary>
	/// The entity whose type we want to know. </summary>
	private readonly Entity entity;

	public Typeof(Entity entity)
		: base("typeof", entity.Type)
	{
		this.entity = entity;
	}

	public virtual Entity Entity
	{
		get
		{
			return entity;
		}
	}

	public override string NodeLabel
	{
		get
		{
			return "typeof<" + entity + ">";
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
	public override void CollectNeededEntities(NeededEntities needs)
	{
		if(!IsGlobalVariable(entity))
		{
			if(entity is GraphEntity)
				needs.Add((GraphEntity)entity);
			else
				needs.Add((Variable)entity);
		}
	}
}

}
