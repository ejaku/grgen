/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.decl
{

using System.Collections.Generic;
using System.Diagnostics;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using ErrorTypeNode = de.unika.ipd.grgen.ast.type.basic.ErrorTypeNode;
using de.unika.ipd.grgen.ast.util;

/// <summary>
/// AST node class representing invalid declarations.
/// </summary>
public class InvalidDeclNode : DeclNode
{
	static InvalidDeclNode()
	{
		SetClassName(typeof(InvalidDeclNode), "invalid declaration");
	}

	private ErrorTypeNode type;

	/// <summary>
	/// Create a resolved and checked invalid DeclNode.
	/// </summary>
	public InvalidDeclNode(IdentNode id)
		: base(id, BasicTypeNode.GetErrorType(id))
	{
		Resolve();
		Check();
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(ident);
			children.Add(GetValidVersion(typeUnresolved, type));
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
			childrenNames.Add("ident");
			childrenNames.Add("type");
			return childrenNames;
		}
	}

	private static DeclarationResolver<ErrorTypeNode> typeResolver = new DeclarationResolver<ErrorTypeNode>(typeof(ErrorTypeNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		type = typeResolver.Resolve(typeUnresolved, this);

		return type != null;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		return true;
	}

	public static string KindStr
	{
		get
		{
			return "undeclared identifier";
		}
	}

	public override string ToString()
	{
		return "undeclared identifier";
	}

	public override TypeNode DeclType
	{
		get
		{
			Debug.Assert(IsResolved());

			return type;
		}
	}
}

}
