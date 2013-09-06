/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
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
import de.unika.ipd.grgen.ir.exprevals.InducedSubgraphExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the induced subgraph of a node set.
 */
public class InducedSubgraphExprNode extends ExprNode {
	static {
		setName(InducedSubgraphExprNode.class, "induced subgraph expr");
	}

	private ExprNode nodeSetExpr;
		
	public InducedSubgraphExprNode(Coords coords, ExprNode nodeSetExpr) {
		super(coords);
		this.nodeSetExpr = nodeSetExpr;
		becomeParent(this.nodeSetExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(nodeSetExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("nodeSetExpr");
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
			nodeSetExpr.reportError("set expected as argument to inducedSubgraph");
			return false;
		}
		SetTypeNode type = (SetTypeNode)nodeSetExpr.getType();
		if(!(type.valueType instanceof NodeTypeNode)) {
			nodeSetExpr.reportError("set of nodes expected as argument to inducedSubgraph");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new InducedSubgraphExpr(nodeSetExpr.checkIR(Expression.class), 
								getType().getType());
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.graphType;
	}
}
