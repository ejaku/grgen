/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.model.IncidenceCountIndex;
import de.unika.ipd.grgen.ir.type.basic.IntType;

public class CountIncidenceFromIndexExpr extends Expression
{
	IncidenceCountIndex index;
	Expression keyExpr;

	public CountIncidenceFromIndexExpr(IncidenceCountIndex target, Expression keyExpr)
	{
		super("count incidence from index access expression", IntType.getType());
		this.index = target;
		this.keyExpr = keyExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(this);
		keyExpr.collectNeededEntities(needs);
	}

	public IncidenceCountIndex getIndex()
	{
		return index;
	}

	public Expression getKeyExpr()
	{
		return keyExpr;
	}
}
