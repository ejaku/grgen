/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.HighlightProc;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class HighlightProcNode extends ProcedureInvocationBaseNode {
	static {
		setName(HighlightProcNode.class, "highlight procedure");
	}

	private CollectNode<ExprNode> highlightChildren = new CollectNode<ExprNode>();

	public HighlightProcNode(Coords coords) {
		super(coords);

		this.highlightChildren = becomeParent(highlightChildren);
	}

	public void addExpressionToHighlight(ExprNode expr) {
		highlightChildren.addChild(expr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(highlightChildren);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("exprs to highlight");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		// TODO: constrain to int, string, node, edge, container
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		Vector<Expression> expressionsToHighlight = new Vector<Expression>();
		for(ExprNode expr : highlightChildren.getChildren()) {
			expressionsToHighlight.add(expr.checkIR(Expression.class));
		}
		return new HighlightProc(expressionsToHighlight);
	}
}
