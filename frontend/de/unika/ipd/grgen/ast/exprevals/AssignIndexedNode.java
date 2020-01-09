/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.containers.*;
import de.unika.ipd.grgen.ir.exprevals.AssignmentIndexed;
import de.unika.ipd.grgen.ir.exprevals.AssignmentVarIndexed;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.exprevals.Qualification;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an indexed assignment.
 */
public class AssignIndexedNode extends EvalStatementNode {
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
	public AssignIndexedNode(Coords coords, QualIdentNode target, ExprNode expr, ExprNode index, int context) {
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
	public AssignIndexedNode(Coords coords, IdentExprNode target, ExprNode expr, ExprNode index, int context, boolean onLHS) {
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
			if((context&BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE)==BaseNode.CONTEXT_FUNCTION) {
				reportError("indexed assignment to attribute of graph element not allowed in function or lhs context");
				return false;
			}

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
			
			if(owner instanceof ConstraintDeclNode) {
				ConstraintDeclNode entity = (ConstraintDeclNode)owner;
				if((entity.context & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {					
					if(getCoords().comesBefore(entity.getCoords())) {
						reportError("Variables (node,edge,var,ref) of computations must be declared before they can be assigned (with index).");
						return false;
					}
				}
			}
		}
		else
		{
			if((lhsVar.context & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {					
				if(getCoords().comesBefore(lhsVar.getCoords())) {
					reportError("Variables (node,edge,var,ref) of computations must be declared before they can be assigned (with index).");
					return false;
				}
			}
			
			if(lhsVar.directlyNestingLHSGraph == null && onLHS) {
				error.error(getCoords(), "indexed assignment to a global variable not allowed from a yield block ("+lhsVar.getIdentNode()+")");
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
	private boolean typeCheckLocal() {
		TypeNode targetType = null;
		if(lhsQual!=null) targetType = lhsQual.getDecl().getDeclType();
		if(lhsVar!=null) targetType = lhsVar.getDeclType();

		boolean valueOk = checkValueType(targetType);
		boolean indexOk = checkIndexType(targetType);
		
		return valueOk && indexOk;
	}

	private boolean checkValueType(TypeNode targetType) {
		TypeNode valueType;
		if(targetType instanceof ArrayTypeNode) {
			valueType = ((ArrayTypeNode)targetType).valueType;
		} else if (targetType instanceof DequeTypeNode) {
			valueType = ((DequeTypeNode)targetType).valueType;
		} else if (targetType instanceof MapTypeNode) {
			valueType = ((MapTypeNode)targetType).valueType;
		} else {
			targetType.reportError("can only do an indexed assignment on an attribute/variable of array/deque/map type");
			return false;
		}

		TypeNode exprType = rhs.getType();
		if (exprType.isEqual(valueType))
			return true;

		rhs = becomeParent(rhs.adjustType(valueType, getCoords()));
		return rhs != ConstNode.getInvalid();
	}

	private boolean checkIndexType(TypeNode targetType) {
		TypeNode keyType;
		if(targetType instanceof MapTypeNode)
			keyType = ((MapTypeNode)targetType).keyType;
		else
			keyType = IntTypeNode.intType;
		TypeNode keyExprType = index.getType();
		
		if (keyExprType instanceof InheritanceTypeNode) {
			if(keyExprType.isCompatibleTo(keyType))
				return true;
			
			String givenTypeName;
			if(keyExprType instanceof InheritanceTypeNode)
				givenTypeName = ((InheritanceTypeNode) keyExprType).getIdentNode().toString();
			else
				givenTypeName = keyExprType.toString();
			String expectedTypeName;
			if(keyType instanceof InheritanceTypeNode)
				expectedTypeName = ((InheritanceTypeNode) keyType).getIdentNode().toString();
			else
				expectedTypeName = keyType.toString();
			reportError("Cannot convert assign index from \""
					+ givenTypeName + "\" to \"" + expectedTypeName + "\"");
			return false;
		} else {
			if (keyExprType.isEqual(keyType))
				return true;

			index = becomeParent(index.adjustType(keyType, getCoords()));
			return index != ConstNode.getInvalid();
		}
	}
	
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
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
