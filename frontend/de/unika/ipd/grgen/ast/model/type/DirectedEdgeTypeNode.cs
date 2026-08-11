/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.model.type
{
using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using de.unika.ipd.grgen.ast;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using ConnAssertNode = de.unika.ipd.grgen.ast.model.ConnAssertNode;
using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;

public class DirectedEdgeTypeNode : EdgeTypeNode
{
	static DirectedEdgeTypeNode()
	{
		SetClassName(typeof(DirectedEdgeTypeNode), "directed edge type");
	}

	/// <summary>
	/// Make a new directed edge type node. </summary>
	/// <param name="ext"> The collect node with all edge classes that this one extends. </param>
	/// <param name="cas"> The collect node with all connection assertion of this type. </param>
	/// <param name="body"> The body of the type declaration. It consists of basic
	/// declarations. </param>
	/// <param name="modifiers"> The modifiers for this type. </param>
	/// <param name="externalName"> The name of the external implementation of this type or null. </param>
	public DirectedEdgeTypeNode(CollectNode<IdentNode> ext, CollectNode<ConnAssertNode> cas, CollectNode<BaseNode> body,
			int modifiers, string externalName)
		: base(ext, cas, body, modifiers, externalName)
	{
	}

	protected internal override EdgeType DirectednessIR
	{
		set
		{
			value.Directedness = EdgeType.DirectednessKind.Directed;
		}
	}

	public static string KindStr
	{
		get
		{
			return "directed edge class";
		}
	}
}

}
