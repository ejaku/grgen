/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Rubino Geiss
/// </summary>

namespace de.unika.ipd.grgen.ast.decl
{

using System.Collections.Generic;
using System.Diagnostics;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using de.unika.ipd.grgen.ast.util;
using ExecVariable = de.unika.ipd.grgen.ir.ExecVariable;
using IR = de.unika.ipd.grgen.ir.IR;

/// <summary>
/// Declaration of a variable in an exec, explicit sequence local or implicit graph global.
/// </summary>
public class ExecVarDeclNode : DeclNode
{
	private static readonly DeclarationResolver<DeclNode> declOfTypeResolver =
			new DeclarationResolver<DeclNode>(typeof(DeclNode));

	private TypeNode type;

	public ExecVarDeclNode(IdentNode id, IdentNode type)
		: base(id, type)
	{
	}

	public ExecVarDeclNode(IdentNode id, TypeNode type)
		: base(id, type)
	{
		this.type = type;
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

	/// <summary>
	/// local resolving of the current node to be implemented by the subclasses, called from the resolve AST walk </summary>
	/// <returns> true, if resolution of the AST locally finished successfully;
	/// false, if there was some error. </returns>
	protected internal override bool ResolveLocal()
	{
		// Type was already known at construction?
		if(type != null)
			return true;

		DeclNode typeDecl = declOfTypeResolver.Resolve(typeUnresolved, this);
		if(typeDecl is InvalidDeclNode)
		{
			typeUnresolved.ReportError("The exec variable " + Ident + " has an unknown type " + typeUnresolved + ".");
			return false;
		}
		type = typeDecl.DeclType;
		return type != null;
	}

	/// <summary>
	/// local checking of the current node to be implemented by the subclasses, called from the check AST walk </summary>
	/// <returns> true, if checking of the AST locally finished successfully;
	/// false, if there was some error. </returns>
	protected internal override bool CheckLocal()
	{
		return true;
	}

	/// <returns> The type node of the declaration </returns>
	public override TypeNode DeclType
	{
		get
		{
			Debug.Assert(IsResolved(), this + " was not resolved");
			return type;
		}
	}

	public static string KindStr
	{
		get
		{
			return "exec variable";
		}
	}

	protected internal override IR ConstructIR()
	{
		return new ExecVariable("ExecVar", Ident.IRIdent, type.IRType, 0);
	}
}

}
