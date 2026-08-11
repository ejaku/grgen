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
using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using OppositeExpr = de.unika.ipd.grgen.ir.expr.graph.OppositeExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node yielding the opposite node of an edge and a node.
/// </summary>
public class OppositeExprNode : BuiltinFunctionInvocationBaseNode
{
	static OppositeExprNode()
	{
		SetClassName(typeof(OppositeExprNode), "opposite expr");
	}

	private ExprNode edge;
	private ExprNode node;

	private IdentNode nodeTypeUnresolved;
	private NodeTypeNode nodeType;

	public OppositeExprNode(Coords coords, ExprNode edge, ExprNode node, IdentNode nodeType)
		: base(coords)
	{
		this.edge = edge;
		BecomeParent(this.edge);
		this.node = node;
		BecomeParent(this.node);
		this.nodeTypeUnresolved = nodeType;
		BecomeParent(this.nodeTypeUnresolved);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(edge);
			children.Add(node);
			children.Add(GetValidVersion(nodeTypeUnresolved, nodeType));
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
			childrenNames.Add("edge");
			childrenNames.Add("node");
			childrenNames.Add("nodeType");
			return childrenNames;
		}
	}

	private static readonly DeclarationTypeResolver<NodeTypeNode> nodeTypeResolver =
			new DeclarationTypeResolver<NodeTypeNode>(typeof(NodeTypeNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		nodeType = nodeTypeResolver.Resolve(nodeTypeUnresolved, this);
		return nodeType != null && Type.Resolve();
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		if(!(edge.Type is EdgeTypeNode))
		{
			ReportError("The function opposite expects as 1. argument (edgeToObtainOppositeNodeFrom) a value of type edge"
					+ " (but is given a value of type " + edge.Type.TypeName + ").");
			return false;
		}
		if(!(node.Type is NodeTypeNode))
		{
			ReportError("The function opposite expects as 2. argument (originalNode) a value of type node"
					+ " (but is given a value of type " + node.Type.TypeName + ").");
			return false;
		}
		return true;
	}

	protected internal override IR ConstructIR()
	{
		edge = edge.Evaluate();
		node = node.Evaluate();
		return new OppositeExpr(edge.CheckIR(typeof(Expression)), node.CheckIR(typeof(Expression)), Type.IRType);
	}

	public override TypeNode Type
	{
		get
		{
			return nodeType;
		}
	}
}

}
