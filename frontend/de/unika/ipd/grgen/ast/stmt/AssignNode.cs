/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack, Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ast.stmt
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
	using ConstraintDeclNode = de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using ArithmeticOperatorNode = de.unika.ipd.grgen.ast.expr.ArithmeticOperatorNode;
	using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using IdentExprNode = de.unika.ipd.grgen.ast.expr.IdentExprNode;
	using MemberAccessExprNode = de.unika.ipd.grgen.ast.expr.MemberAccessExprNode;
	using QualIdentNode = de.unika.ipd.grgen.ast.expr.QualIdentNode;
	using MapInitNode = de.unika.ipd.grgen.ast.expr.map.MapInitNode;
	using SetInitNode = de.unika.ipd.grgen.ast.expr.set.SetInitNode;
	using MemberDeclNode = de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using MapTypeNode = de.unika.ipd.grgen.ast.type.container.MapTypeNode;
	using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Assignment = de.unika.ipd.grgen.ir.stmt.Assignment;
	using AssignmentGraphEntity = de.unika.ipd.grgen.ir.stmt.AssignmentGraphEntity;
	using AssignmentIdentical = de.unika.ipd.grgen.ir.stmt.AssignmentIdentical;
	using AssignmentMember = de.unika.ipd.grgen.ir.stmt.AssignmentMember;
	using AssignmentVar = de.unika.ipd.grgen.ir.stmt.AssignmentVar;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// AST node representing an assignment.
	/// </summary>
	public class AssignNode : EvalStatementNode
	{
		static AssignNode()
		{
			SetClassName(typeof(AssignNode), "Assign");
		}

		internal BaseNode lhsUnresolved;
		internal ExprNode rhs;
		internal int context;
		internal bool onLHS;

		internal QualIdentNode lhsQual;
		internal VarDeclNode lhsVar;
		internal ConstraintDeclNode lhsGraphElement;
		internal MemberDeclNode lhsMember;

		/// <param name="coords"> The source code coordinates of = operator. </param>
		/// <param name="target"> The left hand side. </param>
		/// <param name="expr"> The expression, that is assigned. </param>
		public AssignNode(Coords coords, QualIdentNode target, ExprNode expr, int context)
			: base(coords)
		{
			this.lhsUnresolved = target;
			BecomeParent(this.lhsUnresolved);
			this.rhs = expr;
			BecomeParent(this.rhs);
			this.context = context;
			this.onLHS = false;
		}

		/// <param name="coords"> The source code coordinates of = operator. </param>
		/// <param name="target"> The left hand side. </param>
		/// <param name="expr"> The expression, that is assigned. </param>
		public AssignNode(Coords coords, IdentExprNode target, ExprNode expr, int context, bool onLHS)
			: base(coords)
		{
			this.lhsUnresolved = target;
			BecomeParent(this.lhsUnresolved);
			this.rhs = expr;
			BecomeParent(this.rhs);
			this.context = context;
			this.onLHS = onLHS;
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(lhsUnresolved, lhsQual, lhsVar, lhsGraphElement));
				children.Add(rhs);
				return children;
			}
		}

		/// <summary>
		/// returns names of the children, same order as in getChildren </summary>
		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("lhs");
				childrenNames.Add("rhs");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool successfullyResolved = true;
			if(lhsUnresolved is IdentExprNode)
			{
				IdentExprNode unresolved = (IdentExprNode)lhsUnresolved;
				if(unresolved.Resolve())
				{
					if(unresolved.decl is VarDeclNode)
						lhsVar = (VarDeclNode)unresolved.decl;
					else if(unresolved.decl is ConstraintDeclNode)
						lhsGraphElement = (ConstraintDeclNode)unresolved.decl;
					else if(unresolved.decl is MemberDeclNode)
					{
						//lhsMember = (MemberDeclNode)unresolved.decl;
						ReportError("Error in resolving the left hand side of the assignment (given is " + unresolved.Ident + ")"
								+ " (use this." + unresolved.Ident + " to access a class member inside a method).");
						successfullyResolved = false;
					}
					else
					{
						ReportError("Error in resolving the left hand side of the assignment, a variable or graph element is expected (given is " + unresolved.Ident + ").");
						successfullyResolved = false;
					}
				}
				else
				{
					ReportError("Error in resolving the left hand side of the assignment (given is " + unresolved.Ident + ").");
					successfullyResolved = false;
				}
			}
			else if(lhsUnresolved is QualIdentNode)
			{
				QualIdentNode unresolved = (QualIdentNode)lhsUnresolved;
				if(unresolved.Resolve())
					lhsQual = unresolved;
				else
				{
					ReportError("Error in resolving the qualified attribute on the left hand side of the assignment (given is " + unresolved + ").");
					successfullyResolved = false;
				}
			}
			else
			{
				ReportError("Internal error - invalid left hand side in assignment.");
				successfullyResolved = false;
			}
			return successfullyResolved;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			if(lhsQual != null)
			{
				if(!CheckLhsQual())
					return false;
			}
			else if(lhsGraphElement != null)
			{
				if(!CheckLhsGraphElement())
					return false;
			}
			else if(lhsVar != null)
			{
				if(!CheckLhsVar())
					return false;
			}

			return TypeCheckLocal();
		}

		private bool CheckLhsQual()
		{
			if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION
					&& !lhsQual.IsMatchAssignment()
					&& !lhsQual.IsTransientObjectAssignment())
			{
				ReportError("An assignment to an attribute of a graph element or internal class object"
						+ " is not allowed in function or pattern part context"
						+ " (but occurrs with " + lhsQual + ").");
				return false;
			}

			DeclNode owner = lhsQual.Owner;
			TypeNode ty = owner.DeclType;

			MemberDeclNode member = lhsQual.MemberDecl; // null for match type
			if(member != null && member.IsConst())
			{
				ReportError("An assignment to a const member is not allowed (" + lhsQual.Decl.Ident + lhsQual.Decl.DeclarationCoords + " is constant).");
				return false;
			}

			if(ty is InheritanceTypeNode)
			{
				InheritanceTypeNode inhTy = (InheritanceTypeNode)ty;

				if(inhTy.IsConst())
				{
					ReportError("An assignment to a const type object is not allowed (" + inhTy.ToStringWithDeclarationCoords() + " is constant).");
					return false;
				}
			}

			if(owner is ConstraintDeclNode)
			{
				ConstraintDeclNode entity = (ConstraintDeclNode)owner;
				if((entity.context & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION)
				{
					if(Coords.ComesBefore(entity.Coords))
					{
						ReportError("Variables (node,edge,var,ref) of computations must be declared before they can be assigned to"
								+ " (" + entity.Ident + " was not yet declared).");
						return false;
					}
				}
			}

			return true;
		}

		private bool CheckLhsGraphElement()
		{
			if(lhsGraphElement.defEntityToBeYieldedTo)
			{
				IdentExprNode identExpr = (IdentExprNode)lhsUnresolved;
				if((lhsGraphElement.context & CONTEXT_COMPUTATION) != CONTEXT_COMPUTATION)
				{
					if(!identExpr.yieldedTo)
					{
						ReportError("Only a yield assignment is allowed to a def pattern graph element"
								+ " (" + lhsGraphElement.Ident + " was declared with def)"
								+ " (the typical solution is to prepend a yield to the assignment).");
						return false;
					}
				}
				else
				{
					if(identExpr.yieldedTo)
					{
						ReportError("Only a non-yield assignment is allowed to a computation local def pattern graph element"
								+ " (" + lhsGraphElement.Ident + " was declared with def in computation context)"
								+ " (the typical solution is to remove the yield from the assignment).");
						return false;
					}
				}

				if((lhsGraphElement.context & CONTEXT_COMPUTATION) != CONTEXT_COMPUTATION)
				{
					if((lhsGraphElement.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS
							&& (context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS)
					{
						ReportError("Cannot yield from the right hand side to a left hand side def pattern graph element"
								+ " (" + lhsGraphElement.Ident + " was declared in the pattern part).");
						return false;
					}
				}
			}
			else
			{
				if(lhsGraphElement.directlyNestingLHSGraph != null)
				{
					IdentExprNode identExpr = (IdentExprNode)lhsUnresolved;
					if(identExpr.yieldedTo)
					{
						ReportError("A yield assignment is only allowed to a def pattern graph element"
								+ " (" + lhsGraphElement.Ident + " was declared without def).");
						return false;
					}

					ReportError("Only a def pattern graph element can be assigned to"
							+ " (" + lhsGraphElement.Ident + " was declared without def).");
					return false;
				}

				if(lhsGraphElement.directlyNestingLHSGraph == null && onLHS)
				{
					ReportError("An assignment to a global variable (" + lhsGraphElement.ToStringWithDeclarationCoords() + ") is not allowed from a yield block.");
					return false;
				}
			}

			if((lhsGraphElement.context & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION)
			{
				if(Coords.ComesBefore(lhsGraphElement.Coords))
				{
					ReportError("Variables (node,edge,var,ref) of computations must be declared before they can be assigned to"
							+ " (" + lhsGraphElement.Ident + " was not yet declared).");
					return false;
				}
			}

			return true;
		}

		private bool CheckLhsVar()
		{
			if(lhsVar.defEntityToBeYieldedTo)
			{
				IdentExprNode identExpr = (IdentExprNode)lhsUnresolved;
				if((lhsVar.context & CONTEXT_COMPUTATION) != CONTEXT_COMPUTATION)
				{
					if(!identExpr.yieldedTo)
					{
						ReportError("Only a yield assignment is allowed to a def variable"
								+ " (" + lhsVar.Ident + " was declared with def)"
								+ " (the typical solution is to prepend a yield to the assignment).");
						return false;
					}
				}
				else
				{
					if(identExpr.yieldedTo)
					{
						ReportError("Only a non-yield assignment is allowed to a computation local def variable"
								+ " (" + lhsVar.Ident + " was declared with def in computation context)"
								+ " (the typical solution is to remove the yield from the assignment).");
						return false;
					}
				}

				if((lhsVar.context & CONTEXT_COMPUTATION) != CONTEXT_COMPUTATION)
				{
					if((lhsVar.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS
							&& (context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS)
					{
						ReportError("Cannot yield from the right hand side to a left hand side def variable"
								+ " (" + lhsVar.Ident + " was declared in the pattern part).");
						return false;
					}
				}
			}
			else
			{
				IdentExprNode identExpr = (IdentExprNode)lhsUnresolved;
				if(identExpr.yieldedTo)
				{
					ReportError("A yield assignment is only allowed to a def variable"
							+ " (" + lhsVar.Ident + " was declared without def).");
					return false;
				}

				if(lhsVar.directlyNestingLHSGraph == null && onLHS)
				{
					ReportError("An assignment to a global variable (" + lhsVar.ToStringWithDeclarationCoords() + ") is not allowed from a yield block.");
					return false;
				}
			}

			if((lhsVar.context & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION)
			{
				if(Coords.ComesBefore(lhsVar.Coords))
				{
					ReportError("Variables (node,edge,var,ref) of computations must be declared before they can be assigned to"
							+ " (" + lhsVar.Ident + " was not yet declared).");
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Checks whether the expression has a type equal, compatible or castable
		/// to the type of the target. Inserts implicit cast if compatible. </summary>
		/// <returns> true, if the types are equal or compatible, false otherwise </returns>
		private bool TypeCheckLocal()
		{
			TypeNode targetType = null;
			if(lhsQual != null)
			{
				if(!lhsQual.IsMatchAssignment())
					targetType = lhsQual.Decl.DeclType;
				else
					targetType = lhsQual.Member.DeclType;
			}
			if(lhsVar != null)
				targetType = lhsVar.DeclType;
			if(lhsGraphElement != null)
				targetType = lhsGraphElement.DeclType;
			if(lhsMember != null)
				targetType = lhsMember.DeclType;
			TypeNode exprType = rhs.Type;

			if(exprType.IsEqual(targetType))
				return true;

			rhs = BecomeParent(rhs.AdjustType(targetType, Coords));
			if(rhs == ConstNode.Invalid)
				return false;

			if(targetType is NodeTypeNode && exprType is NodeTypeNode
					|| targetType is EdgeTypeNode && exprType is EdgeTypeNode)
			{
				ICollection<TypeNode> superTypes = new HashSet<TypeNode>();
				exprType.DoGetCompatibleToTypes(superTypes);
				if(!superTypes.Contains(targetType))
				{
					ReportError("Cannot assign a value of type " + exprType.ToStringWithDeclarationCoords()
							+ " to a variable of type " + targetType.ToStringWithDeclarationCoords() + ".");
					return false;
				}
			}
			if(targetType is NodeTypeNode && exprType is EdgeTypeNode
					|| targetType is EdgeTypeNode && exprType is NodeTypeNode)
			{
				ReportError("Cannot assign a value of type " + exprType.ToStringWithDeclarationCoords()
						+ " to a variable of type " + targetType.ToStringWithDeclarationCoords() + ".");
				return false;
			}
			return true;
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			return true;
		}

		/// <summary>
		/// Construct the immediate representation from an assignment node. </summary>
		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
		protected internal override IR ConstructIR()
		{
			// optimize . = . away
			if(IsIdenticalAssignment())
				return new AssignmentIdentical();

			ExprNode rhsEvaluated = rhs.Evaluate();
			if(lhsQual != null)
			{
				Qualification qual = lhsQual.CheckIR<Qualification>(typeof(Qualification));
				if(qual.Owner is Node && ((Node)qual.Owner).ChangesType(null))
					ReportError("An assignment to a node whose type will be changed is not allowed (but occurs for " + lhsQual + ").");
				if(qual.Owner is Edge && ((Edge)qual.Owner).ChangesType(null))
					ReportError("An assignment to an edge whose type will be changed is not allowed (but occurs for " + lhsQual + ").");

				if(CanSetOrMapAssignmentBeBrokenUpIntoStateChangingOperations())
				{
					MarkSetOrMapAssignmentToBeBrokenUpIntoStateChangingOperations();
					return rhsEvaluated.CheckIR<EvalStatement>(typeof(EvalStatement));
				}

				return new Assignment(qual, rhsEvaluated.CheckIR<Expression>(typeof(Expression)));
			}
			else if(lhsVar != null)
			{
				Variable var = lhsVar.CheckIR<Variable>(typeof(Variable));

				// TODO: extend optimization to assignments to variables

				return new AssignmentVar(var, rhsEvaluated.CheckIR<Expression>(typeof(Expression)));
			}
			else if(lhsGraphElement != null)
			{
				GraphEntity graphEntity = lhsGraphElement.CheckIR<GraphEntity>(typeof(GraphEntity));

				// TODO: extend optimization to assignments to (pattern) graph entities

				return new AssignmentGraphEntity(graphEntity, rhsEvaluated.CheckIR<Expression>(typeof(Expression)));
			}
			else
			{
				Entity entity = lhsMember.CheckIR<Entity>(typeof(Entity));

				// TODO: extend optimization to assignments to entities

				return new AssignmentMember(entity, rhsEvaluated.CheckIR<Expression>(typeof(Expression)));
			}
		}

		private bool CanSetOrMapAssignmentBeBrokenUpIntoStateChangingOperations()
		{
			// TODO: extend optimization to rewrite to compound assignment statement if same lhs but non-constructor rhs

			// is it a set or map assignment ?
			if(lhsQual == null || lhsQual.Decl == null) // don't look at match entities
				return false; // TODO: extend optimization to assignments to variables
			QualIdentNode qual = lhsQual;
			if(!(qual.MemberDecl.type is SetTypeNode) && !(qual.MemberDecl.type is MapTypeNode))
				return false;

			// descend and check if constraints are fulfilled which allow breakup
			ExprNode curLoc = rhs; // current location in the expression tree, more exactly: left-deep list
			while(curLoc != null)
			{
				if(curLoc is ArithmeticOperatorNode)
				{
					ArithmeticOperatorNode @operator = (ArithmeticOperatorNode)curLoc;
					if(!(@operator.OperatorDecl.Operator == Operator.BIT_OR)
							&& !(@operator.OperatorDecl.Operator == Operator.EXCEPT))
						return false;
					ICollection<ExprNode> children = @operator.ChildrenExact;
					IEnumerator<ExprNode> it = children.GetEnumerator();
	// JAVA TO C# CONVERTER TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
					ExprNode left = it.Next();
	// JAVA TO C# CONVERTER TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
					ExprNode right = it.Next();
					if(!(right is SetInitNode) && !(right is MapInitNode))
						return false;

					curLoc = left;
				}
				else if(curLoc is MemberAccessExprNode)
				{
					// determine right owner and member, filter for needed types
					MemberAccessExprNode access = (MemberAccessExprNode)curLoc;
					if(!(access.Target is IdentExprNode))
						return false;
					IdentExprNode target = (IdentExprNode)access.Target;
					if(!(target.ResolvedNode is ConstraintDeclNode))
						return false;
					ConstraintDeclNode rightOwner = (ConstraintDeclNode)target.ResolvedNode;
					MemberDeclNode rightMember = access.Decl;
					// determine left owner and member, filter for needed types
					MemberDeclNode leftMember = qual.MemberDecl;
					if(!(qual.Owner is ConstraintDeclNode))
						return false;
					ConstraintDeclNode leftOwner = (ConstraintDeclNode)qual.Owner;
					// check that the accessed set/map is the same on the left and the right hand side
					if(leftOwner != rightOwner)
						return false;
					if(leftMember != rightMember)
						return false;

					curLoc = null;
				}
				else
					return false;
			}

			return true;
		}

		private void MarkSetOrMapAssignmentToBeBrokenUpIntoStateChangingOperations()
		{
			ExprNode curLoc = rhs;
			while(curLoc != null)
			{
				if(curLoc is ArithmeticOperatorNode)
				{
					ArithmeticOperatorNode @operator = (ArithmeticOperatorNode)curLoc;
					@operator.MarkToBreakUpIntoStateChangingOperations(lhsQual);
					ExprNode left = EnumeratorHelper.GetFirstElement(@operator.ChildrenExact);
					curLoc = left;
				}
				else
					curLoc = null;
			}
		}

		private bool IsIdenticalAssignment()
		{
			if(lhsQual != null)
			{
				if(rhs is MemberAccessExprNode)
				{
					MemberAccessExprNode rhsQual = (MemberAccessExprNode)rhs;
					if(!(rhsQual.Target is IdentExprNode))
						return false;
					IdentExprNode target = (IdentExprNode)rhsQual.Target;
					if(lhsQual.Owner == target.decl.Decl
							&& lhsQual.Decl == rhsQual.Decl)
						return true;
				}
			}
			else
			{
				if(rhs is IdentExprNode)
				{
					IdentExprNode rhsVar = (IdentExprNode)rhs;
					if(lhsVar == rhsVar.decl.Decl)
						return true;
				}
			}

			return false;
		}
	}

}
