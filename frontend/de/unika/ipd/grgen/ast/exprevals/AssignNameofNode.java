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
import de.unika.ipd.grgen.ir.exprevals.AssignmentNameof;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a name assignment.
 */
public class AssignNameofNode extends EvalStatementNode {
	static {
		setName(AssignNameofNode.class, "Assign name");
	}

	ExprNode lhs;
	ExprNode rhs;
	
	int context;

	/**
	 * @param coords The source code coordinates of = operator.
	 * @param target The left hand side.
	 * @param expr The expression, that is assigned.
	 */
	public AssignNameofNode(Coords coords, ExprNode target, ExprNode expr, int context) {
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
		if(lhs!=null)
			children.add(lhs);
		children.add(rhs);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		if(lhs!=null)
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
			reportError("assignment to name not allowed in function or lhs context");
			return false;
		}

		if(rhs.getType() != BasicTypeNode.stringType) {
			error.error(getCoords(), "The name to be assigned must be a string value");
			return false;
		}
		
		if(lhs != null) {
			if(lhs.getType().isEqual(BasicTypeNode.graphType)) {
				return true;
			} 
			if(lhs.getType() instanceof EdgeTypeNode) {
				return true;
			}
			if(lhs.getType() instanceof NodeTypeNode) {
				return true;
			}

			reportError("nameof() assignment expects an entity of node or edge or subgraph type");
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
		Expression lhsExpr = null;
		if(lhs!=null)
			lhsExpr = lhs.checkIR(Expression.class);
		ExprNode rhsEvaluated = rhs.evaluate();
		return new AssignmentNameof(lhsExpr, rhsEvaluated.checkIR(Expression.class));
	}
}
