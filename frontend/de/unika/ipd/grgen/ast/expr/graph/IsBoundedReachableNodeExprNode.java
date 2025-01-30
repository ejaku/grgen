/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BooleanTypeNode;
import de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.IsBoundedReachableNodeExpr;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.util.Direction;

/**
 * An ast node telling whether an end node can be reached from a start node within a given number of steps into depth,
 * via incoming/outgoing/incident edges of given type, from/to a node of given type.
 * Should extend IsInNodeNeighborhoodQueryExprNode and BoundedNeighborhoodQueryExprNode, but Java does not support multiple inheritance...
 */
public class IsBoundedReachableNodeExprNode extends NeighborhoodQueryExprNode
{
	static {
		setName(IsBoundedReachableNodeExprNode.class, "is bounded reachable node expr");
	}

	private ExprNode endNodeExpr;
	private ExprNode depthExpr;


	public IsBoundedReachableNodeExprNode(Coords coords, 
			ExprNode startNodeExpr, ExprNode endNodeExpr, ExprNode depthExpr,
			ExprNode incidentTypeExpr, Direction direction,
			ExprNode adjacentTypeExpr)
	{
		super(coords, startNodeExpr, incidentTypeExpr, direction, adjacentTypeExpr);
		this.endNodeExpr = endNodeExpr;
		becomeParent(this.endNodeExpr);
		this.depthExpr = depthExpr;
		becomeParent(this.depthExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(startNodeExpr);
		children.add(endNodeExpr);
		children.add(depthExpr);
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
		childrenNames.add("depth expr");
		childrenNames.add("incident type expr");
		childrenNames.add("adjacent type expr");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		return true;
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
		if(!(depthExpr.getType() instanceof IntTypeNode)) {
			reportError("The function " + shortSignature() + " expects as 3. argument a value of type int"
					+ " (but is given a value of type " + depthExpr.getType().getTypeName() + ").");
			return false;
		}
		if(!(incidentTypeExpr.getType() instanceof EdgeTypeNode)) {
			reportError("The function " + shortSignature() + " expects as 4. argument a value of type edge type"
					+ " (but is given a value of type " + incidentTypeExpr.getType().getTypeName() + ").");
			return false;
		}
		if(!(adjacentTypeExpr.getType() instanceof NodeTypeNode)) {
			reportError("The function " + shortSignature() + " expects as 5. argument a value of type node type"
					+ " (but is given a value of type " + adjacentTypeExpr.getType().getTypeName() + ").");
			return false;
		}
		return true;
	}

	@Override
	protected String shortSignature()
	{
		return "isBoundedReachableNode(.,.,.,.,.)";
	}

	@Override
	protected IR constructIR()
	{
		startNodeExpr = startNodeExpr.evaluate();
		endNodeExpr = endNodeExpr.evaluate();
		incidentTypeExpr = incidentTypeExpr.evaluate();
		adjacentTypeExpr = adjacentTypeExpr.evaluate();
		// assumes that the direction:int of the AST node uses the same values as the direction of the IR expression
		return new IsBoundedReachableNodeExpr(startNodeExpr.checkIR(Expression.class),
				endNodeExpr.checkIR(Expression.class), depthExpr.checkIR(Expression.class),
				incidentTypeExpr.checkIR(Expression.class), direction,
				adjacentTypeExpr.checkIR(Expression.class),
				getType().getType());
	}

	@Override
	public TypeNode getType()
	{
		return BooleanTypeNode.booleanType;
	}
}
