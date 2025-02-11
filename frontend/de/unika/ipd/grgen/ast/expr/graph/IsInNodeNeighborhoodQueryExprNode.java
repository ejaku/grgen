/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
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

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(startNodeExpr);
		children.add(endNodeExpr);
		children.add(incidentTypeExpr);
		children.add(adjacentTypeExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("start node expr");
		childrenNames.add("end node expr");
		childrenNames.add("incident type expr");
		childrenNames.add("adjacent type expr");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		if(!(startNodeExpr.getType() instanceof NodeTypeNode)) {
			reportError("The function " + shortSignature() + " expects as 1. argument a value of type node"
					+ " (but is given a value of type " + startNodeExpr.getType().getTypeName() + ").");
			return false;
		}
		if(!(endNodeExpr.getType() instanceof NodeTypeNode)) {
			reportError("The function " + shortSignature() + " expects as 2. argument a value of type node"
					+ " (but is given a value of type " + endNodeExpr.getType().getTypeName() + ").");
			return false;
		}
		if(!(incidentTypeExpr.getType() instanceof EdgeTypeNode)) {
			reportError("The function " + shortSignature() + " expects as 3. argument a value of type edge type"
					+ " (but is given a value of type " + incidentTypeExpr.getType().getTypeName() + ").");
			return false;
		}
		if(!(adjacentTypeExpr.getType() instanceof NodeTypeNode)) {
			reportError("The function " + shortSignature() + " expects as 4. argument a value of type node type"
					+ " (but is given a value of type " + adjacentTypeExpr.getType().getTypeName() + ").");
			return false;
		}
		return true;
	}
}
