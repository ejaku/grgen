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
import de.unika.ipd.grgen.ir.exprevals.InsertDefinedSubgraphExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding an inserted edge of the insertion of a defined subgraph of an edge set.
 */
public class InsertDefinedSubgraphExprNode extends ExprNode {
	static {
		setName(InsertDefinedSubgraphExprNode.class, "insert defined subgraph expr");
	}

	private ExprNode edgeSetExpr;
	private ExprNode edgeExpr;
		
	public InsertDefinedSubgraphExprNode(Coords coords, ExprNode edgeSetExpr, ExprNode edgeExpr) {
		super(coords);
		this.edgeSetExpr = edgeSetExpr;
		becomeParent(this.edgeSetExpr);
		this.edgeExpr = edgeExpr;
		becomeParent(this.edgeExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(edgeSetExpr);
		children.add(edgeExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("edgeSetExpr");
		childrenNames.add("edgeExpr");
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
			edgeSetExpr.reportError("set expected as 1st argument to insertDefinedSubgraph");
			return false;
		}
		SetTypeNode type = (SetTypeNode)edgeSetExpr.getType();
		if(!(type.valueType instanceof EdgeTypeNode)) {
			edgeSetExpr.reportError("set of edges expected as 1st argument to insertDefinedSubgraph");
			return false;
		}
		if(!(edgeExpr.getType() instanceof EdgeTypeNode)) {
			edgeExpr.reportError("edge expected as 2nd argument to insertDefinedSubgraph");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new InsertDefinedSubgraphExpr(edgeSetExpr.checkIR(Expression.class), 
								edgeExpr.checkIR(Expression.class),
								getType().getType());
	}

	@Override
	public TypeNode getType() {
		return edgeExpr.getType();
	}
}
