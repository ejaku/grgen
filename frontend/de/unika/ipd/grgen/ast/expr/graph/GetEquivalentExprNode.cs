/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.graph
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using GraphTypeNode = de.unika.ipd.grgen.ast.type.basic.GraphTypeNode;
using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using GetEquivalentExpr = de.unika.ipd.grgen.ir.expr.graph.GetEquivalentExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node returning the subgraph from the given set being isomorphic to the given subgraph.
/// </summary>
public class GetEquivalentExprNode : BuiltinFunctionInvocationBaseNode
{
	static GetEquivalentExprNode()
	{
		SetClassName(typeof(GetEquivalentExprNode), "get equivalent expr");
	}

	private ExprNode subgraphExpr;
	private ExprNode subgraphSetExpr;
	private bool includingAttributes;

	public GetEquivalentExprNode(Coords coords, ExprNode subgraphExpr,
			ExprNode subgraphSetExpr, bool includingAttributes)
		: base(coords)
	{
		this.subgraphExpr = subgraphExpr;
		BecomeParent(this.subgraphExpr);
		this.subgraphSetExpr = subgraphSetExpr;
		BecomeParent(this.subgraphSetExpr);
		this.includingAttributes = includingAttributes;
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(subgraphExpr);
			children.Add(subgraphSetExpr);
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
			childrenNames.Add("subgraphExpr");
			childrenNames.Add("subgraphSetExpr");
			return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		return true;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		if(!(subgraphExpr.Type is GraphTypeNode))
		{
			subgraphExpr.ReportError("The function getEquivalent expects as 1. argument (subgraphToCompare) a value of type graph"
					+ " (but is given a value of type " + subgraphExpr.Type.TypeName + ").");
			return false;
		}
		if(!(subgraphSetExpr.Type is SetTypeNode))
		{
			subgraphSetExpr.ReportError("The function getEquivalent expects as 2. argument (setOfSubgraphsToCompareAgainst) a value of type set"
					+ " (but is given a value of type " + subgraphSetExpr.Type.TypeName + ").");
			return false;
		}
		SetTypeNode type = (SetTypeNode)subgraphSetExpr.Type;
		if(!(type.valueType is GraphTypeNode))
		{
			subgraphSetExpr.ReportError("The function getEquivalent expects as 2. argument (setOfSubgraphsToCompareAgainst) a value of type set<graph>"
					+ " (but is given a value of type " + subgraphSetExpr.Type.TypeName + ").");
			return false;
		}
		return true;
	}

	protected internal override IR ConstructIR()
	{
		subgraphExpr = subgraphExpr.Evaluate();
		subgraphSetExpr = subgraphSetExpr.Evaluate();
		return new GetEquivalentExpr(subgraphExpr.CheckIR(typeof(Expression)),
				subgraphSetExpr.CheckIR(typeof(Expression)),
				includingAttributes, Type.IRType);
	}

	public override TypeNode Type
	{
		get
		{
			return BasicTypeNode.graphType;
		}
	}
}

}
