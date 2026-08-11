/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ast.type
{
using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using PrimitiveType = de.unika.ipd.grgen.ir.type.basic.PrimitiveType;

/// <summary>
/// Base class for all AST nodes representing declared types.
/// Declared types have identifiers (and declaration nodes).
/// The location of this type is set by the declaration node's
/// constructor </summary>
/// <seealso cref="DeclNode.DeclNode(IdentNode, BaseNode)"/>
public abstract class DeclaredTypeNode : TypeNode
{
	private DeclNode decl = null;

	/// <summary>
	/// Set the declaration of this type. </summary>
	///  <param name="decl"> The declaration of this type.  </param>
	public virtual DeclNode Decl
	{
		set
		{
			this.decl = value;
		}
		get
		{
			return decl;
		}
	}


	/// <summary>
	/// Get the identifier of the type declaration. </summary>
	/// <returns> The identifier of the type declaration or an invalid
	/// identifier, if the type declaration was not set. </returns>
	public virtual IdentNode Ident
	{
		get
		{
			return decl != null ? decl.Ident : IdentNode.Invalid;
		}
	}

	public virtual PrimitiveType IRPrimitiveType
	{
		get
		{
			return CheckIR(typeof(PrimitiveType));
		}
	}

	public override string TypeName
	{
		get
		{
			return Ident.ToString();
		}
	}
}

}
