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
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using DequeSubdequeExpr = de.unika.ipd.grgen.ir.expr.deque.DequeSubdequeExpr;
using IR = de.unika.ipd.grgen.ir.IR;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class DequeSubdequeNode : DequeFunctionMethodInvocationBaseExprNode
{
	static DequeSubdequeNode()
	{
		SetClassName(typeof(DequeSubdequeNode), "deque subdeque");
	}

	private ExprNode startExpr;
	private ExprNode lengthExpr;

	public DequeSubdequeNode(Coords coords, ExprNode targetExpr, ExprNode startExpr, ExprNode lengthExpr)
		: base(coords, targetExpr)
	{
		this.startExpr = BecomeParent(startExpr);
		this.lengthExpr = BecomeParent(lengthExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(targetExpr);
		children.Add(startExpr);
		children.Add(lengthExpr);
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("targetExpr");
		childrenNames.Add("startExpr");
		childrenNames.Add("lengthExpr");
		return childrenNames;
		}
	}

	protected internal override bool CheckLocal()
	{
		// target type already checked during resolving into this node
		if(!startExpr.Type.IsEqual(BasicTypeNode.intType))
		{
			startExpr.ReportError("The deque function method subdeque expects as 1. argument (start position) a value of type int"
					+ " (but is given a value of type " + startExpr.Type.TypeName + ").");
			return false;
		}
		if(!lengthExpr.Type.IsEqual(BasicTypeNode.intType))
		{
			lengthExpr.ReportError("The deque function method subdeque expects as 2. argument (length) a value of type int"
					+ " (but is given a value of type " + lengthExpr.Type.TypeName + ").");
			return false;
		}
		return true;
	}

	public override TypeNode Type
	{
		get
		{
		return TargetType;
		}
	}

	protected internal override IR ConstructIR()
	{
		targetExpr = targetExpr.Evaluate();
		startExpr = startExpr.Evaluate();
		lengthExpr = lengthExpr.Evaluate();
		return new DequeSubdequeExpr(targetExpr.CheckIR(typeof(Expression)),
				startExpr.CheckIR(typeof(Expression)),
				lengthExpr.CheckIR(typeof(Expression)));
	}
}

}
