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
using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using InducedSubgraphExpr = de.unika.ipd.grgen.ir.expr.graph.InducedSubgraphExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node yielding the induced subgraph of a node set.
/// </summary>
public class InducedSubgraphExprNode : BuiltinFunctionInvocationBaseNode
{
	static InducedSubgraphExprNode()
	{
		SetClassName(typeof(InducedSubgraphExprNode), "induced subgraph expr");
	}

	private ExprNode nodeSetExpr;

	public InducedSubgraphExprNode(Coords coords, ExprNode nodeSetExpr)
		: base(coords)
	{
		this.nodeSetExpr = nodeSetExpr;
		BecomeParent(this.nodeSetExpr);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(nodeSetExpr);
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
			childrenNames.Add("nodeSetExpr");
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
		if(!(nodeSetExpr.Type is SetTypeNode))
		{
			nodeSetExpr.ReportError("The function inducedSubgraph expects as argument a value of type set"
					+ " (but is given a value of type " + nodeSetExpr.Type.TypeName + ").");
			return false;
		}
		SetTypeNode type = (SetTypeNode)nodeSetExpr.Type;
		if(!(type.valueType is NodeTypeNode))
		{
			nodeSetExpr.ReportError("The function inducedSubgraph expects as argument a value of type set<Node>"
					+ " (but is given a value of type " + nodeSetExpr.Type.TypeName + ").");
			return false;
		}
		return true;
	}

	protected internal override IR ConstructIR()
	{
		nodeSetExpr = nodeSetExpr.Evaluate();
		return new InducedSubgraphExpr(nodeSetExpr.CheckIR(typeof(Expression)), Type.IRType);
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
