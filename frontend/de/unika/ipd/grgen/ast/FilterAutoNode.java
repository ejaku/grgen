/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

/**
 * AST node class representing auto-supplied and auto-generated filters
 */
public abstract class FilterAutoNode extends DeclNode {
	static {
		setName(FilterAutoNode.class, "auto filter");
	}
	
	static final FilterFunctionTypeNode filterFunctionType = new FilterFunctionTypeNode(); // dummy type

	public FilterAutoNode(IdentNode ident) {
		super(ident, filterFunctionType);
	}
	
	@Override
	public TypeNode getDeclType() {
		assert isResolved();
	
		return filterFunctionType;
	}
}
