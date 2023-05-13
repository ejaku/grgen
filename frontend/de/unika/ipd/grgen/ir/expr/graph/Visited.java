/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.basic.BooleanType;

public class Visited extends Expression
{
	private Expression visitorID;
	private Expression entity;

	public Visited(Expression visitorID, Expression entity)
	{
		super("visited", BooleanType.getType());
		this.visitorID = visitorID;
		this.entity = entity;
	}

	public Expression getVisitorID()
	{
		return visitorID;
	}

	public Expression getEntity()
	{
		return entity;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		entity.collectNeededEntities(needs);
		visitorID.collectNeededEntities(needs);
	}
}
