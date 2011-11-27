/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

/**
 * A globals access expression (just a slightly special ident expression).
 */
public class GlobalsAccessExprNode extends IdentExprNode {
	static {
		setName(GlobalsAccessExprNode.class, "globals access expression");
	}
	
	public GlobalsAccessExprNode(IdentNode ident) {
		super(ident);
	}

	public GlobalsAccessExprNode(IdentNode ident, boolean yieldedTo) {
		super(ident, yieldedTo);
	}
}
