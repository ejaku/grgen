/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr
{

using System.Collections.Generic;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using ExpressionPair = de.unika.ipd.grgen.ir.expr.ExpressionPair;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class ExprPairNode : BaseNode
{
	static ExprPairNode()
	{
		SetClassName(typeof(ExprPairNode), "expr pair");
	}

	public ExprNode keyExpr; // first
	public ExprNode valueExpr; // second

	public ExprPairNode(Coords coords, ExprNode keyExpr, ExprNode valueExpr)
		: base(coords)
	{
		this.keyExpr = BecomeParent(keyExpr);
		this.valueExpr = BecomeParent(valueExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(keyExpr);
			children.Add(valueExpr);
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("keyExpr");
			childrenNames.Add("valueExpr");
			return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		return true;
	}

	protected internal override bool CheckLocal()
	{
		// All checks are done in MapInitNode
		return true;
	}

	protected internal override IR ConstructIR()
	{
		keyExpr = keyExpr.Evaluate();
		valueExpr = valueExpr.Evaluate();
		return new ExpressionPair(keyExpr.CheckIR(typeof(Expression)), valueExpr.CheckIR(typeof(Expression)));
	}

	public virtual ExpressionPair IRExpressionPair
	{
		get
		{
			return CheckIR(typeof(ExpressionPair));
		}
	}

	public virtual bool NoDefElement(string containingConstruct)
	{
		return keyExpr.NoDefElement(containingConstruct) & valueExpr.NoDefElement(containingConstruct);
	}

	public virtual bool NoIteratedReference(string containingConstruct)
	{
		return keyExpr.NoIteratedReference(containingConstruct) & valueExpr.NoIteratedReference(containingConstruct);
	}

	public virtual bool IteratedNotReferenced(string iterName)
	{
		return keyExpr.IteratedNotReferenced(iterName) & valueExpr.IteratedNotReferenced(iterName);
	}
}

}
