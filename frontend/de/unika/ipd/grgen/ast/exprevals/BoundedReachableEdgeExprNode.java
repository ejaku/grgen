/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.containers.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.BoundedReachableEdgeExpr;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the depth-bounded reachable incident/incoming/outgoing edges of a node.
 */
public class BoundedReachableEdgeExprNode extends ExprNode {
	static {
		setName(BoundedReachableEdgeExprNode.class, "bounded reachable edge expr");
	}

	private ExprNode startNodeExpr;
	private ExprNode depthExpr;
	private ExprNode incidentTypeExpr;
	private ExprNode adjacentTypeExpr;
	private IdentNode resultEdgeType;

	private int direction;
	
	public static final int INCIDENT = 0;
	public static final int INCOMING = 1;
	public static final int OUTGOING = 2;

	
	public BoundedReachableEdgeExprNode(Coords coords,
			ExprNode startNodeExpr, ExprNode depthExpr,
			ExprNode incidentTypeExpr, int direction,
			ExprNode adjacentTypeExpr,
			IdentNode resultEdgeType) {
		super(coords);
		this.startNodeExpr = startNodeExpr;
		becomeParent(this.startNodeExpr);
		this.depthExpr = depthExpr;
		becomeParent(this.depthExpr);
		this.incidentTypeExpr = incidentTypeExpr;
		becomeParent(this.incidentTypeExpr);
		this.direction = direction;
		this.adjacentTypeExpr = adjacentTypeExpr;
		becomeParent(this.adjacentTypeExpr);
		this.resultEdgeType = resultEdgeType;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(startNodeExpr);
		children.add(depthExpr);
		children.add(incidentTypeExpr);
		children.add(adjacentTypeExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("start node expr");
		childrenNames.add("depth expr");
		childrenNames.add("incident type expr");
		childrenNames.add("adjacent type expr");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		return getType().resolve();
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		if(!(startNodeExpr.getType() instanceof NodeTypeNode)) {
			reportError("first argument of boundedReachableEdges(.,.,.,.) must be a node");
			return false;
		}
		if(!(depthExpr.getType() instanceof IntTypeNode)) {
			reportError("second argument of boundedReachableEdges(.,.,.,.) must be an int");
			return false;
		}
		if(!(incidentTypeExpr.getType() instanceof EdgeTypeNode)) {
			reportError("third argument of boundedReachableEdges(.,.,.,.) must be an edge type");
			return false;
		}
		if(!(adjacentTypeExpr.getType() instanceof NodeTypeNode)) {
			reportError("fourth argument of boundedReachableEdges(.,.,.,.) must be a node type");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		// assumes that the direction:int of the AST node uses the same values as the direction of the IR expression
		return new BoundedReachableEdgeExpr(startNodeExpr.checkIR(Expression.class),
								depthExpr.checkIR(Expression.class),
								incidentTypeExpr.checkIR(Expression.class), direction,
								adjacentTypeExpr.checkIR(Expression.class),
								getType().getType());
	}

	@Override
	public TypeNode getType() {
		return SetTypeNode.getSetType(resultEdgeType);
	}
}
