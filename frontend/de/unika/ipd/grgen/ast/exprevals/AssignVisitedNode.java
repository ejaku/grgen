/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.exprevals.AssignmentVisited;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.Visited;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an assignment to a visited flag.
 */
public class AssignVisitedNode extends EvalStatementNode {
	static {
		setName(AssignVisitedNode.class, "Assign visited");
	}

	VisitedNode lhs;
	ExprNode rhs;
	
	int context;

	/**
	 * @param coords The source code coordinates of = operator.
	 * @param target The left hand side.
	 * @param expr The expression, that is assigned.
	 */
	public AssignVisitedNode(Coords coords, VisitedNode target, ExprNode expr, int context) {
		super(coords);
		this.lhs = target;
		becomeParent(this.lhs);
		this.rhs = expr;
		becomeParent(this.rhs);
		this.context = context;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(lhs);
		children.add(rhs);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("lhs");
		childrenNames.add("rhs");
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
		if((context&BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE)==BaseNode.CONTEXT_FUNCTION) {
			reportError("assignment to visited flag not allowed in function or lhs context");
			return false;
		}

		if(rhs.getType() != BasicTypeNode.booleanType) {
			error.error(getCoords(), "Visited flags may only be assigned boolean values");
			return false;
		}
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	/**
	 * Construct the immediate representation from an assignment node.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR() {
		Visited vis = lhs.checkIR(Visited.class);
		ExprNode rhsEvaluated = rhs.evaluate();
		return new AssignmentVisited(vis, rhsEvaluated.checkIR(Expression.class));
	}
}
