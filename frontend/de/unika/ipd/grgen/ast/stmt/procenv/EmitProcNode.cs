/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.procenv
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using BuiltinProcedureInvocationBaseNode = de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using EmitProc = de.unika.ipd.grgen.ir.stmt.procenv.EmitProc;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class EmitProcNode : BuiltinProcedureInvocationBaseNode
{
	static EmitProcNode()
	{
		SetClassName(typeof(EmitProcNode), "emit procedure");
	}

	private CollectNode<ExprNode> exprs = new CollectNode<ExprNode>();
	internal bool isDebug;

	public EmitProcNode(Coords coords, bool isDebug)
		: base(coords)
	{

		this.exprs = BecomeParent(exprs);
		this.isDebug = isDebug;
	}

	public virtual void AddExpression(ExprNode expr)
	{
		exprs.AddChild(expr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(exprs);
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("exprs");
		return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		return true;
	}

	protected internal override bool CheckLocal()
	{
		// any type goes, must be converted toString in implementation
		return true;
	}

	public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	protected internal override IR ConstructIR()
	{
		IList<Expression> expressions = new List<Expression>();
		foreach(ExprNode expr in exprs.ChildrenExact)
		{
			ExprNode exprEvaluated = expr.Evaluate();
			expressions.Add(exprEvaluated.CheckIR(typeof(Expression)));
		}
		return new EmitProc(expressions, isDebug);
	}
}

}
