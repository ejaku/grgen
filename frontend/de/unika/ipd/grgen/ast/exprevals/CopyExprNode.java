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
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.CopyExpr;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the copy of a subgraph.
 */
public class CopyExprNode extends ExprNode {
	static {
		setName(CopyExprNode.class, "copy expr");
	}

	private ExprNode graphExpr;
		
	public CopyExprNode(Coords coords, ExprNode graphExpr) {
		super(coords);
		this.graphExpr = graphExpr;
		becomeParent(this.graphExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(graphExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("graphExpr");
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
		if(!(graphExpr.getType() instanceof GraphTypeNode)) {
			graphExpr.reportError("graph expected as argument to copy");
			return false;
		}
		return true;
	}

	@Override 
	protected IR constructIR() {
		return new CopyExpr(graphExpr.checkIR(Expression.class), 
								getType().getType());
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.graphType;
	}
}
