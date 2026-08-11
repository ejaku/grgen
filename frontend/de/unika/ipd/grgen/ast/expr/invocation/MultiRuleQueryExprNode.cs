/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.invocation
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using MultiRuleQueryExpr = de.unika.ipd.grgen.ir.expr.invocation.MultiRuleQueryExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class MultiRuleQueryExprNode : ExprNode
{
	static MultiRuleQueryExprNode()
	{
		SetClassName(typeof(MultiRuleQueryExprNode), "multi rule query");
	}

	private CollectNode<ExprNode> ruleQueries;
	private IdentNode matchClass;

	private TypeNode arrayOfMatchTypeUnresolved;
	private TypeNode arrayOfMatchType;

	public MultiRuleQueryExprNode(Coords coords, CollectNode<ExprNode> ruleQueries, IdentNode matchClass,
			TypeNode arrayOfMatchType)
		: base(coords)
	{

		this.ruleQueries = BecomeParent(ruleQueries);
		this.matchClass = BecomeParent(matchClass);
		this.arrayOfMatchTypeUnresolved = BecomeParent(arrayOfMatchType);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(ruleQueries);
		children.Add(matchClass);
		children.Add(GetValidVersion(arrayOfMatchTypeUnresolved, arrayOfMatchType));
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("ruleQueries");
		childrenNames.Add("matchClass");
		childrenNames.Add("arrayOfMatchType");
		return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		if(arrayOfMatchTypeUnresolved.Resolve())
			arrayOfMatchType = arrayOfMatchTypeUnresolved;
		return arrayOfMatchType != null;
	}

	protected internal override bool CheckLocal()
	{
		// all actions must implement the match classes of the employed filters
		foreach(ExprNode ruleQuery in ruleQueries.ChildrenExact)
		{
			CallActionNode actionCall = ((RuleQueryExprNode)ruleQuery).CallAction;
			MultiCallActionNode.CheckWhetherCalledActionImplementsMatchClass(matchClass.IRIdent.ToString(), null,
					actionCall);
		}

		return true;
	}

	protected internal override IR ConstructIR()
	{
		return new MultiRuleQueryExpr(Type.IRType);
	}

	public override TypeNode Type
	{
		get
		{
		return arrayOfMatchType;
		}
	}
}

}
