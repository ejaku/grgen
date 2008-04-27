/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Assignment;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an assignment.
 * children: LHS:QualIdentNode, RHS:ExprNode
 */
public class AssignNode extends BaseNode {
	static {
		setName(AssignNode.class, "Assign");
	}

	QualIdentNode lhs;
	ExprNode rhs;

	/**
	 * @param coords The source code coordinates of = operator.
	 * @param qual The left hand side.
	 * @param expr The expression, that is assigned.
	 */
	public AssignNode(Coords coords, QualIdentNode qual, ExprNode expr) {
		super(coords);
		this.lhs = qual;
		becomeParent(this.lhs);
		this.rhs = expr;
		becomeParent(this.rhs);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(lhs);
		children.add(rhs);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("lhs");
		childrenNames.add("rhs");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		DeclNode owner = lhs.getOwner();
		TypeNode ty = owner.getDeclType();

		if(lhs.getDecl().isConst()) {
			error.error(getCoords(), "assignment to a const member is not allowed");
			return false;
		}

		if(ty instanceof InheritanceTypeNode) {
			InheritanceTypeNode inhTy = (InheritanceTypeNode) ty;

			if(inhTy.isConst()) {
				error.error(getCoords(), "assignment to a const type object not allowed");
				return false;
			}
		}

		return typeCheckLocal();
	}

	/**
	 * Checks whether the expression has a type equal, compatible or castable
	 * to the type of the target. Inserts implicit cast if compatible.
	 * @return true, if the types are equal or compatible, false otherwise
	 */
	protected boolean typeCheckLocal() {
		ExprNode expr = rhs;

		TypeNode targetType = lhs.getDecl().getDeclType();
		TypeNode exprType = expr.getType();

		if (! exprType.isEqual(targetType)) {
			expr = expr.adjustType(targetType);
			becomeParent(expr);
			rhs = expr;

			if (expr == ConstNode.getInvalid()) {
				String msg;
				if (exprType.isCastableTo(targetType)) {
					msg = "Assignment of " + exprType + " to " + targetType + " without a cast";
				} else {
					msg = "Incompatible assignment from " + exprType + " to " + targetType;
				}
				error.error(getCoords(), msg);
				return false;
			}
		}
		return true;
	}

	/**
	 * Construct the immediate representation from an assignment node.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		Qualification qual = lhs.checkIR(Qualification.class);
		if(qual.getOwner() instanceof Node && ((Node)qual.getOwner()).changesType()) {
			error.error(getCoords(), "Assignment to an old node of a type changed node is not allowed");
		}
		if(qual.getOwner() instanceof Edge && ((Edge)qual.getOwner()).changesType()) {
			error.error(getCoords(), "Assignment to an old edge of a type changed edge is not allowed");
		}
		return new Assignment(qual, rhs.evaluate().checkIR(Expression.class));
	}
}

