/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast
{
using IteratedDeclNode = de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
using de.unika.ipd.grgen.ast.util;
using Coords = de.unika.ipd.grgen.parser.Coords;

public abstract class FilterInvocationBaseNode : BaseNode
{
	static FilterInvocationBaseNode()
	{
		SetClassName(typeof(FilterInvocationBaseNode), "filter invocation base");
	}

	protected internal IdentNode iteratedUnresolved;
	protected internal IteratedDeclNode iterated;

	public FilterInvocationBaseNode(Coords coords, IdentNode iteratedUnresolved)
		: base(coords)
	{
		this.iteratedUnresolved = BecomeParent(iteratedUnresolved);
	}

	private static readonly DeclarationResolver<IteratedDeclNode> iteratedResolver =
			new DeclarationResolver<IteratedDeclNode>(typeof(IteratedDeclNode));

	protected internal override bool ResolveLocal()
	{
		// owner
		iterated = iteratedResolver.Resolve(iteratedUnresolved, this);
		if(iterated == null)
			return false;
		return true;
	}
}

}
