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
import de.unika.ipd.grgen.ast.containers.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.IncidentEdgeExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the incident/incoming/outgoing edges of a node.
 */
public class IncidentEdgeExprNode extends ExprNode {
	static {
		setName(IncidentEdgeExprNode.class, "incident edge expr");
	}

	private ExprNode startNodeExpr;
	private ExprNode incidentTypeExpr;
	private ExprNode adjacentTypeExpr;
	private IdentNode resultEdgeType;

	private int direction;
	
	public static final int INCIDENT = 0;
	public static final int INCOMING = 1;
	public static final int OUTGOING = 2;
	
	public IncidentEdgeExprNode(Coords coords,
			ExprNode startNodeExpr,
			ExprNode incidentTypeExpr, int direction,
			ExprNode adjacentTypeExpr,
			IdentNode resultEdgeType) {
		super(coords);
		this.startNodeExpr = startNodeExpr;
		becomeParent(this.startNodeExpr);
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
		children.add(incidentTypeExpr);
		children.add(adjacentTypeExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("start node expr");
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
		TypeNode startNodeType = startNodeExpr.getType();
		if(!(startNodeType instanceof NodeTypeNode)) {
			reportError("first argument of incidentEdges(.,.,.) must be a node");
			return false;
		}
		TypeNode incidentType = incidentTypeExpr.getType();
		if(!(incidentType instanceof EdgeTypeNode)) {
			reportError("second argument of incidentEdges(.,.,.) must be an edge type");
			return false;
		}
		TypeNode adjacentType = adjacentTypeExpr.getType();
		if(!(adjacentType instanceof NodeTypeNode)) {
			reportError("third argument of incidentEdges(.,.,.) must be a node type");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		// assumes that the direction:int of the AST node uses the same values as the direction of the IR expression
		return new IncidentEdgeExpr(startNodeExpr.checkIR(Expression.class), 
								incidentTypeExpr.checkIR(Expression.class), direction,
								adjacentTypeExpr.checkIR(Expression.class),
								getType().getType());
	}

	@Override
	public TypeNode getType() {
		return SetTypeNode.getSetType(resultEdgeType);
	}
}
