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
using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using NodesExpr = de.unika.ipd.grgen.ir.expr.graph.NodesExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node yielding the nodes of a node type.
/// </summary>
public class NodesExprNode : BuiltinFunctionInvocationBaseNode
{
	static NodesExprNode()
	{
		SetClassName(typeof(NodesExprNode), "nodes expr");
	}

	private ExprNode nodeType;
	private SetTypeNode setTypeNode;

	public NodesExprNode(Coords coords, ExprNode nodeType)
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
		setTypeNode = new SetTypeNode(GetNodeRoot(nodeType));
		return setTypeNode.Resolve();
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		if(!(nodeType.Type is NodeTypeNode))
		{
			ReportError("The function nodes expects as argument (typeToObtain) a value of type node type"
					+ " (but is given a value of type " + nodeType.Type.TypeName + ").");
			return false;
		}
		return true;
	}

	protected internal override IR ConstructIR()
	{
		nodeType = nodeType.Evaluate();
		return new NodesExpr(nodeType.CheckIR(typeof(Expression)), Type.IRType);
	}

	public override TypeNode Type
	{
		get
		{
			return setTypeNode;
		}
	}
}

}
