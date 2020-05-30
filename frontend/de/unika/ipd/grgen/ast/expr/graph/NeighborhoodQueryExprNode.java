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

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.util.Direction;

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

	protected Direction direction;

	
	protected NeighborhoodQueryExprNode(Coords coords,
			ExprNode startNodeExpr,
			ExprNode incidentTypeExpr, Direction direction,
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
	
	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(startNodeExpr);
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
		childrenNames.add("incident type expr");
		childrenNames.add("adjacent type expr");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		if(!(startNodeExpr.getType() instanceof NodeTypeNode)) {
			reportError("first argument of " + shortSignature() + " must be a node");
			return false;
		}
		if(!(incidentTypeExpr.getType() instanceof EdgeTypeNode)) {
			reportError("second argument of " + shortSignature() + " must be an edge type");
			return false;
		}
		if(!(adjacentTypeExpr.getType() instanceof NodeTypeNode)) {
			reportError("third argument of " + shortSignature() + " must be a node type");
			return false;
		}
		return true;
	}
	
	protected abstract String shortSignature();
}
