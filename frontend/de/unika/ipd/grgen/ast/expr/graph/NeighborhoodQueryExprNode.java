/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.graph;

import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.parser.Coords;

/**
 * Base class for neighborhood graph queries (with members shared by all these queries).
 */
public abstract class NeighborhoodQueryExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(NeighborhoodQueryExprNode.class, "neighborhood query expr");
	}

	protected ExprNode startNodeExpr;
	protected ExprNode incidentTypeExpr;
	protected ExprNode adjacentTypeExpr;

	protected int direction;

	public static final int ADJACENT = 0;
	public static final int INCIDENT = 0;
	public static final int INCOMING = 1;
	public static final int OUTGOING = 2;

	public NeighborhoodQueryExprNode(Coords coords,
			ExprNode startNodeExpr,
			ExprNode incidentTypeExpr, int direction,
			ExprNode adjacentTypeExpr)
	{
		super(coords);
		this.startNodeExpr = startNodeExpr;
		becomeParent(this.startNodeExpr);
		this.incidentTypeExpr = incidentTypeExpr;
		becomeParent(this.incidentTypeExpr);
		this.direction = direction;
		this.adjacentTypeExpr = adjacentTypeExpr;
		becomeParent(this.adjacentTypeExpr);
	}
}
