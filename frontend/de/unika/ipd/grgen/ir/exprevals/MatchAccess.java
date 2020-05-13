/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class MatchAccess extends Expression
{
	Expression expression;
	Entity entity; // member

	public MatchAccess(Expression expression, Entity entity)
	{
		super("match access", entity.getType());
		this.expression = expression;
		this.entity = entity;
	}

	public Expression getExpr()
	{
		return expression;
	}

	public Entity getEntity()
	{
		return entity;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs)
	{
	}
}
