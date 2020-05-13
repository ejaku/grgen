/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class NodeByNameExpr extends Expression
{
	private final Expression name;
	private final Expression nodeType;

	public NodeByNameExpr(Expression name, Expression nodeType, Type type)
	{
		super("node by name expression", type);
		this.name = name;
		this.nodeType = nodeType;
	}

	public Expression getNameExpr()
	{
		return name;
	}

	public Expression getNodeTypeExpr()
	{
		return nodeType;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		name.collectNeededEntities(needs);
		nodeType.collectNeededEntities(needs);
	}
}
