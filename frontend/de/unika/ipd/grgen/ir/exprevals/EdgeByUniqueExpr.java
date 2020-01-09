/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class EdgeByUniqueExpr extends Expression {
	private final Expression unique;
	private final Expression edgeType;

	public EdgeByUniqueExpr(Expression unique, Expression edgeType, Type type) {
		super("edge by unique id expression", type);
		this.unique = unique;
		this.edgeType = edgeType;
	}

	public Expression getUniqueExpr() {
		return unique;
	}

	public Expression getEdgeTypeExpr() {
		return edgeType;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		unique.collectNeededEntities(needs);
		edgeType.collectNeededEntities(needs);
	}
}

