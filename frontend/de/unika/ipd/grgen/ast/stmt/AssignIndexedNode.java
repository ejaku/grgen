/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast.stmt;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.IdentExprNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.stmt.AssignmentIndexed;
import de.unika.ipd.grgen.ir.stmt.AssignmentVarIndexed;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an indexed assignment.
 */
public class AssignIndexedNode extends EvalStatementNode
{
	static {
		setName(AssignIndexedNode.class, "Assign indexed");
	}

	BaseNode lhsUnresolved;
	ExprNode rhs;
	ExprNode index;
	boolean onLHS;

	QualIdentNode lhsQual;
	VarDeclNode lhsVar;

	int context;

	/**
	 * @param coords The source code coordinates of = operator.
	 * @param target The left hand side.
	 * @param expr The expression, that is assigned.
	 * @param index The index expression to the lhs entity.
	 */
	public AssignIndexedNode(Coords coords, QualIdentNode target,
			ExprNode expr, ExprNode index, int context)
	{
		super(coords);
		this.lhsUnresolved = target;
		becomeParent(this.lhsUnresolved);
		this.rhs = expr;
		becomeParent(this.rhs);
		this.index = index;
		becomeParent(this.index);
		this.context = context;
		this.onLHS = false;
	}

	/**
	 * @param coords The source code coordinates of = operator.
	 * @param target The left hand side.
	 * @param expr The expression, that is assigned.
	 * @param index The index expression to the lhs entity.
	 */
	public AssignIndexedNode(Coords coords, IdentExprNode target,
			ExprNode expr, ExprNode index, int context, boolean onLHS)
	{
		super(coords);
		this.lhsUnresolved = target;
		becomeParent(this.lhsUnresolved);
		this.rhs = expr;
		becomeParent(this.rhs);
		this.index = index;
		becomeParent(this.index);
		this.context = context;
		this.onLHS = onLHS;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(lhsUnresolved, lhsQual, lhsVar));
		children.add(rhs);
		children.add(index);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("lhs");
		childrenNames.add("rhs");
		childrenNames.add("index");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = true;
		if(lhsUnresolved instanceof IdentExprNode) {
			IdentExprNode unresolved = (IdentExprNode)lhsUnresolved;
			if(unresolved.resolve()) {
				if(unresolved.decl instanceof VarDeclNode) {
					lhsVar = (VarDeclNode)unresolved.decl;
				} else {
					reportError("Error in resolving the variable on the left hand side of the indexed assignment (given is " + unresolved.getIdent() + ").");
					successfullyResolved = false;
				}
			} else {
				reportError("Error in resolving the variable on the left hand side of the indexed assignment (given is " + unresolved.getIdent() + ").");
				successfullyResolved = false;
			}
		} else if(lhsUnresolved instanceof QualIdentNode) {
			QualIdentNode unresolved = (QualIdentNode)lhsUnresolved;
			if(unresolved.resolve()) {
				lhsQual = unresolved;
			} else {
				reportError("Error in resolving the qualified attribute on the left hand side of the indexed assignment (given is " + unresolved + ").");
				successfullyResolved = false;
			}
		} else {
			reportError("Internal error - invalid left hand side in indexed assignment.");
			successfullyResolved = false;
		}
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		if(lhsQual != null) {
			if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION) {
				reportError("An indexed assignment to an attribute of a graph element is not allowed in function or pattern part context.");
				return false;
			}

			DeclNode owner = lhsQual.getOwner();
			TypeNode ty = owner.getDeclType();

			if(lhsQual.getDecl().isConst()) {
				reportError("An indexed assignment to a const member is not allowed (" + lhsQual.getDecl().getIdentNode() + lhsQual.getDecl().getDeclarationCoords() + " is constant).");
				return false;
			}

			if(ty instanceof InheritanceTypeNode) {
				InheritanceTypeNode inhTy = (InheritanceTypeNode)ty;

				if(inhTy.isConst()) {
					reportError("An indexed assignment to a const type object is not allowed (" + inhTy.toStringWithDeclarationCoords() + " is constant).");
					return false;
				}
			}

			if(owner instanceof ConstraintDeclNode) {
				ConstraintDeclNode entity = (ConstraintDeclNode)owner;
				if((entity.context & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {
					if(getCoords().comesBefore(entity.getCoords())) {
						reportError("Variables (node,edge,var,ref) of computations must be declared before they can be assigned to (with index)"
								+ " (" + entity.getIdentNode() + " was not yet declared).");
						return false;
					}
				}
			}
		} else {
			if((lhsVar.context & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {
				if(getCoords().comesBefore(lhsVar.getCoords())) {
					reportError("Variables (node,edge,var,ref) of computations must be declared before they can be assigned to (with index)"
							+ " (" + lhsVar.getIdentNode() + " was not yet declared).");
					return false;
				}
			}

			if(lhsVar.directlyNestingLHSGraph == null && onLHS) {
				reportError("An indexed assignment to a global variable (" + lhsVar.getIdentNode() + ") is not allowed from a yield block.");
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
	private boolean typeCheckLocal()
	{
		TypeNode targetType = null;
		if(lhsQual != null)
			targetType = lhsQual.getDecl().getDeclType();
		if(lhsVar != null)
			targetType = lhsVar.getDeclType();

		boolean valueOk = checkValueType(targetType);
		boolean indexOk = checkIndexType(targetType);

		return valueOk && indexOk;
	}

	private boolean checkValueType(TypeNode targetType)
	{
		TypeNode valueType;
		if(targetType instanceof ArrayTypeNode) {
			valueType = ((ArrayTypeNode)targetType).valueType;
		} else if(targetType instanceof DequeTypeNode) {
			valueType = ((DequeTypeNode)targetType).valueType;
		} else if(targetType instanceof MapTypeNode) {
			valueType = ((MapTypeNode)targetType).valueType;
		} else {
			targetType.reportError("Can only carry out an indexed assignment on an attribute/variable of array/deque/map type"
					+ " (given is type " + targetType.getTypeName() + ").");
			return false;
		}

		TypeNode exprType = rhs.getType();
		if(exprType.isEqual(valueType))
			return true;

		rhs = becomeParent(rhs.adjustType(valueType, getCoords()));
		return rhs != ConstNode.getInvalid();
	}

	private boolean checkIndexType(TypeNode targetType)
	{
		TypeNode keyType;
		if(targetType instanceof MapTypeNode)
			keyType = ((MapTypeNode)targetType).keyType;
		else
			keyType = IntTypeNode.intType;
		TypeNode keyExprType = index.getType();

		if(keyExprType instanceof InheritanceTypeNode) {
			if(keyExprType.isCompatibleTo(keyType))
				return true;

			reportError("Cannot convert index in assignment"
					+ " from " + keyExprType.toStringWithDeclarationCoords()
					+ " to the expected " + keyType.toStringWithDeclarationCoords() + ".");
			return false;
		} else {
			if(keyExprType.isEqual(keyType))
				return true;

			index = becomeParent(index.adjustType(keyType, getCoords()));
			return index != ConstNode.getInvalid();
		}
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	/**
	 * Construct the immediate representation from an indexed assignment node.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR()
	{
		if(lhsQual != null) {
			Qualification qual = lhsQual.checkIR(Qualification.class);
			if(qual.getOwner() instanceof Node && ((Node)qual.getOwner()).changesType(null)) {
				reportError("An assignment to a node whose type will be changed is not allowed.");
			}
			if(qual.getOwner() instanceof Edge && ((Edge)qual.getOwner()).changesType(null)) {
				reportError("An assignment to an edge whose type will be changed is not allowed.");
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
