/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.BoundedReachableNodeExpr;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.util.Direction;

/**
 * A node yielding the depth-bounded reachable nodes/reachable nodes via incoming edges/reachable nodes via outgoing edges of a node.
 */
public class BoundedReachableNodeExprNode extends BoundedNeighborhoodQueryExprNode
{
	static {
		setName(BoundedReachableNodeExprNode.class, "bounded reachable node expr");
	}

	private SetTypeNode setTypeNode;
	
	
	public BoundedReachableNodeExprNode(Coords coords,
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
		setTypeNode = new SetTypeNode(getNodeRoot(adjacentTypeExpr));
		return setTypeNode.resolve();
	}

	@Override
	protected String shortSignature()
	{
		return "boundedReachable(.,.,.,.)";
	}

	@Override
	protected IR constructIR()
	{
		startNodeExpr = startNodeExpr.evaluate();
		depthExpr = depthExpr.evaluate();
		incidentTypeExpr = incidentTypeExpr.evaluate();
		adjacentTypeExpr = adjacentTypeExpr.evaluate();
		// assumes that the direction:int of the AST node uses the same values as the direction of the IR expression
		return new BoundedReachableNodeExpr(startNodeExpr.checkIR(Expression.class),
				depthExpr.checkIR(Expression.class),
				incidentTypeExpr.checkIR(Expression.class), direction,
				adjacentTypeExpr.checkIR(Expression.class),
				getType().getType());
	}

	@Override
	public TypeNode getType()
	{
		return setTypeNode;
	}
}
