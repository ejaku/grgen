/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;

public class Nameof extends Expression
{
	/** The entity whose name we want to know. */
	private final Expression namedEntity;

	public Nameof(Expression entity, Type type)
	{
		super("nameof", type);
		this.namedEntity = entity;
	}

	public Expression getNamedEntity()
	{
		return namedEntity;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();

		if(namedEntity != null) {
			namedEntity.collectNeededEntities(needs);
		}
	}
}
