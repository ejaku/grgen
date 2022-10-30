/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.graph.AssignmentNameof;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a name assignment.
 */
public class AssignNameofNode extends EvalStatementNode
{
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
	public AssignNameofNode(Coords coords, ExprNode target, ExprNode expr, int context)
	{
		super(coords);
		this.lhs = target;
		becomeParent(this.lhs);
		this.rhs = expr;
		becomeParent(this.rhs);
		this.context = context;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		if(lhs != null)
			children.add(lhs);
		children.add(rhs);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		if(lhs != null)
			childrenNames.add("lhs");
		childrenNames.add("rhs");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION) {
			reportError("The nameof() assignment is not allowed in function or pattern part context.");
			return false;
		}

		TypeNode rhsType = rhs.getType();
		if(rhsType != BasicTypeNode.stringType) {
			reportError("The nameof() assignment expects as name to be assigned"
					+ " a value of type string"
					+ " (but is given a value of type " + rhsType.toStringWithDeclarationCoords() + ").");
			return false;
		}

		if(lhs != null) {
			TypeNode lhsType = lhs.getType();
			if(lhsType.isEqual(BasicTypeNode.graphType)) {
				return true;
			}
			if(lhsType instanceof EdgeTypeNode) {
				return true;
			}
			if(lhsType instanceof NodeTypeNode) {
				return true;
			}

			reportError("The nameof() assignment expects as entity to assign to its name"
					+ " a value of type Node or Edge or graph"
					+ " (but is given a value of type " + lhsType.toStringWithDeclarationCoords() + ").");
			return false;
		}

		return true;
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	/**
	 * Construct the immediate representation from an assignment node.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR()
	{
		Expression lhsExpr = null;
		if(lhs != null) {
			lhs = lhs.evaluate();
			lhsExpr = lhs.checkIR(Expression.class);
		}
		rhs = rhs.evaluate();
		return new AssignmentNameof(lhsExpr, rhs.checkIR(Expression.class));
	}
}
