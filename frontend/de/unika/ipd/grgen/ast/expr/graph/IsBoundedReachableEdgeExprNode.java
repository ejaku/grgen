/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.expr.graph.IsBoundedReachableEdgeExpr;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.util.Direction;

/**
 * An ast node telling whether an end edge can be reached from a start node within a given number of steps into depth,
 * via incoming/outgoing/incident edges of given type, from/to a node of given type.
 * Should extend IsInEdgeNeighborhoodQueryExprNode and BoundedNeighborhoodQueryExprNode, but Java does not support multiple inheritance...
 */
public class IsBoundedReachableEdgeExprNode extends NeighborhoodQueryExprNode
{
	static {
		setName(IsBoundedReachableEdgeExprNode.class, "is bounded reachable edge expr");
	}

	private ExprNode endEdgeExpr;
	private ExprNode depthExpr;

	
	public IsBoundedReachableEdgeExprNode(Coords coords, 
			ExprNode startNodeExpr, ExprNode endEdgeExpr, ExprNode depthExpr,
			ExprNode incidentTypeExpr, Direction direction,
			ExprNode adjacentTypeExpr)
	{
		super(coords, startNodeExpr, incidentTypeExpr, direction, adjacentTypeExpr);
		this.endEdgeExpr = endEdgeExpr;
		becomeParent(this.endEdgeExpr);
		this.depthExpr = depthExpr;
		becomeParent(this.depthExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(startNodeExpr);
		children.add(endEdgeExpr);
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
		childrenNames.add("end edge expr");
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
			reportError("first argument of " + shortSignature() + " must be a node");
			return false;
		}
		if(!(endEdgeExpr.getType() instanceof EdgeTypeNode)) {
			reportError("second argument of " + shortSignature() + " must be an edge");
			return false;
		}
		if(!(depthExpr.getType() instanceof IntTypeNode)) {
			reportError("third argument of " + shortSignature() + " must be an int");
			return false;
		}
		if(!(incidentTypeExpr.getType() instanceof EdgeTypeNode)) {
			reportError("fourth argument of " + shortSignature() + " must be an edge type");
			return false;
		}
		if(!(adjacentTypeExpr.getType() instanceof NodeTypeNode)) {
			reportError("fifth argument of " + shortSignature() + " must be a node type");
			return false;
		}
		return true;
	}

	@Override
	protected String shortSignature()
	{
		return "isBoundedReachableEdge(.,.,.,.,.)";
	}

	@Override
	protected IR constructIR()
	{
		startNodeExpr = startNodeExpr.evaluate();
		endEdgeExpr = endEdgeExpr.evaluate();
		incidentTypeExpr = incidentTypeExpr.evaluate();
		adjacentTypeExpr = adjacentTypeExpr.evaluate();
		// assumes that the direction:int of the AST node uses the same values as the direction of the IR expression
		return new IsBoundedReachableEdgeExpr(startNodeExpr.checkIR(Expression.class),
				endEdgeExpr.checkIR(Expression.class), depthExpr.checkIR(Expression.class),
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
