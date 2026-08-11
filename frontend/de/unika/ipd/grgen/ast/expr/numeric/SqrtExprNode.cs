/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.numeric
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using SqrtExpr = de.unika.ipd.grgen.ir.expr.numeric.SqrtExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class SqrtExprNode : BuiltinFunctionInvocationBaseNode
{
	static SqrtExprNode()
	{
		SetClassName(typeof(SqrtExprNode), "sqrt expr");
	}

	private ExprNode expr;

	public SqrtExprNode(Coords coords, ExprNode expr)
		: base(coords)
	{

		this.expr = BecomeParent(expr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(expr);
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("expr");
		return childrenNames;
		}
	}

	protected internal override bool CheckLocal()
	{
		if(expr.Type.IsEqual(BasicTypeNode.doubleType))
			return true;
		ReportError("The function Math::sqrt() expects as argument a value of type double"
				+ " (but is given a value of type " + expr.Type.TypeName + ").");
		return false;
	}

	protected internal override IR ConstructIR()
	{
		expr = expr.Evaluate();
		return new SqrtExpr(expr.CheckIR(typeof(Expression)));
	}

	public override TypeNode Type
	{
		get
		{
		return BasicTypeNode.doubleType;
		}
	}
}

}
