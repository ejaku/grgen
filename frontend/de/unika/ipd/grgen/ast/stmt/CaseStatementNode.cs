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
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using CaseStatement = de.unika.ipd.grgen.ir.stmt.CaseStatement;
using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// AST node representing a case statement from a switch statement.
/// </summary>
public class CaseStatementNode : NestingStatementNode
{
	static CaseStatementNode()
	{
		SetClassName(typeof(CaseStatementNode), "CaseStatement");
	}

	internal ExprNode caseConstantExpr; // null for the "else" (aka default) case

	public CaseStatementNode(Coords coords, ExprNode caseConstExpr,
			CollectNode<EvalStatementNode> statements)
		: base(coords, statements)
	{
		this.caseConstantExpr = caseConstExpr;
		BecomeParent(caseConstExpr);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		if(caseConstantExpr != null)
			children.Add(caseConstantExpr);
		children.Add(statements);
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
		if(caseConstantExpr != null)
			childrenNames.Add("caseConstant");
		childrenNames.Add("statements");
		return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		return true;
	}

	protected internal override bool CheckLocal()
	{
		return true;
	}

	public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	protected internal override IR ConstructIR()
	{
		if(caseConstantExpr != null)
			caseConstantExpr = caseConstantExpr.Evaluate();
		CaseStatement caseStmt = new CaseStatement(
				caseConstantExpr != null ? caseConstantExpr.CheckIR(typeof(Expression)) : null);
		foreach(EvalStatementNode statement in statements.ChildrenExact)
			caseStmt.AddStatement(statement.CheckIR(typeof(EvalStatement)));
		return caseStmt;
	}
}

}
