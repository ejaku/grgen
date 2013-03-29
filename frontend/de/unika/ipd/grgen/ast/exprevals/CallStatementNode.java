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
import de.unika.ipd.grgen.ir.exprevals.CallStatement;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a call statement (of a function normally returning a value, throwing that value away).
 */
public class CallStatementNode extends EvalStatementNode {
	static {
		setName(CallStatementNode.class, "CallStatement");
	}

	private ExprNode calledFunction;

	public CallStatementNode(Coords coords, ExprNode calledFunction) {
		super(coords);
		this.calledFunction = calledFunction;
		becomeParent(calledFunction);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(calledFunction);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("calledFunction");
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
		return new CallStatement(calledFunction.checkIR(Expression.class));
	}
}
