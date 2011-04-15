/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack, Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Iterator;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Assignment;
import de.unika.ipd.grgen.ir.AssignmentGraphEntity;
import de.unika.ipd.grgen.ir.AssignmentIdentical;
import de.unika.ipd.grgen.ir.AssignmentVar;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EvalStatement;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an assignment.
 */
public class AssignNode extends EvalStatementNode {
	static {
		setName(AssignNode.class, "Assign");
	}

	BaseNode lhsUnresolved;
	ExprNode rhs;
	
	QualIdentNode lhsQual;
	VarDeclNode lhsVar;
	ConstraintDeclNode lhsGraphElement;

	/**
	 * @param coords The source code coordinates of = operator.
	 * @param target The left hand side.
	 * @param expr The expression, that is assigned.
	 */
	public AssignNode(Coords coords, QualIdentNode target, ExprNode expr) {
		super(coords);
		this.lhsUnresolved = target;
		becomeParent(this.lhsUnresolved);
		this.rhs = expr;
		becomeParent(this.rhs);
	}

	/**
	 * @param coords The source code coordinates of = operator.
	 * @param target The left hand side.
	 * @param expr The expression, that is assigned.
	 */
	public AssignNode(Coords coords, IdentExprNode target, ExprNode expr) {
		super(coords);
		this.lhsUnresolved = target;
		becomeParent(this.lhsUnresolved);
		this.rhs = expr;
		becomeParent(this.rhs);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(lhsUnresolved, lhsQual, lhsVar, lhsGraphElement));
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
		boolean successfullyResolved = true;
		if(lhsUnresolved instanceof IdentExprNode) {
			IdentExprNode unresolved = (IdentExprNode)lhsUnresolved;
			if(unresolved.resolve()) {
				if(unresolved.decl instanceof VarDeclNode) {
					lhsVar = (VarDeclNode)unresolved.decl;
				} else if(unresolved.decl instanceof ConstraintDeclNode) {
					lhsGraphElement = (ConstraintDeclNode)unresolved.decl;
				} else {
					reportError("error in resolving lhs of assignment, unexpected type.");
					successfullyResolved = false;
				}
			} else {
				reportError("error in resolving lhs of assignment.");
				successfullyResolved = false;
			}
		} else if(lhsUnresolved instanceof QualIdentNode) {
			QualIdentNode unresolved = (QualIdentNode)lhsUnresolved;
			if(unresolved.resolve()) {
				lhsQual = unresolved;
			} else {
				reportError("error in resolving lhs of qualified attribute assignment.");
				successfullyResolved = false;
			}
		} else {
			reportError("internal error - invalid target in assign");
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
		if(lhsGraphElement!=null) targetType = lhsGraphElement.getDeclType();
		TypeNode exprType = rhs.getType();

		if (exprType.isEqual(targetType))
			return true;

		rhs = becomeParent(rhs.adjustType(targetType, getCoords()));
		return rhs != ConstNode.getInvalid();
	}

	/**
	 * Construct the immediate representation from an assignment node.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR() {
		// optimize . = . away
		if(isIdenticalAssignment()) {
			return new AssignmentIdentical();
		}
		
		if(lhsQual!=null) {
			Qualification qual = lhsQual.checkIR(Qualification.class);
			if(qual.getOwner() instanceof Node && ((Node)qual.getOwner()).changesType(null)) {
				error.error(getCoords(), "Assignment to an old node of a type changed node is not allowed");
			}
			if(qual.getOwner() instanceof Edge && ((Edge)qual.getOwner()).changesType(null)) {
				error.error(getCoords(), "Assignment to an old edge of a type changed edge is not allowed");
			}
	
			if(canSetOrMapAssignmentBeBrokenUpIntoStateChangingOperations()) {
				markSetOrMapAssignmentToBeBrokenUpIntoStateChangingOperations();
				ExprNode rhsEvaluated = rhs.evaluate();
				return rhsEvaluated.checkIR(EvalStatement.class);
			}
	
			ExprNode rhsEvaluated = rhs.evaluate();
			return new Assignment(qual, rhsEvaluated.checkIR(Expression.class));
		} else if(lhsVar!=null) {
			Variable var = lhsVar.checkIR(Variable.class);
		
			// TODO: extend optimization to assignments to variables
			
			ExprNode rhsEvaluated = rhs.evaluate();
			return new AssignmentVar(var, rhsEvaluated.checkIR(Expression.class));
		} else {
			GraphEntity graphEntity = lhsGraphElement.checkIR(GraphEntity.class);

			// TODO: extend optimization to assignments to graph entities
			
			ExprNode rhsEvaluated = rhs.evaluate();
			return new AssignmentGraphEntity(graphEntity, rhsEvaluated.checkIR(Expression.class));
		}
	}

	private boolean canSetOrMapAssignmentBeBrokenUpIntoStateChangingOperations()
	{
		// TODO: extend optimization to rewrite to compound assignment statement if same lhs but non-constructor rhs
		
		// is it a set or map assignment ?
		if(lhsQual == null) {
			return false; // TODO: extend optimization to assignments to variables
		}
		QualIdentNode qual = lhsQual;
		if(!(qual.getDecl().type instanceof SetTypeNode)
				&& !(qual.getDecl().type instanceof MapTypeNode )) {
			return false;
		}

		// descend and check if constraints are fulfilled which allow breakup
		ExprNode curLoc = rhs; // current location in the expression tree, more exactly: left-deep list
		while(curLoc!=null) {
			if(curLoc instanceof ArithmeticOpNode) {
				ArithmeticOpNode operator = (ArithmeticOpNode)curLoc;
				if(!(operator.getOperator().getOpId()==OperatorSignature.BIT_OR)
						&& !(operator.getOperator().getOpId()==OperatorSignature.EXCEPT)) {
					return false;
				}
				Collection<ExprNode> children = operator.getChildren();
				Iterator<ExprNode> it = children.iterator();
				ExprNode left = it.next();
				ExprNode right = it.next();
				if(!(right instanceof SetInitNode) && !(right instanceof MapInitNode)) {
					return false;
				}

				curLoc = left;
			}
			else if(curLoc instanceof MemberAccessExprNode) {
				// determine right owner and member, filter for needed types
				MemberAccessExprNode access = (MemberAccessExprNode)curLoc;
				if(!(access.getTarget() instanceof IdentExprNode)) return false;
				IdentExprNode target = (IdentExprNode)access.getTarget();
				if(!(target.getResolvedNode() instanceof ConstraintDeclNode)) return false;
				ConstraintDeclNode rightOwner = (ConstraintDeclNode)target.getResolvedNode();
				MemberDeclNode rightMember = access.getDecl();
				// determine left owner and member, filter for needed types
				MemberDeclNode leftMember = qual.getDecl();
				if(!(qual.getOwner() instanceof ConstraintDeclNode)) return false;
				ConstraintDeclNode leftOwner = (ConstraintDeclNode)qual.getOwner();
				// check that the accessed set/map is the same on the left and the right hand side
				if(leftOwner!=rightOwner) return false;
				if(leftMember!=rightMember) return false;

				curLoc = null;
			} else {
				return false;
			}
		}

		return true;
	}

	private void markSetOrMapAssignmentToBeBrokenUpIntoStateChangingOperations()
	{
		ExprNode curLoc = rhs;
		while(curLoc!=null) {
			if(curLoc instanceof ArithmeticOpNode) {
				ArithmeticOpNode operator = (ArithmeticOpNode)curLoc;
				operator.markToBreakUpIntoStateChangingOperations(lhsQual);
				ExprNode left = operator.getChildren().iterator().next();
				curLoc = left;
			} else {
				curLoc = null;
			}
		}
	}
	
	private boolean isIdenticalAssignment()
	{
		if(lhsQual!=null) {
			if(rhs instanceof MemberAccessExprNode) {
				MemberAccessExprNode rhsQual = (MemberAccessExprNode)rhs;
				if(!(rhsQual.getTarget() instanceof IdentExprNode)) return false;
				IdentExprNode target = (IdentExprNode)rhsQual.getTarget();
				if(lhsQual.getOwner()==target.decl.getDecl()
						&& lhsQual.getDecl()==rhsQual.getDecl())
					return true;
			}
		} else {
			if(rhs instanceof IdentExprNode) {
				IdentExprNode rhsVar = (IdentExprNode)rhs;
				if(lhsVar==rhsVar.decl.getDecl())
					return true;
			}
		}
		
		return false;
	}
}
