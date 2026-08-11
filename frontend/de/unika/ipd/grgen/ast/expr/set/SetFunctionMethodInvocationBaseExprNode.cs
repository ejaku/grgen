/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.set
{

	using System.Collections.Generic;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using ContainerFunctionMethodInvocationBaseExprNode = de.unika.ipd.grgen.ast.expr.ContainerFunctionMethodInvocationBaseExprNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public abstract class SetFunctionMethodInvocationBaseExprNode : ContainerFunctionMethodInvocationBaseExprNode
	{
		static SetFunctionMethodInvocationBaseExprNode()
		{
			SetClassName(typeof(SetFunctionMethodInvocationBaseExprNode), "set function method invocation base expression");
		}

		public SetFunctionMethodInvocationBaseExprNode(Coords coords, ExprNode targetExpr)
			: base(coords, targetExpr)
		{
		}

		protected internal virtual SetTypeNode TargetTypeExact
		{
			get
			{
				return (SetTypeNode)TargetType;
			}
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(targetExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("targetExpr");
				return childrenNames;
			}
		}

		protected internal override bool CheckLocal()
		{
			// target type already checked during resolving into this node
			return true;
		}
	}

}
