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
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.util.Direction;

/**
 * Base class for is in node neighborhood graph queries (with members shared by all these queries).
 */
public abstract class IsInNodeNeighborhoodQueryExprNode extends NeighborhoodQueryExprNode
{
	static {
		setName(IsInNodeNeighborhoodQueryExprNode.class, "is in node neighborhood query expr");
	}

	protected ExprNode endNodeExpr;


	protected IsInNodeNeighborhoodQueryExprNode(Coords coords, 
			ExprNode startNodeExpr, ExprNode endNodeExpr,
			ExprNode incidentTypeExpr, Direction direction,
			ExprNode adjacentTypeExpr)
	{
		super(coords, startNodeExpr, incidentTypeExpr, direction, adjacentTypeExpr);
		this.endNodeExpr = endNodeExpr;
		becomeParent(this.endNodeExpr);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		if(!(startNodeExpr.getType() instanceof NodeTypeNode)) {
			reportError("first argument of " + shortSignature() + " must be a node");
			return false;
		}
		if(!(endNodeExpr.getType() instanceof NodeTypeNode)) {
			reportError("second argument of " + shortSignature() + " must be a node");
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
