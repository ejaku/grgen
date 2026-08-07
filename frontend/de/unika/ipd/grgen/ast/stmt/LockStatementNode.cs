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
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
using LockStatement = de.unika.ipd.grgen.ir.stmt.LockStatement;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// AST node representing a lock statement.
/// </summary>
public class LockStatementNode : NestingStatementNode
{
	static LockStatementNode()
	{
		SetClassName(typeof(LockStatementNode), "LockStatement");
	}

	private ExprNode lockObjectExpr;

	public LockStatementNode(Coords coords, ExprNode lockObjectExpr, CollectNode<EvalStatementNode> lockedStatements)
		: base(coords, lockedStatements)
	{
		this.lockObjectExpr = lockObjectExpr;
		BecomeParent(lockObjectExpr);
		this.statements = lockedStatements;
		BecomeParent(this.statements);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(lockObjectExpr);
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
		childrenNames.Add("lockObject");
		childrenNames.Add("lockedStatements");
		return childrenNames;
		}
	}

	protected internal override bool CheckLocal()
	{
		TypeNode lockObjectExprType = lockObjectExpr.Type;
		if(!lockObjectExprType.IsLockableType())
		{
			lockObjectExpr.ReportError("The lock statement expects as lock object a value that is not of basic type (with exception of type object)"
					+ " (but is given a value of type " + lockObjectExprType.ToStringWithDeclarationCoords() + ").");
			return false;
		}
		return true;
	}

	protected internal override bool ResolveLocal()
	{
		return true;
	}

	public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	protected internal override IR ConstructIR()
	{
		lockObjectExpr = lockObjectExpr.Evaluate();
		LockStatement ls = new LockStatement(lockObjectExpr.CheckIR(typeof(Expression)));
		foreach(EvalStatementNode lockedStatement in statements.ChildrenExact)
			ls.AddStatement(lockedStatement.CheckIR(typeof(EvalStatement)));
		return ls;
	}
}

}
