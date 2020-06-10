/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.CountBoundedReachableEdgeExpr;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.util.Direction;

/**
 * A node yielding the count of the depth-bounded reachable incident/incoming/outgoing edges of a node.
 */
public class CountBoundedReachableEdgeExprNode extends BoundedNeighborhoodQueryExprNode
{
	static {
		setName(CountBoundedReachableEdgeExprNode.class, "count bounded reachable edge expr");
	}

	public CountBoundedReachableEdgeExprNode(Coords coords,
			ExprNode startNodeExpr, ExprNode depthExpr,
			ExprNode incidentTypeExpr, Direction direction,
			ExprNode adjacentTypeExpr)
	{
		super(coords, startNodeExpr, depthExpr, incidentTypeExpr, direction, adjacentTypeExpr);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		return getType().resolve();
	}

	@Override
	protected String shortSignature()
	{
		return "countBoundedReachableEdges(.,.,.,.)";
	}

	@Override
	protected IR constructIR()
	{
		startNodeExpr = startNodeExpr.evaluate();
		depthExpr = depthExpr.evaluate();
		incidentTypeExpr = incidentTypeExpr.evaluate();
		adjacentTypeExpr = adjacentTypeExpr.evaluate();
		// assumes that the direction:int of the AST node uses the same values as the direction of the IR expression
		return new CountBoundedReachableEdgeExpr(startNodeExpr.checkIR(Expression.class),
				depthExpr.checkIR(Expression.class),
				incidentTypeExpr.checkIR(Expression.class), direction,
				adjacentTypeExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.intType;
	}
}
