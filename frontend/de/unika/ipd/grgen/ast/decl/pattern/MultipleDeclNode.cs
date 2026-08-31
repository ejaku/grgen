/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.decl.pattern
{
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;

	/// <summary>
	/// AST node for a multiple pattern, maybe including replacements.
	/// </summary>
	public class MultipleDeclNode : IteratedDeclNode
	{
		static MultipleDeclNode()
		{
			SetClassName(typeof(MultipleDeclNode), "multiple");
		}

		public MultipleDeclNode(IdentNode id, PatternGraphLhsNode left, RhsDeclNode right)
			: base(id, left, right)
		{
		}

		protected internal override int MinMatches
		{
			get
			{
				return 1;
			}
		}

		protected internal override int MaxMatches
		{
			get
			{
				return 0;
			}
		}

		public static new string KindStr
		{
			get
			{
				return "multiple";
			}
		}
	}

}
