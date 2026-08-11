/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Rubino Geiss
/// </summary>

namespace de.unika.ipd.grgen.ast
{

using System.Collections.Generic;
using System.Diagnostics;

using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using OrderedReplacementNode = de.unika.ipd.grgen.ast.pattern.OrderedReplacementNode;
using Emit = de.unika.ipd.grgen.ir.Emit;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class EmitNode : OrderedReplacementNode
{
	static EmitNode()
	{
		SetClassName(typeof(EmitNode), "emit");
	}

	private IList<ExprNode> childrenUnresolved = new List<ExprNode>();
	public bool isDebug;

	public EmitNode(Coords coords, bool isDebug)
		: base(coords)
	{
		this.isDebug = isDebug;
	}

	public virtual void AddChild(ExprNode n)
	{
		Debug.Assert((!IsResolved()));
		BecomeParent(n);
		childrenUnresolved.Add(n);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		return new List<BaseNode>(childrenUnresolved);
		}
	}

	/// <summary>
	/// returns names of the children, same order as in getChildren </summary>
	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		// nameless children
		return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		return true;
	}

	protected internal override bool CheckLocal()
	{
		if(childrenUnresolved.Count == 0)
		{
			ReportError("The emit statement is empty.");
			return false;
		}
		return true;
	}

	public override Color NodeColor
	{
		get
		{
		return Color.PINK;
		}
	}

	protected internal override IR ConstructIR()
	{
		List<Expression> arguments = new List<Expression>();
		foreach(ExprNode child in childrenUnresolved)
		{
			ExprNode childEvaluated = child.Evaluate();
			arguments.Add(childEvaluated.CheckIR(typeof(Expression)));
		}
		Emit res = new Emit(arguments, isDebug);
		return res;
	}
}

}
