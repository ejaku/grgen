/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using ExternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.ExternalObjectTypeNode;
using InternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.InternalObjectTypeNode;
using InternalTransientObjectTypeNode = de.unika.ipd.grgen.ast.model.type.InternalTransientObjectTypeNode;
using MatchTypeNode = de.unika.ipd.grgen.ast.type.MatchTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using GraphTypeNode = de.unika.ipd.grgen.ast.type.basic.GraphTypeNode;
using ObjectTypeNode = de.unika.ipd.grgen.ast.type.basic.ObjectTypeNode;
using ContainerTypeNode = de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using CopyExpr = de.unika.ipd.grgen.ir.expr.CopyExpr;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node yielding the copy of a subgraph, or a match, or a container.
/// </summary>
public class CopyExprNode : BuiltinFunctionInvocationBaseNode
{
	static CopyExprNode()
	{
		SetClassName(typeof(CopyExprNode), "copy expr");
	}

	private ExprNode sourceExpr;
	private bool deep;

	public CopyExprNode(Coords coords, ExprNode sourceExpr, bool deep)
		: base(coords)
	{
		this.sourceExpr = sourceExpr;
		BecomeParent(this.sourceExpr);
		this.deep = deep;
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(sourceExpr);
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
			childrenNames.Add("source expression");
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
		TypeNode type = sourceExpr.Type;
		if(deep)
		{
			if(!(type is GraphTypeNode)
					&& !(type is ContainerTypeNode)
					&& !(type is InternalObjectTypeNode)
					&& !(type is InternalTransientObjectTypeNode)
					&& !(type is ExternalObjectTypeNode)
					&& !(type is ObjectTypeNode))
			{
				sourceExpr.ReportError("The copy construct expects as argument a value of type container or graph or class object or transient class object or external object"
						 + " (but is given a value of " + type.Kind + " " + type.TypeName + ").");
				return false;
			}
		}
		else
		{
			if(!(type is MatchTypeNode)
					&& !(type is ContainerTypeNode)
					&& !(type is InternalObjectTypeNode)
					&& !(type is InternalTransientObjectTypeNode)
					&& !(type is ExternalObjectTypeNode)
					&& !(type is ObjectTypeNode))
			{
				sourceExpr.ReportError("The clone construct expects as argument a value of type container or match or class object or transient class object or external object"
						+ " (but is given a value of " + type.Kind + " " + type.TypeName + ").");
				return false;
			}
		}
		return true;
	}

	protected internal override IR ConstructIR()
	{
		sourceExpr = sourceExpr.Evaluate();
		return new CopyExpr(sourceExpr.CheckIR(typeof(Expression)), Type.IRType, deep);
	}

	public override TypeNode Type
	{
		get
		{
			if(sourceExpr.Type is MatchTypeNode
					|| sourceExpr.Type is ContainerTypeNode
					|| sourceExpr.Type is InternalObjectTypeNode
					|| sourceExpr.Type is InternalTransientObjectTypeNode
					|| sourceExpr.Type is ExternalObjectTypeNode
					|| sourceExpr.Type is ObjectTypeNode)
				return sourceExpr.Type;
			else
				return BasicTypeNode.graphType;
		}
	}
}

}
