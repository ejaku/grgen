/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ast.stmt
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using ConstraintDeclNode = de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using IdentExprNode = de.unika.ipd.grgen.ast.expr.IdentExprNode;
using QualIdentNode = de.unika.ipd.grgen.ast.expr.QualIdentNode;
using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using IntTypeNode = de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
using DequeTypeNode = de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
using MapTypeNode = de.unika.ipd.grgen.ast.type.container.MapTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using AssignmentIndexed = de.unika.ipd.grgen.ir.stmt.AssignmentIndexed;
using AssignmentVarIndexed = de.unika.ipd.grgen.ir.stmt.AssignmentVarIndexed;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
using Node = de.unika.ipd.grgen.ir.pattern.Node;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// AST node representing an indexed assignment.
/// </summary>
public class AssignIndexedNode : EvalStatementNode
{
	static AssignIndexedNode()
	{
		SetClassName(typeof(AssignIndexedNode), "Assign indexed");
	}

	internal BaseNode lhsUnresolved;
	internal ExprNode rhs;
	internal ExprNode index;
	internal bool onLHS;

	internal QualIdentNode lhsQual;
	internal VarDeclNode lhsVar;

	internal int context;

	/// <param name="coords"> The source code coordinates of = operator. </param>
	/// <param name="target"> The left hand side. </param>
	/// <param name="expr"> The expression, that is assigned. </param>
	/// <param name="index"> The index expression to the lhs entity. </param>
	public AssignIndexedNode(Coords coords, QualIdentNode target,
			ExprNode expr, ExprNode index, int context)
		: base(coords)
	{
		this.lhsUnresolved = target;
		BecomeParent(this.lhsUnresolved);
		this.rhs = expr;
		BecomeParent(this.rhs);
		this.index = index;
		BecomeParent(this.index);
		this.context = context;
		this.onLHS = false;
	}

	/// <param name="coords"> The source code coordinates of = operator. </param>
	/// <param name="target"> The left hand side. </param>
	/// <param name="expr"> The expression, that is assigned. </param>
	/// <param name="index"> The index expression to the lhs entity. </param>
	public AssignIndexedNode(Coords coords, IdentExprNode target,
			ExprNode expr, ExprNode index, int context, bool onLHS)
		: base(coords)
	{
		this.lhsUnresolved = target;
		BecomeParent(this.lhsUnresolved);
		this.rhs = expr;
		BecomeParent(this.rhs);
		this.index = index;
		BecomeParent(this.index);
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
			children.Add(GetValidVersion(lhsUnresolved, lhsQual, lhsVar));
			children.Add(rhs);
			children.Add(index);
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
			childrenNames.Add("index");
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
				else
				{
					ReportError("Error in resolving the variable on the left hand side of the indexed assignment (given is " + unresolved.Ident + ").");
					successfullyResolved = false;
				}
			}
			else
			{
				ReportError("Error in resolving the variable on the left hand side of the indexed assignment (given is " + unresolved.Ident + ").");
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
				ReportError("Error in resolving the qualified attribute on the left hand side of the indexed assignment (given is " + unresolved + ").");
				successfullyResolved = false;
			}
		}
		else
		{
			ReportError("Internal error - invalid left hand side in indexed assignment.");
			successfullyResolved = false;
		}
		return successfullyResolved;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		if(lhsQual != null)
		{
			if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION)
			{
				ReportError("An indexed assignment to an attribute of a graph element is not allowed in function or pattern part context.");
				return false;
			}

			DeclNode owner = lhsQual.Owner;
			TypeNode ty = owner.DeclType;

			if(lhsQual.MemberDecl.IsConst())
			{
				ReportError("An indexed assignment to a const member is not allowed (" + lhsQual.Decl.Ident + lhsQual.Decl.DeclarationCoords + " is constant).");
				return false;
			}

			if(ty is InheritanceTypeNode)
			{
				InheritanceTypeNode inhTy = (InheritanceTypeNode)ty;

				if(inhTy.IsConst())
				{
					ReportError("An indexed assignment to a const type object is not allowed (" + inhTy.ToStringWithDeclarationCoords() + " is constant).");
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
						ReportError("Variables (node,edge,var,ref) of computations must be declared before they can be assigned to (with index)"
								+ " (" + entity.Ident + " was not yet declared).");
						return false;
					}
				}
			}
		}
		else
		{
			if((lhsVar.context & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION)
			{
				if(Coords.ComesBefore(lhsVar.Coords))
				{
					ReportError("Variables (node,edge,var,ref) of computations must be declared before they can be assigned to (with index)"
							+ " (" + lhsVar.Ident + " was not yet declared).");
					return false;
				}
			}

			if(lhsVar.directlyNestingLHSGraph == null && onLHS)
			{
				ReportError("An indexed assignment to a global variable (" + lhsVar.Ident + ") is not allowed from a yield block.");
				return false;
			}
		}

		return TypeCheckLocal();
	}

	/// <summary>
	/// Checks whether the expression has a type equal, compatible or castable
	/// to the type of the target. Inserts implicit cast if compatible. </summary>
	/// <returns> true, if the types are equal or compatible, false otherwise </returns>
	private bool TypeCheckLocal()
	{
		TypeNode targetType = null;
		if(lhsQual != null)
			targetType = lhsQual.Decl.DeclType;
		if(lhsVar != null)
			targetType = lhsVar.DeclType;

		bool valueOk = CheckValueType(targetType);
		bool indexOk = CheckIndexType(targetType);

		return valueOk && indexOk;
	}

	private bool CheckValueType(TypeNode targetType)
	{
		TypeNode valueType;
		if(targetType is ArrayTypeNode)
			valueType = ((ArrayTypeNode)targetType).valueType;
		else if(targetType is DequeTypeNode)
			valueType = ((DequeTypeNode)targetType).valueType;
		else if(targetType is MapTypeNode)
			valueType = ((MapTypeNode)targetType).valueType;
		else
		{
			targetType.ReportError("Can only carry out an indexed assignment on an attribute/variable of array/deque/map type"
					+ " (given is type " + targetType.TypeName + ").");
			return false;
		}

		TypeNode exprType = rhs.Type;
		if(exprType.IsEqual(valueType))
			return true;

		rhs = BecomeParent(rhs.AdjustType(valueType, Coords));
		return rhs != ConstNode.Invalid;
	}

	private bool CheckIndexType(TypeNode targetType)
	{
		TypeNode keyType;
		if(targetType is MapTypeNode)
			keyType = ((MapTypeNode)targetType).keyType;
		else
			keyType = IntTypeNode.intType;
		TypeNode keyExprType = index.Type;

		if(keyExprType is InheritanceTypeNode)
		{
			if(keyExprType.IsCompatibleTo(keyType))
				return true;

			ReportError("Cannot convert index in assignment"
					+ " from " + keyExprType.ToStringWithDeclarationCoords()
					+ " to the expected " + keyType.ToStringWithDeclarationCoords() + ".");
			return false;
		}
		else
		{
			if(keyExprType.IsEqual(keyType))
				return true;

			index = BecomeParent(index.AdjustType(keyType, Coords));
			return index != ConstNode.Invalid;
		}
	}

	public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	/// <summary>
	/// Construct the immediate representation from an indexed assignment node. </summary>
	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
	protected internal override IR ConstructIR()
	{
		if(lhsQual != null)
		{
			Qualification qual = lhsQual.CheckIR(typeof(Qualification));
			if(qual.Owner is Node && ((Node)qual.Owner).ChangesType(null))
				ReportError("An assignment to a node whose type will be changed is not allowed.");
			if(qual.Owner is Edge && ((Edge)qual.Owner).ChangesType(null))
				ReportError("An assignment to an edge whose type will be changed is not allowed.");

			ExprNode rhsEvaluated = rhs.Evaluate();
			ExprNode indexEvaluated = index.Evaluate();
			return new AssignmentIndexed(qual, rhsEvaluated.CheckIR(typeof(Expression)),
					indexEvaluated.CheckIR(typeof(Expression)));
		}
		else
		{
			Variable var = lhsVar.CheckIR(typeof(Variable));

			ExprNode rhsEvaluated = rhs.Evaluate();
			ExprNode indexEvaluated = index.Evaluate();
			return new AssignmentVarIndexed(var, rhsEvaluated.CheckIR(typeof(Expression)),
					indexEvaluated.CheckIR(typeof(Expression)));
		}
	}
}

}
