/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;


/**
 * AST node base base class representing index declarations (attribute index and incidence index being its specializations)
 */
public abstract class IndexDeclNode extends DeclNode {
	static {
		setName(IndexDeclNode.class, "index declaration");
	}

	public IndexDeclNode(IdentNode id, TypeNode indexType) {
		super(id, indexType);
	}
}


