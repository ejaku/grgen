/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.parser.Coords;

public abstract class FilterInvocationBaseNode extends BaseNode
{
	static {
		setName(FilterInvocationBaseNode.class, "filter invocation base");
	}

	protected IdentNode iteratedUnresolved;
	protected IteratedDeclNode iterated;

	public FilterInvocationBaseNode(Coords coords, IdentNode iteratedUnresolved)
	{
		super(coords);
		this.iteratedUnresolved = becomeParent(iteratedUnresolved);
	}

	private static final DeclarationResolver<IteratedDeclNode> iteratedResolver =
			new DeclarationResolver<IteratedDeclNode>(IteratedDeclNode.class);

	@Override
	protected boolean resolveLocal()
	{
		// owner
		iterated = iteratedResolver.resolve(iteratedUnresolved, this);
		if(iterated == null)
			return false;
		return true;
	}
}
