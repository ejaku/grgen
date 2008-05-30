/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
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
import de.unika.ipd.grgen.ir.Visited;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an assignment.
 * children: LHS:QualIdentNode|VisitedNode, RHS:ExprNode
 */
public class AssignNode extends BaseNode {
	static {
		setName(AssignNode.class, "Assign");
	}

	BaseNode lhs;
	ExprNode rhs;

	/**
	 * @param coords The source code coordinates of = operator.
	 * @param target The left hand side.
	 * @param expr The expression, that is assigned.
	 */
	public AssignNode(Coords coords, BaseNode target, ExprNode expr) {
		super(coords);
		this.lhs = target;
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
		if(lhs instanceof QualIdentNode) {
			QualIdentNode qual = (QualIdentNode) lhs;
			DeclNode owner = qual.getOwner();
			TypeNode ty = owner.getDeclType();

			if(qual.getDecl().isConst()) {
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
		else if(lhs instanceof VisitedNode) {
			if(rhs.getType() != BasicTypeNode.booleanType) {
				error.error(getCoords(), "Visited flags may only be assigned boolean values");
				return false;
			}
			return true;
		}
		else throw new UnsupportedOperationException("Unsupported target: \"" + lhs + "\"");
	}

	/**
	 * Checks whether the expression has a type equal, compatible or castable
	 * to the type of the target. Inserts implicit cast if compatible.
	 * @return true, if the types are equal or compatible, false otherwise
	 */
	protected boolean typeCheckLocal() {
		QualIdentNode qual = (QualIdentNode) lhs;
		ExprNode expr = rhs;

		TypeNode targetType = qual.getDecl().getDeclType();
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
		Expression target;

		if(lhs instanceof QualIdentNode) {
			Qualification qual = lhs.checkIR(Qualification.class);
			if(qual.getOwner() instanceof Node && ((Node)qual.getOwner()).changesType()) {
				error.error(getCoords(), "Assignment to an old node of a type changed node is not allowed");
			}
			if(qual.getOwner() instanceof Edge && ((Edge)qual.getOwner()).changesType()) {
				error.error(getCoords(), "Assignment to an old edge of a type changed edge is not allowed");
			}
			target = qual;
		}
		else if(lhs instanceof VisitedNode) {
			target = lhs.checkIR(Visited.class);
		}
		else throw new UnsupportedOperationException("Unsupported LHS of assignment: \"" + lhs + "\"");

		return new Assignment(target, rhs.evaluate().checkIR(Expression.class));
	}
}

