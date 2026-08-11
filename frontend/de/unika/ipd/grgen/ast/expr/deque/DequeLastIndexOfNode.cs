/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.deque
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using DequeTypeNode = de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using DequeLastIndexOfExpr = de.unika.ipd.grgen.ir.expr.deque.DequeLastIndexOfExpr;
using IR = de.unika.ipd.grgen.ir.IR;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class DequeLastIndexOfNode : DequeFunctionMethodInvocationBaseExprNode
{
	static DequeLastIndexOfNode()
	{
		SetClassName(typeof(DequeLastIndexOfNode), "deque last index of");
	}

	private ExprNode valueExpr;
	private ExprNode startIndexExpr;

	public DequeLastIndexOfNode(Coords coords, ExprNode targetExpr, ExprNode valueExpr)
		: base(coords, targetExpr)
	{
		this.valueExpr = BecomeParent(valueExpr);
	}

	public DequeLastIndexOfNode(Coords coords, ExprNode targetExpr, ExprNode valueExpr, ExprNode startIndexExpr)
		: base(coords, targetExpr)
	{
		this.valueExpr = BecomeParent(valueExpr);
		this.startIndexExpr = BecomeParent(startIndexExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(targetExpr);
		children.Add(valueExpr);
		if(startIndexExpr != null)
			children.Add(startIndexExpr);
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("targetExpr");
		childrenNames.Add("valueExpr");
		if(startIndexExpr != null)
			childrenNames.Add("startIndex");
		return childrenNames;
		}
	}

	protected internal override bool CheckLocal()
	{
		// target type already checked during resolving into this node
		TypeNode valueType = valueExpr.Type;
		DequeTypeNode dequeType = TargetTypeExact;
		if(!valueType.IsEqual(dequeType.valueType))
		{
			ExprNode valueExprOld = valueExpr;
			valueExpr = BecomeParent(valueExpr.AdjustType(dequeType.valueType, Coords));
			if(valueExpr == ConstNode.Invalid)
			{
				valueExprOld.ReportError("The deque function method lastIndexOf expects as 1. argument (valueToSearchFor) a value of type " + dequeType.valueType.ToStringWithDeclarationCoords()
						+ " (but is given a value of type " + valueType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
		}
		if(startIndexExpr != null && !startIndexExpr.Type.IsEqual(BasicTypeNode.intType))
		{
			startIndexExpr.ReportError("The deque function method lastIndexOf expects as 2. argument (startIndex) a value of type int"
					+ " (but is given a value of type " + startIndexExpr.Type.TypeName + ").");
			return false;
		}
		return true;
	}

	public override TypeNode Type
	{
		get
		{
		return BasicTypeNode.intType;
		}
	}

	protected internal override IR ConstructIR()
	{
		targetExpr = targetExpr.Evaluate();
		valueExpr = valueExpr.Evaluate();
		if(startIndexExpr != null)
		{
			startIndexExpr = startIndexExpr.Evaluate();
			return new DequeLastIndexOfExpr(targetExpr.CheckIR(typeof(Expression)),
					valueExpr.CheckIR(typeof(Expression)), startIndexExpr.CheckIR(typeof(Expression)));
		}
		else
		{
			return new DequeLastIndexOfExpr(targetExpr.CheckIR(typeof(Expression)),
					valueExpr.CheckIR(typeof(Expression)));
		}
	}
}

}
