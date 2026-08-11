/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt
{

using System.Collections.Generic;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
using QualIdentNode = de.unika.ipd.grgen.ast.expr.QualIdentNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using ContainerTypeNode = de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
using Coords = de.unika.ipd.grgen.parser.Coords;

public abstract class ContainerProcedureMethodInvocationBaseNode : BuiltinProcedureInvocationBaseNode
{
	static ContainerProcedureMethodInvocationBaseNode()
	{
		SetClassName(typeof(ContainerProcedureMethodInvocationBaseNode), "container procedure method invocation base");
	}

	protected internal QualIdentNode target;
	protected internal VarDeclNode targetVar;

	protected internal ContainerProcedureMethodInvocationBaseNode(Coords coords, QualIdentNode target)
		: base(coords)
	{
		this.target = BecomeParent(target);
	}

	protected internal ContainerProcedureMethodInvocationBaseNode(Coords coords, VarDeclNode targetVar)
		: base(coords)
	{
		this.targetVar = BecomeParent(targetVar);
	}

	protected internal virtual ContainerTypeNode TargetType
	{
		get
		{
			if(target != null)
			{
				TypeNode targetType = target.Decl.DeclType;
				return (ContainerTypeNode)targetType;
			}
			else
			{
				TypeNode targetType = targetVar.DeclType;
				return (ContainerTypeNode)targetType;
			}
		}
	}

	protected internal virtual BaseNode ValidTarget
	{
		get
		{
			return target != null ? (BaseNode)target : (BaseNode)targetVar;
		}
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(ValidTarget);
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("target");
			return childrenNames;
		}
	}

	protected internal override bool CheckLocal()
	{
		// target type already checked during resolving into this node
		return true;
	}

	public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}
}

}
