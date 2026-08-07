/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.graph
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using AssignmentNameof = de.unika.ipd.grgen.ir.stmt.graph.AssignmentNameof;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// AST node representing a name assignment.
/// </summary>
public class AssignNameofNode : EvalStatementNode
{
	static AssignNameofNode()
	{
		SetClassName(typeof(AssignNameofNode), "Assign name");
	}

	internal ExprNode lhs;
	internal ExprNode rhs;

	internal int context;

	/// <param name="coords"> The source code coordinates of = operator. </param>
	/// <param name="target"> The left hand side. </param>
	/// <param name="expr"> The expression, that is assigned. </param>
	public AssignNameofNode(Coords coords, ExprNode target, ExprNode expr, int context)
		: base(coords)
	{
		this.lhs = target;
		BecomeParent(this.lhs);
		this.rhs = expr;
		BecomeParent(this.rhs);
		this.context = context;
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		if(lhs != null)
			children.Add(lhs);
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
		if(lhs != null)
			childrenNames.Add("lhs");
		childrenNames.Add("rhs");
		return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		return true;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION)
		{
			ReportError("The nameof() assignment is not allowed in function or pattern part context.");
			return false;
		}

		TypeNode rhsType = rhs.Type;
		if(rhsType != BasicTypeNode.stringType)
		{
			ReportError("The nameof() assignment expects as name to be assigned"
					+ " a value of type string"
					+ " (but is given a value of type " + rhsType.ToStringWithDeclarationCoords() + ").");
			return false;
		}

		if(lhs != null)
		{
			TypeNode lhsType = lhs.Type;
			if(lhsType.IsEqual(BasicTypeNode.graphType))
				return true;
			if(lhsType is EdgeTypeNode)
				return true;
			if(lhsType is NodeTypeNode)
				return true;

			ReportError("The nameof() assignment expects as entity to assign to its name"
					+ " a value of type Node or Edge or graph"
					+ " (but is given a value of type " + lhsType.ToStringWithDeclarationCoords() + ").");
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
		Expression lhsExpr = null;
		if(lhs != null)
		{
			lhs = lhs.Evaluate();
			lhsExpr = lhs.CheckIR(typeof(Expression));
		}
		rhs = rhs.Evaluate();
		return new AssignmentNameof(lhsExpr, rhs.CheckIR(typeof(Expression)));
	}
}

}
