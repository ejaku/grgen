/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.type.basic.GraphType;

public class Uniqueof extends BuiltinFunctionInvocationExpr
{
	/** The entity whose unique id we want to know. */
	private final Expression entity;

	public Uniqueof(Expression entity, Type type)
	{
		super("uniqueof", type);
		this.entity = entity;
	}

	public Expression getEntity()
	{
		return entity;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		if(entity == null || entity.getType() instanceof GraphType)
			needs.needsGraph();
		if(entity != null)
			entity.collectNeededEntities(needs);
	}
}
