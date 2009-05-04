/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Iterator;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Assignment;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EvalStatement;
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
public class AssignNode extends EvalStatementNode {
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
		TypeNode targetType = qual.getDecl().getDeclType();
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
			
			if(canSetOrMapAssignmentBeBrokenUpIntoStateChangingOperations()) {
				markSetOrMapAssignmentToBeBrokenUpIntoStateChangingOperations();
				ExprNode rhsEvaluated = rhs.evaluate(); 
				return rhsEvaluated.checkIR(EvalStatement.class);
			}
		}
		else if(lhs instanceof VisitedNode) {
			target = lhs.checkIR(Visited.class);
		}
		else throw new UnsupportedOperationException("Unsupported LHS of assignment: \"" + lhs + "\"");

		ExprNode rhsEvaluated = rhs.evaluate(); 
		return new Assignment(target, rhsEvaluated.checkIR(Expression.class));
	}
	
	protected boolean canSetOrMapAssignmentBeBrokenUpIntoStateChangingOperations()
	{
		// is it a set or map assignment ?
		if(!(lhs instanceof QualIdentNode)) {
			return false;
		}
		QualIdentNode qual = (QualIdentNode)lhs;
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

	protected void markSetOrMapAssignmentToBeBrokenUpIntoStateChangingOperations()
	{
		ExprNode curLoc = rhs;
		while(curLoc!=null) {
			if(curLoc instanceof ArithmeticOpNode) {
				ArithmeticOpNode operator = (ArithmeticOpNode)curLoc;
				operator.markToBreakUpIntoStateChangingOperations((QualIdentNode)lhs);
				ExprNode left = operator.getChildren().iterator().next();			
				curLoc = left;
			} else { 
				curLoc = null;
			}
		}
	}
}
