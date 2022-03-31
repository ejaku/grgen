/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Variable;

public class Typeof extends Expression
{
	/** The entity whose type we want to know. */
	private final Entity entity;

	public Typeof(Entity entity)
	{
		super("typeof", entity.getType());
		this.entity = entity;
	}

	public Entity getEntity()
	{
		return entity;
	}

	@Override
	public String getNodeLabel()
	{
		return "typeof<" + entity + ">";
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		if(!isGlobalVariable(entity)) {
			if(entity instanceof GraphEntity)
				needs.add((GraphEntity)entity);
			else
				needs.add((Variable)entity);
		}
	}
}
