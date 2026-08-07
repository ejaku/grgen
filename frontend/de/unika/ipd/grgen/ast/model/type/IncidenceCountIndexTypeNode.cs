/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.model.type
{

using System.Collections.Generic;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;

/// <summary>
/// Type of incidence count index node declaration.
/// </summary>
public class IncidenceCountIndexTypeNode : TypeNode
{
	static IncidenceCountIndexTypeNode()
	{
		SetClassName(typeof(IncidenceCountIndexTypeNode), "incidence count index type");
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		// no children
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
		// no children
		return childrenNames;
		}
	}
}

}
