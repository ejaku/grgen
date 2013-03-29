/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.exprevals.InsertInducedSubgraphExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding an inserted node of the insertion of an induced subgraph of a node set.
 */
public class InsertInducedSubgraphExprNode extends ExprNode {
	static {
		setName(InsertInducedSubgraphExprNode.class, "insert induced subgraph expr");
	}

	private ExprNode nodeSetExpr;
	private ExprNode nodeExpr;
		
	public InsertInducedSubgraphExprNode(Coords coords, ExprNode nodeSetExpr, ExprNode nodeExpr) {
		super(coords);
		this.nodeSetExpr = nodeSetExpr;
		becomeParent(this.nodeSetExpr);
		this.nodeExpr = nodeExpr;
		becomeParent(this.nodeExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(nodeSetExpr);
		children.add(nodeExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("nodeSetExpr");
		childrenNames.add("nodeExpr");
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
		if(!(nodeSetExpr.getType() instanceof SetTypeNode)) {
			nodeSetExpr.reportError("set expected as 1st argument to insertInducedSubgraph");
			return false;
		}
		SetTypeNode type = (SetTypeNode)nodeSetExpr.getType();
		if(!(type.valueType instanceof NodeTypeNode)) {
			nodeSetExpr.reportError("set of nodes expected as 1st argument to insertInducedSubgraph");
			return false;
		}
		if(!(nodeExpr.getType() instanceof NodeTypeNode)) {
			nodeExpr.reportError("node expected as 2nd argument to insertInducedSubgraph");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new InsertInducedSubgraphExpr(nodeSetExpr.checkIR(Expression.class), 
								nodeExpr.checkIR(Expression.class),
								getType().getType());
	}

	@Override
	public TypeNode getType() {
		return nodeExpr.getType();
	}
}
