/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.model.IncidenceCountIndex;
import de.unika.ipd.grgen.ir.type.basic.IntType;

public class IndexedIncidenceCountIndexAccessExpr extends Expression
{
	IncidenceCountIndex target;
	Expression keyExpr;

	public IndexedIncidenceCountIndexAccessExpr(IncidenceCountIndex target, Expression keyExpr)
	{
		super("indexed incidence count index access expression", IntType.getType());
		this.target = target;
		this.keyExpr = keyExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(this);
		keyExpr.collectNeededEntities(needs);
	}

	public IncidenceCountIndex getTarget()
	{
		return target;
	}

	public Expression getKeyExpr()
	{
		return keyExpr;
	}
}
