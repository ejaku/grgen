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
import de.unika.ipd.grgen.ir.exprevals.DefinedSubgraphExpr;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the defined subgraph of an edge set.
 */
public class DefinedSubgraphExprNode extends ExprNode {
	static {
		setName(DefinedSubgraphExprNode.class, "defined subgraph expr");
	}

	private ExprNode edgeSetExpr;
		
	public DefinedSubgraphExprNode(Coords coords, ExprNode edgeSetExpr) {
		super(coords);
		this.edgeSetExpr = edgeSetExpr;
		becomeParent(this.edgeSetExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(edgeSetExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("edgeSetExpr");
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
		if(!(edgeSetExpr.getType() instanceof SetTypeNode)) {
			edgeSetExpr.reportError("set expected as argument to definedSubgraph");
			return false;
		}
		SetTypeNode type = (SetTypeNode)edgeSetExpr.getType();
		if(!(type.valueType instanceof EdgeTypeNode)) {
			edgeSetExpr.reportError("set of edges expected as argument to definedSubgraph");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new DefinedSubgraphExpr(edgeSetExpr.checkIR(Expression.class), 
								getType().getType());
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.graphType;
	}
}
