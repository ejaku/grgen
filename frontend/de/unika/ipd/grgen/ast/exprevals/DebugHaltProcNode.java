/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.exprevals.DebugHaltProc;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class DebugHaltProcNode extends ProcedureInvocationBaseNode {
	static {
		setName(DebugHaltProcNode.class, "debug halt procedure");
	}

	private CollectNode<ExprNode> exprs = new CollectNode<ExprNode>();

	public DebugHaltProcNode(Coords coords) {
		super(coords);

		this.exprs = becomeParent(exprs);
	}

	public void addExpression(ExprNode expr) {
		exprs.addChild(expr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(exprs);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("exprs");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		if(!(exprs.children.get(0).getType().equals(BasicTypeNode.stringType))) {
			reportError("the first/message argument of Debug::halt() must be of string type");
			return false;
		}
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		Vector<Expression> expressions = new Vector<Expression>();
		for(ExprNode expr : exprs.getChildren()) {
			expressions.add(expr.checkIR(Expression.class));
		}
		return new DebugHaltProc(expressions);
	}
}
