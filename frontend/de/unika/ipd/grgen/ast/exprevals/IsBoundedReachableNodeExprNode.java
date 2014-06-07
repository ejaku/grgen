/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.IsBoundedReachableNodeExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * An ast node telling whether an end node can be reached from a start node within a given number of steps into depth,
 * via incoming/outgoing/incident edges of given type, from/to a node of given type.
 */
public class IsBoundedReachableNodeExprNode extends ExprNode {
	static {
		setName(IsBoundedReachableNodeExprNode.class, "is bounded reachable node expr");
	}

	private ExprNode startNodeExpr;
	private ExprNode endNodeExpr;
	private ExprNode depthExpr;
	private ExprNode incidentTypeExpr;
	private ExprNode adjacentTypeExpr;

	private int direction;
	
	public static final int ADJACENT = 0;
	public static final int INCOMING = 1;
	public static final int OUTGOING = 2;
	
	public IsBoundedReachableNodeExprNode(Coords coords, 
			ExprNode startNodeExpr, ExprNode endNodeExpr, ExprNode depthExpr,
			ExprNode incidentTypeExpr, int direction,
			ExprNode adjacentTypeExpr) {
		super(coords);
		this.startNodeExpr = startNodeExpr;
		becomeParent(this.startNodeExpr);
		this.endNodeExpr = endNodeExpr;
		becomeParent(this.endNodeExpr);
		this.depthExpr = depthExpr;
		becomeParent(this.depthExpr);
		this.incidentTypeExpr = incidentTypeExpr;
		becomeParent(this.incidentTypeExpr);
		this.direction = direction;
		this.adjacentTypeExpr = adjacentTypeExpr;
		becomeParent(this.adjacentTypeExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
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
	public Collection<String> getChildrenNames() {
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
	protected boolean resolveLocal() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		if(!(startNodeExpr.getType() instanceof NodeTypeNode)) {
			reportError("first argument of isBoundedReachableNode(.,.,.,.,.) must be a node");
			return false;
		}
		if(!(endNodeExpr.getType() instanceof NodeTypeNode)) {
			reportError("second argument of isBoundedReachableNode(.,.,.,.,.) must be a node");
			return false;
		}
		if(!(depthExpr.getType() instanceof IntTypeNode)) {
			reportError("third argument of isBoundedReachableEdge(.,.,.,.,.,.) must be an int");
			return false;
		}
		if(!(incidentTypeExpr.getType() instanceof EdgeTypeNode)) {
			reportError("fourth argument of isBoundedReachableNode(.,.,.,.,.) must be an edge type");
			return false;
		}
		if(!(adjacentTypeExpr.getType() instanceof NodeTypeNode)) {
			reportError("fifth argument of isBoundedReachableNode(.,.,.,.,.) must be a node type");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		// assumes that the direction:int of the AST node uses the same values as the direction of the IR expression
		return new IsBoundedReachableNodeExpr(startNodeExpr.checkIR(Expression.class),
								endNodeExpr.checkIR(Expression.class), depthExpr.checkIR(Expression.class), 
								incidentTypeExpr.checkIR(Expression.class), direction,
								adjacentTypeExpr.checkIR(Expression.class),
								getType().getType());
	}

	@Override
	public TypeNode getType() {
		return BooleanTypeNode.booleanType;
	}
}
