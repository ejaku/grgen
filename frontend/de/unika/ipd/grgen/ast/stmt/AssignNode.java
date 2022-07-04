/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack, Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast.stmt;

import java.util.Collection;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ArithmeticOperatorNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.IdentExprNode;
import de.unika.ipd.grgen.ast.expr.MemberAccessExprNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ast.expr.map.MapInitNode;
import de.unika.ipd.grgen.ast.expr.set.SetInitNode;
import de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.stmt.Assignment;
import de.unika.ipd.grgen.ir.stmt.AssignmentGraphEntity;
import de.unika.ipd.grgen.ir.stmt.AssignmentIdentical;
import de.unika.ipd.grgen.ir.stmt.AssignmentMember;
import de.unika.ipd.grgen.ir.stmt.AssignmentVar;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an assignment.
 */
public class AssignNode extends EvalStatementNode
{
	static {
		setName(AssignNode.class, "Assign");
	}

	BaseNode lhsUnresolved;
	ExprNode rhs;
	int context;
	boolean onLHS;

	QualIdentNode lhsQual;
	VarDeclNode lhsVar;
	ConstraintDeclNode lhsGraphElement;
	MemberDeclNode lhsMember;

	/**
	 * @param coords The source code coordinates of = operator.
	 * @param target The left hand side.
	 * @param expr The expression, that is assigned.
	 */
	public AssignNode(Coords coords, QualIdentNode target, ExprNode expr, int context)
	{
		super(coords);
		this.lhsUnresolved = target;
		becomeParent(this.lhsUnresolved);
		this.rhs = expr;
		becomeParent(this.rhs);
		this.context = context;
		this.onLHS = false;
	}

	/**
	 * @param coords The source code coordinates of = operator.
	 * @param target The left hand side.
	 * @param expr The expression, that is assigned.
	 */
	public AssignNode(Coords coords, IdentExprNode target, ExprNode expr, int context, boolean onLHS)
	{
		super(coords);
		this.lhsUnresolved = target;
		becomeParent(this.lhsUnresolved);
		this.rhs = expr;
		becomeParent(this.rhs);
		this.context = context;
		this.onLHS = onLHS;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(lhsUnresolved, lhsQual, lhsVar, lhsGraphElement));
		children.add(rhs);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("lhs");
		childrenNames.add("rhs");
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
				} else if(unresolved.decl instanceof ConstraintDeclNode) {
					lhsGraphElement = (ConstraintDeclNode)unresolved.decl;
				} else if(unresolved.decl instanceof MemberDeclNode) {
					//lhsMember = (MemberDeclNode)unresolved.decl;
					reportError("Error in resolving the LHS of the assignment (given is " + unresolved.getIdent() + ")"
							+ " (use this." + unresolved.getIdent() + " to access a class member inside a method).");
					successfullyResolved = false;
				} else {
					reportError("Error in resolving the LHS of the assignment, a variable or graph element is expected (given is " + unresolved.getIdent() + ").");
					successfullyResolved = false;
				}
			} else {
				reportError("Error in resolving the LHS of the assignment (given is " + unresolved.getIdent() + ").");
				successfullyResolved = false;
			}
		} else if(lhsUnresolved instanceof QualIdentNode) {
			QualIdentNode unresolved = (QualIdentNode)lhsUnresolved;
			if(unresolved.resolve()) {
				lhsQual = unresolved;
			} else {
				reportError("Error in resolving the qualified attribute on the LHS of the assignment (given is " + unresolved + ").");
				successfullyResolved = false;
			}
		} else {
			reportError("Internal error - invalid LHS in assignment.");
			successfullyResolved = false;
		}
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		if(lhsQual != null) {
			if(!checkLhsQual())
				return false;
		} else if(lhsGraphElement != null) {
			if(!checkLhsGraphElement())
				return false;
		} else if(lhsVar != null) {
			if(!checkLhsVar())
				return false;
		}

		return typeCheckLocal();
	}

	private boolean checkLhsQual()
	{
		if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION
				&& !lhsQual.isMatchAssignment()
				&& !lhsQual.isTransientObjectAssignment()) {
			reportError("An assignment to an attribute of a graph element or internal class object is not allowed in function or pattern part context.");
			return false;
		}

		DeclNode owner = lhsQual.getOwner();
		TypeNode ty = owner.getDeclType();

		MemberDeclNode member = lhsQual.getDecl(); // null for match type
		if(member != null && member.isConst()) {
			error.error(getCoords(), "An assignment to a const member is not allowed (but " + lhsQual.getDecl().getIdentNode() + " is contant).");
			return false;
		}

		if(ty instanceof InheritanceTypeNode) {
			InheritanceTypeNode inhTy = (InheritanceTypeNode)ty;

			if(inhTy.isConst()) {
				error.error(getCoords(), "An assignment to a const type object is not allowed (but " + inhTy + " is constant).");
				return false;
			}
		}

		if(owner instanceof ConstraintDeclNode) {
			ConstraintDeclNode entity = (ConstraintDeclNode)owner;
			if((entity.context & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {
				if(getCoords().comesBefore(entity.getCoords())) {
					reportError("Variables (node,edge,var,ref) of computations must be declared before they can be assigned to"
							+ " (" + entity.getIdentNode() + " was not yet declared).");
					return false;
				}
			}
		}
		
		return true;
	}

	private boolean checkLhsGraphElement()
	{
		if(lhsGraphElement.defEntityToBeYieldedTo) {
			IdentExprNode identExpr = (IdentExprNode)lhsUnresolved;
			if((lhsGraphElement.context & CONTEXT_COMPUTATION) != CONTEXT_COMPUTATION) {
				if(!identExpr.yieldedTo) {
					error.error(getCoords(), "Only a yield assignment is allowed to a def pattern graph element"
							+ " (" + lhsGraphElement.getIdentNode() + ").");
					return false;
				}
			} else {
				if(identExpr.yieldedTo) {
					error.error(getCoords(), "Use a non-yield assignment to a computation local def pattern graph element"
							+ " (" + lhsGraphElement.getIdentNode() + ").");
					return false;
				}
			}

			if((lhsGraphElement.context & CONTEXT_COMPUTATION) != CONTEXT_COMPUTATION) {
				if((lhsGraphElement.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS
						&& (context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
					error.error(getCoords(), "Cannot yield from the RHS to a LHS def pattern graph element"
							+ " (" + lhsGraphElement.getIdentNode() + ").");
					return false;
				}
			}
		} else {
			if(lhsGraphElement.directlyNestingLHSGraph != null) {
				IdentExprNode identExpr = (IdentExprNode)lhsUnresolved;
				if(identExpr.yieldedTo) {
					error.error(getCoords(), "A yield assignment is only allowed to a def pattern graph element"
							+ " (" + lhsGraphElement.getIdentNode() + " was declared without def).");
					return false;
				}
				error.error(getCoords(), "Only a def pattern graph element can be assigned to"
						+ " (" + lhsGraphElement.getIdentNode() + " was declared without def).");
				return false;
			}

			if(lhsGraphElement.directlyNestingLHSGraph == null && onLHS) {
				error.error(getCoords(), "An assignment to a global variable (" + lhsGraphElement.getIdentNode() + ") is not allowed from a yield block.");
				return false;
			}
		}

		if((lhsGraphElement.context & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {
			if(getCoords().comesBefore(lhsGraphElement.getCoords())) {
				reportError("Variables (node,edge,var,ref) of computations must be declared before they can be assigned to"
						+ " (" + lhsGraphElement.getIdentNode() + " was not yet declared).");
				return false;
			}
		}
		
		return true;
	}

	private boolean checkLhsVar()
	{
		if(lhsVar.defEntityToBeYieldedTo) {
			IdentExprNode identExpr = (IdentExprNode)lhsUnresolved;
			if((lhsVar.context & CONTEXT_COMPUTATION) != CONTEXT_COMPUTATION) {
				if(!identExpr.yieldedTo) {
					error.error(getCoords(), "Only a yield assignment is allowed to a def variable"
							+ " (" + lhsVar.getIdentNode() + ").");
					return false;
				}
			} else {
				if(identExpr.yieldedTo) {
					error.error(getCoords(), "Use a non-yield assignment to a computation local def variable"
							+ " (" + lhsVar.getIdentNode() + ").");
					return false;
				}
			}

			if((lhsVar.context & CONTEXT_COMPUTATION) != CONTEXT_COMPUTATION) {
				if((lhsVar.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS
						&& (context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
					error.error(getCoords(), "Cannot yield from the RHS to a LHS def variable"
							+ " (" + lhsVar.getIdentNode() + ").");
					return false;
				}
			}
		} else {
			IdentExprNode identExpr = (IdentExprNode)lhsUnresolved;
			if(identExpr.yieldedTo) {
				error.error(getCoords(), "A yield assignment is only allowed to a def variable"
						+ " (" + lhsVar.getIdentNode() + " was declared without def).");
				return false;
			}

			if(lhsVar.directlyNestingLHSGraph == null && onLHS) {
				error.error(getCoords(), "An assignment to a global variable (" + lhsVar.getIdentNode() + ") is not allowed from a yield block.");
				return false;
			}
		}

		if((lhsVar.context & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {
			if(getCoords().comesBefore(lhsVar.getCoords())) {
				reportError("Variables (node,edge,var,ref) of computations must be declared before they can be assigned to"
						+ " (" + lhsVar.getIdentNode() + " was not yet declared).");
				return false;
			}
		}
		
		return true;
	}

	/**
	 * Checks whether the expression has a type equal, compatible or castable
	 * to the type of the target. Inserts implicit cast if compatible.
	 * @return true, if the types are equal or compatible, false otherwise
	 */
	private boolean typeCheckLocal()
	{
		TypeNode targetType = null;
		if(lhsQual != null) {
			if(!lhsQual.isMatchAssignment())
				targetType = lhsQual.getDecl().getDeclType();
			else
				targetType = lhsQual.getMember().getDeclType();
		}
		if(lhsVar != null)
			targetType = lhsVar.getDeclType();
		if(lhsGraphElement != null)
			targetType = lhsGraphElement.getDeclType();
		if(lhsMember != null)
			targetType = lhsMember.getDeclType();
		TypeNode exprType = rhs.getType();

		if(exprType.isEqual(targetType))
			return true;

		rhs = becomeParent(rhs.adjustType(targetType, getCoords()));
		if(rhs == ConstNode.getInvalid())
			return false;

		if(targetType instanceof NodeTypeNode && exprType instanceof NodeTypeNode
				|| targetType instanceof EdgeTypeNode && exprType instanceof EdgeTypeNode) {
			Collection<TypeNode> superTypes = new HashSet<TypeNode>();
			exprType.doGetCompatibleToTypes(superTypes);
			if(!superTypes.contains(targetType)) {
				error.error(getCoords(), "Cannot assign a value of type " + exprType + " to a variable of type " + targetType + ".");
				return false;
			}
		}
		if(targetType instanceof NodeTypeNode && exprType instanceof EdgeTypeNode
				|| targetType instanceof EdgeTypeNode && exprType instanceof NodeTypeNode) {
			error.error(getCoords(), "Cannot assign a value of type " + exprType + " to a variable of type " + targetType + ".");
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
		// optimize . = . away
		if(isIdenticalAssignment()) {
			return new AssignmentIdentical();
		}

		ExprNode rhsEvaluated = rhs.evaluate();
		if(lhsQual != null) {
			Qualification qual = lhsQual.checkIR(Qualification.class);
			if(qual.getOwner() instanceof Node && ((Node)qual.getOwner()).changesType(null)) {
				error.error(getCoords(), "An assignment to a node whose type will be changed is not allowed.");
			}
			if(qual.getOwner() instanceof Edge && ((Edge)qual.getOwner()).changesType(null)) {
				error.error(getCoords(), "An assignment to an edge whose type will be changed is not allowed.");
			}

			if(canSetOrMapAssignmentBeBrokenUpIntoStateChangingOperations()) {
				markSetOrMapAssignmentToBeBrokenUpIntoStateChangingOperations();
				return rhsEvaluated.checkIR(EvalStatement.class);
			}

			return new Assignment(qual, rhsEvaluated.checkIR(Expression.class));
		} else if(lhsVar != null) {
			Variable var = lhsVar.checkIR(Variable.class);

			// TODO: extend optimization to assignments to variables

			return new AssignmentVar(var, rhsEvaluated.checkIR(Expression.class));
		} else if(lhsGraphElement != null) {
			GraphEntity graphEntity = lhsGraphElement.checkIR(GraphEntity.class);

			// TODO: extend optimization to assignments to (pattern) graph entities

			return new AssignmentGraphEntity(graphEntity, rhsEvaluated.checkIR(Expression.class));
		} else {
			Entity entity = lhsMember.checkIR(Entity.class);

			// TODO: extend optimization to assignments to entities

			return new AssignmentMember(entity, rhsEvaluated.checkIR(Expression.class));
		}
	}

	private boolean canSetOrMapAssignmentBeBrokenUpIntoStateChangingOperations()
	{
		// TODO: extend optimization to rewrite to compound assignment statement if same lhs but non-constructor rhs

		// is it a set or map assignment ?
		if(lhsQual == null || lhsQual.getDecl() == null) { // don't look at match entities
			return false; // TODO: extend optimization to assignments to variables
		}
		QualIdentNode qual = lhsQual;
		if(!(qual.getDecl().type instanceof SetTypeNode) && !(qual.getDecl().type instanceof MapTypeNode)) {
			return false;
		}

		// descend and check if constraints are fulfilled which allow breakup
		ExprNode curLoc = rhs; // current location in the expression tree, more exactly: left-deep list
		while(curLoc != null) {
			if(curLoc instanceof ArithmeticOperatorNode) {
				ArithmeticOperatorNode operator = (ArithmeticOperatorNode)curLoc;
				if(!(operator.getOperatorDecl().getOperator() == OperatorDeclNode.Operator.BIT_OR)
						&& !(operator.getOperatorDecl().getOperator() == OperatorDeclNode.Operator.EXCEPT)) {
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
			} else if(curLoc instanceof MemberAccessExprNode) {
				// determine right owner and member, filter for needed types
				MemberAccessExprNode access = (MemberAccessExprNode)curLoc;
				if(!(access.getTarget() instanceof IdentExprNode))
					return false;
				IdentExprNode target = (IdentExprNode)access.getTarget();
				if(!(target.getResolvedNode() instanceof ConstraintDeclNode))
					return false;
				ConstraintDeclNode rightOwner = (ConstraintDeclNode)target.getResolvedNode();
				MemberDeclNode rightMember = access.getDecl();
				// determine left owner and member, filter for needed types
				MemberDeclNode leftMember = qual.getDecl();
				if(!(qual.getOwner() instanceof ConstraintDeclNode))
					return false;
				ConstraintDeclNode leftOwner = (ConstraintDeclNode)qual.getOwner();
				// check that the accessed set/map is the same on the left and the right hand side
				if(leftOwner != rightOwner)
					return false;
				if(leftMember != rightMember)
					return false;

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
		while(curLoc != null) {
			if(curLoc instanceof ArithmeticOperatorNode) {
				ArithmeticOperatorNode operator = (ArithmeticOperatorNode)curLoc;
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
		if(lhsQual != null) {
			if(rhs instanceof MemberAccessExprNode) {
				MemberAccessExprNode rhsQual = (MemberAccessExprNode)rhs;
				if(!(rhsQual.getTarget() instanceof IdentExprNode))
					return false;
				IdentExprNode target = (IdentExprNode)rhsQual.getTarget();
				if(lhsQual.getOwner()==target.decl.getDecl()
						&& lhsQual.getDecl()==rhsQual.getDecl())
					return true;
			}
		} else {
			if(rhs instanceof IdentExprNode) {
				IdentExprNode rhsVar = (IdentExprNode)rhs;
				if(lhsVar == rhsVar.decl.getDecl())
					return true;
			}
		}

		return false;
	}
}
