/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.procenv
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using BuiltinProcedureInvocationBaseNode = de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using Coords = de.unika.ipd.grgen.parser.Coords;

public abstract class DebugProcNode : BuiltinProcedureInvocationBaseNode
{
	static DebugProcNode()
	{
		SetClassName(typeof(DebugProcNode), "debug procedure");
	}

	protected internal CollectNode<ExprNode> exprs = new CollectNode<ExprNode>();

	public DebugProcNode(Coords coords)
		: base(coords)
	{

		this.exprs = BecomeParent(exprs);
	}

	public virtual void AddExpression(ExprNode expr)
	{
		exprs.AddChild(expr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(exprs);
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("exprs");
			return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		return true;
	}

	protected internal override bool CheckLocal()
	{
		ExprNode message = exprs.Get(0);
		TypeNode messageType = message.Type;
		if(!(messageType.Equals(BasicTypeNode.stringType)))
		{
			ReportError("The " + ShortSignature() + " procedure expects as argument (message)"
					+ " a value of type string"
					+ " (but is given a value of type " + messageType.ToStringWithDeclarationCoords() + ").");
			return false;
		}
		return true;
	}

	protected internal abstract string ShortSignature();

	public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}
}

}
