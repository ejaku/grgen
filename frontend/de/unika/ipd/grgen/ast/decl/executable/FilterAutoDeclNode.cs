/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.decl.executable
{
	using System.Diagnostics;

	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using FilterFunctionTypeNode = de.unika.ipd.grgen.ast.type.executable.FilterFunctionTypeNode;

	/// <summary>
	/// AST node class representing auto-supplied and auto-generated filter declarations
	/// </summary>
	public abstract class FilterAutoDeclNode : DeclNode
	{
		static FilterAutoDeclNode()
		{
			SetClassName(typeof(FilterAutoDeclNode), "auto filter");
		}

		internal static readonly FilterFunctionTypeNode filterFunctionType = new FilterFunctionTypeNode(); // dummy type

		public FilterAutoDeclNode(IdentNode ident)
			: base(ident, filterFunctionType)
		{
		}

		public override TypeNode DeclType
		{
			get
			{
				Debug.Assert(IsResolved());

				return filterFunctionType;
			}
		}
	}

}
