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
using LogExpr = de.unika.ipd.grgen.ir.expr.numeric.LogExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class LogExprNode : BuiltinFunctionInvocationBaseNode
{
	static LogExprNode()
	{
		SetClassName(typeof(LogExprNode), "log expr");
	}

	private ExprNode leftExpr;
	private ExprNode rightExpr;

	public LogExprNode(Coords coords, ExprNode leftExpr, ExprNode rightExpr)
		: base(coords)
	{

		this.leftExpr = BecomeParent(leftExpr);
		this.rightExpr = BecomeParent(rightExpr);
	}

	public LogExprNode(Coords coords, ExprNode leftExpr)
		: base(coords)
	{

		this.leftExpr = BecomeParent(leftExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(leftExpr);
			if(rightExpr != null)
				children.Add(rightExpr);
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("left");
			if(rightExpr != null)
				childrenNames.Add("right");
			return childrenNames;
		}
	}

	protected internal override bool CheckLocal()
	{
		if(!leftExpr.Type.IsEqual(BasicTypeNode.doubleType))
		{
			ReportError("The function Math::log() expects as 1. argument a value of type double"
					+ " (but is given a value of type " + leftExpr.Type.TypeName + ").");
			return false;
		}
		if(rightExpr != null && !rightExpr.Type.IsEqual(BasicTypeNode.doubleType))
		{
			ReportError("The function Math::log() expects as 2. argument a value of type double"
					+ " (but is given a value of type " + rightExpr.Type.TypeName + ").");
			return false;
		}
		return true;
	}

	protected internal override IR ConstructIR()
	{
		leftExpr = leftExpr.Evaluate();
		if(rightExpr != null)
		{
			rightExpr = rightExpr.Evaluate();
			return new LogExpr(leftExpr.CheckIR(typeof(Expression)), rightExpr.CheckIR(typeof(Expression)));
		}
		else
			return new LogExpr(leftExpr.CheckIR(typeof(Expression)));
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
