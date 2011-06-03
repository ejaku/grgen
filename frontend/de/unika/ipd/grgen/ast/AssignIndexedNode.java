/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.AssignmentIndexed;
import de.unika.ipd.grgen.ir.AssignmentVarIndexed;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an indexed assignment.
 * for now array only, MAP TODO: extend to map
 */
public class AssignIndexedNode extends EvalStatementNode {
	static {
		setName(AssignIndexedNode.class, "Assign indexed");
	}

	BaseNode lhsUnresolved;
	ExprNode rhs;
	ExprNode index;

	QualIdentNode lhsQual;
	VarDeclNode lhsVar;

	/**
	 * @param coords The source code coordinates of = operator.
	 * @param target The left hand side.
	 * @param expr The expression, that is assigned.
	 * @param index The index expression to the lhs entity.
	 */
	public AssignIndexedNode(Coords coords, QualIdentNode target, ExprNode expr, ExprNode index) {
		super(coords);
		this.lhsUnresolved = target;
		becomeParent(this.lhsUnresolved);
		this.rhs = expr;
		becomeParent(this.rhs);
		this.index = index;
		becomeParent(this.index);
	}

	/**
	 * @param coords The source code coordinates of = operator.
	 * @param target The left hand side.
	 * @param expr The expression, that is assigned.
	 * @param index The index expression to the lhs entity.
	 */
	public AssignIndexedNode(Coords coords, IdentExprNode target, ExprNode expr, ExprNode index) {
		super(coords);
		this.lhsUnresolved = target;
		becomeParent(this.lhsUnresolved);
		this.rhs = expr;
		becomeParent(this.rhs);
		this.index = index;
		becomeParent(this.index);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(lhsUnresolved, lhsQual, lhsVar));
		children.add(rhs);
		children.add(index);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("lhs");
		childrenNames.add("rhs");
		childrenNames.add("index");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		if(lhsUnresolved instanceof IdentExprNode) {
			IdentExprNode unresolved = (IdentExprNode)lhsUnresolved;
			if(unresolved.resolve()) {
				if(unresolved.decl instanceof VarDeclNode) {
					lhsVar = (VarDeclNode)unresolved.decl;
				} else {
					reportError("error in resolving lhs of indexed assignment, unexpected type.");
					successfullyResolved = false;
				}
			} else {
				reportError("error in resolving lhs of indexed assignment.");
				successfullyResolved = false;
			}
		} else if(lhsUnresolved instanceof QualIdentNode) {
			QualIdentNode unresolved = (QualIdentNode)lhsUnresolved;
			if(unresolved.resolve()) {
				lhsQual = unresolved;
			} else {
				reportError("error in resolving lhs of qualified attribute indexed assignment.");
				successfullyResolved = false;
			}
		} else {
			reportError("internal error - invalid target in indexed assign");
			successfullyResolved = false;
		}
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		if(lhsQual!=null)
		{
			DeclNode owner = lhsQual.getOwner();
			TypeNode ty = owner.getDeclType();

			if(lhsQual.getDecl().isConst()) {
				error.error(getCoords(), "indexed assignment to a const member is not allowed");
				return false;
			}

			if(ty instanceof InheritanceTypeNode) {
				InheritanceTypeNode inhTy = (InheritanceTypeNode) ty;

				if(inhTy.isConst()) {
					error.error(getCoords(), "indexed assignment to a const type object not allowed");
					return false;
				}
			}
		}

		return typeCheckLocal();
	}

	/**
	 * Checks whether the expression has a type equal, compatible or castable
	 * to the type of the target. Inserts implicit cast if compatible.
	 * @return true, if the types are equal or compatible, false otherwise
	 */
	private boolean typeCheckLocal() {
		TypeNode targetType = null;
		if(lhsQual!=null) targetType = lhsQual.getDecl().getDeclType();
		if(lhsVar!=null) targetType = lhsVar.getDeclType();
		if(!(targetType instanceof ArrayTypeNode)) {
			targetType.reportError("can only do an indexed assignment on an attribute/variable of array type");
		}
		TypeNode valueType = ((ArrayTypeNode)targetType).valueType;

		TypeNode exprType = rhs.getType();

		if (exprType.isEqual(valueType))
			return true;

		rhs = becomeParent(rhs.adjustType(valueType, getCoords()));
		return rhs != ConstNode.getInvalid();
	}

	/**
	 * Construct the immediate representation from an indexed assignment node.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR() {
		if(lhsQual!=null) {
			Qualification qual = lhsQual.checkIR(Qualification.class);
			if(qual.getOwner() instanceof Node && ((Node)qual.getOwner()).changesType(null)) {
				error.error(getCoords(), "Assignment to an old node of a type changed node is not allowed");
			}
			if(qual.getOwner() instanceof Edge && ((Edge)qual.getOwner()).changesType(null)) {
				error.error(getCoords(), "Assignment to an old edge of a type changed edge is not allowed");
			}

			ExprNode rhsEvaluated = rhs.evaluate();
			ExprNode indexEvaluated = index.evaluate();
			return new AssignmentIndexed(qual, rhsEvaluated.checkIR(Expression.class),
					indexEvaluated.checkIR(Expression.class));
		} else {
			Variable var = lhsVar.checkIR(Variable.class);

			ExprNode rhsEvaluated = rhs.evaluate();
			ExprNode indexEvaluated = index.evaluate();
			return new AssignmentVarIndexed(var, rhsEvaluated.checkIR(Expression.class),
					indexEvaluated.checkIR(Expression.class));
		}
	}
}
