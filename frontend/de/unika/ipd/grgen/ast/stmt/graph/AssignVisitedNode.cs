/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.graph
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using VisitedNode = de.unika.ipd.grgen.ast.expr.graph.VisitedNode;
using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Visited = de.unika.ipd.grgen.ir.expr.graph.Visited;
using AssignmentVisited = de.unika.ipd.grgen.ir.stmt.graph.AssignmentVisited;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// AST node representing an assignment to a visited flag.
/// </summary>
public class AssignVisitedNode : EvalStatementNode
{
	static AssignVisitedNode()
	{
		SetClassName(typeof(AssignVisitedNode), "Assign visited");
	}

	internal VisitedNode lhs;
	internal ExprNode rhs;

	internal int context;

	/// <param name="coords"> The source code coordinates of = operator. </param>
	/// <param name="target"> The left hand side. </param>
	/// <param name="expr"> The expression, that is assigned. </param>
	public AssignVisitedNode(Coords coords, VisitedNode target, ExprNode expr, int context)
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
			ReportError("The visited[] assignment is not allowed in function or pattern part context.");
			return false;
		}

		TypeNode rhsType = rhs.Type;
		if(rhsType != BasicTypeNode.booleanType)
		{
			ReportError("The visited[] assignment expects as value to be assigned"
					+ " a value of type boolean"
					+ " (but is given a value of type " + rhsType.ToStringWithDeclarationCoords() + ").");
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
		Visited vis = lhs.CheckIR(typeof(Visited));
		ExprNode rhsEvaluated = rhs.Evaluate();
		return new AssignmentVisited(vis, rhsEvaluated.CheckIR(typeof(Expression)));
	}
}

}
