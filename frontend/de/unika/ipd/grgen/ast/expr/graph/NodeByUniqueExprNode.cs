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
using IntTypeNode = de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using NodeByUniqueExpr = de.unika.ipd.grgen.ir.expr.graph.NodeByUniqueExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node retrieving a node from a unique id.
/// </summary>
public class NodeByUniqueExprNode : BuiltinFunctionInvocationBaseNode
{
	static NodeByUniqueExprNode()
	{
		SetClassName(typeof(NodeByUniqueExprNode), "node by unique expr");
	}

	private ExprNode unique;
	private ExprNode nodeType;

	public NodeByUniqueExprNode(Coords coords, ExprNode unique, ExprNode nodeType)
		: base(coords)
	{
		this.unique = unique;
		BecomeParent(this.unique);
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
			children.Add(unique);
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
			childrenNames.Add("name");
			childrenNames.Add("nodeType");
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
		if(!(unique.Type is IntTypeNode))
		{
			ReportError("The function nodeByUnique expects as 1. argument (uniqueIdToSearchFor) a value of type int"
					+ " (but is given a value of type " + unique.Type.TypeName + ").");
			return false;
		}
		if(!(nodeType.Type is NodeTypeNode))
		{
			ReportError("The function nodeByUnique expects as 2. argument (typeToObtain) a value of type node type"
					+ " (but is given a value of type " + nodeType.Type.TypeName + ").");
			return false;
		}
		if(!UnitNode.Root.Model.IsUniqueIndexDefined())
		{
			ReportError("The function nodeByUnique expects a model with a unique index, but the required index unique; declaration is missing in the model specification.");
			return false;
		}
		return true;
	}

	protected internal override IR ConstructIR()
	{
		unique = unique.Evaluate();
		nodeType = nodeType.Evaluate();
		return new NodeByUniqueExpr(unique.CheckIR(typeof(Expression)),
				nodeType.CheckIR(typeof(Expression)), Type.IRType);
	}

	public override TypeNode Type
	{
		get
		{
			return nodeType.Type;
		}
	}
}

}
