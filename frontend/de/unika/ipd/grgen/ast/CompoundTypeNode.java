/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;


/**
 * Base class for all AST nodes representing compound types.
 * Note: The scope stored in the node
 * (accessible via {@link BaseNode#getScope()}) is the scope,
 * this compound type owns, not the scope it is declared in.
 */
public abstract class CompoundTypeNode extends DeclaredTypeNode
	implements ScopeOwner
{
	public boolean fixupDefinition(IdentNode id) {
		return fixupDefinition(id, getScope(), true);
	}
}
