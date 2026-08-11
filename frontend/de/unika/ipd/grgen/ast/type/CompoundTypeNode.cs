/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.type
{
	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using ScopeOwner = de.unika.ipd.grgen.ast.ScopeOwner;

	/// <summary>
	/// Base class for all AST nodes representing compound types.
	/// Note: The scope stored in the node
	/// (accessible via <seealso cref="BaseNode.getScope()"/>) is the scope,
	/// this compound type owns, not the scope it is declared in.
	/// </summary>
	public abstract class CompoundTypeNode : DeclaredTypeNode, ScopeOwner
	{
		public virtual bool FixupDefinition(IdentNode id)
		{
			return FixupDefinition(id, Scope, true);
		}
	}

}
