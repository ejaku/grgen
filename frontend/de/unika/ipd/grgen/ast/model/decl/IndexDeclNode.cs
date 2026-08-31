/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.model.decl
{
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;

	/// <summary>
	/// AST node base base class representing index declarations (attribute index and incidence index being its specializations)
	/// </summary>
	public abstract class IndexDeclNode : DeclNode
	{
		static IndexDeclNode()
		{
			SetClassName(typeof(IndexDeclNode), "index declaration");
		}

		public IndexDeclNode(IdentNode id, TypeNode indexType)
			: base(id, indexType)
		{
		}

		public abstract InheritanceTypeNode Type {get;}

		public abstract TypeNode ExpectedAccessType {get;}

		public static new string KindStr
		{
			get
			{
				return "index";
			}
		}
	}

}
