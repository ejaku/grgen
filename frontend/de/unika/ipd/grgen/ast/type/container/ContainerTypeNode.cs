/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.type.container
{
using DeclaredTypeNode = de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;

public abstract class ContainerTypeNode : DeclaredTypeNode
{
	static ContainerTypeNode()
	{
		SetClassName(typeof(ContainerTypeNode), "container type");
	}

	public override string Name
	{
		get
		{
		return TypeName;
		}
	}

	// returns value type for array|deque|set and key type for map
	public abstract TypeNode ElementType {get;}
}

}
