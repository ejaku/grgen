/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class EdgeByNameExpr extends Expression
{
	private final Expression name;
	private final Expression edgeType;

	public EdgeByNameExpr(Expression name, Expression edgeType, Type type)
	{
		super("edge by name expression", type);
		this.name = name;
		this.edgeType = edgeType;
	}

	public Expression getNameExpr()
	{
		return name;
	}

	public Expression getEdgeTypeExpr()
	{
		return edgeType;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		name.collectNeededEntities(needs);
		edgeType.collectNeededEntities(needs);
	}
}
