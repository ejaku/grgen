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
import de.unika.ipd.grgen.ir.exprevals.EqualsAnyExpr;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node comparing a subgraph against a subgraph set.
 */
public class EqualsAnyExprNode extends ExprNode {
	static {
		setName(EqualsAnyExprNode.class, "equals any expr");
	}

	private ExprNode subgraphExpr;
	private ExprNode subgraphSetExpr;
	private boolean includingAttributes;
		
	public EqualsAnyExprNode(Coords coords, ExprNode subgraphExpr, ExprNode subgraphSetExpr, boolean includingAttributes) {
		super(coords);
		this.subgraphExpr = subgraphExpr;
		becomeParent(this.subgraphExpr);
		this.subgraphSetExpr = subgraphSetExpr;
		becomeParent(this.subgraphSetExpr);
		this.includingAttributes = includingAttributes;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(subgraphExpr);
		children.add(subgraphSetExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("subgraphExpr");
		childrenNames.add("subgraphSetExpr");
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
		if(!(subgraphExpr.getType() instanceof GraphTypeNode)) {
			subgraphExpr.reportError("(sub)graph expected as first argument to equalsAny");
			return false;
		}
		if(!(subgraphSetExpr.getType() instanceof SetTypeNode)) {
			subgraphSetExpr.reportError("set expected as second argument to equalsAny");
			return false;
		}
		SetTypeNode type = (SetTypeNode)subgraphSetExpr.getType();
		if(!(type.valueType instanceof GraphTypeNode)) {
			subgraphSetExpr.reportError("set of (sub)graphs expected as second argument to equalsAny");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new EqualsAnyExpr(subgraphExpr.checkIR(Expression.class), 
								subgraphSetExpr.checkIR(Expression.class), 
								includingAttributes,
								getType().getType());
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.booleanType;
	}
}
