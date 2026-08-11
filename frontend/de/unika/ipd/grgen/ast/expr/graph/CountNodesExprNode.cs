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
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using CountNodesExpr = de.unika.ipd.grgen.ir.expr.graph.CountNodesExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node yielding the count of the nodes of a node type.
/// </summary>
public class CountNodesExprNode : BuiltinFunctionInvocationBaseNode
{
	static CountNodesExprNode()
	{
		SetClassName(typeof(CountNodesExprNode), "count nodes expr");
	}

	private ExprNode nodeType;

	public CountNodesExprNode(Coords coords, ExprNode nodeType)
		: base(coords)
	{
		this.nodeType = nodeType;
		BecomeParent(this.nodeType);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(nodeType);
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
		childrenNames.Add("node type");
		return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		return Type.Resolve();
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		if(!(nodeType.Type is NodeTypeNode))
		{
			ReportError("The function countNodes expects as argument a value of type node"
					+ " (but is given a value of type " + nodeType.Type.TypeName + ").");
			return false;
		}
		return true;
	}

	protected internal override IR ConstructIR()
	{
		nodeType = nodeType.Evaluate();
		return new CountNodesExpr(nodeType.CheckIR(typeof(Expression)));
	}

	public override TypeNode Type
	{
		get
		{
		return BasicTypeNode.intType;
		}
	}
}

}
