/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.decl.executable;

import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.executable.FilterFunctionTypeNode;

/**
 * AST node class representing auto-supplied and auto-generated filter declarations
 */
public abstract class FilterAutoDeclNode extends DeclNode
{
	static {
		setName(FilterAutoDeclNode.class, "auto filter");
	}

	static final FilterFunctionTypeNode filterFunctionType = new FilterFunctionTypeNode(); // dummy type

	public FilterAutoDeclNode(IdentNode ident)
	{
		super(ident, filterFunctionType);
	}

	@Override
	public TypeNode getDeclType()
	{
		assert isResolved();

		return filterFunctionType;
	}
}
