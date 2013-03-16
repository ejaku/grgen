/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.GraphRetypeEdgeExpr;
import de.unika.ipd.grgen.ir.exprevals.GraphRetypeNodeExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node for retyping a node or an edge to a new type.
 */
public class GraphRetypeExprNode extends ExprNode {
	static {
		setName(GraphRetypeExprNode.class, "retype expr");
	}

	private ExprNode entity;
	private ExprNode entityType;
	
	public GraphRetypeExprNode(Coords coords, ExprNode entity,
			ExprNode entityType) {
		super(coords);
		this.entity = entity;
		becomeParent(this.entity);
		this.entityType = entityType;
		becomeParent(this.entityType);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(entity);
		children.add(entityType);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("entity");
		childrenNames.add("new type");
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
		if(entity.getType() instanceof NodeTypeNode && entityType.getType() instanceof NodeTypeNode) {
			return true;
		}
		if(entity.getType() instanceof EdgeTypeNode && entityType.getType() instanceof EdgeTypeNode) {
			return true;
		}
		reportError("retype(.,.) can only retype a node to a node type, or an edge to an edge type");
		return false;
	}

	@Override
	protected IR constructIR() {
		if(entityType.getType() instanceof NodeTypeNode)
			return new GraphRetypeNodeExpr(entity.checkIR(Expression.class), 
							entityType.checkIR(Expression.class),
							getType().getType());
		else
			return new GraphRetypeEdgeExpr(entity.checkIR(Expression.class), 
							entityType.checkIR(Expression.class),
							getType().getType());			
	}

	@Override
	public TypeNode getType() {
		return entityType.getType();
	}
}
