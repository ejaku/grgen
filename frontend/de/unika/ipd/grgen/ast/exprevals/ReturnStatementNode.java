/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
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
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.ReturnStatement;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a return statement.
 */
public class ReturnStatementNode extends EvalStatementNode {
	static {
		setName(ReturnStatementNode.class, "ReturnStatement");
	}

	private ExprNode returnValueExpr;

	public ReturnStatementNode(Coords coords, ExprNode returnValueExpr) {
		super(coords);
		this.returnValueExpr = returnValueExpr;
		becomeParent(returnValueExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(returnValueExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("return value expression");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}
	
	@Override
	protected IR constructIR() {
		return new ReturnStatement(returnValueExpr.checkIR(Expression.class));
	}
}
