/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.util.Direction;

/**
 * Base class for bounded neighborhood graph queries (with members shared by all these queries).
 */
public abstract class BoundedNeighborhoodQueryExprNode extends NeighborhoodQueryExprNode
{
	static {
		setName(BoundedNeighborhoodQueryExprNode.class, "bounded neighborhood query node expr");
	}

	protected ExprNode depthExpr;
	
	
	protected BoundedNeighborhoodQueryExprNode(Coords coords,
			ExprNode startNodeExpr, ExprNode depthExpr,
			ExprNode incidentTypeExpr, Direction direction,
			ExprNode adjacentTypeExpr)
	{
		super(coords, startNodeExpr, incidentTypeExpr, direction, adjacentTypeExpr);
		this.depthExpr = depthExpr;
		becomeParent(this.depthExpr);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		if(!(startNodeExpr.getType() instanceof NodeTypeNode)) {
			reportError("first argument of " + shortSignature() + " must be a node");
			return false;
		}
		if(!(depthExpr.getType() instanceof IntTypeNode)) {
			reportError("second argument of " + shortSignature() + " must be an int");
			return false;
		}
		if(!(incidentTypeExpr.getType() instanceof EdgeTypeNode)) {
			reportError("third argument of " + shortSignature() + " must be an edge type");
			return false;
		}
		if(!(adjacentTypeExpr.getType() instanceof NodeTypeNode)) {
			reportError("fourth argument of " + shortSignature() + " must be a node type");
			return false;
		}
		return true;
	}
}
