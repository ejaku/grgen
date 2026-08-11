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
using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using CountEdgesExpr = de.unika.ipd.grgen.ir.expr.graph.CountEdgesExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node yielding the count of the edges of an edge type.
/// </summary>
public class CountEdgesExprNode : BuiltinFunctionInvocationBaseNode
{
	static CountEdgesExprNode()
	{
		SetClassName(typeof(CountEdgesExprNode), "count edges expr");
	}

	private ExprNode edgeType;

	public CountEdgesExprNode(Coords coords, ExprNode edgeType)
		: base(coords)
	{
		this.edgeType = edgeType;
		BecomeParent(this.edgeType);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(edgeType);
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
		childrenNames.Add("edge type");
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
		if(!(edgeType.Type is EdgeTypeNode))
		{
			ReportError("The function countEdges expects as argument a value of type edge"
					+ " (but is given a value of type " + edgeType.Type.TypeName + ").");
			return false;
		}
		return true;
	}

	protected internal override IR ConstructIR()
	{
		edgeType = edgeType.Evaluate();
		return new CountEdgesExpr(edgeType.CheckIR(typeof(Expression)));
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
