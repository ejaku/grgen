/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll
/// </summary>

namespace de.unika.ipd.grgen.ast.type
{

	using System.Collections.Generic;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;

	/// <summary>
	/// Type of constructor declaration nodes.
	/// </summary>
	public class ConstructorTypeNode : TypeNode
	{
		internal static IList<BaseNode> emptyChildren = new List<BaseNode>();
		internal static IList<string> emptyChildrenNames = new List<string>();
		static ConstructorTypeNode()
		{
			SetClassName(typeof(ConstructorTypeNode), "constructor type");
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				return emptyChildren;
			}
		}

		/// <summary>
		/// returns names of the children, same order as in getChildren </summary>
		public override ICollection<string> ChildrenNames
		{
			get
			{
				return emptyChildrenNames;
			}
		}
	}

}
