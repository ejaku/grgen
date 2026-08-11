/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll
/// </summary>

namespace de.unika.ipd.grgen.ast.expr
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using TypeDeclNode = de.unika.ipd.grgen.ast.decl.TypeDeclNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Constant = de.unika.ipd.grgen.ir.expr.Constant;

/// <summary>
/// An identifier expression.
/// </summary>
public class IdentExprNode : DeclExprNode
{
	static IdentExprNode()
	{
		SetClassName(typeof(IdentExprNode), "ident expression");
	}

	public bool yieldedTo = false;

	public IdentExprNode(IdentNode ident)
		: base(ident)
	{
	}

	public IdentExprNode(IdentNode ident, bool yieldedTo)
		: base(ident)
	{
		this.yieldedTo = yieldedTo;
	}

	public virtual void SetYieldedTo()
	{
		yieldedTo = true;
	}

	protected internal override bool ResolveLocal()
	{
		decl = ((DeclaredCharacter)declUnresolved).Decl;
		if(decl is TypeDeclNode)
			return true;

		return base.ResolveLocal();
	}

	public virtual IdentNode Ident
	{
		get
		{
			return (IdentNode)declUnresolved;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("ident");
			return childrenNames;
		}
	}

	protected internal override IR ConstructIR()
	{
		BaseNode declNode = (BaseNode)decl;
		if(declNode is TypeDeclNode)
			return new Constant(BasicTypeNode.typeType.GetIRType(), ((TypeDeclNode)decl).DeclType.IR);
		else
			return base.ConstructIR();
	}
}

}
