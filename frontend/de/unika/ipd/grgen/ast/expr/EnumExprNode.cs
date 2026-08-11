/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.expr
{

using System.Collections.Generic;
using System.Diagnostics;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using EnumItemDeclNode = de.unika.ipd.grgen.ast.model.decl.EnumItemDeclNode;
using EnumTypeNode = de.unika.ipd.grgen.ast.model.type.EnumTypeNode;
using de.unika.ipd.grgen.ast.util;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using EnumExpression = de.unika.ipd.grgen.ir.expr.EnumExpression;
using EnumItem = de.unika.ipd.grgen.ir.model.EnumItem;
using EnumType = de.unika.ipd.grgen.ir.model.type.EnumType;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class EnumExprNode : QualIdentNode
{
	static EnumExprNode()
	{
		SetClassName(typeof(EnumExprNode), "enum access expression");
	}

	public EnumExprNode(Coords coords, IdentNode owner, IdentNode member)
		: base(coords, owner, member)
	{
	}

	private EnumTypeNode owner;

	private EnumItemDeclNode member;

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(GetValidVersion(ownerUnresolved, owner));
			children.Add(GetValidVersion(memberUnresolved, member));
			return children;
		}
	}
	// TODO Missing getChildrenNames()...

	private static readonly DeclarationTypeResolver<EnumTypeNode> ownerResolver =
			new DeclarationTypeResolver<EnumTypeNode>(typeof(EnumTypeNode));

	private static readonly DeclarationResolver<EnumItemDeclNode> memberResolver =
			new DeclarationResolver<EnumItemDeclNode>(typeof(EnumItemDeclNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool successfullyResolved = true;
		owner = ownerResolver.Resolve(ownerUnresolved, this);
		successfullyResolved = owner != null && successfullyResolved;

		if(owner != null)
		{
			owner.FixupDefinition(memberUnresolved);

			member = memberResolver.Resolve(memberUnresolved, this);
			successfullyResolved = member != null && successfullyResolved;
		}
		else
			successfullyResolved = false;

		return successfullyResolved;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.DeclaredCharacter.getDecl() "/>
	public override EnumItemDeclNode Decl
	{
		get
		{
			Debug.Assert(IsResolved());

			return member;
		}
	}

	public override DeclNode Owner
	{
		get
		{
			Debug.Assert(IsResolved());

			return DeclNode.Invalid;
		}
	}

	/// <summary>
	/// Build the IR of an enum expression. </summary>
	/// <returns> An enum expression IR object. </returns>
	protected internal override IR ConstructIR()
	{
		EnumType et = owner.CheckIR(typeof(EnumType));
		EnumItem it = member.CheckIR(typeof(EnumItem));
		return new EnumExpression(et, it);
	}
}

}
