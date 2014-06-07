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
import de.unika.ipd.grgen.ir.exprevals.CanonizeExpr;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class CanonizeExprNode extends ExprNode {
	static {
		setName(CanonizeExprNode.class, "canonize expr");
	}

	private ExprNode graphExpr;


	public CanonizeExprNode(Coords coords, ExprNode graphExpr) {
		super(coords);

		this.graphExpr = becomeParent(graphExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(graphExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("graph");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		if(graphExpr.getType().isEqual(BasicTypeNode.graphType)) {
			return true;
		} else {
			reportError("canonize(.) expects a subgraph of graph type");
			return false;
		}
	}

	@Override
	protected IR constructIR() {
		return new CanonizeExpr(graphExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.stringType;
	}
}
