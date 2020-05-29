/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.BoundedReachableNodeWithRemainingDepthExpr;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.util.Direction;

/**
 * A node yielding the depth-bounded reachable nodes/reachable nodes via incoming edges/reachable nodes via outgoing edges of a node.
 */
public class BoundedReachableNodeWithRemainingDepthExprNode extends BoundedNeighborhoodQueryExprNode
{
	static {
		setName(BoundedReachableNodeWithRemainingDepthExprNode.class,
				"bounded reachable node with remaining depth expr");
	}

	private MapTypeNode mapTypeNode;

	
	public BoundedReachableNodeWithRemainingDepthExprNode(Coords coords,
			ExprNode startNodeExpr, ExprNode depthExpr,
			ExprNode incidentTypeExpr, Direction direction,
			ExprNode adjacentTypeExpr)
	{
		super(coords, startNodeExpr, depthExpr, incidentTypeExpr, direction, adjacentTypeExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(startNodeExpr);
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
		childrenNames.add("depth expr");
		childrenNames.add("incident type expr");
		childrenNames.add("adjacent type expr");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		mapTypeNode = new MapTypeNode(getNodeRoot(adjacentTypeExpr), BasicTypeNode.intType.getIdentNode());
		return mapTypeNode.resolve();
	}

	@Override
	protected String shortSignature()
	{
		return "boundedReachableWithRemainingDepth(.,.,.,.)";
	}

	@Override
	protected IR constructIR()
	{
		// assumes that the direction:int of the AST node uses the same values as the direction of the IR expression
		return new BoundedReachableNodeWithRemainingDepthExpr(startNodeExpr.checkIR(Expression.class), 
				depthExpr.checkIR(Expression.class),
				incidentTypeExpr.checkIR(Expression.class), direction,
				adjacentTypeExpr.checkIR(Expression.class),
				getType().getType());
	}

	@Override
	public TypeNode getType()
	{
		return mapTypeNode;
	}
}
